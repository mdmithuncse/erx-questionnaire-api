using System.Text.Json.Serialization;

namespace Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AnswerSourceType
    {
        Text = 1,
        Url = 2
    }
}
