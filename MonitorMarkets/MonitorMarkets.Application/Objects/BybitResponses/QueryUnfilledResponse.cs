using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class QueryUnfilledResponse
    {
        public QueryUnfilledResponse(int resultTotalSize, string cursor, IReadOnlyList<QueryOrderHistoryData> dataList)
        {
            ResultTotalSize = resultTotalSize;
            Cursor = cursor;
            DataList = dataList;
        }
        public int ResultTotalSize { get; set; }
        public string Cursor { get; set; }
        public IReadOnlyList<QueryOrderHistoryData> DataList { get; set; }

    }
}