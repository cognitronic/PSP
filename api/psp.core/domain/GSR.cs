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
        public string site { get; set; }
        [DataMember]
        public DateTime gsrdate { get; set; }
        [DataMember]
        public int primeshine_count { get; set; }
        [DataMember]
        public decimal primeshine_dollars { get; set; }
        [DataMember]
        public int fleetpsx_count { get; set; }
        [DataMember]
        public decimal fleetpsx_dollars { get; set; }
        [DataMember]
        public int psx_count { get; set; }
        [DataMember]
        public decimal psx_dollars { get; set; }
        [DataMember]
        public int primeshinerewash_count { get; set; }
        [DataMember]
        public decimal primeshinerewash_dollars { get; set; }
        [DataMember]
        public int unlimitedpsx_count { get; set; }
        [DataMember]
        public decimal unlimitedpsx_dollars { get; set; }
        [DataMember]
        public int unlimiteducw_count { get; set; }
        [DataMember]
        public decimal unlimiteducw_dollars { get; set; }
        [DataMember]
        public int sw_totalprimeshine_count { get; set; }
        [DataMember]
        public int wl_totalprimeshine_count { get; set; }
        [DataMember]
        public decimal totalprimeshine_dollars { get; set; }
        [DataMember]
        public decimal totalprimeshine_diff { get; set; }
        [DataMember]
        public int premierwash_count { get; set; }
        [DataMember]
        public decimal premierwash_dollars { get; set; }
        [DataMember]
        public decimal totalpremier_dollars { get; set; }
        [DataMember]
        public decimal totalpremier_diff { get; set; }
        [DataMember]
        public int premierrewash_count { get; set; }
        [DataMember]
        public decimal premierrewash_dollars { get; set; }
        [DataMember]
        public int fleetpremier_count { get; set; }
        [DataMember]
        public decimal fleetpremier_dollars { get; set; }
        [DataMember]
        public int enhancepsxtopre_count { get; set; }
        [DataMember]
        public decimal enhancepsxtopre_dollars { get; set; }
        [DataMember]
        public int enhancepsxwtgpre_count { get; set; }
        [DataMember]
        public decimal enhancepsxwtgpre_dollars { get; set; }
        [DataMember]
        public int enhanceplattopre_count { get; set; }
        [DataMember]
        public decimal enhanceplattopre_dollars { get; set; }
        [DataMember]
        public int enhanceplatwtgpre_count { get; set; }
        [DataMember]
        public decimal enhanceplatwtgpre_dollars { get; set; }
        [DataMember]
        public int enhanceprotopre_count { get; set; }
        [DataMember]
        public decimal enhanceprotopre_dollars { get; set; }
        [DataMember]
        public int enhanceprowtgpre_count { get; set; }
        [DataMember]
        public decimal enhanceprowtgpre_dollars { get; set; }
        [DataMember]
        public int platinumwash_count { get; set; }
        [DataMember]
        public decimal platinumwash_dollars { get; set; }
        [DataMember]
        public int platinumrewash_count { get; set; }
        [DataMember]
        public decimal platinumrewash_dollars { get; set; }
        [DataMember]
        public int enhancepsxtopro_count { get; set; }
        [DataMember]
        public decimal enhancepsxtopro_dollars { get; set; }
        [DataMember]
        public int enhanceplattopro_count { get; set; }
        [DataMember]
        public decimal enhanceplattopro_dollars { get; set; }
        [DataMember]
        public int enhancepsxtoplat_count { get; set; }
        [DataMember]
        public decimal enhancepsxtoplat_dollars { get; set; }
        [DataMember]
        public int unlimitedplatinum_count { get; set; }
        [DataMember]
        public decimal unlimitedplatinum_dollars { get; set; }
        [DataMember]
        public int fleetplatinum_count { get; set; }
        [DataMember]
        public decimal fleetplatinum_dollars { get; set; }
        [DataMember]
        public int unlimitedug_count { get; set; }
        [DataMember]
        public decimal unlimitedug_decimal { get; set; }
        [DataMember]
        public int sw_totalpremier_count { get; set; }
        [DataMember]
        public int wl_totalpremier_count { get; set; }
        [DataMember]
        public int wl_totalplatinum_count { get; set; }
        [DataMember]
        public int sw_totalplatinum_count { get; set; }
        [DataMember]
        public decimal totalplatinum_dollars { get; set; }
        [DataMember]
        public decimal totalplatinum_diff { get; set; }
        [DataMember]
        public int protexwash_count { get; set; }
        [DataMember]
        public decimal protexwash_dollars { get; set; }
        [DataMember]
        public int protexrewash_count { get; set; }
        [DataMember]
        public decimal protexrewash_dollars { get; set; }
        [DataMember]
        public int unlimitedprotex_count { get; set; }
        [DataMember]
        public decimal unlimitedprotex_dollars { get; set; }
        [DataMember]
        public int fleetprotex_count { get; set; }
        [DataMember]
        public decimal fleetprotex_dollars { get; set; }
        [DataMember]
        public int sw_totalprotex_count { get; set; }
        [DataMember]
        public int wl_totalprotex_count { get; set; }
        [DataMember]
        public decimal totalprotex_dollars { get; set; }
        [DataMember]
        public decimal totalprotex_diff { get; set; }
        [DataMember]
        public int wl_tiregloss_count { get; set; }
        [DataMember]
        public int sw_tiregloss_count { get; set; }
        [DataMember]
        public decimal tiregloss_dollars { get; set; }
        [DataMember]
        public decimal tiregloss_diff { get; set; }
        [DataMember]
        public int fleettiregloss_count { get; set; }
        [DataMember]
        public decimal fleettiregloss_dollars { get; set; }
        [DataMember]
        public int reapplytiregloss_count { get; set; }
        [DataMember]
        public decimal reapplytiregloss_dollars { get; set; }
        [DataMember]
        public int sw_totaltiregloss_count { get; set; }
        [DataMember]
        public int wl_totaltiregloss_count { get; set; }
        [DataMember]
        public decimal totaltiregloss_dollars { get; set; }
        [DataMember]
        public decimal totaltiregloss_diff { get; set; }
        [DataMember]
        public int wl_totalwashes_count { get; set; }
        [DataMember]
        public int sw_totalwashes_count { get; set; }
        [DataMember]
        public decimal totalwashes_dollars { get; set; }
        [DataMember]
        public decimal totalwashes_diff { get; set; }
        [DataMember]
        public decimal webconnect { get; set; }
        [DataMember]
        public decimal impulseitems { get; set; }
        [DataMember]
        public decimal totalwashservices { get; set; }
        [DataMember]
        public decimal machinesales { get; set; }
        [DataMember]
        public decimal prepaids { get; set; }
        [DataMember]
        public decimal couponsanddiscounts { get; set; }
        [DataMember]
        public decimal totalpaidoutrefunds { get; set; }
        [DataMember]
        public decimal totaltoaccountfor { get; set; }
        [DataMember]
        public decimal cashdeposit { get; set; }
        [DataMember]
        public decimal creditcards { get; set; }
        [DataMember]
        public decimal totaloverunder_dollars { get; set; }
        [DataMember]
        public decimal totaloverunder_diff { get; set; }
        [DataMember]
        public decimal amounttoaudit { get; set; }
        [DataMember]
        public int unpsxwtg_count { get; set; }
        [DataMember]
        public decimal unpsxwtg_dollars { get; set; }
        [DataMember]
        public int unplatwtg_count { get; set; }
        [DataMember]
        public decimal unplatwtg_dollars { get; set; }
        [DataMember]
        public int unprowtg_count { get; set; }
        [DataMember]
        public decimal unprowtg_dollars { get; set; }
        [DataMember]
        public int enhancepsxwtgplat_count { get; set; }
        [DataMember]
        public decimal enhancepsxwtgplat_dollars { get; set; }
        [DataMember]
        public int enhancepsxwtgpro_count { get; set; }
        [DataMember]
        public decimal enhancepsxwtgpro_dollars { get; set; }
        [DataMember]
        public int enhanceplatwtgpro_count { get; set; }
        [DataMember]
        public decimal enhanceplatwtgpro_dollars { get; set; }
        [DataMember]
        public int armprimeshinerdm_count { get; set; }
        [DataMember]
        public decimal armprimeshinerdm_dollars { get; set; }
        [DataMember]
        public int armpswtgrdm_count { get; set; }
        [DataMember]
        public decimal armpswtgrdm_dollars { get; set; }
        [DataMember]
        public int armplatrdm_count { get; set; }

        [DataMember]
        public decimal armplatrdm_dollars { get; set; }
        [DataMember]
        public int armplatwtgrdm_count { get; set; }
        [DataMember]
        public decimal armplatwtgrdm_dollars { get; set; }
        [DataMember]
        public int armprordm_count { get; set; }
        [DataMember]
        public decimal armprordm_dollars { get; set; }
        [DataMember]
        public int armprowtgrdm_count { get; set; }
        [DataMember]
        public decimal armprowtgrdm_dollars { get; set; }
        [DataMember]
        public int psxfleetpremier_count { get; set; }
        [DataMember]
        public decimal psxfleetpremier_dollars { get; set; }
        [DataMember]
        public int psxfleetpremiernogloss_count { get; set; }
        [DataMember]
        public decimal psxfleetpremiernogloss_dollars { get; set; }
        [DataMember]
        public int psxfleetprotex_count { get; set; }
        [DataMember]
        public decimal psxfleetprotex_dollars { get; set; }


    }
}
