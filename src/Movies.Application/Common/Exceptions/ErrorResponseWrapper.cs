using Newtonsoft.Json;

namespace Movies.Application.Common.Exceptions;
public class ErrorResponseWrapper<T>
{
    [JsonProperty("error")]
    public T Error { get; set; }
}
