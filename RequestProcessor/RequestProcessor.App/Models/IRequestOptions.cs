using Newtonsoft.Json;

namespace RequestProcessor.App.Models
{
    /// <summary>
    /// Request options.
    /// </summary>
    internal interface IRequestOptions
    {
        /// <summary>
        /// Optional friendly name.
        /// </summary>
        [JsonProperty("name")]

        string Name { get; }

        /// <summary>
        /// Request address.
        /// Should be valid Uri.
        /// </summary>
        [JsonProperty("address")]
        string Address { get; }

        /// <summary>
        /// Request method.
        /// </summary>
        [JsonProperty("method")]

        RequestMethod Method { get; }

        /// <summary>
        /// Request content type.
        /// Can be <c>null</c> when <see cref="Body"/> is null.
        /// </summary>
        [JsonProperty("contentType")]
        string ContentType { get; }

        /// <summary>
        /// Request content.
        /// Optional property.
        /// </summary>
        [JsonProperty("body")]
        string Body { get;}

        /// <summary>
        /// Indicates that options are valid.
        /// </summary>
        bool IsValid { get; }
    }
}
