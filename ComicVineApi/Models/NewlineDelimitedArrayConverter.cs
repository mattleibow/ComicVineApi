using System;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    internal class NewlineDelimitedArrayConverter : JsonConverter<string[]?>
    {
        public override bool CanWrite => false;

        public override string[]? ReadJson(JsonReader reader, Type objectType, string[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var value = serializer.Deserialize<string>(reader);

            return value.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override void WriteJson(JsonWriter writer, string[]? untypedValue, JsonSerializer serializer) =>
            throw new NotSupportedException("Serialization is not supported.");
    }
}
