using System.Runtime.Serialization;

namespace algolia_exploration;

public static class EnumConvertor
{
    public static string ToEnumMemberAttrValue(this Enum @enum) {
        var attr =
            @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.GetCustomAttributes(false)
                .OfType<EnumMemberAttribute>().FirstOrDefault();

        return attr?.Value ?? @enum.ToString();
    }
}