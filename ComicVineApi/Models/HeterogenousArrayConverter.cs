using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ComicVineApi.Models
{
    internal class HeterogenousArrayConverter : CustomCreationConverter<object>
    {
        public override object Create(Type objectType) =>
            throw new NotImplementedException();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            var array = JArray.Load(reader);

            var resArray = new List<object>(array.Count);
            foreach (JObject item in array)
            {
                // Create target object based on JObject 
                var type = (string)item.Property("resource_type", StringComparison.OrdinalIgnoreCase);
                var target = Create(type, serializer);

                // Populate the object properties
                if (target != null)
                {
                    using var objReader = item.CreateReader();
                    serializer.Populate(objReader, target);
                    resArray.Add(target);
                }
            }

            return resArray.ToArray();
        }

        private object? Create(string type, JsonSerializer serializer)
        {
            switch (type)
            {
                case "character":
                    return new Character();
                case "series":
                    return new Series();
                case "publisher":
                    return new Publisher();
                case "volume":
                    return new Volume();
            }

            if (serializer.MissingMemberHandling == MissingMemberHandling.Error)
                throw new JsonSerializationException($"The resource type of '{type}' could not be found.");

            return null;
        }
    }
}
