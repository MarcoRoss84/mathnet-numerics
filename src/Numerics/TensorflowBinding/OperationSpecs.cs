#if NET461 || NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TensorFlow;

namespace MathNet.Numerics.TensorflowBinding
{
    public class OperationSpecs
    {
        public string Name { get; }
        public TFShape Shape { get; }
        public TFDataType DataType { get; }
        public TFOperation Operation { get; }
        
        public OperationSpecs(string name, TFShape shape, TFDataType dataType, TFOperation operation)
        {
            Name = name;
            Shape = shape;
            DataType = dataType;
            Operation = operation;
        }

    }

    public abstract class PlaceholderSpecs : OperationSpecs
    {
        protected PlaceholderSpecs(string name, TFShape shape, TFDataType dataType, TFOperation operation) : base(name, shape, dataType, operation)
        {
        }

        public abstract TFTensor GetValue(Graph graph);
    }

    public class PlaceholderSpecs<T> : PlaceholderSpecs where T : Graph
    {
        private readonly PropertyInfo _property;

        public PlaceholderSpecs(string name, TFShape shape, TFDataType dataType, TFOperation operation) : base(name, shape, dataType, operation)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var inputAttribute = property.GetCustomAttributes(true).OfType<InputAttribute>().FirstOrDefault();
                if (inputAttribute == null) continue;
                if (inputAttribute.Name != name) continue;
                if (!property.PropertyType.IsArray)
                {
                    throw new Exception("Input properties bound to a tensorflow graph must be of an array type");
                }
                if (property.PropertyType.GetArrayRank() != shape.NumDimensions)
                {
                    throw new Exception("Property " + property + " has array rank " + property.PropertyType.GetArrayRank() + ", expected " + shape.NumDimensions);
                }
                if (!Graph.IsCompatible(property.PropertyType.GetElementType(), dataType))
                {
                    throw new Exception("Element type of property " + property + " is not compatible with " + dataType + ": " + property.PropertyType.GetElementType());
                }
                _property = property;
                break;
            }
            if (_property == null)
            {
                throw new Exception("No input property was found for placeholder " + name);
            }
        }

        public override TFTensor GetValue(Graph graph)
        {
            if (graph.GetType() != typeof(T))
            {
                throw new ArgumentException("Expected graph of type " + typeof(T) + ", but received " + graph.GetType());
            }
            var array = _property.GetValue(graph) as Array;
            if (array == null)
            {
                throw new Exception("Value for property " + _property + " is not set!");
            }
            var tensor = new TFTensor(array);
            return tensor;
        }
    }
}
#endif
