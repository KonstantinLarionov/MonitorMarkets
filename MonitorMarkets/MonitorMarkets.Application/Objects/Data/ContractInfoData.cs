using System;

namespace MonitorMarkets.Application.Objects.Data
{
    public class ContractInfoData
    {
        public ContractInfoData(
      string symbol,
      string status,
      string baseCoin,
      string quoteCoin,
      Decimal takerFeeRate,
      Decimal makerFeeRate,
      int? minLeverage,
      int? maxLeverage,
      decimal? leverageStep,
      decimal? minPrice,
      decimal? maxPrice,
      decimal? tickSize,
      decimal? minTradingQty,
      decimal? maxTradingQty,
      decimal? qtyStep,
      string deliveryFreeRate,
      string deliveryTime)
    {
      this.Symbol = symbol;
      this.Status = status;
      this.BaseCoin = baseCoin;
      this.QuoteCoin = quoteCoin;
      this.TakerFeeRate = takerFeeRate;
      this.MakerFeeRate = makerFeeRate;
      this.MinLeverage = minLeverage;
      this.MaxLeverage = maxLeverage;
      this.LeverageStep = leverageStep;
      this.MinPrice = minPrice;
      this.MaxPrice = maxPrice;
      this.TickSize = tickSize;
      this.MinTradingQty = minTradingQty;
      this.MaxTradingQty = maxTradingQty;
      this.QtyStep = qtyStep;
      this.DeliveryFreeRate = deliveryFreeRate;
      this.DeliveryTime = deliveryTime;
    }

    public string Symbol { get; set; }

    public string Status { get; set; }

    public string BaseCoin { get; set; }

    public string QuoteCoin { get; set; }

    public Decimal TakerFeeRate { get; set; }

    public Decimal MakerFeeRate { get; set; }

    public int? MinLeverage { get; set; }

    public int? MaxLeverage { get; set; }

    public decimal? LeverageStep { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public decimal? TickSize { get; set; }

    public decimal? MinTradingQty { get; set; }

    public decimal? MaxTradingQty { get; set; }

    public decimal? QtyStep { get; set; }

    public string DeliveryFreeRate { get; set; }

    public string DeliveryTime { get; set; }
    }
}