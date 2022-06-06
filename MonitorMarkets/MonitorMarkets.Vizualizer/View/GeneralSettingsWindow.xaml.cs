using System;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows;
using RestSharp;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Contexts;
using MonitorMarkets.Contexts.Entities;
using RestSharp.Authenticators;
using Swagger.Net.Annotations;

namespace MonitorMarkets.Vizualizer.View
{
    
    protected async ValueTask<Parameter> GetAuthenticationParameter(string accessToken) {
        var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, token);
    }
    async Task<string> GetToken() {
        var options = new RestClientOptions("https://localhost:7116");
        using var client = new RestClient(options) {
            Authenticator = new HttpBasicAuthenticator(root, root),
        };

        var request = new RestRequest("oauth2/token")
            .AddParameter("grant_type", "client_credentials");
        var response = await client.PostAsync<TokenResponse>(request);
        return quot;{response!.TokenType} {response!.AccessToken}";
    }
    public partial class GeneralSettingsWindow : Window
    {
        readonly string _baseUrl;
        readonly string _clientId;
        readonly string _clientSecret;
        
       
        public GeneralSettingsWindow(string baseUrl, string clientId, string clientSecret)
        {
            _baseUrl = baseUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
            InitializeComponent();
            GetKeys();
        }
        

        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string publicKey = PublicKeyTextBox.Text;
            string secretKey= PublicKeyTextBox.Text;
            string passPhrase = PublicKeyTextBox.Text;
            string marketsEnum = PublicKeyTextBox.Text;
            ChangeKeys(publicKey, secretKey, passPhrase, marketsEnum);
        }
        public void ChangeKeys(string publicKey,string secretKey, string passPhrase, string marketsEnum)
        {
            var request = new RestRequest("https://localhost:7116/UpdateKey");
            request.AddOrUpdateParameter("PublicKey", publicKey);
            request.AddOrUpdateParameter("SecretKey", secretKey );
            request.AddOrUpdateParameter("PassPhrase", passPhrase);
            request.AddOrUpdateParameter("MarketEnums",marketsEnum);
        }

        public void GetKeys()
        {
            var request = new RestRequest("https://localhost:7116/GetKey");
            var response = request.Method == Method.GET;
        }
    }
    
}