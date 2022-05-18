using System;
using System.Net;
using BybitMapper.Requests;
using BybitMapper.UsdcPerpetual.RestV2;
using RestSharp;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    internal class ByBitClient
    {
        private UsdcPepetualHandlerComposition m_HandlerComposition;
        internal UsdcPepetualHandlerComposition HandlerComposition => m_HandlerComposition;
        RequestArranger m_RequestArranger;

        internal void UsdcPerpetualRestClient(RequestArranger ra)
        {
            m_HandlerComposition = new UsdcPepetualHandlerComposition(new UsdcPepetualHandlerFactory());
            m_RequestArranger = ra;
        }

        #region [Base]

        RestClient m_RestClient;

        internal void SetUrl(string rest_url)
        {
            m_RestClient = new RestClient(rest_url);
        }

        internal void SetProxy(string ip, string port, string username = null, string password = null)
        {
            try
            {
                var proxy = new WebProxy($"{ip}:{port}");
                if (!string.IsNullOrWhiteSpace(username) || !string.IsNullOrWhiteSpace(password))
                {
                    proxy.Credentials = new NetworkCredential(username, password);
                }

                m_RestClient.Proxy = proxy;
            }
            catch (Exception ex)
            {
                OnLogEx(ex);
            }
        }
        #endregion
    }
}