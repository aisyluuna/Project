using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var attr = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttributes()
                .FirstOrDefault(attr => attr is DisplayAttribute);

            var displAttr = attr as DisplayAttribute;

            return displAttr?.Name ?? string.Empty;
        }
    }
}
