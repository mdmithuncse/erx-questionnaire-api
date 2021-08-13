using System.Text.Json.Serialization;

namespace Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InputType
    {
        DropDown = 1,
        CheckBox = 2,
        RadioButton = 3,
        SingleLineText = 4,
        MultilineText = 5
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AnswerDataSource
    {
        Text = 1,
        Url = 2
    }
}
