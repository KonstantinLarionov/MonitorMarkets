/// <summary>
    /// USDT Perpetual Клиент
    /// </summary>
    internal class PerpetualRestClient
    {
        readonly PerpetualHandlerComposition m_HandlerComposition;
        internal PerpetualHandlerComposition HandlerComposition => m_HandlerComposition;

        readonly RequestArranger m_RequestsArranger;

        /// <summary>
        /// 
        /// </summary>
        internal PerpetualRestClient(RequestArranger ra)
        {
            m_HandlerComposition = new PerpetualHandlerComposition(new PerpetualHandlerFactory());
            m_RequestsArranger = ra;

        }

        #region [Base]

        RestClient m_RestClient;
        /// <summary>
        /// 
        /// </summary>
        internal void SetUrl(string rest_url)
        {
            m_RestClient = new RestClient(rest_url);
        }
        /// <summary>
        /// 
        /// </summary>
        internal void SetProxy(string ip, string port, string username = null, string password = null)
        {
            try
            {
                var proxy = new WebProxy($"{ip}:{port}");
                if (!string.IsNullOrWhiteSpace(username) || !string.IsNullOrWhiteSpace(password))
                { proxy.Credentials = new NetworkCredential(username, password); }
                m_RestClient.Proxy = proxy;
            }
            catch (Exception ex)
            { OnLogEx(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void ResetProxy()
        {
            m_RestClient.Proxy = null;
        }

        internal delegate void Log_Dlg(string sender, string message);
        internal Log_Dlg Log;
        internal bool LogResponceEnabled = false;
        internal bool LogExEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        void OnLogResponce(string responce)
        {
            if (LogResponceEnabled)
            { Log?.Invoke("RestClient", string.Concat("Responce: ", responce)); }
        }
        /// <summary>
        /// 
        /// </summary>
        void OnLogEx(Exception ex, string responce = null)
        {
            if (LogExEnabled)
            { Log?.Invoke("RestClient", string.Concat("Exception: ", ex.Message, "; ", ex?.InnerException, " - ", responce)); }
        }

        /// <summary>
        /// 
        /// </summary>
        string SendRestRequest(IRequestContent message)
        {
            Method method;

            switch (message.Method)
            {
                case RequestMethod.GET:
                    method = Method.GET;
                    break;
                case RequestMethod.POST:
                    method = Method.POST;
                    break;
                case RequestMethod.PUT:
                    method = Method.PUT;
                    break;
                case RequestMethod.DELETE:
                    method = Method.DELETE;
                    break;
                default:
                    throw new NotImplementedException("Unknown request method");
            }

            var request = new RestRequest(message.Query, method);
            if (message.Body != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(message.Body);
            }
            if (message.Headers != null)
            {
                foreach (var header in message.Headers)
                { request.AddHeader(header.Key, header.Value); }
            }

            return m_RestClient.Execute(request).Content;
        }

        #endregion

        #region [Requests]
        /// <summary>
        /// 
        /// </summary>
        bool RequestTemplate(/*out _V.Responses.XXX data*/)
        {
            //data = null;

            //var request = m_RequestsArranger.Arrange(new _V.Requests.XXX());
            //string responce = string.Empty;
            //try
            //{
            //    responce = SendRestRequest(request);
            //    data = m_HandlerComposition.HandleXXX(responce);
            //    OnLogResponce(responce);
            //    return true;
            //}
            //catch (Exception ex)
            //{ OnLogEx(ex, responce); }
            return false;
        }

        internal bool RequestPositionModeSwitch(out PositionModeSwitchResponse data, string symbol, PositionModeType mode)
        {
            data = null;
            var request = m_RequestsArranger.Arrange(new PositionModeSwitchRequest(symbol, mode));

            string responce = string.Empty;
            try
            {
                responce = SendRestRequest(request);
                data = m_HandlerComposition.HandleAddPositionModeSwitchResponse(responce);
                OnLogResponce(responce);
                return data.RetCode == 0;
            }
            catch (Exception ex)
            {
                OnLogEx(ex, responce);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        internal bool RequestServerTime(out ServerTimeResponse data)
        {
            data = null;

            var request = m_RequestsArranger.Arrange(new ServerTimeRequest());

            string responce = string.Empty;
            try
            {
                responce = SendRestRequest(request);
                data = m_HandlerComposition.HandlerServerTimeResponse(responce);
                //OnLogResponce(responce);
                return true;
            }
            catch (Exception ex)
            { OnLogEx(ex, responce); }
            return false;
        }
    }