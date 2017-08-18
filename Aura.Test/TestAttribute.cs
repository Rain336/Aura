using System;

namespace Aura.Test
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute
    {
        public string Name { get; set; }
        
        public TestAttribute()
        {
        }

        public TestAttribute(string name)
        {
            Name = name;
        }
    }
}