using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Reflection;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading;

namespace MPGSConfig
{
    public class Merchant
    {
        public string id { get; set; }
        public DateTime updated { get; set; }
    }

    public class Mso
    {
        public string id { get; set; }
    }

    public class MerchnatList
    {
        public List<Merchant> merchant { get; set; }
        public Mso mso { get; set; }
        public string result { get; set; }
    }
    public class Program
    {
        public static string username = "";
        public static string password = ""; 
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Program start");
                
                MerchnatList list = getMPGSMerchantList();
                getMPGSMerchantConfig(list.merchant);

                //List<Merchant> list = new List<Merchant>() { 
                //    new Merchant(){ id = "760076303278", updated= DateTime.Now },
                //    new Merchant(){ id = "120815000239", updated= DateTime.Now },
                //    new Merchant(){ id = "120811000013", updated= DateTime.Now },
                //    new Merchant(){ id = "120816200023", updated= DateTime.Now },
                //    new Merchant(){ id = "120815000287", updated= DateTime.Now },
                //    new Merchant(){ id = "120816200017", updated= DateTime.Now },
                //};
                //getMPGSMerchantConfig(list);

                Console.WriteLine("Program ends");
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        public static MerchnatList getMPGSMerchantList()
        {
            try
            {
                string baseUrl = "https://adcb.gateway.mastercard.com/api/rest/version/82/mso/ADCB_MSO_01/merchant";
                var options = new RestClientOptions(baseUrl);
                options.Authenticator = new HttpBasicAuthenticator(username, password);
                var client = new RestClient(options);

                RestRequest request = new RestRequest();
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MerchnatList MerchnatList = JsonConvert.DeserializeObject<MerchnatList>(response.Content);
                    using (var file = File.CreateText(@"C:\Users\muham\Desktop\Projects\Config_MPGS\mpgsmid.csv"))
                    {
                        file.WriteLine("MPGS_MID");
                        foreach (Merchant merchant in MerchnatList.merchant)
                        {
                            file.WriteLine(merchant.id);
                        }
                    }
                    return MerchnatList;
                }
                else
                {
                    Console.WriteLine("error while calling MPGS Merchants API");
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void getMPGSMerchantConfig(List<Merchant> list)
        {
            try
            {
                AppendText("MID,NAME,STATUS,ACQ LINK ID,ACQ LINK STATUS,ACQ LINK ID,ACQ LINK STATUS");
                foreach (Merchant item in list)
                {
                    string baseUrl = "https://adcb.gateway.mastercard.com/api/rest/version/82/mso/ADCB_MSO_01/merchant/" + item.id;
                    var options = new RestClientOptions(baseUrl);
                    options.Authenticator = new HttpBasicAuthenticator(username, password);
                    var client = new RestClient(options);

                    RestRequest request = new RestRequest();
                    var response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        MerchantDetail merchantDetail = JsonConvert.DeserializeObject<MerchantDetail>(response.Content);
                        string MerchantData = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                            merchantDetail._merchant.id,
                            merchantDetail._merchant.name,
                            merchantDetail._merchant.state,
                            merchantDetail._merchant.acquirerLink.ADCB_031200_S2I01 != null ? merchantDetail._merchant.acquirerLink.ADCB_031200_S2I01.acquirerId : "",
                            merchantDetail._merchant.acquirerLink.ADCB_031200_S2I01 != null ? merchantDetail._merchant.acquirerLink.ADCB_031200_S2I01.status : "",
                            merchantDetail._merchant.acquirerLink.ADCB_S2I01 != null ? merchantDetail._merchant.acquirerLink.ADCB_S2I01.acquirerId : "",
                            merchantDetail._merchant.acquirerLink.ADCB_S2I01 != null ? merchantDetail._merchant.acquirerLink.ADCB_S2I01.status : "");

                        AppendText(MerchantData);
                    }
                    else
                    {
                        Console.WriteLine("error while calling MPGS Merchant Detail API of MID: " + item.id);
                    }
                    Console.WriteLine("Writing config of MID: " + item.id);
                    Thread.Sleep(4000);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void AppendText(string text)
        {
            string path = @"C:\Users\muham\Desktop\Projects\Config_MPGS\mpgsmidconfig.csv";
            File.AppendAllText(path, text + Environment.NewLine);
        }
    }
}
