


using System.Globalization;

public class Ingest
{
    public static List<LogEntry> logs = [];

 public static string TickerNormalizer(string raw)
  {
    if(string.IsNullOrEmpty(raw)) return "";

    raw = raw.Trim().ToUpper();

    if (raw.Contains(":"))
    {
      raw = raw.Split(":")[1];
    }

    if (raw.Contains("."))
    {
      raw = raw.Split(".")[0];
    }

    return raw;
  }

 public static void ErroReader(int line, string error, string reason)
  {
    var log = new LogEntry(line,error, reason);
    logs.Add(log);
  }
  public static List<PriceData> CsvReader (string filePath)
  {

    var prices = new List<PriceData>();


    int failedCount = 0;
    if (File.Exists(filePath))
    {
      var lines = File.ReadLines(filePath)
                      .Select((line, index) => new{ Text = line , lineNum = index + 1})
                      .Skip(1);

      foreach(var line in lines)
      {
        if(string.IsNullOrEmpty(line.Text))
        {
          ErroReader(line.lineNum, line.Text, "The Line is empty");
          failedCount++;
          continue;
        }
        ;
        var row = line.Text.Split(",");

        if(row.Length is 7 && DateOnly.TryParse(row[1], CultureInfo.InvariantCulture, out var date) &&
        decimal.TryParse(row[2], CultureInfo.InvariantCulture, out var open) &&
        decimal.TryParse(row[3], CultureInfo.InvariantCulture, out var high) &&
        decimal.TryParse(row[4], CultureInfo.InvariantCulture, out var low) &&
        decimal.TryParse(row[5], CultureInfo.InvariantCulture, out var close) &&
        int.TryParse(row[6], CultureInfo.InvariantCulture, out var volume)
        )
        {

          bool isValid = !string.IsNullOrWhiteSpace(row[0]) &&
                          open > 0 && close >0 && high >= low && volume >=0;

          if (isValid)
          {
            row[0] = TickerNormalizer(row[0]);
            var priceRecord  = new PriceData(row[0], date, open, high, low, close, volume);
            prices.Add(priceRecord);
            Console.WriteLine(row[1]);
          }
          else
          {
            ErroReader(line.lineNum, "Domain Rule violation", line.Text);
            failedCount++;
            continue;
          }
        }
        else
        {
          ErroReader(line.lineNum, "Parseing/Column structure erro", line.Text);
          failedCount++;
          continue;
        }

      }

      Console.WriteLine($"Successfully added {prices.Count} and {failedCount} failed!");


    }
    else
    {
      ErroReader(0, "No file Found", "No file Found");
      Console.WriteLine("File does not exists.");
    }
    File.WriteAllLines("errors.logs", logs.Select(log => $"Line {log.line}: [{log.Error} -> {log.Reason}]"));
    return prices;
  }

}
