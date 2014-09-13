using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using psp.api.helpers;
using psp.core.domain;

namespace psp.api.Reports
{
    public class GSRReport
    {
        SiteWatch sitewatch = new SiteWatch();
        WashLink washlink = new WashLink();

        public GSR GetAmountToAudit(Site site, DateTime reportdate)
        {
            var swData = sitewatch.SitewatchSalesBySiteDate(site.sitewatchid, reportdate);
            var  wlData = washlink.WashLinkWashTotalsBySiteDate(site, reportdate); 
            var gsr = new GSR();
            gsr.siteId = site.sitewatchid;
            gsr.siteName = site.name;
            gsr.sid = site.Id.ToString();
            gsr.gsrDate = reportdate.ToShortDateString();

            #region WashLink Wash and Tire Gloss Counts
            if (wlData != null)
            {
                // Prime Shine Wash
                gsr.washLinkTotalPrimeShine_count = wlData.primeshinewash > 0 ? wlData.primeshinewash: 0;
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
                //gsr.washLinkTotalPlusPlus_count = wlData.plusplus > 0 ? wlData.pluplus : 0;
                //gsr.washLinkTotalPlusPlus_dollars = gsr.washLinkTotalPlusPlus_count * (int)GSRMultiplier.PLUS_THREE;

                // RainX
                gsr.washLinkTotalRainX_count = wlData.rainx > 0 ? wlData.rainx : 0;
                gsr.washLinkTotalRainX_dollars = gsr.washLinkTotalRainX_count * (int)GSRMultiplier.PLUS_THREE;



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
                            gsr.siteWatchTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchTireGloss_count += int.Parse(item.total);
                            break;
                        case "49501617": // Fleet Tire Gloss
                            gsr.siteWatchFleetTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.siteWatchFleetTireGloss_count += int.Parse(item.total);
                            break;
                        case "608638": // Unlimited Tire Gloss
                            gsr.siteWatchUnlimitedTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.siteWatchUnlimitedTireGloss_count += int.Parse(item.total);
                            break;
                        case "49501613": // Reapply Tire Gloss
                            gsr.siteWatchReapplyTireGloss_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWO;
                            gsr.siteWatchReapplyTireGloss_count += int.Parse(item.total);
                            break;
                        case "606590": // Fleet PSX
                            gsr.siteWatchFleetPsx_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.siteWatchFleetPsx_count = int.Parse(item.total);
                            gsr.siteWatchTotalPrimeShine_dollars += gsr.siteWatchFleetPsx_dollars;
                            break;
                        case "49501440": // PSX Wash
                            gsr.siteWatchPsx_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.siteWatchPsx_count = int.Parse(item.total);
                            gsr.siteWatchTotalPrimeShine_dollars += gsr.siteWatchPsx_dollars;
                            break;
                        case "49501680": // PSX Wash W/TG
                            gsr.siteWatchUnlimitedPsxWithTireGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_NINE;
                            gsr.siteWatchUnlimitedPsxWithTireGloss_count = int.Parse(item.total);
                            gsr.siteWatchTotalPrimeShine_dollars += gsr.siteWatchUnlimitedPsxWithTireGloss_dollars;
                            break;
                        case "606552": // Prime Shine Rewash
                            gsr.siteWatchPrimeShineRewash_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.siteWatchPrimeShineRewash_count = int.Parse(item.total);
                            gsr.siteWatchTotalPrimeShine_dollars += gsr.siteWatchPrimeShineRewash_dollars;
                            break;
                        case "49500138": // Unlimited UCW
                            gsr.siteWatchUnlimitedUcw_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.siteWatchUnlimitedUcw_count = int.Parse(item.total);
                            gsr.siteWatchTotalPrimeShine_dollars += gsr.siteWatchUnlimitedUcw_dollars;
                            break;
                        case "608663": // Enhance PSX With Tire Gloss To Protex
                            gsr.siteWatchEnhancePsxWithTireGlossToProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.siteWatchEnhancePsxWithTireGlossToProtex_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchEnhancePsxWithTireGlossToProtex_dollars;
                            break;
                        case "49500144": // Enhance PSX To Pro
                            gsr.siteWatchEnhancePsxToProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.siteWatchEnhancePsxToProtex_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchEnhancePsxToProtex_dollars;
                            break;
                        case "49501448": // Unlimited PSX Wash
                            gsr.siteWatchUnlimitedPsx_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.siteWatchUnlimitedPsx_count = int.Parse(item.total);
                            gsr.siteWatchTotalPrimeShine_dollars += gsr.siteWatchUnlimitedPsx_dollars;
                            break;
                        case "49501521": // Protex Rewash
                            gsr.siteWatchProtexRewash_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.siteWatchProtexRewash_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchProtexRewash_dollars;
                            break;
                        case "49501535": // Fleet Protex
                            gsr.siteWatchFleetProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.siteWatchFleetProtex_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchFleetProtex_dollars;
                            break;
                        case "49501616": // PSX Fleet Protex
                            gsr.siteWatchPsxFleetProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.siteWatchPsxFleetProtex_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchPsxFleetProtex_dollars;
                            break;
                        case "49501531": // Unlimited Protex Wash
                            gsr.siteWatchUnlimitedProtex_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.siteWatchUnlimitedProtex_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchUnlimitedProtex_dollars;
                            break;
                        case "608644": // Unlimited Protex W/TG Wash
                            gsr.siteWatchUnlimitedProtexWithTireGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWELVE;
                            gsr.siteWatchUnlimitedProtexWithTireGloss_count = int.Parse(item.total);
                            gsr.siteWatchTotalProtex_dollars += gsr.siteWatchUnlimitedProtexWithTireGloss_dollars;
                            break;
                        case "608809": // Premier Rewash
                            gsr.siteWatchPremierRewash_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.siteWatchPremierRewash_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchPremierRewash_dollars;
                            break;
                        case "608917": // Unlimited Premier
                            gsr.siteWatchUnlimitedPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_TWELVE;
                            gsr.siteWatchUnlimitedPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchUnlimitedPremier_dollars;
                            break;
                        case "608918": // Unlimited Premier W/TG
                            gsr.siteWatchUnlimitedPremierWithTireGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FOURTEEN;
                            gsr.siteWatchUnlimitedPremierWithTireGloss_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchUnlimitedPremierWithTireGloss_dollars;
                            break;
                        case "608817": // Fleet Premier
                            gsr.siteWatchFleetPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.siteWatchFleetPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchFleetPremier_dollars;
                            break;
                        case "608832": // PSX Fleet Premier
                            gsr.siteWatchPsxFleetPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_SEVEN;
                            gsr.siteWatchPsxFleetPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchPsxFleetPremier_dollars;
                            break;
                        case "608771": // PSX Fleet Premier No Gloss
                            gsr.siteWatchPsxFleetPremierNoGloss_dollars = int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_FIVE;
                            gsr.siteWatchPsxFleetPremierNoGloss_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchPsxFleetPremierNoGloss_dollars;
                            break;
                        case "608814": // Enhance PSX To Premier
                            gsr.siteWatchEnhancePsxToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_FIVE;
                            gsr.siteWatchEnhancePsxToPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchEnhancePsxToPremier_dollars;
                            break;
                        case "608815": // Enhance PSX With Tire Gloss To Premier
                            gsr.siteWatchEnhancePsxWithTireGlossToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_SEVEN;
                            gsr.siteWatchEnhancePsxWithTireGlossToPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchEnhancePsxWithTireGlossToPremier_dollars;
                            break;
                        case "608819": // Enhance Protex To Premier
                            gsr.siteWatchEnhanceProtexToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhanceProtexToPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchEnhanceProtexToPremier_dollars;
                            break;
                        case "608820": // Enhance Pro With Tire Gloss Premier
                            gsr.siteWatchEnhanceProtexWithTireGlossToPremier_dollars = int.Parse(item.total) * (int)GSRMultiplier.PLUS_FOUR;
                            gsr.siteWatchEnhanceProtexWithTireGlossToPremier_count = int.Parse(item.total);
                            gsr.siteWatchTotalPremier_dollars += gsr.siteWatchEnhanceProtexWithTireGlossToPremier_dollars;
                            break;
                        case "606297": // Prime Shine Wash
                            gsr.siteWatchPrimeShine_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_SEVEN;
                            gsr.siteWatchPrimeShine_count += int.Parse(item.total);
                            break;
                        case "49501519": // Protex Wash
                            gsr.siteWatchProtexWash_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TEN;
                            gsr.siteWatchProtexWash_count += int.Parse(item.total);
                            break;
                        case "608775": // Premier Wash
                            gsr.siteWatchPremierWash_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWELVE;
                            gsr.siteWatchPremierWash_count += int.Parse(item.total);
                            break;
                        case "49501691": // Plus+
                            gsr.siteWatchPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_THREE;
                            gsr.siteWatchPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49501690": // RainX
                            gsr.siteWatchRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchRainX_dollars += int.Parse(item.total);
                            break;
                        case "49501693": // Reapply RainX
                            gsr.siteWatchReapplyRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchReapplyRainX_count += int.Parse(item.total);
                            break;
                        case "609015": // Unlimited PSX With Plus+
                            gsr.siteWatchUnlimitedPsxWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchUnlimitedPsxWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609014": // Unlimited PSX With RainX
                            gsr.siteWatchUnlimitedPsxWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchUnlimitedPsxWithRainX_count += int.Parse(item.total);
                            break;
                        case "609019": // Unlimited Protex With Plus+
                            gsr.siteWatchUnlimitedProtexWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchUnlimitedProtexWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609018": // Unlimited Protex With RainX
                            gsr.siteWatchUnlimitedProtexWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchUnlimitedProtexWithRainX_count += int.Parse(item.total);
                            break;
                        case "609021": // Unlimited Premier With Plus+
                            gsr.siteWatchUnlimitedPremierWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchUnlimitedPremierWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "609020": // Unlimited Premier With RainX
                            gsr.siteWatchUnlimitedPremierWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchUnlimitedPremierWithRainX_count += int.Parse(item.total);
                            break;
                        case "49000311": // Enhance PSX To Protex RainX
                            gsr.siteWatchEnhancePsxToProtexWithRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxToProtexWithRainX_count += int.Parse(item.total);
                            break;
                        case "49000310": // Enhance PSX To Protex Plus+
                            gsr.siteWatchEnhancePsxToProtexWithPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxToProtexWithPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000312": // Enhance PSX With Tire Gloss To Protex Plus+
                            gsr.siteWatchEnhancePsxTireGlossToProtexPlusPlus_dollars+= int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxTireGlossToProtexPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000313": // Enhance PSX With Tire Gloss To Protex RainX
                            gsr.siteWatchEnhancePsxTireGlossToProtexRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxTireGlossToProtexRainX_count += int.Parse(item.total);
                            break;
                        case "49000315": // Enhance PSX To Premier With RainX
                            gsr.siteWatchEnhancePsxToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000316": // Enhance PSX To Premier With Plus+
                            gsr.siteWatchEnhancePsxToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000317": // Enhance PSX With Tire Gloss To Premier With Plus+
                            gsr.siteWatchEnhancePsxTireGlossToPremierPlusPlus_dollars+= int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxTireGlossToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000318": // Enhance PSX With Tire Gloss To Premier With RainX
                            gsr.siteWatchEnhancePsxTireGlossToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhancePsxTireGlossToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000319": // Enhance Protex To Premier With Plus+
                            gsr.siteWatchEnhanceProtexToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhanceProtexToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000320": // Enhance Protex To Premier With RainX
                            gsr.siteWatchEnhanceProtexToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhanceProtexToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000321": // Enhance Protex To Premier With RainX
                            gsr.siteWatchEnhanceProtexTireGlossToPremierRainX_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhanceProtexTireGlossToPremierRainX_count += int.Parse(item.total);
                            break;
                        case "49000322": // Enhance Protex To Premier With Plus+
                            gsr.siteWatchEnhanceProtexTireGlossToPremierPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.PLUS_TWO;
                            gsr.siteWatchEnhanceProtexTireGlossToPremierPlusPlus_count += int.Parse(item.total);
                            break;
                        case "49000324": // Reapply Fleet Plus+
                            gsr.sitewatchReapplyFleetPlusPlus_dollars += int.Parse(item.total) * (int)GSRMultiplier.NEGATIVE_THREE;
                            gsr.sitewatchReapplyFleetPlusPlus_count += int.Parse(item.total);
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
            gsr.siteWatchTotalProtex_count = gsr.siteWatchProtexWash_count +
                gsr.siteWatchProtexRewash_count +
                gsr.siteWatchEnhancePsxToProtexWithPlusPlus_count +
                gsr.siteWatchEnhancePsxToProtexWithRainX_count +
                gsr.siteWatchEnhancePsxWithTireGlossToProtex_count +
                gsr.siteWatchUnlimitedProtex_count +
                gsr.siteWatchFleetProtex_count +
                gsr.siteWatchPsxFleetProtex_count +
                gsr.siteWatchEnhancePsxToProtex_count +
                gsr.siteWatchUnlimitedProtexWithTireGloss_count +
                gsr.siteWatchUnlimitedProtexWithPlusPlus_count +
                gsr.siteWatchUnlimitedProtexWithRainX_count;
            // Protex Total Dollars
            gsr.totalProtex_dollars = gsr.washLinkTotalProtex_dollars +
                gsr.siteWatchProtexRewash_dollars +
                gsr.siteWatchEnhancePsxToProtexWithPlusPlus_dollars +
                gsr.siteWatchEnhancePsxToProtexWithRainX_dollars +
                gsr.siteWatchEnhancePsxWithTireGlossToProtex_dollars +
                gsr.siteWatchUnlimitedProtex_dollars +
                gsr.siteWatchFleetProtex_dollars +
                gsr.siteWatchPsxFleetProtex_dollars +
                gsr.siteWatchEnhancePsxToProtex_dollars +
                gsr.siteWatchUnlimitedProtexWithTireGloss_dollars +
                gsr.siteWatchUnlimitedProtexWithPlusPlus_dollars +
                gsr.siteWatchUnlimitedProtexWithRainX_dollars;

            gsr.totalProtex_diff = (gsr.siteWatchTotalProtex_count - gsr.washLinkTotalProtex_count) * (int)GSRMultiplier.PLUS_TEN;
            
            //SW Premier Total
            gsr.siteWatchTotalPremier_count = gsr.siteWatchPremierWash_count +
                gsr.siteWatchPremierRewash_count +
                gsr.siteWatchEnhancePsxToPremier_count +
                gsr.siteWatchEnhancePsxToPremierPlusPlus_count +
                gsr.siteWatchEnhancePsxToPremierRainX_count +
                gsr.siteWatchEnhancePsxWithTireGlossToPremier_count +
                gsr.siteWatchEnhanceProtexToPremier_count +
                gsr.siteWatchEnhanceProtexWithTireGlossToPremier_count +
                gsr.siteWatchPsxFleetPremier_count +
                gsr.siteWatchUnlimitedPremier_count +
                gsr.siteWatchUnlimitedPremierWithPlusPlus_count +
                gsr.siteWatchUnlimitedPremierWithRainX_count + 
                gsr.siteWatchUnlimitedPremierWithTireGloss_count +
                gsr.siteWatchPsxFleetPremierNoGloss_count +
                gsr.siteWatchFleetPremier_count;
            // Premier Total Dollars
            gsr.totalPremier_dollars  = gsr.washLinkTotalPremier_dollars +
                gsr.siteWatchPremierRewash_dollars +
                gsr.siteWatchEnhancePsxToPremier_dollars +
                gsr.siteWatchEnhancePsxToPremierPlusPlus_dollars +
                gsr.siteWatchEnhancePsxToPremierRainX_dollars +
                gsr.siteWatchEnhancePsxWithTireGlossToPremier_dollars +
                gsr.siteWatchEnhanceProtexToPremier_dollars +
                gsr.siteWatchEnhanceProtexWithTireGlossToPremier_dollars +
                gsr.siteWatchPsxFleetPremier_dollars +
                gsr.siteWatchUnlimitedPremier_dollars +
                gsr.siteWatchUnlimitedPremierWithPlusPlus_dollars +
                gsr.siteWatchUnlimitedPremierWithRainX_dollars +
                gsr.siteWatchUnlimitedPremierWithTireGloss_dollars +
                gsr.siteWatchPsxFleetPremierNoGloss_dollars +
                gsr.siteWatchFleetPremier_dollars;

            gsr.totalPremier_diff = (gsr.siteWatchTotalPremier_count - gsr.washLinkTotalPremier_count) * (int)GSRMultiplier.PLUS_TWELVE;

            //SW Prime Shine Total
            gsr.siteWatchTotalPrimeShine_count = (gsr.siteWatchPrimeShine_count +
                gsr.siteWatchFleetPsx_count +
                gsr.siteWatchPrimeShineRewash_count +
                gsr.siteWatchUnlimitedPsx_count +
                gsr.siteWatchUnlimitedPsxWithTireGloss_count +
                gsr.siteWatchUnlimitedPsxWithPlusPlus_count +
                gsr.siteWatchUnlimitedPsxWithRainX_count +
                gsr.siteWatchUnlimitedUcw_count) - (gsr.siteWatchTotalProtex_count + gsr.siteWatchTotalPremier_count);
            // Prime Shine Total Dollars
            gsr.totalPrimeShine_dollars = gsr.washLinkTotalPrimeShine_dollars +
                gsr.siteWatchFleetPsx_dollars +
                gsr.siteWatchPsx_dollars +
                gsr.siteWatchPrimeShineRewash_dollars +
                gsr.siteWatchUnlimitedPsx_dollars +
                gsr.siteWatchUnlimitedUcw_dollars +
                gsr.siteWatchUnlimitedPsxWithTireGloss_dollars +
                gsr.siteWatchUnlimitedPsxWithPlusPlus_dollars +
                gsr.siteWatchUnlimitedPsxWithRainX_dollars;
            gsr.totalPrimeShine_diff = (gsr.siteWatchTotalPrimeShine_count - gsr.washLinkTotalPrimeShine_count) * (int)GSRMultiplier.PLUS_SEVEN;
            
            // SW Tire Gloss
            

            gsr.siteWatchTotalTireGloss_count = gsr.siteWatchTireGloss_count +
                gsr.siteWatchFleetTireGloss_count + 
                gsr.siteWatchReapplyTireGloss_count + 
                gsr.siteWatchUnlimitedTireGloss_count +
                gsr.siteWatchEnhancePsxWithTireGlossToProtex_count +
                gsr.siteWatchEnhancePsxWithTireGlossToPremier_count +
                gsr.siteWatchUnlimitedPremierWithTireGloss_count +
                gsr.siteWatchUnlimitedProtexWithTireGloss_count +
                gsr.siteWatchUnlimitedPsxWithTireGloss_count;

            gsr.totalTireGloss_dollars = gsr.washLinkTotalTireGloss_dollars +
                gsr.siteWatchFleetTireGloss_dollars +
                gsr.siteWatchUnlimitedTireGloss_dollars +
                gsr.siteWatchReapplyTireGloss_dollars;

            gsr.tireGloss_diff = (gsr.siteWatchTotalTireGloss_count - gsr.washLinkTotalTireGloss_count) * (int)GSRMultiplier.PLUS_TWO;

            // Rain-X
            gsr.sitewatchTotalRainX_count = gsr.siteWatchRainX_count +
                gsr.siteWatchReapplyRainX_count +
                gsr.siteWatchUnlimitedPremierWithRainX_count +
                gsr.siteWatchUnlimitedProtexWithRainX_count +
                gsr.siteWatchUnlimitedPsxWithRainX_count +
                gsr.siteWatchEnhanceProtexTireGlossToPremierRainX_count +
                gsr.siteWatchEnhanceProtexToPremierRainX_count +
                gsr.siteWatchEnhancePsxTireGlossToProtexRainX_count +
                gsr.siteWatchEnhancePsxToProtexWithRainX_count +
                gsr.siteWatchEnhancePsxToPremierRainX_count +
                gsr.sitewatchReapplyFleetRainX_count +
                gsr.sitewatchUnlimitedRainX_count +
                gsr.siteWatchEnhancePsxTireGlossToPremierRainX_count;

            gsr.totalRainX_dollars = gsr.washLinkTotalRainX_dollars +
                gsr.sitewatchReapplyFleetRainX_dollars +
                gsr.sitewatchUnlimitedRainX_dollars +
                gsr.siteWatchReapplyRainX_dollars;
            gsr.totalRainX_diff = (gsr.sitewatchTotalRainX_count - gsr.washLinkTotalRainX_count) * (int)GSRMultiplier.PLUS_TWO;


            gsr.sitewatchTotalPlusPlus_count = gsr.siteWatchPlusPlus_count +
                gsr.sitewatchReapplyPlusPlus_count +
                gsr.sitewatchReapplyFleetPlusPlus_count +
                gsr.sitewatchUnlimitedPlusPlus_count +
                gsr.siteWatchUnlimitedPremierWithPlusPlus_count +
                gsr.siteWatchUnlimitedProtexWithPlusPlus_count +
                gsr.siteWatchUnlimitedPsxWithPlusPlus_count +
                gsr.siteWatchEnhanceProtexTireGlossToPremierPlusPlus_count +
                gsr.siteWatchEnhanceProtexToPremierPlusPlus_count +
                gsr.siteWatchEnhancePsxTireGlossToProtexPlusPlus_count +
                gsr.siteWatchEnhancePsxToProtexWithPlusPlus_count +
                gsr.siteWatchEnhancePsxToPremierPlusPlus_count +
                gsr.siteWatchEnhancePsxTireGlossToPremierPlusPlus_count;

            gsr.totalPlusPlus_dollars = gsr.washLinkTotalPlusPlus_dollars +
                gsr.sitewatchReapplyPlusPlus_dollars +
                gsr.sitewatchReapplyFleetPlusPlus_dollars +
                gsr.sitewatchUnlimitedPlusPlus_dollars;
            gsr.totalPlusPlus_diff = (gsr.sitewatchTotalPlusPlus_count - gsr.washLinkTotalPlusPlus_count) * (int)GSRMultiplier.PLUS_THREE;
            //CHANGED - 6/10/2010.  Added the unlimited wTG  
            //SW Tire Gloss
            
            
            //gsr.washLinkTotalTireGloss_dollars = gsr.washLinkTireGloss_dollars + 
            //    gsr.siteWatchFleetTireGloss_dollars +
            //    gsr.siteWatchReapplyTireGloss_dollars +
            //    gsr.siteWatchUnlimitedTireGloss_dollars;
            gsr.totalTireGloss_diff = (gsr.siteWatchTotalTireGloss_count - gsr.washLinkTotalTireGloss_count) * (int)GSRMultiplier.PLUS_TWO;

            gsr.siteWatchTotalWashes_count = gsr.siteWatchTotalPrimeShine_count + 
                gsr.siteWatchTotalProtex_count + 
                gsr.siteWatchTotalPremier_count;

            gsr.totalWashes_diff = gsr.totalTireGloss_diff + 
                gsr.totalPrimeShine_diff + 
                gsr.totalProtex_diff + 
                gsr.totalPremier_diff;

            gsr.totalWashes_dollars = gsr.totalPrimeShine_dollars +
                gsr.totalProtex_dollars +
                gsr.totalPremier_dollars +
                gsr.totalTireGloss_dollars;
            #endregion

            

            if(swData.Count > 0)
            {
                #region Impulse Items
                foreach(var impulseItem in swData)
                {
                    if(impulseItem.reportcategory.Equals("500025"))
                    {
                        if(!string.IsNullOrEmpty(impulseItem.total))
                        {
                            gsr.impulseItems += (Math.Abs(int.Parse(impulseItem.total)) * decimal.Parse(impulseItem.val));
                        }
                    }
                }
                #endregion

                #region Machine Sales
                foreach(var machineSales in swData)
                {
                    if(machineSales.reportcategory.Equals("500036") || 
                        machineSales.reportcategory.Equals("500037") ||
                        machineSales.reportcategory.Equals("500091") ||
                        machineSales.reportcategory.Equals("500057"))
                    {
                        if(!string.IsNullOrEmpty(machineSales.total))
                        {
                            gsr.machineSales += (Math.Abs(int.Parse(machineSales.total)) * decimal.Parse(machineSales.val));
                        }
                    }
                }
                #endregion

                #region Web connect
                foreach(var webConnect in swData)
                {
                    if(webConnect.reportcategory.Equals("500103") ||
                        webConnect.reportcategory.Equals("500103") ||
                        webConnect.reportcategory.Equals("500100") ||
                        webConnect.reportcategory.Equals("500102") ||
                        webConnect.reportcategory.Equals("500101") ||
                        webConnect.reportcategory.Equals("49000002"))
                    {
                        if(!string.IsNullOrEmpty(webConnect.total))
                        {
                            if(!string.IsNullOrEmpty(webConnect.val))
                                gsr.webConnect += (Math.Abs(int.Parse(webConnect.total) * decimal.Parse(webConnect.val))) * (int)GSRMultiplier.NEGATIVE_ONE;
                            else
                                gsr.webConnect += (Math.Abs(int.Parse(webConnect.total) * decimal.Parse(webConnect.amt))) * (int)GSRMultiplier.NEGATIVE_ONE;
                        }
                    }
                }
                #endregion

                #region Pre Paids

                foreach(var prePaids in swData)
                {
                    if(prePaids.reportcategory.Equals("52167") || prePaids.reportcategory.Equals("500058"))
                    {
                        if(!string.IsNullOrEmpty(prePaids.total))
                        {
                            gsr.prePaids += (Math.Abs(int.Parse(prePaids.total)) * decimal.Parse(prePaids.amt));
                        }
                    }
                }
                #endregion

                #region Pump Code Wash

                foreach(var pumpCode in swData)
                {
                    if (pumpCode.reportcategory.Equals("500052"))
                    {
                        if(!string.IsNullOrEmpty(pumpCode.total))
                        {
                            gsr.pumpCodeWash += (Math.Abs(int.Parse(pumpCode.total)) * decimal.Parse(pumpCode.amt));
                        }
                    }
                }
                #endregion

                #region Coupons and Discounts
                foreach(var coupons in swData)
                {
                    if(coupons.reportcategory.Equals("59458") ||
                        coupons.reportcategory.Equals("49500003") ||
                        coupons.reportcategory.Equals("49500001") ||
                        coupons.reportcategory.Equals("500028") ||
                        coupons.reportcategory.Equals("500064") ||
                        coupons.reportcategory.Equals("1000003") ||
                        coupons.reportcategory.Equals("49500344") ||
                        coupons.reportcategory.Equals("49500342"))
                    {
                        if(!string.IsNullOrEmpty(coupons.total))
                        {
                            if(!string.IsNullOrEmpty(coupons.val))
                                gsr.couponsAndDiscounts += ((int.Parse(coupons.total) * Math.Abs(decimal.Parse(coupons.amt))) * (int)GSRMultiplier.NEGATIVE_ONE);
                        }
                    }
                }
                #endregion

                #region Total Paidout / Refunds

                foreach(var totalPaidOut in swData)
                {
                    if(totalPaidOut.reportcategory.Equals("103965"))
                    {
                        if(!string.IsNullOrEmpty(totalPaidOut.total))
                        {
                            gsr.totalPaidoutRefunds += (Math.Abs(int.Parse(totalPaidOut.total) * decimal.Parse(totalPaidOut.amt))) * (int)GSRMultiplier.NEGATIVE_ONE;
                        }
                    }
                }
                #endregion

                #region Cash Deposit

                foreach(var cashDeposit in swData)
                {
                    if(cashDeposit.reportcategory.Equals("500044"))
                    {
                        if(!string.IsNullOrEmpty(cashDeposit.total))
                        {
                            gsr.cashDeposit += (Math.Abs(int.Parse(cashDeposit.total)) * decimal.Parse(cashDeposit.amt));
                        }
                    }
                }
                #endregion

                #region Credit Cards

                foreach(var creditCards in swData)
                {
                    if(creditCards.reportcategory.Equals("500050"))
                    {
                        if(!string.IsNullOrEmpty(creditCards.total))
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

            // Total Over
            gsr.totalOverUnder_dollars = ((gsr.cashDeposit + gsr.creditCards) + gsr.totalToAccountFor) * (int)GSRMultiplier.NEGATIVE_ONE;
            gsr.totalOverUnder_diff = gsr.totalTireGloss_diff + 
                gsr.totalPrimeShine_diff + 
                gsr.totalProtex_diff + 
                gsr.totalPremier_diff;

            // Total Over Excluding Cars
            int iExcludingCars = ((gsr.siteWatchTotalPrimeShine_count - gsr.washLinkTotalPrimeShine_count) * (int)GSRMultiplier.PLUS_SEVEN) +
                ((gsr.siteWatchTotalProtex_count - gsr.washLinkTotalProtex_count) * (int)GSRMultiplier.PLUS_TEN) +
                ((gsr.siteWatchTotalPremier_count - gsr.washLinkTotalPremier_count) * (int)GSRMultiplier.PLUS_TWELVE) +
                gsr.totalTireGloss_diff;
            gsr.amountToAudit = gsr.totalOverUnder_dollars +((iExcludingCars) * (int)GSRMultiplier.NEGATIVE_ONE);
            
            return gsr;
        }
    }
}