using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psp.core
{
    public class ResourceStrings
    {
        #region Cache
        public static string Cache_BlogPosts = "Cache_Users";
        public static string Cache_Projects = "Cache_Projects";
        public static string Cache_Banners = "Cache_Banners";
        public static string Cache_Programs = "Cache_Programs";
        #endregion

        #region Mongo
        public static string Mongo_Users_Collection = "user";
        public static string Mongo_Site_Collection = "site";
        public static string Mongo_Blog_Collection = "blogs";
        public static string Mongo_Tags_Collection = "tags";
        public static string Mongo_Schedule_Collection = "schedule";
        public static string Mongo_Notification_Collection = "notifications";
        public static string Mongo_Client_Collection = "client";
        public static string Mongo_GSR_Collection = "gsr";
        public static string Mongo_Coupon_Collection = "coupon";
        public static string Mongo_CouponCode_Collection = "coupon_code";
        public static string Mongo_Audit_Collection = "audit";
        #endregion

        #region Audit Types
        public static string Audit_Connectivity = "Connectivity";
        public static string Audit_Report = "Reports";
        public static string Audit_Notification = "Notification";
        #endregion
    }
}
