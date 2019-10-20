using System;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Image
    {
        [JsonProperty("icon_url")]
        public Uri? IconUrl { get; set; }

        [JsonProperty("medium_url")]
        public Uri? MediumUrl { get; set; }

        [JsonProperty("screen_url")]
        public Uri? ScreenUrl { get; set; }

        [JsonProperty("screen_large_url")]
        public Uri? ScreenLargeUrl { get; set; }

        [JsonProperty("small_url")]
        public Uri? SmallUrl { get; set; }

        [JsonProperty("super_url")]
        public Uri? SuperUrl { get; set; }

        [JsonProperty("thumb_url")]
        public Uri? ThumbUrl { get; set; }

        [JsonProperty("tiny_url")]
        public Uri? TinyUrl { get; set; }

        [JsonProperty("original_url")]
        public Uri? OriginalUrl { get; set; }

        [JsonProperty("image_tags")]
        public string? ImageTags { get; set; }
    }
}
