using System.ComponentModel;

namespace Timesheet.Web.Utils
{
  public static class EnumExtension
  {
    public static string GetDescription(this Enum enumValue)
    {
      var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

      if (fieldInfo is not null && Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
      {
        return attribute.Description;
      }

      return enumValue.ToString();
    }
  }

}
