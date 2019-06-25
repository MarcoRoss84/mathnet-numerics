#if NET461 || NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TensorFlow;

namespace MathNet.Numerics.TensorflowBinding
{
    public abstract class Graph : IDisposable
    {
        private TFGraph _graph;
        private Dictionary<string, PlaceholderSpecs> _placeholders = new Dictionary<string, PlaceholderSpecs>();
        private Dictionary<string, OperationSpecs> _operations = new Dictionary<string, OperationSpecs>();
        private string[] _outputs;
        private Dictionary<string, PropertyInfo> _outputProperties = new Dictionary<string, PropertyInfo>();

        private void BindGraph<T>(TFGraph graph) where T : Graph {
            _graph = graph;
            var operations = graph.GetEnumerator().Select(GetOperationSpecs<T>);
            foreach (var op in operations)
            {
                _operations.Add(op.Name, op);
                if (op is PlaceholderSpecs) _placeholders.Add(op.Name, op as PlaceholderSpecs);
            }
            var outputs = new List<string>();
            foreach (var property in typeof(T).GetProperties())
            {
                var outputAttribute = property.GetCustomAttributes(true).OfType<OutputAttribute>().FirstOrDefault();
                if (outputAttribute == null) continue;
                if (!_operations.ContainsKey(outputAttribute.Name))
                {
                    throw new Exception("Could not bind property " + property + " to any operation node of the graph, no such element: " + outputAttribute.Name);
                }
                if (!property.PropertyType.IsArray)
                {
                    throw new Exception("Output properties bound to a tensorflow graph must be of an array type");
                }
                var op = _operations[outputAttribute.Name];
                if (!IsCompatible(property.PropertyType.GetElementType(), op.DataType))
                {
                    throw new Exception("Element type of property " + property + " is not compatible with " + op.DataType + ": " + property.PropertyType.GetElementType());
                }
                outputs.Add(outputAttribute.Name);
                _outputProperties.Add(outputAttribute.Name, property);
            }
            _outputs = outputs.ToArray();
        }

        internal static bool IsCompatible(Type type, TFDataType dataType)
        {
            switch (dataType)
            {
                case TFDataType.Float: return type == typeof(float);
                case TFDataType.Double: return type == typeof(double);
                case TFDataType.Int8: return type == typeof(byte);
                case TFDataType.UInt16: return type == typeof(ushort);
                case TFDataType.Int16: return type == typeof(short);
                case TFDataType.UInt32: return type == typeof(uint);
                case TFDataType.Int32: return type == typeof(int);
                case TFDataType.UInt64: return type == typeof(ulong);
                case TFDataType.Int64: return type == typeof(long);
                case TFDataType.Bool: return type == typeof(bool);
            }
            return false;
        }

        private OperationSpecs GetOperationSpecs<T>(TFOperation op) where T : Graph
        {
            TFShape shape;
            try
            {
                shape = op.GetAttributeShape("shape", (int)op.GetAttributeMetadata("shape").TotalSize);
            }
            catch (Exception)
            {
                shape = null;
            }
            TFDataType dataType;
            try
            {
                dataType = op.GetAttributeType("dtype");
            }
            catch (Exception)
            {
                try
                {
                    dataType = op.GetAttributeType("T");
                }
                catch (Exception)
                {
                    dataType = TFDataType.Unknown;
                }
            }
            if (op.OpType == "Placeholder") return new PlaceholderSpecs<T>(op.Name, shape, dataType, op);
            return new OperationSpecs(op.Name, shape, dataType, op);
        }

        public void Dispose()
        {
            if (_graph != null)
            {
                _graph.Dispose();
                _graph = null;
            }
            _operations.Clear();
            _placeholders.Clear();
        }

        public IReadOnlyDictionary<string, PlaceholderSpecs> Placeholders => _placeholders;
        public IReadOnlyDictionary<string, OperationSpecs> Operations => _operations;

        public void Run()
        {
            using (var session = new TFSession(_graph))
            {
                var runner = session.GetRunner();
                foreach (var placeholder in Placeholders.Values)
                {
                    runner.AddInput(placeholder.Name, placeholder.GetValue(this));
                }
                runner.Fetch(_outputs);
                var results = runner.Run();
                for (int i = 0; i < _outputs.Length; i++)
                {
                    var array = results[i].GetValue();
                    _outputProperties[_outputs[i]].SetValue(this, array);
                }
            }
        }

        public static T FromProtocolBuffer<T>(string path) where T : Graph, new()
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return FromProtocolBuffer<T>(stream);
            }
        }

        public static T FromProtocolBuffer<T>(Stream stream) where T : Graph, new()
        {
            try {
                stream.Seek(0, SeekOrigin.Begin);
                var n = stream.Length;
                var bytes = new byte[n];
                if (stream.Read(bytes, 0, bytes.Length) != n)
                {
                    throw new IOException("Failed to read entire file");
                }
                var tfGraph = new TFGraph();
                tfGraph.Import(bytes);
                var graph = new T();
                graph.BindGraph<T>(tfGraph);
                return graph;
            } finally
            {
                stream.Close();
            }
        }

        
    }
}
#endif
