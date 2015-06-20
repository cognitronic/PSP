using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using psp.api.helpers;
using psp.core.domain;
using psp.api.Controllers;
using psp.repository.mongo.Repositories;
using System.Reflection;
using System.Text;

namespace psp.api.Reports
{
    public class GSRReport
    {
        SiteWatch sitewatch = new SiteWatch();
        WashLink washlink = new WashLink();

        public GSR GetAmountToAudit(Site site, DateTime reportdate, string fromTime, string toTime)
        {
            var swData = sitewatch.SitewatchSalesBySiteDate(site.sitewatchid.ToString(), reportdate, fromTime, toTime);
            var wlData = washlink.WashLinkWashTotalsBySiteDate(site, reportdate, fromTime, toTime);
            var gsr = new GSR();
            gsr.siteId = site.sitewatchid.ToString();
            gsr.siteName = site.location;
            gsr.sid = site.Id.ToString();
            gsr.gsrDate = reportdate;

            #region WashLink Wash and Tire Gloss Counts
            if (wlData != null)
            {
                // Prime Shine Wash
                gsr.washLinkTotalPrimeShine_count = wlData.primeshinewash > 0 ? wlData.primeshinewash : 0;
                gsr.washLinkTotalPrimeShine_dollars = gsr.washLinkTotalPrimeShine_count * (int)GSRMultiplier.PLUS_SEVEN;

                // Protex Wash
                gsr.washLinkTotalProtex_count = wlData.protexwash > 0 ? wlData.protexwash : 0;
                gsr.washLinkTotalProtex_dollars = gsr.washLinkTotalProtex_count * (int)GSRMultiplier.PLUS_TEN;

                // Premier Wash
                gsr.washLinkTotalPremier_count = wlData.premierwash > 0 ? wlData.premierwash : 0;
                gsr.washLinkTotalPremier_dollars = gsr.washLinkTotalPremier_count * (int)GSRMultiplier.PLUS_TWELVE;

                // Tire Shine
                gsr.washLinkTotalTireGloss_count = wlData.tireshine > 0 ? wlData.tireshine : 0;
                gsr.washLinkTotalTireGloss_dollars = gsr.washLinkTotalTireGloss_count * (int)GSRMultiplier.PLUS_TWO;

                //Plus+
                gsr.washLinkTotalPlusPlus_count = wlData.plusplus > 0 ? wlData.plusplus : 0;
                gsr.washLinkTotalPlusPlus_dollars = gsr.washLinkTotalPlusPlus_count * (int)GSRMultiplier.PLUS_THREE;

                // RainX
                gsr.washLinkTotalRainX_count = wlData.rainx > 0 ? wlData.rainx : 0;
                gsr.washLinkTotalRainX_dollars = gsr.washLinkTotalRainX_count * (int)GSRMultiplier.PLUS_TWO;



            }
            #endregion

            #region SiteWatch Sales Items and Services
            if (swData.Count > 0)
            {
                foreach (var item in swData)
                {
                    switch (item.objid)
                    {
                        case "49501608": // Tire Gloss
                            gsr.sitewatchTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchTireGloss_count += int.Parse(item.total);
                            break;
                        case "49501617": // Fleet Tire Gloss
                            gsr.sitewatchFleetTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchFleetTireGloss_count += int.Parse(item.total);
                            break;
                        case "608638": // Unlimited Tire Gloss
                            gsr.sitewatchUnlimitedTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchUnlimitedTireGloss_count += int.Parse(item.total);
                            break;
                        case "49501613": // Reapply Tire Gloss
                            gsr.sitewatchReapplyTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchReapplyTireGloss_count += int.Parse(item.total);
                            break;
                        case "606590": // Fleet PSX
                            gsr.sitewatchFleetPsx_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.sitewatchFleetPsx_count = int.Parse(item.total);
                            gsr.sitewatchTotalPrimeShine_dollars += gsr.sitewatchFleetPsx_dollars;
                            break;
                        case "49501440": // PSX Wash
                            gsr.sitewatchPsx_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.sitewatchPsx_count = int.Parse(item.total);
                            gsr.sitewatchTotalPrimeShine_dollars += gsr.sitewatchPsx_dollars;
                            break;
                        case "49501680": // PSX Wash W/TG
                            gsr.sitewatchUnlimitedPsxWithTireGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_NINE;
                            gsr.sitewatchUnlimitedPsxWithTireGloss_count = int.Parse(item.total);
                            gsr.sitewatchTotalPrimeShine_dollars += gsr.sitewatchUnlimitedPsxWithTireGloss_dollars;
                            break;
                        case "606552": // Prime Shine Rewash
                            gsr.sitewatchPrimeShineRewash_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.sitewatchPrimeShineRewash_count = int.Parse(item.total);
                            gsr.sitewatchTotalPrimeShine_dollars += gsr.sitewatchPrimeShineRewash_dollars;
                            break;
                        case "49500138": // Unlimited UCW
                            gsr.sitewatchUnlimitedUcw_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.sitewatchUnlimitedUcw_count = int.Parse(item.total);
                            gsr.sitewatchTotalPrimeShine_dollars += gsr.sitewatchUnlimitedUcw_dollars;
                            break;
                        case "608663": // Enhance PSX With Tire Gloss To Protex
                            gsr.sitewatchEnhancePsxWithTireGlossToProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchEnhancePsxWithTireGlossToProtex_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchEnhancePsxWithTireGlossToProtex_dollars;
                            break;
                        case "49500144": // Enhance PSX To Pro
                            gsr.sitewatchEnhancePsxToProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchEnhancePsxToProtex_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchEnhancePsxToProtex_dollars;
                            break;
                        case "49501448": // Unlimited PSX Wash
                            gsr.sitewatchUnlimitedPsx_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.sitewatchUnlimitedPsx_count = int.Parse(item.total);
                            gsr.sitewatchTotalPrimeShine_dollars += gsr.sitewatchUnlimitedPsx_dollars;
                            break;
                        case "49501521": // Protex Rewash
                            gsr.sitewatchProtexRewash_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchProtexRewash_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchProtexRewash_dollars;
                            break;
                        case "49501535": // Fleet Protex
                            gsr.sitewatchFleetProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchFleetProtex_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchFleetProtex_dollars;
                            break;
                        case "49501616": // PSX Fleet Protex
                            gsr.sitewatchPsxFleetProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchPsxFleetProtex_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchPsxFleetProtex_dollars;
                            break;
                        case "49501531": // Unlimited Protex Wash
                            gsr.sitewatchUnlimitedProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchUnlimitedProtex_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchUnlimitedProtex_dollars;
                            break;
                        case "608644": // Unlimited Protex W/TG Wash
                            gsr.sitewatchUnlimitedProtexWithTireGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWELVE;
                            gsr.sitewatchUnlimitedProtexWithTireGloss_count = int.Parse(item.total);
                            gsr.sitewatchTotalProtex_dollars += gsr.sitewatchUnlimitedProtexWithTireGloss_dollars;
                            break;
                        case "608809": // Premier Rewash
                            gsr.sitewatchPremierRewash_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.sitewatchPremierRewash_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchPremierRewash_dollars;
                            break;
                        case "608917": // Unlimited Premier
                            gsr.sitewatchUnlimitedPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWELVE;
                            gsr.sitewatchUnlimitedPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchUnlimitedPremier_dollars;
                            break;
                        case "608918": // Unlimited Premier W/TG
                            gsr.sitewatchUnlimitedPremierWithTireGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FOURTEEN;
                            gsr.sitewatchUnlimitedPremierWithTireGloss_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchUnlimitedPremierWithTireGloss_dollars;
                            break;
                        case "608817": // Fleet Premier
                            gsr.sitewatchFleetPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.sitewatchFleetPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchFleetPremier_dollars;
                            break;
                        case "608832": // PSX Fleet Premier
                            gsr.sitewatchPsxFleetPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.sitewatchPsxFleetPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchPsxFleetPremier_dollars;
                            break;
                        case "608771": // PSX Fleet Premier No Gloss
                            gsr.sitewatchPsxFleetPremierNoGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.sitewatchPsxFleetPremierNoGloss_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchPsxFleetPremierNoGloss_dollars;
                            break;
                        case "608814": // Enhance PSX To Premier
                            gsr.sitewatchEnhancePsxToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_FIVE;
                            gsr.sitewatchEnhancePsxToPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchEnhancePsxToPremier_dollars;
                            break;
                        case "608815": // Enhance PSX With Tire Gloss To Premier
                            gsr.sitewatchEnhancePsxWithTireGlossToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_SEVEN;
                            gsr.sitewatchEnhancePsxWithTireGlossToPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchEnhancePsxWithTireGlossToPremier_dollars;
                            break;
                        case "608819": // Enhance Protex To Premier
                            gsr.sitewatchEnhanceProtexToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchEnhanceProtexToPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchEnhanceProtexToPremier_dollars;
                            break;
                        case "608820": // Enhance Pro With Tire Gloss Premier
                            gsr.sitewatchEnhanceProtexWithTireGlossToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_FOUR;
                            gsr.sitewatchEnhanceProtexWithTireGlossToPremier_count = int.Parse(item.total);
                            gsr.sitewatchTotalPremier_dollars += gsr.sitewatchEnhanceProtexWithTireGlossToPremier_dollars;
                            break;
                        case "606297": // Prime Shine Wash
                            gsr.sitewatchPrimeShine_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_SEVEN;
                            gsr.sitewatchPrimeShine_count += int.Parse(item.total);
                            break;
                        case "49501519": // Protex Wash
                            gsr.sitewatchProtexWash_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TEN;
                            gsr.sitewatchProtexWash_count += int.Parse(item.total);
                            break;
                        case "608775": // Premier Wash
                            gsr.sitewatchPremierWash_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWELVE;
                            gsr.sitewatchPremierWash_count += int.Parse(item.total);
                            break;
                        case "49501691": // Plus+
                            gsr.sitewatchPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49501690": // RainX
                            gsr.sitewatchRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchRainX_count += int.Parse(item.total);
                            break;
                        case "49501693": // Reapply RainX
                            gsr.sitewatchReapplyRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchReapplyRainX_count += int.Parse(item.total);
                            break;
                        case "609013": // Fleet RainX
                            gsr.sitewatchFleetRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchFleetRainX_count += int.Parse(item.total);
                            break;
                        case "609015": // Unlimited PSX With Plus+
                            gsr.sitewatchUnlimitedPsxWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchUnlimitedPsxWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609014": // Unlimited PSX With RainX
                            gsr.sitewatchUnlimitedPsxWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchUnlimitedPsxWithRainX_count += int.Parse(item.total);
                            break;
                        case "609019": // Unlimited Protex With Plus+
                            gsr.sitewatchUnlimitedProtexWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchUnlimitedProtexWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609018": // Unlimited Protex With RainX
                            gsr.sitewatchUnlimitedProtexWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchUnlimitedProtexWithRainX_count += int.Parse(item.total);
                            break;
                        case "609021": // Unlimited Premier With Plus+
                            gsr.sitewatchUnlimitedPremierWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchUnlimitedPremierWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609020": // Unlimited Premier With RainX
                            gsr.sitewatchUnlimitedPremierWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchUnlimitedPremierWithRainX_count += int.Parse(item.total);
                            break;
                        case "49000311": // Enhance PSX To Protex RainX
                            gsr.sitewatchEnhancePsxToProtexWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchEnhancePsxToProtexWithRainX_count += int.Parse(item.total);
                            break;
                        case "49000310": // Enhance PSX To Protex Plus+
                            gsr.sitewatchEnhancePsxToProtexWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchEnhancePsxToProtexWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000312": // Enhance PSX With Tire Gloss To Protex Plus+
                            gsr.sitewatchEnhancePsxTireGlossToProtexPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchEnhancePsxTireGlossToProtexPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000313": // Enhance PSX With Tire Gloss To Protex RainX
                            gsr.sitewatchEnhancePsxTireGlossToProtexRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.sitewatchEnhancePsxTireGlossToProtexRainX_count += int.Parse(item.total);
                            break;
                        case "49000315": // Enhance PSX To Premier With RainX
                            gsr.sitewatchEnhancePsxToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_FIVE;
                            gsr.sitewatchEnhancePsxToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000316": // Enhance PSX To Premier With Plus+
                            gsr.sitewatchEnhancePsxToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_FIVE;
                            gsr.sitewatchEnhancePsxToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000317": // Enhance PSX With Tire Gloss To Premier With Plus+
                            gsr.sitewatchEnhancePsxTireGlossToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_FIVE;
                            gsr.sitewatchEnhancePsxTireGlossToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000318": // Enhance PSX With Tire Gloss To Premier With RainX
                            gsr.sitewatchEnhancePsxTireGlossToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_FIVE;
                            gsr.sitewatchEnhancePsxTireGlossToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000319": // Enhance Protex To Premier With Plus+
                            gsr.sitewatchEnhanceProtexToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchEnhanceProtexToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000320": // Enhance Protex To Premier With RainX
                            gsr.sitewatchEnhanceProtexToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchEnhanceProtexToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000321": // Enhance Protex To Premier With RainX
                            gsr.sitewatchEnhanceProtexTireGlossToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchEnhanceProtexTireGlossToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000322": // Enhance Protex To Premier With Plus+
                            gsr.sitewatchEnhanceProtexTireGlossToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.sitewatchEnhanceProtexTireGlossToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000324": // Reapply Fleet Plus+
                            gsr.sitewatchReapplyFleetPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchReapplyFleetPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609016": // Fleet Plus+
                            gsr.sitewatchFleetPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchFleetPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000325": // Reapply Fleet RainX
                            gsr.sitewatchReapplyFleetRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchReapplyFleetRainX_count += int.Parse(item.total);
                            break;
                        case "49000326": // Reapply Plus+
                            gsr.sitewatchReapplyPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchReapplyPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609017": // Unlimited Plus+
                            gsr.sitewatchUnlimitedPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchUnlimitedPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609012": // Unlimited RainX
                            gsr.sitewatchUnlimitedRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.sitewatchUnlimitedRainX_count += int.Parse(item.total);
                            break;
                    }
                }
            }

            #endregion

            #region Unlimited PSX and Totals



            gsr.washLinkTotalWashes_count = gsr.washLinkTotalPrimeShine_count +
                gsr.washLinkTotalProtex_count +
                gsr.washLinkTotalPremier_count;

            //SW Protex Total
            gsr.sitewatchTotalProtex_count = gsr.sitewatchProtexWash_count +
                gsr.sitewatchProtexRewash_count +
                gsr.sitewatchEnhancePsxToProtexWithPlusPlus_count +
                gsr.sitewatchEnhancePsxToProtexWithRainX_count +
                gsr.sitewatchEnhancePsxWithTireGlossToProtex_count +
                gsr.sitewatchUnlimitedProtex_count +
                gsr.sitewatchFleetProtex_count +
                gsr.sitewatchPsxFleetProtex_count +
                gsr.sitewatchEnhancePsxToProtex_count +
                gsr.sitewatchUnlimitedProtexWithTireGloss_count +
                gsr.sitewatchUnlimitedProtexWithPlusPlus_count +
                gsr.sitewatchUnlimitedProtexWithRainX_count;
            //SW Protex Total Dollars
            gsr.sitewatchTotalProtex_dollars = (gsr.sitewatchTotalProtex_count * (int)GSRMultiplier.PLUS_TEN) +
                gsr.sitewatchProtexRewash_dollars +
                //gsr.sitewatchEnhancePsxToProtexWithPlusPlus_dollars +
                //gsr.sitewatchEnhancePsxToProtexWithRainX_dollars +
                //gsr.sitewatchEnhancePsxWithTireGlossToProtex_dollars +
                gsr.sitewatchUnlimitedProtex_dollars +
                gsr.sitewatchFleetProtex_dollars +
                gsr.sitewatchPsxFleetProtex_dollars +
                //gsr.sitewatchEnhancePsxToProtex_dollars +
                gsr.sitewatchUnlimitedProtexWithTireGloss_dollars +
                gsr.sitewatchUnlimitedProtexWithPlusPlus_dollars +
                gsr.sitewatchUnlimitedProtexWithRainX_dollars;

            // Protex Total Dollars
            gsr.totalProtex_dollars = gsr.washLinkTotalProtex_dollars +
                (gsr.sitewatchProtexRewash_count * (int)GSRMultiplier.NEGATIVE_TEN) +
                (gsr.sitewatchUnlimitedProtex_count * (int)GSRMultiplier.NEGATIVE_TEN) +
                (gsr.sitewatchFleetProtex_count * (int)GSRMultiplier.NEGATIVE_TEN) +
                (gsr.sitewatchPsxFleetProtex_count * (int)GSRMultiplier.NEGATIVE_TEN) +
                (gsr.sitewatchUnlimitedProtexWithTireGloss_count * (int)GSRMultiplier.NEGATIVE_TEN) +
                (gsr.sitewatchUnlimitedProtexWithPlusPlus_count * (int)GSRMultiplier.NEGATIVE_TEN) +
                (gsr.sitewatchUnlimitedProtexWithRainX_count * (int)GSRMultiplier.NEGATIVE_TEN);

            //gsr.totalProtex_diff = (gsr.sitewatchTotalProtex_count - gsr.washLinkTotalProtex_count) * (int)GSRMultiplier.PLUS_TEN;
            gsr.totalProtex_diff = (gsr.sitewatchTotalProtex_dollars - gsr.totalProtex_dollars);

            //SW Premier Total
            gsr.sitewatchTotalPremier_count = gsr.sitewatchPremierWash_count +
                gsr.sitewatchPremierRewash_count +
                gsr.sitewatchEnhancePsxToPremier_count +
                gsr.sitewatchEnhancePsxToPremierPlusPlus_count +
                gsr.sitewatchEnhancePsxToPremierRainX_count +
                gsr.sitewatchEnhancePsxWithTireGlossToPremier_count +
                gsr.sitewatchEnhanceProtexToPremier_count +
                gsr.sitewatchEnhanceProtexWithTireGlossToPremier_count +
                gsr.sitewatchPsxFleetPremier_count +
                gsr.sitewatchUnlimitedPremier_count +
                gsr.sitewatchUnlimitedPremierWithPlusPlus_count +
                gsr.sitewatchUnlimitedPremierWithRainX_count +
                gsr.sitewatchUnlimitedPremierWithTireGloss_count +
                gsr.sitewatchPsxFleetPremierNoGloss_count +
                gsr.sitewatchFleetPremier_count;

            //SW Premier Total Dollars
            gsr.sitewatchTotalPremier_dollars = (gsr.sitewatchTotalPremier_count * (int)GSRMultiplier.PLUS_TWELVE) +
                gsr.sitewatchPremierRewash_dollars +
                gsr.sitewatchPsxFleetPremier_dollars +
                gsr.sitewatchUnlimitedPremier_dollars +
                gsr.sitewatchUnlimitedPremierWithPlusPlus_dollars +
                gsr.sitewatchUnlimitedPremierWithRainX_dollars +
                gsr.sitewatchUnlimitedPremierWithTireGloss_dollars +
                gsr.sitewatchPsxFleetPremierNoGloss_dollars +
                gsr.sitewatchFleetPremier_dollars;

            // Premier Total Dollars
            gsr.totalPremier_dollars = gsr.washLinkTotalPremier_dollars +
                (gsr.sitewatchPremierRewash_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchPsxFleetPremier_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchUnlimitedPremier_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchUnlimitedPremierWithPlusPlus_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchUnlimitedPremierWithRainX_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchUnlimitedPremierWithTireGloss_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchPsxFleetPremierNoGloss_count * (int)GSRMultiplier.NEGATIVE_TWELVE) +
                (gsr.sitewatchFleetPremier_count * (int)GSRMultiplier.NEGATIVE_TWELVE);

            gsr.totalPremier_diff = (gsr.sitewatchTotalPremier_dollars - gsr.totalPremier_dollars);

            //SW Prime Shine Total
            gsr.sitewatchTotalPrimeShine_count = (gsr.sitewatchPrimeShine_count +
                gsr.sitewatchFleetPsx_count +
                gsr.sitewatchPsx_count +
                gsr.sitewatchPrimeShineRewash_count +
                gsr.sitewatchUnlimitedPsx_count +
                gsr.sitewatchUnlimitedPsxWithTireGloss_count +
                gsr.sitewatchUnlimitedPsxWithPlusPlus_count +
                gsr.sitewatchUnlimitedPsxWithRainX_count +
                gsr.sitewatchUnlimitedUcw_count) - (gsr.sitewatchTotalProtex_count + gsr.sitewatchTotalPremier_count);

            // Prime Shine Total Dollars
            gsr.totalPrimeShine_dollars = gsr.washLinkTotalPrimeShine_dollars +
                gsr.sitewatchFleetPsx_dollars +
                gsr.sitewatchPsx_dollars +
                gsr.sitewatchPrimeShineRewash_dollars +
                gsr.sitewatchUnlimitedPsx_dollars +
                gsr.sitewatchUnlimitedUcw_dollars +
                gsr.sitewatchUnlimitedPsxWithTireGloss_dollars +
                gsr.sitewatchUnlimitedPsxWithPlusPlus_dollars +
                gsr.sitewatchUnlimitedPsxWithRainX_dollars;
            gsr.totalPrimeShine_diff = (gsr.sitewatchTotalPrimeShine_count - gsr.washLinkTotalPrimeShine_count) * (int)GSRMultiplier.PLUS_SEVEN;

            // SW Tire Gloss
            gsr.sitewatchTotalTireGloss_count = gsr.sitewatchTireGloss_count +
                gsr.sitewatchFleetTireGloss_count +
                gsr.sitewatchReapplyTireGloss_count +
                gsr.sitewatchUnlimitedTireGloss_count +
                gsr.sitewatchEnhancePsxWithTireGlossToProtex_count +
                gsr.sitewatchEnhancePsxWithTireGlossToPremier_count +
                gsr.sitewatchEnhanceProtexWithTireGlossToPremier_count +
                gsr.sitewatchUnlimitedPremierWithTireGloss_count +
                gsr.sitewatchUnlimitedProtexWithTireGloss_count +
                gsr.sitewatchEnhanceProtexTireGlossToPremierPlusPlus_count +
                gsr.sitewatchEnhanceProtexTireGlossToPremierRainX_count +
                gsr.sitewatchEnhancePsxTireGlossToPremierPlusPlus_count +
                gsr.sitewatchEnhancePsxTireGlossToPremierRainX_count +
                gsr.sitewatchEnhancePsxTireGlossToProtexPlusPlus_count +
                gsr.sitewatchEnhancePsxTireGlossToProtexRainX_count +
                gsr.sitewatchUnlimitedPsxWithTireGloss_count;

            gsr.totalTireGloss_dollars = gsr.washLinkTotalTireGloss_dollars +
                gsr.sitewatchFleetTireGloss_dollars +
                gsr.sitewatchUnlimitedTireGloss_dollars +
                gsr.sitewatchReapplyTireGloss_dollars;

            gsr.tireGloss_diff = (gsr.sitewatchTotalTireGloss_count - gsr.washLinkTotalTireGloss_count) * (int)GSRMultiplier.PLUS_TWO;
            gsr.totalTireGloss_diff = (gsr.sitewatchTotalTireGloss_count - gsr.washLinkTotalTireGloss_count) * (int)GSRMultiplier.PLUS_TWO;

            // Rain-X
            gsr.sitewatchTotalRainX_count = gsr.sitewatchRainX_count +
                gsr.sitewatchReapplyRainX_count +
                gsr.sitewatchUnlimitedPremierWithRainX_count +
                gsr.sitewatchUnlimitedProtexWithRainX_count +
                gsr.sitewatchUnlimitedPsxWithRainX_count +
                gsr.sitewatchEnhanceProtexTireGlossToPremierRainX_count +
                gsr.sitewatchEnhanceProtexToPremierRainX_count +
                gsr.sitewatchEnhancePsxTireGlossToProtexRainX_count +
                gsr.sitewatchEnhancePsxToProtexWithRainX_count +
                gsr.sitewatchEnhancePsxToPremierRainX_count +
                gsr.sitewatchReapplyFleetRainX_count +
                gsr.sitewatchFleetRainX_count +
                gsr.sitewatchUnlimitedRainX_count +
                gsr.sitewatchEnhancePsxTireGlossToPremierRainX_count;

            gsr.totalRainX_dollars = gsr.washLinkTotalRainX_dollars +
                gsr.sitewatchFleetRainX_dollars +
                gsr.sitewatchUnlimitedRainX_dollars +
                gsr.sitewatchReapplyRainX_dollars;
            gsr.totalRainX_diff = (gsr.sitewatchTotalRainX_count - gsr.washLinkTotalRainX_count) * (int)GSRMultiplier.PLUS_TWO;

            // Plus+
            gsr.sitewatchTotalPlusPlus_count = gsr.sitewatchPlusPlus_count +
                gsr.sitewatchReapplyPlusPlus_count +
                gsr.sitewatchReapplyFleetPlusPlus_count +
                gsr.sitewatchFleetPlusPlus_count +
                gsr.sitewatchUnlimitedPlusPlus_count;// +
            //gsr.sitewatchUnlimitedPremierWithPlusPlus_count +
            //gsr.sitewatchUnlimitedProtexWithPlusPlus_count +
            //gsr.sitewatchUnlimitedPsxWithPlusPlus_count +
            //gsr.sitewatchEnhanceProtexTireGlossToPremierPlusPlus_count +
            //gsr.sitewatchEnhanceProtexToPremierPlusPlus_count +
            //gsr.sitewatchEnhancePsxTireGlossToProtexPlusPlus_count +
            //gsr.sitewatchEnhancePsxToProtexWithPlusPlus_count +
            //gsr.sitewatchEnhancePsxToPremierPlusPlus_count +
            //gsr.sitewatchEnhancePsxTireGlossToPremierPlusPlus_count;

            gsr.totalPlusPlus_dollars = gsr.washLinkTotalPlusPlus_dollars +
                gsr.sitewatchReapplyPlusPlus_dollars +
                gsr.sitewatchReapplyFleetPlusPlus_dollars +
                gsr.sitewatchFleetPlusPlus_dollars +
                gsr.sitewatchUnlimitedPlusPlus_dollars;
            gsr.totalPlusPlus_diff = (gsr.sitewatchTotalPlusPlus_count - gsr.washLinkTotalPlusPlus_count) * (int)GSRMultiplier.PLUS_THREE;

            gsr.sitewatchTotalWashes_count = gsr.sitewatchTotalPrimeShine_count +
                gsr.sitewatchTotalProtex_count +
                gsr.sitewatchTotalPremier_count;

            gsr.totalWashes_diff = gsr.totalTireGloss_diff +
                gsr.totalPrimeShine_diff +
                gsr.totalProtex_diff +
                gsr.totalPlusPlus_diff +
                gsr.totalRainX_diff +
                gsr.totalPremier_diff;

            gsr.totalWashes_dollars = gsr.totalPrimeShine_dollars +
                gsr.totalProtex_dollars +
                gsr.totalPremier_dollars +
                gsr.totalRainX_dollars +
                gsr.totalPlusPlus_dollars +
                gsr.totalTireGloss_dollars;
            #endregion



            if (swData.Count > 0)
            {
                #region Impulse Items
                foreach (var impulseItem in swData)
                {
                    if (impulseItem.reportcategory.Equals("500025"))
                    {
                        if (!string.IsNullOrEmpty(impulseItem.total))
                        {
                            gsr.impulseItems += (Math.Abs(int.Parse(impulseItem.total)) * decimal.Parse(impulseItem.val));
                        }
                    }
                }
                #endregion

                #region Machine Sales
                foreach (var machineSales in swData)
                {
                    if (machineSales.reportcategory.Equals("500036") ||
                        machineSales.reportcategory.Equals("500037") ||
                        machineSales.reportcategory.Equals("500091") ||
                        machineSales.reportcategory.Equals("500057"))
                    {
                        if (!string.IsNullOrEmpty(machineSales.total))
                        {
                            gsr.machineSales += (Math.Abs(int.Parse(machineSales.total)) * decimal.Parse(machineSales.val));
                        }
                    }
                }
                #endregion

                #region Web connect
                foreach (var webConnect in swData)
                {
                    if (webConnect.reportcategory.Equals("500103") ||
                        webConnect.reportcategory.Equals("500103") ||
                        webConnect.reportcategory.Equals("500100") ||
                        webConnect.reportcategory.Equals("500102") ||
                        webConnect.reportcategory.Equals("500101") ||
                        webConnect.reportcategory.Equals("49000002"))
                    {
                        if (!string.IsNullOrEmpty(webConnect.total))
                        {
                            if (!string.IsNullOrEmpty(webConnect.val))
                                gsr.webConnect += (Math.Abs(int.Parse(webConnect.total) * decimal.Parse(webConnect.val))) * (int)GSRMultiplier.NEGATIVE_ONE;
                            else
                                gsr.webConnect += (Math.Abs(int.Parse(webConnect.total) * decimal.Parse(webConnect.amt))) * (int)GSRMultiplier.NEGATIVE_ONE;
                        }
                    }
                }
                #endregion

                #region Pre Paids

                foreach (var prePaids in swData)
                {
                    if (prePaids.reportcategory.Equals("52167") || prePaids.reportcategory.Equals("500058"))
                    {
                        if (!string.IsNullOrEmpty(prePaids.total))
                        {
                            gsr.prePaids += (Math.Abs(int.Parse(prePaids.total)) * decimal.Parse(prePaids.amt));
                        }
                    }
                }
                #endregion

                #region Pump Code Wash

                foreach (var pumpCode in swData)
                {
                    if (pumpCode.reportcategory.Equals("500052"))
                    {
                        if (!string.IsNullOrEmpty(pumpCode.total))
                        {
                            gsr.pumpCodeWash += (Math.Abs(int.Parse(pumpCode.total)) * decimal.Parse(pumpCode.amt));
                        }
                    }
                }
                #endregion

                #region Coupons and Discounts
                foreach (var coupons in swData)
                {
                    if (coupons.reportcategory.Equals("59458") ||
                        coupons.reportcategory.Equals("49500003") ||
                        coupons.reportcategory.Equals("49500001") ||
                        coupons.reportcategory.Equals("500028") ||
                        coupons.reportcategory.Equals("500064") ||
                        coupons.reportcategory.Equals("1000003") ||
                        coupons.reportcategory.Equals("49500344") ||
                        coupons.reportcategory.Equals("49500342"))
                    {
                        if (!string.IsNullOrEmpty(coupons.total))
                        {
                            if (!string.IsNullOrEmpty(coupons.amt))
                                gsr.couponsAndDiscounts += ((int.Parse(coupons.total) * Math.Abs(decimal.Parse(coupons.amt))) * (int)GSRMultiplier.NEGATIVE_ONE);
                        }
                    }
                }
                #endregion

                #region Total Paidout / Refunds

                foreach (var totalPaidOut in swData)
                {
                    if (totalPaidOut.reportcategory.Equals("103965"))
                    {
                        if (!string.IsNullOrEmpty(totalPaidOut.total))
                        {
                            gsr.totalPaidoutRefunds += (Math.Abs(int.Parse(totalPaidOut.total) * decimal.Parse(totalPaidOut.amt))) * (int)GSRMultiplier.NEGATIVE_ONE;
                        }
                    }
                }
                #endregion

                #region Cash Deposit

                foreach (var cashDeposit in swData)
                {
                    if (cashDeposit.reportcategory.Equals("500044"))
                    {
                        if (!string.IsNullOrEmpty(cashDeposit.total))
                        {
                            gsr.cashDeposit += (Math.Abs(int.Parse(cashDeposit.total)) * decimal.Parse(cashDeposit.amt));
                        }
                    }
                }
                #endregion

                #region Credit Cards

                foreach (var creditCards in swData)
                {
                    if (creditCards.reportcategory.Equals("500050"))
                    {
                        if (!string.IsNullOrEmpty(creditCards.total))
                        {
                            gsr.creditCards += (Math.Abs(int.Parse(creditCards.total)) * decimal.Parse(creditCards.amt));
                        }
                    }
                }
                #endregion

            }

            // Total Wash Service
            gsr.totalWashServices = gsr.totalWashes_dollars + gsr.impulseItems + gsr.webConnect;

            // Total To Account For
            gsr.totalToAccountFor = gsr.totalWashServices +
                gsr.machineSales +
                gsr.pumpCodeWash +
                gsr.prePaids -
                ((gsr.couponsAndDiscounts * (int)GSRMultiplier.NEGATIVE_ONE) - gsr.totalPaidoutRefunds);
            if (gsr.washLinkTotalWashes_count > 0)
            {
                gsr.totalToAccountForPerCar_dollars = Math.Round(Convert.ToDouble(gsr.totalToAccountFor / gsr.washLinkTotalWashes_count), 2);
            }
            else
            {
                gsr.totalToAccountForPerCar_dollars = 0;
            }

            // Total Over
            gsr.totalOverUnder_dollars = ((gsr.cashDeposit + gsr.creditCards) + gsr.totalToAccountFor) * (int)GSRMultiplier.NEGATIVE_ONE;
            gsr.totalOverUnder_diff = gsr.totalTireGloss_diff +
                gsr.totalPlusPlus_diff +
                gsr.totalRainX_diff +
                gsr.totalPrimeShine_diff +
                gsr.totalProtex_diff +
                gsr.totalPremier_diff;

            // Total Over Excluding Cars
            int iExcludingCars = ((gsr.sitewatchTotalPrimeShine_count - gsr.washLinkTotalPrimeShine_count) * (int)GSRMultiplier.PLUS_SEVEN) +
                ((gsr.sitewatchTotalProtex_count - gsr.washLinkTotalProtex_count) * (int)GSRMultiplier.PLUS_TEN) +
                ((gsr.sitewatchTotalPremier_count - gsr.washLinkTotalPremier_count) * (int)GSRMultiplier.PLUS_TWELVE) +
                gsr.sitewatchTotalPlusPlus_count +
                gsr.sitewatchTotalRainX_count +
                gsr.sitewatchTotalTireGloss_count;
            decimal dExcludingCars = gsr.totalPrimeShine_diff +
                gsr.totalProtex_diff +
                gsr.totalPremier_diff +
                gsr.totalPlusPlus_diff +
                gsr.totalRainX_diff +
                gsr.totalTireGloss_diff;

            gsr.amountToAudit = gsr.totalOverUnder_dollars + ((dExcludingCars) * (int)GSRMultiplier.NEGATIVE_ONE);

            return gsr;
        }

