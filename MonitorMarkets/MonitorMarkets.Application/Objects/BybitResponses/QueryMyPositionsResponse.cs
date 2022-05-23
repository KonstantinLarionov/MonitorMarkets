using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Positions;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class QueryMyPositionsResponse
    {
        public QueryMyPositionsResponse(int resultTotalSize, string cursor,
            IReadOnlyList<QueryMyPositionsData> dataList)
        {
            ResultTotalSize = resultTotalSize;
            Cursor = cursor;
            DataList = dataList;
        }
        public int ResultTotalSize { get; set; }
        public string Cursor { get; set; }
        public IReadOnlyList<QueryMyPositionsData> DataList { get; set; }
    }
}