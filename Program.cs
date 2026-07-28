
        var rand = new Random();

        // var symbols = new List<string> { "AAPL", "MSFT", "AMZN", "TSLA", "GOOG" };
        // var priceData = new List<PriceData>();

        // foreach (var symbol in symbols)
        // {
        //     decimal closePrice = 0;
        //     var date = new DateOnly(2025, 12, 20);
        //     const int year = 2025;

        //     decimal open = rand.Next(10, 500);
        //     closePrice = open;

        //     date = SkipWeekends(date);
        //     while (date.Year == year)
        //     {
        //         open = closePrice + ((decimal)rand.NextDouble() - (decimal)rand.NextDouble()) * 4;
        //         closePrice = open + ((decimal)rand.NextDouble() - (decimal)rand.NextDouble()) * 4;
        //         var high = Math.Max(open, closePrice);
        //         high += (decimal)rand.NextDouble() * 4;
        //         var low = Math.Min(open, closePrice);
        //         low -= (decimal)rand.NextDouble() * 4;
        //         var volume = rand.Next(100000, 10000000);

        //         var prices = new PriceData(symbol, date, open, high, low, closePrice, volume);
        //         priceData.Add(prices);
        //         date = date.AddDays(1);
        //         date = SkipWeekends(date);
        //     }
        // }

        // var sortedData = priceData.OrderBy(o => o.Symbol).ThenBy(d => d.Date).ToList();
        // var lines = new List<string> { "symbol,date,open,high,low,close,volume" };

        // foreach (var (symbol, date, open, high, low, close, volume) in sortedData)
        // {
        //     lines.Add($"{symbol},{date:MM/dd/yyyy},{open:F2},{high:F2},{low:F2},{close:F2},{volume}");
        // }

        // File.WriteAllLines("prices.csv", lines);
        // Console.WriteLine($"Wrote {lines.Count - 1} rows to prices.csv");
        var readCsv = Ingest.CsvReader("prices.csv");






 DateOnly SkipWeekends(DateOnly d)
    {
        while (d.DayOfWeek is DayOfWeek.Saturday || d.DayOfWeek is DayOfWeek.Sunday)
        {
            d = d.AddDays(1);
        }

        return d;
    }

public record PriceData(string Symbol, DateOnly Date, decimal Open, decimal High, decimal Low, decimal Close, int Volume);

public record LogEntry(int line, string Error, string Reason);