        public IList<SiteWatchSalesItem> BuildNotificationData(string gsrDate, bool saveReport)
        {
            var sites = new SiteController().Get();
            var list = new List<SiteWatchSalesItem>();
            AuditService.SaveLog(new AuditLog
            {
                auditDate = DateTime.Now,
                message = "Inside the build notification data",
                type = psp.core.ResourceStrings.Audit_Report,
                name = "Saving GSR"
            });
            foreach (var site in sites)
            {
                var gsr = new GSRReport().GetAmountToAudit(site, DateTime.Parse(gsrDate), "", "");
                if (gsr != null)
                {
                    list.Add(new SiteWatchSalesItem
                    {
                        total = gsr.sitewatchTotalWashes_count.ToString(),
                        locationid = site.sitewatchid,
                        sitename = site.name,
                        val = gsr.totalOverUnder_dollars.ToString(),
                        amt = gsr.amountToAudit.ToString()
                    });
                    try
                    {
                        if (saveReport)
                        {
                            new psp.repository.mongo.Repositories.GSRRepository().Save(gsr);
                            AuditService.SaveLog(new AuditLog
                            {
                                auditDate = DateTime.Now,
                                message = "GSR saved for date " + gsrDate + " and site: " + site.name,
                                type = psp.core.ResourceStrings.Audit_Report,
                                notification = gsr,
                                name = "Saving GSR"
                            });
                        }
                    }
                    catch (Exception exc)
                    {
                        AuditService.SaveLog(new AuditLog
                        {
                            auditDate = DateTime.Now,
                            message = "Could not save gsr for" + site.name + ".  The error thrown is: " + exc.Message,
                            type = psp.core.ResourceStrings.Audit_Report,
                            name = "Saving GSR"
                        });
                    }
                }
            }
            return list;
        }

