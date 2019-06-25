using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathNet.Numerics.TensorflowBinding
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InputAttribute : Attribute
    {
        public InputAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OutputAttribute : Attribute
    {
        public OutputAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
