namespace marketstocks;


public record PriceData(string Symbol, DateOnly Date, decimal Open, decimal High, decimal Low, decimal Close, int Volume)
{
  public decimal Spread => High - Low;
  public decimal DailyReturnPercentage => (((Close - Open) / Open) * 100m);
}
