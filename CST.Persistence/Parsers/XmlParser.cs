using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace CST.Persistence.Parsers
{
    public class XmlParser
    {
        public XDocument LoadXml(string path)
        {
            return XDocument.Load(path);
        }

        public string GetElementValue(XElement? element, string name, string defaultValue = "")
        {
            var value = element?.Element(name)?.Value;
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public DateTime ParseDate(string dateValue)
        {
            if (!DateTime.TryParse(dateValue, out DateTime date))
            {
                throw new FormatException($"Ошибка преобразования даты: {dateValue}");
            }
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }

        public decimal ParseDecimal(string decimalValue)
        {
            if (!decimal.TryParse(decimalValue, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
            {
                throw new FormatException($"Ошибка преобразования десятичного числа: {decimalValue}");
            }
            return result;
        }

        public int ParseInt(string intValue)
        {
            if (!int.TryParse(intValue, out int result))
            {
                throw new FormatException($"Ошибка преобразования целого числа: {intValue}");
            }
            return result;
        }
    }
}
