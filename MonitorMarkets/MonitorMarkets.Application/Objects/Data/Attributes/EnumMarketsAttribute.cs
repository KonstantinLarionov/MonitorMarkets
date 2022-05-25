using System;

namespace MonitorMarkets.Application.Objects.Data.Attributes
{
    public class EnumMarketsAttribute : Attribute
    {
        public EnumMarketsAttribute(string nameMarket, string apiKey, string secretKey, string passPhrase, bool isActive)
        {
            NameMarket = nameMarket;
            ApiKey = apiKey;
            SecretKey = secretKey;
            PassPhrase = passPhrase;
            IsActive = isActive;
        }

        public string NameMarket { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string PassPhrase { get; set; }
        public bool IsActive { get; }
        public bool isActive { get; set; }
    }
}