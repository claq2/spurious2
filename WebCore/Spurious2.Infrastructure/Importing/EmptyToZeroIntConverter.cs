using System.Globalization;
using CsvHelper;

namespace Spurious2.Infrastructure.Importing;

public sealed class EmptyToZeroIntConverter : ICsvConverter<int>
{
    public int Read(CsvReader reader, int index)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var value = reader.GetString(index);
        if (string.IsNullOrWhiteSpace(value))
        {
            return 0;
        }

        return int.Parse(value, CultureInfo.InvariantCulture);
    }

    public void Write(CsvWriter writer, int value) => throw new NotImplementedException();
}
