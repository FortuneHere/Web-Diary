using System;

namespace WebDiary.DB.Models
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ShortDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public ShortDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}