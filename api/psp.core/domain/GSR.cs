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

        //[DataMember]
        //public int washLinkPrimeShine_count { get; set; }

        //[DataMember]
        //public decimal washLinkPrimeShine_dollars { get; set; }

        [DataMember]
        public int siteWatchPrimeShine_count { get; set; }

        [DataMember]
        public decimal siteWatchPrimeShine_dollars { get; set; }

        [DataMember]
        public decimal siteWatchTotalPrimeShine_dollars { get; set; }

        [DataMember]
        public decimal siteWatchTotalProtex_dollars { get; set; }

        [DataMember]
        public decimal siteWatchTotalPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchFleetPsx_count { get; set; }

        [DataMember]
        public decimal siteWatchFleetPsx_dollars { get; set; }

        [DataMember]
        public int siteWatchPsx_count { get; set; }

        [DataMember]
        public decimal siteWatchPsx_dollars { get; set; }

        [DataMember]
        public int siteWatchPrimeShineRewash_count { get; set; }

        [DataMember]
        public decimal siteWatchPrimeShineRewash_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPsx_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPsx_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedUcw_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedUcw_dollars { get; set; }
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
        public int siteWatchTotalPrimeShine_count { get; set; }

        [DataMember]
        public int washLinkTotalPrimeShine_count { get; set; }

        [DataMember]
        public decimal washLinkTotalPrimeShine_dollars { get; set; }

        [DataMember]
        public decimal totalPrimeShine_diff { get; set; }

        [DataMember]
        public int siteWatchPremierWash_count { get; set; }

        [DataMember]
        public decimal siteWatchPremierWash_dollars { get; set; }

        [DataMember]
        public decimal washLinkTotalPremier_dollars { get; set; }

        [DataMember]
        public decimal totalPremier_diff { get; set; }

        [DataMember]
        public int siteWatchPremierRewash_count { get; set; }

        [DataMember]
        public decimal siteWatchPremierRewash_dollars { get; set; }

        [DataMember]
        public int siteWatchFleetPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchFleetPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxToPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxToPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxWithTireGlossToPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxWithTireGlossToPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhanceProtexToPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhanceProtexToPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhanceProtexWithTireGlossToPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhanceProtexWithTireGlossToPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxToProtex_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxToProtex_dollars { get; set; }

        //[DataMember]
        //public int unlimitedUg_count { get; set; }

        //[DataMember]
        //public decimal unlimitedUg_dollars { get; set; }

        [DataMember]
        public int siteWatchTotalPremier_count { get; set; }

        [DataMember]
        public int washLinkTotalPremier_count { get; set; }

        [DataMember]
        public int siteWatchProtexWash_count { get; set; }

        [DataMember]
        public decimal siteWatchProtexWash_dollars { get; set; }

        [DataMember]
        public int siteWatchProtexRewash_count { get; set; }

        [DataMember]
        public decimal siteWatchProtexRewash_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedProtex_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedProtex_dollars { get; set; }

        [DataMember]
        public int siteWatchFleetProtex_count { get; set; }

        [DataMember]
        public decimal siteWatchFleetProtex_dollars { get; set; }

        [DataMember]
        public int siteWatchTotalProtex_count { get; set; }

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
        public int siteWatchTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchTireGloss_dollars { get; set; }

        [DataMember]
        public decimal tireGloss_diff { get; set; }

        [DataMember]
        public int siteWatchFleetTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchFleetTireGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchReapplyTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchReapplyTireGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchTotalTireGloss_count { get; set; }

        [DataMember]
        public int washLinkTotalTireGloss_count { get; set; }

        [DataMember]
        public decimal washLinkTotalTireGloss_dollars { get; set; }

        [DataMember]
        public decimal totalTireGloss_diff { get; set; }

        [DataMember]
        public int washLinkTotalWashes_count { get; set; }

        [DataMember]
        public int siteWatchTotalWashes_count { get; set; }

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
        public int siteWatchUnlimitedPsxWithTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPsxWithTireGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedProtexWithTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedProtexWithTireGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxWithTireGlossToProtex_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxWithTireGlossToProtex_dollars { get; set; }

        [DataMember]
        public int siteWatchPsxFleetPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchPsxFleetPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchPsxFleetPremierNoGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchPsxFleetPremierNoGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchPsxFleetProtex_count { get; set; }

        [DataMember]
        public decimal siteWatchPsxFleetProtex_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPremier_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPremier_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPremierWithTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPremierWithTireGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedTireGloss_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedTireGloss_dollars { get; set; }

        [DataMember]
        public int siteWatchReapplyRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchReapplyRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPsxWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPsxWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPsxWithRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPsxWithRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedProtexWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedProtexWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedProtexWithRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedProtexWithRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPremierWithRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPremierWithRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchUnlimitedPremierWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchUnlimitedPremierWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxToProtexWithRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxToProtexWithRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxToProtexWithPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxToProtexWithPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxTireGlossToProtexPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxTireGlossToProtexPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxTireGlossToProtexRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxTireGlossToProtexRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxToPremierRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxToPremierRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxToPremierPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxTireGlossToPremierRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxTireGlossToPremierRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhancePsxTireGlossToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhancePsxTireGlossToPremierPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhanceProtexToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhanceProtexToPremierPlusPlus_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhanceProtexToPremierRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhanceProtexToPremierRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhanceProtexTireGlossToPremierRainX_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhanceProtexTireGlossToPremierRainX_dollars { get; set; }

        [DataMember]
        public int siteWatchEnhanceProtexTireGlossToPremierPlusPlus_count { get; set; }

        [DataMember]
        public decimal siteWatchEnhanceProtexTireGlossToPremierPlusPlus_dollars { get; set; }
        
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
        public decimal sitewatchTotalRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchTotalPlusPlus_count { get; set; }

        [DataMember]
        public int sitewatchReapplyFleetPlusPlus_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyFleetPlusPlus_dollars { get; set; }

        [DataMember]
        public int sitewatchReapplyFleetRainX_count { get; set; }

        [DataMember]
        public decimal sitewatchReapplyFleetRainX_dollars { get; set; }

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
    }
}