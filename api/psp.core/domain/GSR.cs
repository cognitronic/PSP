using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class GSR 
    {
        [DataMember]
        public ObjectId Id { get; set; }

        [DataMember]
        public string sid { get; set; }

        [DataMember]
        public string siteName { get; set; }

        [DataMember]
        public string siteId { get; set; }

        [DataMember]
        public DateTime gsrDate { get; set; }

        [DataMember]
        public decimal washLinkPrimeShine_dollars { get; set; }

        [DataMember]
        public int washLinkPrimeShine_count { get; set; }

        [DataMember]
        public decimal washLinkProtex_dollars { get; set; }

        [DataMember]
        public int washLinkProtex_count { get; set; }

        [DataMember]
        public decimal washLinkPremier_dollars { get; set; }

        [DataMember]
        public int washLinkPremier_count { get; set; }

        [DataMember]
        public int sitewatchPrimeShine_count { get; set; }

        [DataMember]
        public decimal sitewatchPrimeShine_dollars { get; set; }

        [DataMember]
        public decimal sitewatchProtex_dollars { get; set; }

        [DataMember]
        public int sitewatchProtex_count { get; set; }

        [DataMember]
        public decimal sitewatchPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchTotalPrimeShine_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalProtex_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchFleetPsx_count { get; set; }

        [DataMember]
        public decimal sitewatchFleetPsx_dollars { get; set; }

        [DataMember]
        public int sitewatchPsx_count { get; set; }

        [DataMember]
        public int primeShineFleet_count { get; set; }

        [DataMember]
        public decimal primeShineFleet_dollars { get; set; }

        [DataMember]
        public int protexFleet_count { get; set; }

        [DataMember]
        public decimal protexFleet_dollars { get; set; }

        [DataMember]
        public int premierFleet_count { get; set; }

        [DataMember]
        public decimal premierFleet_dollars { get; set; }


        [DataMember]
        public int tireGlossFleet_count { get; set; }

        [DataMember]
        public decimal tireGlossFleet_dollars { get; set; }


        [DataMember]
        public int rainXFleet_count { get; set; }

        [DataMember]
        public decimal rainXFleet_dollars { get; set; }

        [DataMember]
        public int plusPlusFleet_count { get; set; }

        [DataMember]
        public decimal plusPlusFleet_dollars { get; set; }



        [DataMember]
        public int tireGlossUnlimited_count { get; set; }

        [DataMember]
        public decimal tireGlossUnlimited_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalTireGloss_dollars { get; set; }


        [DataMember]
        public int rainXUnlimited_count { get; set; }

        [DataMember]
        public decimal rainXUnlimited_dollars { get; set; }

        [DataMember]
        public int plusPlusUnlimited_count { get; set; }

        [DataMember]
        public decimal plusPlusUnlimited_dollars { get; set; }



        [DataMember]
        public int primeShineUnlimited_count { get; set; }

        [DataMember]
        public decimal primeShineUnlimited_dollars { get; set; }

        [DataMember]
        public int protexUnlimited_count { get; set; }

        [DataMember]
        public decimal protexUnlimited_dollars { get; set; }

        [DataMember]
        public int premierUnlimited_count { get; set; }

        [DataMember]
        public decimal premierUnlimited_dollars { get; set; }


        [DataMember]
        public decimal sitewatchPsx_dollars { get; set; }

        [DataMember]
        public int sitewatchPrimeShineRewash_count { get; set; }

        [DataMember]
        public decimal sitewatchPrimeShineRewash_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPsx_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPsx_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedUcw_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedUcw_dollars { get; set; }
        [DataMember]
        public decimal totalTireGloss_dollars { get; set; }
        [DataMember]
        public decimal totalPrimeShine_dollars { get; set; }
        [DataMember]
        public decimal totalProtex_dollars { get; set; }
        [DataMember]
        public decimal totalPremier_dollars { get; set; }

        [DataMember]
        public decimal totalPlusPlus_dollars { get; set; }

        [DataMember]
        public decimal totalRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchTotalPrimeShine_count { get; set; }

        [DataMember]
        public int washLinkTotalPrimeShine_count { get; set; }

        [DataMember]
        public decimal washLinkTotalPrimeShine_dollars { get; set; }

        [DataMember]
        public decimal totalPrimeShine_diff { get; set; }

        [DataMember]
        public int sitewatchPremierWash_count { get; set; }

        [DataMember]
        public decimal sitewatchPremierWash_dollars { get; set; }

        [DataMember]
        public decimal washLinkTotalPremier_dollars { get; set; }

        [DataMember]
        public decimal totalPremier_diff { get; set; }

        [DataMember]
        public int sitewatchPremierRewash_count { get; set; }

        [DataMember]
        public decimal sitewatchPremierRewash_dollars { get; set; }

        [DataMember]
        public int sitewatchFleetPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchFleetPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxToPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxToPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxWithTireGlossToPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxWithTireGlossToPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhanceProtexToPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhanceProtexToPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhanceProtexWithTireGlossToPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhanceProtexWithTireGlossToPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxToProtex_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxToProtex_dollars { get; set; }

        //[DataMember]
        //public int unlimitedUg_count { get; set; }

        //[DataMember]
        //public decimal unlimitedUg_dollars { get; set; }

        [DataMember]
        public int sitewatchTotalPremier_count { get; set; }

        [DataMember]
        public int washLinkTotalPremier_count { get; set; }

        [DataMember]
        public int sitewatchProtexWash_count { get; set; }

        [DataMember]
        public decimal sitewatchProtexWash_dollars { get; set; }

        [DataMember]
        public int sitewatchProtexRewash_count { get; set; }

        [DataMember]
        public decimal sitewatchProtexRewash_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedProtex_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedProtex_dollars { get; set; }

        [DataMember]
        public int sitewatchFleetProtex_count { get; set; }

        [DataMember]
        public decimal sitewatchFleetProtex_dollars { get; set; }

        [DataMember]
        public int sitewatchTotalProtex_count { get; set; }

        [DataMember]
        public int washLinkTotalProtex_count { get; set; }

        [DataMember]
        public decimal washLinkTotalProtex_dollars { get; set; }

        [DataMember]
        public decimal totalProtex_diff { get; set; }

        [DataMember]
        public int washLinkTireGloss_count { get; set; }

        [DataMember]
        public decimal washLinkTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchTireGloss_dollars { get; set; }

        [DataMember]
        public decimal tireGloss_diff { get; set; }

        [DataMember]
        public int sitewatchFleetTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchFleetTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchReapplyTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchTotalTireGloss_count { get; set; }

        [DataMember]
        public int washLinkTotalTireGloss_count { get; set; }

        [DataMember]
        public decimal washLinkTotalTireGloss_dollars { get; set; }

        [DataMember]
        public decimal totalTireGloss_diff { get; set; }

        [DataMember]
        public int washLinkTotalWashes_count { get; set; }

        [DataMember]
        public int sitewatchTotalWashes_count { get; set; }

        [DataMember]
        public decimal totalWashes_dollars { get; set; }

        [DataMember]
        public decimal totalWashes_diff { get; set; }

        [DataMember]
        public decimal webConnect { get; set; }

        [DataMember]
        public decimal impulseItems { get; set; }

        [DataMember]
        public decimal totalWashServices { get; set; }

        [DataMember]
        public decimal machineSales { get; set; }

        [DataMember]
        public decimal prePaids { get; set; }

        [DataMember]
        public decimal pumpCodeWash { get; set; }

        [DataMember]
        public decimal couponsAndDiscounts { get; set; }

        [DataMember]
        public decimal totalPaidoutRefunds { get; set; }

        [DataMember]
        public decimal totalToAccountFor { get; set; }

        [DataMember]
        public decimal cashDeposit { get; set; }

        [DataMember]
        public decimal creditCards { get; set; }

        [DataMember]
        public decimal totalOverUnder_dollars { get; set; }

        [DataMember]
        public decimal totalOverUnder_diff { get; set; }

        [DataMember]
        public decimal amountToAudit { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPsxWithTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPsxWithTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedProtexWithTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedProtexWithTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxWithTireGlossToProtex_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxWithTireGlossToProtex_dollars { get; set; }

        [DataMember]
        public int sitewatchPsxFleetPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchPsxFleetPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchPsxFleetPremierNoGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchPsxFleetPremierNoGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchPsxFleetProtex_count { get; set; }

        [DataMember]
        public decimal sitewatchPsxFleetProtex_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPremier_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPremier_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPremierWithTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPremierWithTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedTireGloss_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedTireGloss_dollars { get; set; }

        [DataMember]
        public int sitewatchReapplyRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPsxWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPsxWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPsxWithRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPsxWithRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedProtexWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedProtexWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedProtexWithRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedProtexWithRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPremierWithRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPremierWithRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPremierWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPremierWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxToProtexWithRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxToProtexWithRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxToProtexWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxToProtexWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxTireGlossToProtexPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxTireGlossToProtexPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxTireGlossToProtexRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxTireGlossToProtexRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxToPremierRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxToPremierRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxToPremierPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxTireGlossToPremierRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxTireGlossToPremierRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhancePsxTireGlossToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhancePsxTireGlossToPremierPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhanceProtexToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhanceProtexToPremierPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhanceProtexToPremierRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhanceProtexToPremierRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhanceProtexTireGlossToPremierRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhanceProtexTireGlossToPremierRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchEnhanceProtexTireGlossToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchEnhanceProtexTireGlossToPremierPlusPlus_dollars { get; set; }
        
        [DataMember]
        public int washLinkTotalPlusPlus_count { get; set; }

        [DataMember]
        public decimal washLinkTotalPlusPlus_dollars { get; set; }

        [DataMember]
        public int washLinkTotalRainX_count { get; set; }

        [DataMember]
        public decimal washLinkTotalRainX_dollars { get; set; }

        [DataMember]
        public decimal totalRainX_diff { get; set; }

        [DataMember]
        public decimal totalPlusPlus_diff { get; set; }

        [DataMember]
        public int sitewatchTotalRainX_count { get; set; }

        [DataMember]
        public int sitewatchTotalPlusPlus_count { get; set; }

        [DataMember]
        public int sitewatchReapplyFleetPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyFleetPlusPlus_dollars { get; set; }
        [DataMember]
        public int sitewatchFleetPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchFleetPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchReapplyFleetRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyFleetRainX_dollars { get; set; }
        [DataMember]
        public int sitewatchFleetRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchFleetRainX_dollars { get; set; }

        [DataMember]
        public int sitewatchReapplyPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchUnlimitedRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchUnlimitedRainX_dollars { get; set; }

        [DataMember]
        public double totalToAccountForPerCar_dollars { get; set; }

        [DataMember]
        public int washLinkPlusPlus_count { get; set; }

        [DataMember]
        public decimal washLinkPlusPlus_dollars { get; set; }

        [DataMember]
        public int washLinkRainX_count { get; set; }

        [DataMember]
        public decimal washLinkRainX_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalRainX_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalPlusPlus_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalWashes_dollars { get; set; }

        [DataMember]
        public decimal washLinkTotalWashes_dollars { get; set; }

        [DataMember]
        public decimal sitewatchTotalWashServices { get; set; }

        [DataMember]
        public decimal washLinkTotalWashServices { get; set; }

        [DataMember]
        public decimal totalWashServicesDiff { get; set; }

        [DataMember]
        public decimal sitewatchTotalToAccountFor { get; set; }

        [DataMember]
        public decimal washLinkTotalToAccountFor { get; set; }

        [DataMember]
        public decimal totalToAccountForDiff { get; set; }

        [DataMember]
        public decimal sitewatchTotalOverUnder_dollars { get; set; }
        
        [DataMember]
        public decimal washLinkTotalOverUnder_dollars { get; set; }

    }
}