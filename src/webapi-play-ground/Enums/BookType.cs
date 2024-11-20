using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace webapi_play_ground.Enums
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookType
    {
        //[EnumMember(Value = "Fiction")]
        Fiction,
        //[EnumMember(Value = "Novel")]
        Novel,
        //[EnumMember(Value = "SciFi")]
        SciFi
    }
}