        public NotificationMessage BuildNotifications(IList<SiteWatchSalesItem> items, string date)
        {
            var sb = NotificationTemplates.StandardNotificationHeader("Prime Shine GSR Report", date);
            sb.Append("<table style='width: 400px; font-weight: 900; border-collapse: collapse;'>");
            int i = 0;
            sb.Append("<tr><td width='250px'><h4>Location</h4></td><td width='75px'><h4>Amt. To Audit</h4></td><td width='75px'><h4>Total O/U</h4></td></tr>");
            decimal ttlOU = 0;
            decimal ttlaa = 0;
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
                if (item.amt.Contains("-"))
                {
                    sb.Append("<td width='75px' style='color: #ff0000;'>$(");
                    sb.Append(item.amt);
                    sb.Append(")</td>");
                }
                else
                {
                    sb.Append("<td width='75px'>$");
                    sb.Append(item.amt);
                    sb.Append("</td>");
                }

                if (item.val.Contains("-"))
                {
                    sb.Append("<td width='75px' style='color: #ff0000;'>$(");
                    sb.Append(item.val);
                    sb.Append(")</td>");
                }
                else
                {
                    sb.Append("<td width='75px'>$");
                    sb.Append(item.val);
                    sb.Append("</td>");
                }
                sb.Append("</td>");
                sb.Append("</tr>");

                ttlaa += Convert.ToDecimal(item.amt);
                ttlOU += Convert.ToDecimal(item.val);
            }
            sb.Append("<tr style='border-bottom: solid 20px #ffffff;'>");
            sb.Append("<td width='250px'><h4>Total:</h4>");
            if (ttlaa < 0)
            {
                sb.Append("<td width='75px' style='color: #ff0000;'>$(");
                sb.Append(ttlaa.ToString());
                sb.Append(")</td>");
            }
            else
            {
                sb.Append("<td width='75px'>$");
                sb.Append(ttlaa.ToString());
                sb.Append("</td>");
            }
            if (ttlOU < 0)
            {
                sb.Append("<td width='75px' style='color: #ff0000;'>$(");
                sb.Append(ttlOU.ToString());
                sb.Append(")</td>");
            }
            else
            {
                sb.Append("<td width='75px'>$");
                sb.Append(ttlOU.ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr></table>");

            var notification = new NotificationsController().GetByName("GSR_Audit");
            var message = new NotificationMessage();
            foreach (var email in notification.recipients)
            {
                message.ToEmails.Add(email);
            }
            foreach (var bcc in notification.bccemails)
            {
                message.Bccs.Add(bcc);
            }
            message.FromEmail = notification.fromemail;
            message.Subject = notification.subject.Replace("!!date!!", date);
            message.MessageBody = NotificationTemplates.StandardNotificationFooter(sb);
            return message;
        }

        public String ExportGSRToCSV(IList<Site> sites, string dateRange)
        {
            var gsrList = new List<GSR>();
            var dates = psp.core.helpers.DateUtilities.GetRangesByName(dateRange);
            foreach (var site in sites)
            {
                if (site != null)
                {
                    gsrList.AddRange(new GSRRepository().GetBySiteDateRange(dates["StartDate"], dates["EndDate"], site.location));
                }
            }

            var sb = new StringBuilder();
            PropertyInfo[] properties;
            properties = typeof(GSR).GetProperties();
            var gsrProps = "";

            //build header row
            foreach (var prop in properties)
            {
                gsrProps += prop.Name + ",";
            }
            gsrProps = gsrProps.Substring(0, gsrProps.Length - 1) + "\r\n";
            sb.Append(gsrProps);

            //build data rows
            foreach (var gsr in gsrList)
            {
                var gsrVals = "";
                foreach (var vals in properties)
                {
                    gsrVals += vals.GetValue(gsr) + ",";
                }
                gsrVals = gsrVals.Substring(0, gsrVals.Length - 1) + "\r\n";
                sb.Append(gsrVals);
            }
            return sb.ToString();
        }
    }
}
