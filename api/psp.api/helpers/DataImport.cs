﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Data;
using System.Data.SqlClient;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using System.Configuration;

namespace psp.api.helpers
{
    public class DataImport : BaseHelpers
    {
        private readonly ClientRepository _clientRepository;

        public DataImport()
        {
            _clientRepository = new ClientRepository();
        }

        public int ImportClientsIntoMongoFromSqlServer()
        {
            var sQuery = "";
                sQuery = @"select * from clients";
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["hqwebConnectionString"]))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds, "Clients");
                }
            if(ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    var client = new Client();
                    client.address = row["address"].ToString();
                    client.addressverified = (bool)row["addresseverified"];
                    client.birthdate = row["birthdate"].ToString();
                    client.birthdaycouponsent = (bool)row["birthdaycouponsent"];
                    client.city = row["city"].ToString();
                    client.dateregistered = DateTime.Parse(row["dateentered"].ToString());
                    client.email = row["email"].ToString();
                    client.firstname = row["firstname"].ToString();
                    client.isactive = (bool)row["isactive"];
                    client.lastname = row["lastname"].ToString();
                    client.phone = row["phone"].ToString();
                    client.receivenotifications = (bool)row["receivenotifications"];
                    client.state = row["state"].ToString();
                    client.zip = row["zip"].ToString();
                    _clientRepository.Save(client);
                }
            }

            return 1;
        }

        public int ImportCouponCodesIntoMongoFromSqlServer()
        {
            var sQuery = "";
            sQuery = @"select * from couponcode";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["hqwebConnectionString"]))
            {
                SqlDataAdapter da = new SqlDataAdapter(sQuery, con);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(ds, "Coupons");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var coupon = new CouponCode();
                    coupon.isassigned = (bool)row["IsAssigned"];
                    coupon.isredeemed = (bool)row["IsRedeemed"];
                    coupon.code = row["Code"].ToString();
                    coupon.codetext = row["CodeText"].ToString();
                    switch(Convert.ToInt16(row["CouponID"]))
                    {
                        case 1:
                            coupon.coupon = "birthday";
                            break;
                        case 2:
                            coupon.coupon = "random";
                            break;
                        case 3:
                            coupon.coupon = "complimentary";
                            break;
                        case 4:
                            coupon.coupon = "employee";
                            break;
                        case 5:
                            coupon.coupon = "25percent_off";
                            break;
                    }
                    new CouponCodeRepository().Save(coupon);
                }
            }

            return 1;
        }

        public int ImportGSRIntoMongoFromSqlServer()
        {
            var sQuery = "";
            sQuery = @"select * from gsr";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["hqwebConnectionString"]))
            {
                SqlDataAdapter da = new SqlDataAdapter(sQuery, con);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(ds, "gsr");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var gsr = new GSR();
                    gsr.amountToAudit = row["amounttoaudit"].ToString() != "" ? (decimal)row["amounttoaudit"] : 0;
                    gsr.cashDeposit = row["cashdeposit"].ToString() != "" ? (decimal)row["cashdeposit"] : 0;
                    gsr.couponsAndDiscounts = row["couponsanddiscounts"].ToString() != "" ? (decimal)row["couponsanddiscounts"] : 0;
                    gsr.creditCards = row["creditcards"].ToString() != "" ? (decimal)row["creditcards"] : 0;
                    gsr.gsrDate = DateTime.Parse(row["gsrdate"].ToString());
                    gsr.impulseItems = row["impulseitems"].ToString() != "" ? (decimal)row["impulseitems"] : 0;
                    gsr.machineSales = row["machinesales"].ToString() != "" ? (decimal)row["machinesales"] : 0;
                    gsr.prePaids = row["prepaids"].ToString() != "" ? (decimal)row["prepaids"] : 0;
                    gsr.pumpCodeWash = row["pumpcodewash"].ToString() != "" ? (decimal)row["pumpcodewash"] : 0;
                    gsr.siteId = row["locationid"].ToString();
                    gsr.siteName = row["locationid"].ToString();
                    gsr.siteWatchEnhanceProtexToPremier_count = row["enhanceprotopre_count"].ToString() != "" ? (int)row["enhanceprotopre_count"] : 0;
                    gsr.siteWatchEnhanceProtexToPremier_dollars = row["enhanceprotopre_dollars"].ToString() != "" ? (decimal)row["enhanceprotopre_dollars"] : 0;
                    gsr.siteWatchEnhanceProtexWithTireGlossToPremier_count = row["enhanceprowtgpre_count"].ToString() != "" ? (int)row["enhanceprowtgpre_count"] : 0;
                    gsr.siteWatchEnhanceProtexWithTireGlossToPremier_dollars = row["enhanceprowtgpre_dollars"].ToString() != "" ? (decimal)row["enhanceprowtgpre_dollars"] : 0;
                    gsr.siteWatchEnhancePsxToPremier_count = row["enhancepsxtopre_count"].ToString() != "" ? (int)row["enhancepsxtopre_count"] : 0;
                    gsr.siteWatchEnhancePsxToPremier_dollars = row["enhancepsxtopre_dollars"].ToString() != "" ? (decimal)row["enhancepsxtopre_dollars"] : 0;
                    gsr.siteWatchEnhancePsxToProtex_count = row["enhancepsxtopro_count"].ToString() != "" ? (int)row["enhancepsxtopro_count"] : 0;
                    gsr.siteWatchEnhancePsxToProtex_dollars = row["enhancepsxtopro_dollars"].ToString() != "" ? (decimal)row["enhancepsxtopro_dollars"] : 0;
                    gsr.siteWatchEnhanceProtexTireGlossToPremierPlusPlus_count = 0;
                    gsr.siteWatchEnhanceProtexTireGlossToPremierPlusPlus_dollars = 0;
                    gsr.siteWatchEnhanceProtexTireGlossToPremierRainX_count = 0;
                    gsr.siteWatchEnhanceProtexTireGlossToPremierRainX_dollars = 0;
                    gsr.siteWatchEnhanceProtexToPremierPlusPlus_count = 0;
                    gsr.siteWatchEnhanceProtexToPremierPlusPlus_dollars = 0;
                    gsr.siteWatchEnhanceProtexToPremierRainX_count = 0;
                    gsr.siteWatchEnhanceProtexToPremierRainX_dollars = 0;
                    gsr.siteWatchEnhancePsxTireGlossToPremierPlusPlus_count = 0;
                    gsr.siteWatchEnhancePsxTireGlossToPremierPlusPlus_dollars = 0;
                    gsr.siteWatchEnhancePsxTireGlossToPremierRainX_count = 0;
                    gsr.siteWatchEnhancePsxTireGlossToPremierRainX_dollars = 0;
                    gsr.siteWatchEnhancePsxTireGlossToProtexPlusPlus_count = 0;
                    gsr.siteWatchEnhancePsxTireGlossToProtexPlusPlus_dollars = 0;
                    gsr.siteWatchEnhancePsxTireGlossToProtexRainX_count = 0;
                    gsr.siteWatchEnhancePsxTireGlossToProtexRainX_dollars = 0;
                    gsr.siteWatchEnhancePsxToPremierPlusPlus_count = 0;
                    gsr.siteWatchEnhancePsxToPremierPlusPlus_dollars = 0;
                    gsr.siteWatchEnhancePsxToPremierRainX_count = 0;
                    gsr.siteWatchEnhancePsxToPremierRainX_dollars = 0;
                    gsr.siteWatchEnhancePsxToProtexWithPlusPlus_count = 0;
                    gsr.siteWatchEnhancePsxToProtexWithPlusPlus_dollars = 0;
                    gsr.siteWatchEnhancePsxToProtexWithRainX_count = 0;
                    gsr.siteWatchEnhancePsxToProtexWithRainX_dollars = 0;
                    gsr.siteWatchEnhancePsxWithTireGlossToPremier_count = row["enhancepsxwtgpre_count"].ToString() != "" ? (int)row["enhancepsxwtgpre_count"] : 0;
                    gsr.siteWatchEnhancePsxWithTireGlossToPremier_dollars = row["enhancepsxwtgpre_dollars"].ToString() != "" ? (decimal)row["enhancepsxwtgpre_dollars"] : 0;
                    gsr.siteWatchEnhancePsxWithTireGlossToProtex_count = row["enhancepsxwtgpro_count"].ToString() != "" ? (int)row["enhancepsxwtgpro_count"] : 0;
                    gsr.siteWatchEnhancePsxWithTireGlossToProtex_dollars = row["enhancepsxwtgpro_dollars"].ToString() != "" ? (decimal)row["enhancepsxwtgpro_dollars"] : 0;
                    gsr.siteWatchFleetPremier_count = row["fleetpremier_count"].ToString() != "" ? (int)row["fleetpremier_count"] : 0;
                    gsr.siteWatchFleetPremier_dollars = row["fleetpremier_dollars"].ToString() != "" ? (decimal)row["fleetpremier_dollars"] : 0;
                    gsr.siteWatchFleetProtex_count = row["fleetprotex_count"].ToString() != "" ? (int)row["fleetprotex_count"] : 0;
                    gsr.siteWatchFleetProtex_dollars = row["fleetprotex_dollars"].ToString() != "" ? (decimal)row["fleetprotex_dollars"] : 0;
                    gsr.siteWatchFleetPsx_count = row["fleetpsx_count"].ToString() != "" ? (int)row["fleetpsx_count"] : 0;
                    gsr.siteWatchFleetPsx_dollars = row["fleetpsx_dollars"].ToString() != "" ? (decimal)row["fleetpsx_dollars"] : 0;
                    gsr.siteWatchFleetTireGloss_count = row["fleettiregloss_count"].ToString() != "" ? (int)row["fleettiregloss_count"] : 0;
                    gsr.siteWatchFleetTireGloss_dollars = row["fleettiregloss_dollars"].ToString() != "" ? (decimal)row["fleettiregloss_dollars"] : 0;
                    gsr.siteWatchPlusPlus_count = 0;
                    gsr.siteWatchPlusPlus_dollars = 0;
                    gsr.siteWatchPremierRewash_count = row["premierrewash_count"].ToString() != "" ? (int)row["premierrewash_count"] : 0;
                    gsr.siteWatchPremierRewash_dollars = row["premierrewash_dollars"].ToString() != "" ? (decimal)row["premierrewash_dollars"] : 0;
                    gsr.siteWatchPremierWash_count = row["premierwash_count"].ToString() != "" ? (int)row["premierwash_count"] : 0;
                    gsr.siteWatchPremierWash_dollars = row["premierwash_dollars"].ToString() != "" ? (decimal)row["premierwash_dollars"] : 0;
                    gsr.siteWatchPrimeShine_count = row["primeshine_count"].ToString() != "" ? (int)row["primeshine_count"] : 0;
                    gsr.siteWatchPrimeShine_dollars = row["primeshine_dollars"].ToString() != "" ? (decimal)row["primeshine_dollars"] : 0;
                    gsr.siteWatchPrimeShineRewash_count = row["primeshinerewash_count"].ToString() != "" ? (int)row["primeshinerewash_count"] : 0;
                    gsr.siteWatchPrimeShineRewash_dollars = row["primeshinerewash_dollars"].ToString() != "" ? (decimal)row["primeshinerewash_dollars"] : 0;
                    gsr.siteWatchProtexRewash_count = row["protexrewash_count"].ToString() != "" ? (int)row["protexrewash_count"] : 0;
                    gsr.siteWatchProtexRewash_dollars = row["protexrewash_dollars"].ToString() != "" ? (decimal)row["protexrewash_dollars"] : 0;
                    gsr.siteWatchProtexWash_count = row["protexwash_count"].ToString() != "" ? (int)row["protexwash_count"] : 0;
                    gsr.siteWatchProtexWash_dollars = row["protexwash_dollars"].ToString() != "" ? (decimal)row["protexwash_dollars"] : 0;
                    gsr.siteWatchPsx_count = row["psx_count"].ToString() != "" ? (int)row["psx_count"] : 0;
                    gsr.siteWatchPsx_dollars = row["psx_dollars"].ToString() != "" ? (decimal)row["psx_dollars"] : 0;
                    gsr.siteWatchPsxFleetPremier_count = row["psxfleetpremier_count"].ToString() != "" ? (int)row["psxfleetpremier_count"] : 0;
                    gsr.siteWatchPsxFleetPremier_dollars = row["psxfleetpremier_dollars"].ToString() != "" ? (decimal)row["psxfleetpremier_dollars"] : 0;
                    gsr.siteWatchPsxFleetPremierNoGloss_count = row["psxfleetpremiernogloss_count"].ToString() != "" ? (int)row["psxfleetpremiernogloss_count"] : 0;
                    gsr.siteWatchPsxFleetPremierNoGloss_dollars = row["psxfleetpremiernogloss_dollars"].ToString() != "" ? (decimal)row["psxfleetpremiernogloss_dollars"] : 0;
                    gsr.siteWatchPsxFleetProtex_count = row["psxfleetprotex_count"].ToString() != "" ? (int)row["psxfleetprotex_count"] : 0;
                    gsr.siteWatchPsxFleetProtex_dollars = row["psxfleetprotex_dollars"].ToString() != "" ? (decimal)row["psxfleetprotex_dollars"] : 0;
                    gsr.siteWatchRainX_count = 0;
                    gsr.siteWatchRainX_dollars = 0;
                    gsr.sitewatchReapplyFleetPlusPlus_count = 0;
                    gsr.sitewatchReapplyFleetPlusPlus_dollars = 0;
                    gsr.sitewatchReapplyFleetRainX_count = 0;
                    gsr.sitewatchReapplyFleetRainX_dollars = 0;
                    gsr.sitewatchReapplyPlusPlus_count = 0;
                    gsr.sitewatchReapplyPlusPlus_dollars = 0;
                    gsr.siteWatchReapplyRainX_count = 0;
                    gsr.siteWatchReapplyRainX_dollars = 0;
                    gsr.siteWatchReapplyTireGloss_count = row["reapplytiregloss_count"].ToString() != "" ? (int)row["reapplytiregloss_count"] : 0;
                    gsr.siteWatchReapplyTireGloss_dollars = row["reapplytiregloss_dollars"].ToString() != "" ? (decimal)row["reapplytiregloss_dollars"] : 0;
                    gsr.siteWatchTireGloss_count = row["sw_totaltiregloss_count"].ToString() != "" ? (int)row["sw_totaltiregloss_count"] : 0;
                    gsr.siteWatchTireGloss_dollars = row["tiregloss_dollars"].ToString() != "" ? (decimal)row["tiregloss_dollars"] : 0;
                    gsr.sitewatchTotalPlusPlus_count = 0;
                    gsr.siteWatchTotalPremier_count = row["sw_totalpremier_count"].ToString() != "" ? (int)row["sw_totalpremier_count"] : 0;
                    gsr.siteWatchTotalPremier_dollars = row["totalpremier_dollars"].ToString() != "" ? (decimal)row["totalpremier_dollars"] : 0;
                    gsr.siteWatchTotalPrimeShine_count = row["sw_totalprimeshine_count"].ToString() != "" ? (int)row["sw_totalprimeshine_count"] : 0;
                    gsr.siteWatchTotalPrimeShine_dollars = row["totalprimeshine_dollars"].ToString() != "" ? (decimal)row["totalprimeshine_dollars"] : 0;
                    gsr.siteWatchTotalProtex_count = row["sw_totalprotex_count"].ToString() != "" ? (int)row["sw_totalprotex_count"] : 0;
                    gsr.siteWatchTotalProtex_dollars = row["totalprotex_dollars"].ToString() != "" ? (decimal)row["totalprotex_dollars"] : 0;
                    gsr.sitewatchTotalRainX_count = 0;
                    gsr.siteWatchTotalTireGloss_count = row["sw_totaltiregloss_count"].ToString() != "" ? (int)row["sw_totaltiregloss_count"] : 0;
                    gsr.siteWatchTotalWashes_count = row["sw_totalwashes_count"].ToString() != "" ? (int)row["sw_totalwashes_count"] : 0;
                    gsr.sitewatchUnlimitedPlusPlus_count = 0;
                    gsr.sitewatchUnlimitedPlusPlus_dollars = 0;
                    gsr.siteWatchUnlimitedPremier_count = row["unlimitedpremier_count"].ToString() != "" ? (int)row["unlimitedpremier_count"] : 0;
                    gsr.siteWatchUnlimitedPremier_dollars = row["unlimitedpremier_dollars"].ToString() != "" ? (decimal)row["unlimitedpremier_dollars"] : 0;
                    gsr.siteWatchUnlimitedPremierWithPlusPlus_count = 0;
                    gsr.siteWatchUnlimitedPremierWithPlusPlus_dollars = 0;
                    gsr.siteWatchUnlimitedPremierWithRainX_count = 0;
                    gsr.siteWatchUnlimitedPremierWithRainX_dollars = 0;
                    gsr.siteWatchUnlimitedPremierWithTireGloss_count = row["unlimitedpremierwtg_count"].ToString() != "" ? (int)row["unlimitedpremierwtg_count"] : 0;
                    gsr.siteWatchUnlimitedPremierWithTireGloss_dollars = row["unlimitedpremierwtg_dollars"].ToString() != "" ? (decimal)row["unlimitedpremierwtg_dollars"] : 0;
                    gsr.siteWatchUnlimitedProtex_count = row["unlimitedprotex_count"].ToString() != "" ? (int)row["unlimitedprotex_count"] : 0;
                    gsr.siteWatchUnlimitedProtex_dollars = row["unlimitedprotex_dollars"].ToString() != "" ? (decimal)row["unlimitedprotex_dollars"] : 0;
                    gsr.siteWatchUnlimitedProtexWithPlusPlus_count = 0;
                    gsr.siteWatchUnlimitedProtexWithPlusPlus_dollars = 0;
                    gsr.siteWatchUnlimitedProtexWithRainX_count = 0;
                    gsr.siteWatchUnlimitedProtexWithRainX_dollars = 0;
                    gsr.siteWatchUnlimitedProtexWithTireGloss_count = row["unprowtg_count"].ToString() != "" ? (int)row["unprowtg_count"] : 0;
                    gsr.siteWatchUnlimitedProtexWithTireGloss_dollars = row["unprowtg_dollars"].ToString() != "" ? (decimal)row["unprowtg_dollars"] : 0;
                    gsr.siteWatchUnlimitedPsx_count = row["unlimitedpsx_count"].ToString() != "" ? (int)row["unlimitedpsx_count"] : 0;
                    gsr.siteWatchUnlimitedPsx_dollars = row["unlimitedpsx_dollars"].ToString() != "" ? (decimal)row["unlimitedpsx_dollars"] : 0;
                    gsr.siteWatchUnlimitedPsxWithPlusPlus_count = 0;
                    gsr.siteWatchUnlimitedPsxWithPlusPlus_dollars = 0;
                    gsr.siteWatchUnlimitedPsxWithRainX_count = 0;
                    gsr.siteWatchUnlimitedPsxWithRainX_dollars = 0;
                    gsr.siteWatchUnlimitedPsxWithTireGloss_count = row["unpsxwtg_count"].ToString() != "" ? (int)row["unpsxwtg_count"] : 0;
                    gsr.siteWatchUnlimitedPsxWithTireGloss_dollars = row["unpsxwtg_dollars"].ToString() != "" ? (decimal)row["unpsxwtg_dollars"] : 0;
                    gsr.sitewatchUnlimitedRainX_count = 0;
                    gsr.sitewatchUnlimitedRainX_dollars = 0;
                    gsr.siteWatchUnlimitedTireGloss_count = row["unlimitedtiregloss_count"].ToString() != "" ? (int)row["unlimitedtiregloss_count"] : 0;
                    gsr.siteWatchUnlimitedTireGloss_dollars = row["unlimitedtiregloss_dollars"].ToString() != "" ? (decimal)row["unlimitedtiregloss_dollars"] : 0;
                    gsr.siteWatchUnlimitedUcw_count = row["unlimiteducw_count"].ToString() != "" ? (int)row["unlimiteducw_count"] : 0;
                    gsr.siteWatchUnlimitedUcw_dollars = row["unlimiteducw_dollars"].ToString() != "" ? (decimal)row["unlimiteducw_dollars"] : 0;
                    gsr.tireGloss_diff = row["totaltiregloss_diff"].ToString() != "" ? (decimal)row["totaltiregloss_diff"] : 0;
                    gsr.totalOverUnder_diff = row["totaloverunder_diff"].ToString() != "" ? (decimal)row["totaloverunder_diff"] : 0;
                    gsr.totalOverUnder_dollars = row["totaloverunder_dollars"].ToString() != "" ? (decimal)row["totaloverunder_dollars"] : 0;
                    gsr.totalPaidoutRefunds = row["totalpaidoutrefunds"].ToString() != "" ? (decimal)row["totalpaidoutrefunds"] : 0;
                    gsr.totalPlusPlus_diff = 0;
                    gsr.totalPlusPlus_dollars = 0;
                    gsr.totalPremier_diff = row["totalpremier_diff"].ToString() != "" ? (decimal)row["totalpremier_diff"] : 0;
                    gsr.totalPremier_dollars = row["totalpremier_dollars"].ToString() != "" ? (decimal)row["totalpremier_dollars"] : 0;
                    gsr.totalPrimeShine_diff = row["totalprimeshine_diff"].ToString() != "" ? (decimal)row["totalprimeshine_diff"] : 0;
                    gsr.totalPrimeShine_dollars = row["totalprimeshine_dollars"].ToString() != "" ? (decimal)row["totalprimeshine_dollars"] : 0;
                    gsr.totalProtex_diff = row["totalprotex_diff"].ToString() != "" ? (decimal)row["totalprotex_diff"] : 0;
                    gsr.totalProtex_dollars = row["totalprotex_dollars"].ToString() != "" ? (decimal)row["totalprotex_dollars"] : 0;
                    gsr.totalRainX_diff = 0;
                    gsr.totalRainX_dollars = 0;
                    gsr.totalTireGloss_diff = row["totaltiregloss_diff"].ToString() != "" ? (decimal)row["totaltiregloss_diff"] : 0;
                    gsr.totalTireGloss_dollars = row["totaltiregloss_dollars"].ToString() != "" ? (decimal)row["totaltiregloss_dollars"] : 0;
                    gsr.totalToAccountFor = row["totaltoaccountfor"].ToString() != "" ? (decimal)row["totaltoaccountfor"] : 0;
                    gsr.totalToAccountForPerCar_dollars = 0;
                    gsr.totalWashes_diff = row["totalwashes_diff"].ToString() != "" ? (decimal)row["totalwashes_diff"] : 0;
                    gsr.totalWashes_dollars = row["totalwashes_dollars"].ToString() != "" ? (decimal)row["totalwashes_dollars"] : 0;
                    gsr.totalWashServices = row["totalwashservices"].ToString() != "" ? (decimal)row["totalwashservices"] : 0;
                    gsr.washLinkTireGloss_count = row["wl_tiregloss_count"].ToString() != "" ? (int)row["wl_tiregloss_count"] : 0;
                    gsr.washLinkTireGloss_dollars = 0;
                    gsr.washLinkTotalPlusPlus_count = 0;
                    gsr.washLinkTotalPlusPlus_dollars = 0;
                    gsr.washLinkTotalPremier_count = row["wl_totalpremier_count"].ToString() != "" ? (int)row["wl_totalpremier_count"] : 0;
                    gsr.washLinkTotalPremier_dollars = 0;
                    gsr.washLinkTotalPrimeShine_count = row["wl_totalprimeshine_count"].ToString() != "" ? (int)row["wl_totalprimeshine_count"] : 0;
                    gsr.washLinkTotalPrimeShine_dollars = 0;
                    gsr.washLinkTotalProtex_count = row["wl_totalprotex_count"].ToString() != "" ? (int)row["wl_totalprotex_count"] : 0;
                    gsr.washLinkTotalProtex_dollars = 0;
                    gsr.washLinkTotalRainX_count = 0;
                    gsr.washLinkTotalRainX_dollars = 0;
                    gsr.washLinkTotalTireGloss_count = row["wl_totaltiregloss_count"].ToString() != "" ? (int)row["wl_totaltiregloss_count"] : 0;
                    gsr.washLinkTotalTireGloss_dollars = 0;
                    gsr.washLinkTotalWashes_count = row["wl_totalwashes_count"].ToString() != "" ? (int)row["wl_totalwashes_count"] : 0;
                    gsr.webConnect = row["webconnect"].ToString() != "" ? (decimal)row["webconnect"] : 0;
                    new GSRRepository().Save(gsr);
                }
            }

            return 1;
        }
    }
}