using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroExpress.Help
{

    public class SMSSend
    {

       public static void SendOneToOneSingleSms(string userName,string Password,string mobile,string text,string type, string Mask,string campaignName)
        {   
            try
            {
                var sms = new com.onnorokomsms.api2.SendSms();
                string returnValue = sms.OneToOne(userName, Password, mobile, text,
                type, Mask, campaignName);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public static bool SendOneToOneSingleSmsUsingAPI(string apikey, string mobile, string text, string type, string Mask, string campaignName)
        {
            try
            {
                if (Mask == null)
                    Mask = "";
                var sms = new com.onnorokomsms.api2.SendSms();
                string returnValue = sms.NumberSms(apikey,  text, mobile,type,Mask,campaignName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string GetBalanceByAPIKey(string ApiKey)
        {
            try
            {
                var sms = new com.onnorokomsms.api2.SendSms();
                string returnValue = sms.GetCurrentBalance(ApiKey);
                return returnValue;
            }
            catch (Exception ex)
            {
                return "N/A";
            }
        }
        public static string GetBalanceByUserNamePassword(string userName,string password)
        {
            try
            {
                var sms = new com.onnorokomsms.api2.SendSms();
                string returnValue = sms.GetBalance(userName, password);
                return returnValue;
            }
            catch (Exception ex)
            {
                return "N/A";
            }
        }
    }
}