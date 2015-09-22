using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using psp.core.domain;
using psp.api.Controllers;
using System.Text;
using psp.api.helpers;
using psp.api.Reports.domain;
using MongoDB.Bson;
using psp.repository.mongo.Repositories;
using psp.core.helpers;

namespace psp.api.Reports
{
    public class VolumeReport
    {
        public  IList<SiteWatchSalesItem> BuildVolumeData(string gsrDate)
        {
            var sites = new SiteController().Get();
            var list = new List<SiteWatchSalesItem>();
            foreach (var site in sites)
            {
                var gsr = new GSRReport().GetAmountToAudit(site, DateTime.Parse(gsrDate), "", "");
                list.Add(new SiteWatchSalesItem
                {
                    total = gsr.washLinkTotalGrossWashes_count.ToString(),

                    locationid = site.sitewatchid,
                    sitename = site.description,
                    val = gsr.totalToAccountForPerCar_dollars.ToString()
                });
            }
            return list;
        }

        public  NotificationMessage BuildNotifications(IList<SiteWatchSalesItem> items, string date)
        {
            var sb = NotificationTemplates.StandardNotificationHeader("Prime Shine Volume Reports", date);
            sb.Append("<table style='width: 400px; font-weight: 900; border-collapse: collapse;'>");
            int i = 0;
            sb.Append("<tr><td width='250px'><h4>Location</h4></td><td width='75px'><h4>Count</h4></td><td width='75px'><h4>$/Car</h4></td></tr>");
            int ttl = 0;
            foreach (var item in items.OrderBy(o => o.locationid))
            {
                if (i % 2 == 0)
                {
                    sb.Append("<tr style='border-bottom: solid 20px #ffffff;'>");
                }
                else
                {
                    sb.Append("<tr style='border-bottom: solid 20px #ffffff; background-color: #f0f0f0;'>");
                }
                sb.Append("<td width='250px'>");
                sb.Append(item.sitename);
                sb.Append("</td>");
                sb.Append("<td width='75px'>");
                sb.Append(item.total);
                sb.Append("</td>");
                sb.Append("<td width='75px'>$");
                sb.Append(item.val);
                sb.Append("</td>");
                sb.Append("</tr>");
                ttl += Convert.ToInt16(item.total);
            }
            sb.Append("<tr style='border-bottom: solid 20px #ffffff;'><td><h4>Total Count: </h4></td><td><h4>");
            sb.Append(ttl.ToString());
            sb.Append("</h4></td></tr>");
            sb.Append("</table>");
            
            var notification = new NotificationsController().GetByName("Volume_Report");
            var message = new NotificationMessage();
            foreach(var email in notification.recipients)
            {
                message.ToEmails.Add(email);
            }
            foreach (var bcc in notification.bccemails)
            {
                message.Bccs.Add(bcc);
            }
            message.FromEmail = notification.fromemail;
            message.Subject = notification.subject.Replace("!!date!!", date);
            message.MessageBody =  NotificationTemplates.StandardNotificationFooter(sb);
            return message;
        }

        public VolumeChart GetVolumeByDate(string date)
        {
            var results = new List<VolumeChartSeries>();
            List<DateTime> myDates = DateUtilities.GetDateByWeek(DateTime.Parse(date), DateUtilities.GetWeekOfMonthByDate(DateTime.Parse(date)), (int)DateTime.Parse(date).DayOfWeek, 5);
            var years = new List<GSR>();
            foreach (var dt in myDates)
            {
                years.AddRange(new GSRRepository().GetAllRecordsByDate(dt));
            }
            var categories = new SiteRepository().GetAll(); // GSRRepository().GetAllRecordsByDate(date).OrderBy(n => n.siteName).ThenByDescending(d => d.gsrDate).Select(i => i.siteName).Distinct();
            foreach (var year in years.OrderBy(n => n.siteName).ThenByDescending(d => d.gsrDate).GroupBy(o => o.gsrDate))
            {
                var dtos = new List<VolumeChartSeriesPoint>();
                int count = 0;
                //match up the correct location in case of missing days from a site
                var gsrs = year.ToArray();
                foreach(var location in categories) 
                {
                    try
                    {
                        if (location.location == gsrs[count].siteName)
                        {
                            var gsr = gsrs[count];
                            decimal ppc = 0;
                            if (gsr.sitewatchTotalWashes_count > 0)
                            {
                                ppc = Math.Round(gsr.totalToAccountFor / gsr.sitewatchTotalWashes_count, 2);
                            }
                            dtos.Add(new VolumeChartSeriesPoint
                            {
                                name = "Price Per Car: $" + ppc,
                                y = gsr.sitewatchTotalWashes_count,
                                dto = new VolumeDto
                                {
                                    carCount = gsr.sitewatchTotalWashes_count,
                                    pricePerCar = ppc,
                                    reportDate = gsr.gsrDate.ToShortDateString(),
                                    siteName = gsr.siteName
                                }
                            });
                            count++;
                        }
                        else
                        {
                            dtos.Add(new VolumeChartSeriesPoint
                            {
                                name = "Price Per Car: No Data",
                                y = 0,
                                dto = new VolumeDto
                                {
                                    carCount = 0,
                                    pricePerCar = 0,
                                    reportDate = year.First().gsrDate.ToShortDateString(),
                                    siteName = location.location
                                }
                            });
                        }
                    }
                    catch(IndexOutOfRangeException ioore)
                    {
                        dtos.Add(new VolumeChartSeriesPoint
                        {
                            name = "Price Per Car: No Data",
                            y = 0,
                            dto = new VolumeDto
                            {
                                carCount = 0,
                                pricePerCar = 0,
                                reportDate = year.First().gsrDate.ToShortDateString(),
                                siteName = location.location
                            }
                        });
                    }
                }
                if (dtos.Count > 0)
                {
                    var chartType = "";

                    if (DateTime.Parse(dtos[0].dto.reportDate).Year == DateTime.Now.Year)
                    {
                        chartType = "column";
                    }
                    else
                    {
                        chartType = "spline";
                    }
                    results.Add(new VolumeChartSeries
                    {
                        name = dtos[0].dto.reportDate,
                        data = dtos,
                        type = chartType
                    });
                }
            }

            var chart = new VolumeChart
            {
                categories = categories.Select(s => s.location).ToList<string>(),
                data = results,
                name = "Total Volume By Site For The " + DateUtilities.GetWeekOfMonthByDate(DateTime.Parse(date)).ToString() + " " + DateTime.Parse(date).DayOfWeek + " Of " + DateUtilities.GetMonthName(DateTime.Parse(date).Month)
            };

            return chart;
        }

