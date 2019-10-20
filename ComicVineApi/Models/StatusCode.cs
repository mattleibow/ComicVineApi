namespace ComicVineApi.Models
{
    public enum StatusCode
    {
        Unknown = 0,

        Ok = 1,
        InvalidApiKey = 100,
        ObjectNotFound = 101,
        ErrorInUrlFormat = 102,
        JsonpFormatRequiresJsonCallbackArgument = 103,
        FilterError = 104,
        SubscriberOnlyVideoIsForSubscribersOnly = 105
    }
}
