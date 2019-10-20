using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    internal class SometimesArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                var listType = typeof(List<>).MakeGenericType(objectType);
                var enumerable = serializer.Deserialize(reader, listType) as IList;
                return enumerable?.Count > 0 ? enumerable[0] : null;
            }
            else
            {
                return serializer.Deserialize(reader, objectType);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotSupportedException("Serialization is not supported.");
    }
}
