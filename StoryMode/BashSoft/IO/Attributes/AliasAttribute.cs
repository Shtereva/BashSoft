using System;

namespace BashSoft.IO.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AliasAttribute : Attribute
    {
        private string name;

        public string Name => this.name;

        public AliasAttribute(string name)
        {
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            return this.name.Equals(obj);
        }
    }
}
