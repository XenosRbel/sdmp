using System;
using Newtonsoft.Json;

namespace Laba_2.Models.OnlinerProduct
{
	internal class CurrencyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Currency) || t == typeof(Currency?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BYN":
                    return Currency.Byn;
                case "BYR":
                    return Currency.Byr;
            }
            throw new Exception("Cannot unmarshal type Currency");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Currency)untypedValue;
            switch (value)
            {
                case Currency.Byn:
                    serializer.Serialize(writer, "BYN");
                    return;
                case Currency.Byr:
                    serializer.Serialize(writer, "BYR");
                    return;
            }
            throw new Exception("Cannot marshal type Currency");
        }

        public static readonly CurrencyConverter Singleton = new CurrencyConverter();
    }
}
