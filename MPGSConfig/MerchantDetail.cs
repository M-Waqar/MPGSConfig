using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPGSConfig
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class _3DS1
    {
        public string merchantId { get; set; }
        public string cardAcceptorId { get; set; }
        public string cardAcceptorTerminalId { get; set; }
    }

    public class AcquirerLink
    {
        public ADCB031200S2I01 ADCB_031200_S2I01 { get; set; }
        public ADCBS2I01 ADCB_S2I01 { get; set; }
    }

    public class ADCB031200S2I01
    {
        public bool accountUpdaterEnabled { get; set; }
        public string acquirerId { get; set; }
        public List<string> allowableTransactionFrequency { get; set; }
        public List<string> allowableTransactionSource { get; set; }
        public Authentication authentication { get; set; }
        public string bankMerchantId { get; set; }
        public List<string> cardType { get; set; }
        public List<string> currency { get; set; }
        public string defaultTransactionFrequency { get; set; }
        public string defaultTransactionSource { get; set; }
        public List<string> enforceCscOnTransactionSource { get; set; }
        public string paymentType { get; set; }
        public string status { get; set; }
        public List<string> terminalId { get; set; }
    }

    public class ADCBS2I01
    {
        public bool accountUpdaterEnabled { get; set; }
        public string acquirerId { get; set; }
        public List<string> allowableTransactionFrequency { get; set; }
        public List<string> allowableTransactionSource { get; set; }
        public Authentication authentication { get; set; }
        public string bankMerchantId { get; set; }
        public List<string> cardType { get; set; }
        public List<string> currency { get; set; }
        public string defaultTransactionFrequency { get; set; }
        public string defaultTransactionSource { get; set; }
        public List<string> enforceCscOnTransactionSource { get; set; }
        public string paymentType { get; set; }
        public string status { get; set; }
        public List<string> terminalId { get; set; }
    }

    public class Address
    {
        public string city { get; set; }
        public string countryCode { get; set; }
        public string postcode { get; set; }
        public string stateProvince { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
    }

    public class Authentication
    {
        public MasterCardSecureCode masterCardSecureCode { get; set; }
        public VerifiedByVisa verifiedByVisa { get; set; }
    }

    public class Download
    {
        public int paymentAuthenticationMaximumRows { get; set; }
    }

    public class Gatekeeper
    {
        public string sendGatewayMerchantId { get; set; }
        public string serviceLevel { get; set; }
    }

    public class IpCountry
    {
        public bool rejectAnonymousProxy { get; set; }
        public bool rejectUnknownCountry { get; set; }
    }

    public class MasterCardSecureCode
    {
        [JsonProperty("3DS1")]
        public _3DS1 _3DS1 { get; set; }
    }

    public class _Merchant
    {
        public AcquirerLink acquirerLink { get; set; }
        public Address address { get; set; }
        public string apiAuthenticationMode { get; set; }
        public string cardMaskingFormat { get; set; }
        public string categoryCode { get; set; }
        public string contactName { get; set; }
        public string defaultOrderCertainty { get; set; }
        public Download download { get; set; }
        public string email { get; set; }
        public string goodsDescription { get; set; }
        public string id { get; set; }
        public string locale { get; set; }
        public MerchantManagedRiskAssessment merchantManagedRiskAssessment { get; set; }
        public string name { get; set; }
        public PartnerManagedRiskAssessment partnerManagedRiskAssessment { get; set; }
        public List<string> privilege { get; set; }
        public bool sendWelcomeEmail { get; set; }
        public List<string> service { get; set; }
        public string state { get; set; }
        public string systemCapturedCardMaskingFormat { get; set; }
        public string timeZone { get; set; }
        public TransactionFiltering transactionFiltering { get; set; }
        public DateTime updated { get; set; }
        public string webSite { get; set; }
    }

    public class MerchantManagedRiskAssessment
    {
        public string assessmentInitiation { get; set; }
        public string enableRiskAssessment { get; set; }
        public Gatekeeper gatekeeper { get; set; }
        public Profile profile { get; set; }
        public string providerId { get; set; }
    }

    public class PartnerManagedRiskAssessment
    {
        public TransactionRiskAssessment transactionRiskAssessment { get; set; }
    }

    public class Profile
    {
        public string defaultActionWhenUnableToPerformRiskAssessment { get; set; }
        public string id { get; set; }
        public string supportRiskScoringFor { get; set; }
    }

    public class MerchantDetail
    {
        [JsonProperty("Merchant")]
        public _Merchant _merchant { get; set; }
        public string result { get; set; }
    }

    public class TransactionFiltering
    {
        public bool dynamic3DSecure { get; set; }
        public IpCountry ipCountry { get; set; }
    }

    public class TransactionRiskAssessment
    {
        public string configuration { get; set; }
    }

    public class VerifiedByVisa
    {
        [JsonProperty("3DS1")]
        public _3DS1 _3DS1 { get; set; }
    }
}
