using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MonitorMarkets.Application.MarketsAdaptor;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Application.Abstraction
{
    public class FactoryClientService
    {
        /// <summary>
        /// Коллекция бирж
        /// </summary>
        private Dictionary<MarketsEnum, IMarketClient> _collection;

        /// <summary>
        /// Синглтон
        /// </summary>
        private static FactoryClientService _instance = null;

        /// <summary>
        /// FactoryClientService PRIVATE
        /// </summary>
        private FactoryClientService()
        {
            //Тут заполняем коллекцию стыкую с перечислением
            _collection = new Dictionary<MarketsEnum, IMarketClient>()
            {

            };
        }

        /// <summary>
        /// Синглтон
        /// </summary>
        /// <returns></returns>
        public static FactoryClientService GetInstance()
        {
            if (_instance is null)
                _instance = new FactoryClientService();
            return _instance;
        }

        /// <summary>
        /// Фабрика
        /// </summary>
        /// <param name="marketEnum"></param>
        /// <returns></returns>
        public IMarketClient GetMarket(MarketsEnum marketEnum) => _collection[marketEnum];
    }
}