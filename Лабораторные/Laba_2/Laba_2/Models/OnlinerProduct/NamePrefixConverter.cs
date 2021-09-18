using System;
using Newtonsoft.Json;

namespace Laba_2.Models.OnlinerProduct
{
	internal class NamePrefixConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NamePrefix) || t == typeof(NamePrefix?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Мобильный телефон":
                    return NamePrefix.МобильныйТелефон;
                case "Смартфон":
                    return NamePrefix.Смартфон;
                case "Телескоп":
                    return NamePrefix.Телескоп;
            }
            throw new Exception("Cannot unmarshal type NamePrefix");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NamePrefix)untypedValue;
            switch (value)
            {
                case NamePrefix.МобильныйТелефон:
                    serializer.Serialize(writer, "Мобильный телефон");
                    return;
                case NamePrefix.Смартфон:
                    serializer.Serialize(writer, "Смартфон");
                    return;
                case NamePrefix.Телескоп:
                    serializer.Serialize(writer, "Телескоп");
                    return;
            }
            throw new Exception("Cannot marshal type NamePrefix");
        }

        public static readonly NamePrefixConverter Singleton = new NamePrefixConverter();
    }
}