        public VolumeChart GetVolumeByWashes(string date)
        {
            var sites = new SiteRepository().GetAll();
            var results = new List<VolumeChartSeries>();

            var packages = new List<string>();
            packages.Add("primeshinewash");
            packages.Add("protexwash");
            packages.Add("premierwash");
            packages.Add("tireshine");
            packages.Add("rainx");
            packages.Add("plusplus");
            var washResults = new GSRRepository().GetSiteWashTotals(DateTime.Parse(date));
            foreach(var package in packages)
            {
                var dtos = new List<VolumeChartSeriesPoint>();
                foreach(var site in sites)
                {
                    //var washResults = new WashLink().WashLinkWashTotalsBySiteDate(site, DateTime.Parse(date));
                    if(washResults != null)
                    {
                        var washResult = washResults.Where(s => s.sitename == site.location);
                        if(washResult != null && washResult.Count() == 1)
                        {
                            if (package == "primeshinewash")
                            {
                                dtos.Add(new VolumeChartSeriesPoint
                                {
                                    name = "Prime Shine Wash",
                                    y = washResult.First().primeshinewash,
                                    dto = new VolumeDto
                                    {
                                        carCount = washResult.First().primeshinewash,
                                        pricePerCar = 0,
                                        reportDate = washResult.First().date.ToShortDateString(),
                                        siteName = site.location
                                    }
                                });
                            }
                            else if (package == "protexwash")
                            {
                                dtos.Add(new VolumeChartSeriesPoint
                                {
                                    name = "Protex Wash",
                                    y = washResult.First().protexwash,
                                    dto = new VolumeDto
                                    {
                                        carCount = washResult.First().protexwash,
                                        pricePerCar = 0,
                                        reportDate = washResult.First().date.ToShortDateString(),
                                        siteName = site.location
                                    }
                                });
                            }
                            else if (package == "premierwash")
                            {
                                dtos.Add(new VolumeChartSeriesPoint
                                {
                                    name = "Premier Wash",
                                    y = washResult.First().premierwash,
                                    dto = new VolumeDto
                                    {
                                        carCount = washResult.First().premierwash,
                                        pricePerCar = 0,
                                        reportDate = washResult.First().date.ToShortDateString(),
                                        siteName = site.location
                                    }
                                });
                            }
                            else if (package == "tireshine")
                            {
                                dtos.Add(new VolumeChartSeriesPoint
                                {
                                    name = "Tire Shine",
                                    y = washResult.First().tireshine,
                                    dto = new VolumeDto
                                    {
                                        carCount = washResult.First().tireshine,
                                        pricePerCar = 0,
                                        reportDate = washResult.First().date.ToShortDateString(),
                                        siteName = site.location
                                    }
                                });
                            }
                            else if (package == "rainx")
                            {
                                dtos.Add(new VolumeChartSeriesPoint
                                {
                                    name = "RainX",
                                    y = washResult.First().rainx,
                                    dto = new VolumeDto
                                    {
                                        carCount = washResult.First().rainx,
                                        pricePerCar = 0,
                                        reportDate = washResult.First().date.ToShortDateString(),
                                        siteName = site.location
                                    }
                                });
                            }
                            else if (package == "plusplus")
                            {
                                dtos.Add(new VolumeChartSeriesPoint
                                {
                                    name = "Plus+",
                                    y = washResult.First().plusplus,
                                    dto = new VolumeDto
                                    {
                                        carCount = washResult.First().plusplus,
                                        pricePerCar = 0,
                                        reportDate = washResult.First().date.ToShortDateString(),
                                        siteName = site.location
                                    }
                                });
                            }
                        }
                    }

                }
                if (package == "primeshinewash" || package == "protexwash" || package == "premierwash")
                {
                    results.Add(new VolumeChartSeries
                    {
                        name = package,
                        data = dtos,
                        type = "column",
                        stack = "washes"
                    });
                }
                else
                {
                    results.Add(new VolumeChartSeries
                    {
                        name = package,
                        data = dtos,
                        type = "column",
                        stack = "options"
                    });
                }
            }
            var chart = new VolumeChart
            {
                categories = sites.Select(s => s.location).ToArray<string>(),
                data = results
            };

            return chart;
        }
    }
}