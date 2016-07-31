using System;
using System.ComponentModel;
using System.Reflection;

namespace Notes.Web.Infrastructure.Models
{
    public class StatusBarInfo
    {
        public enum StatusBarType
        {
            [Description("alert alert-success")]
            Success,
            [Description("alert alert-info")]
            Info,
            [Description("alert alert-warning")]
            Warning,
            [Description("alert alert-danger")]
            Danger
        }

        public string Message { get; set; }

        public StatusBarType Type { get; set; }

        public string CssClasses 
        {
            get
            {
                Type type = Type.GetType();
                FieldInfo fieldInfo = type.GetField(Type.ToString());

                if (fieldInfo != null)
                {
                    DescriptionAttribute descriptionAttr = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);

                    if (descriptionAttr != null)
                        return descriptionAttr.Description;
                }

                return String.Empty;
            }
        }
    }
}
