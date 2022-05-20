using Newtonsoft.Json;

namespace Movies.Application.Common.Exceptions;
public class ErrorResponse
{
    [JsonProperty("correlationId")]
    public string CorrelationId { get; set; }


    [JsonProperty("message")]
    public List<string> Message { get; private set; }

    public ErrorResponse(string description)
    {
        Message = new List<string>();
        this.Message.Add(description);
    }
    public ErrorResponse(List<string> description)
    {
        this.Message = description;
    }
}
