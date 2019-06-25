using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.TensorflowBinding;
using NUnit.Framework;
using MathNet.Numerics.TestData;
using TensorFlow;

namespace MathNet.Numerics.Tests.TensorflowBinding
{
    [TestFixture, Category("TensorflowBinding")]
    public class TestGraph
    {

        [OneTimeSetUp]
        public void Verify64Bit()
        {
            Assume.That(Environment.Is64BitProcess, "Test is not running in a 64 bit environment!");
        }

        [Test]
        public void LoadGraphFromProtocolBuffer()
        {
            using (var graph = Graph.FromProtocolBuffer<SimpleGraph>(Data.ReadStream("Tensorflow.SimpleGraph.pb")))
            {
                Assert.That(graph, Is.Not.Null);
            }
        }

        [Test]
        public void IdentifyPlaceholders()
        {
            using (var graph = Graph.FromProtocolBuffer<SimpleGraph>(Data.ReadStream("Tensorflow.SimpleGraph.pb")))
            {
                Assume.That(graph, Is.Not.Null);

                Assert.That(graph.Placeholders.Count, Is.EqualTo(2));
                var x = graph.Placeholders["x"];
                var y = graph.Placeholders["y"];

                Assert.That(x, Is.Not.Null);
                Assert.That(x.Shape.ToArray(), Is.EqualTo(new TFShape(-1, -1, 5).ToArray()));
                Assert.That(x.DataType, Is.EqualTo(TFDataType.Float));
                Assert.That(x.Operation, Is.Not.Null);

                Assert.That(y, Is.Not.Null);
                Assert.That(y.Shape.ToArray(), Is.EqualTo(new TFShape(-1, -1, 5).ToArray()));
                Assert.That(y.DataType, Is.EqualTo(TFDataType.Float));
                Assert.That(y.Operation, Is.Not.Null);

            }
        }

        [Test]
        public void IdentifyOutputs()
        {
            using (var graph = Graph.FromProtocolBuffer<SimpleGraph>(Data.ReadStream("Tensorflow.SimpleGraph.pb")))
            {
                Assume.That(graph, Is.Not.Null);

                Assert.That(graph.Operations, Contains.Key("z"));
                var z = graph.Operations["z"];

                Assert.That(z, Is.Not.Null);
                //Assert.That(z.Shape.ToArray(), Is.EqualTo(new TFShape(-1, -1, 5).ToArray()));
                Assert.That(z.DataType, Is.EqualTo(TFDataType.Float));
                Assert.That(z.Operation, Is.Not.Null);
            }
        }

        [Test]
        public void EvaluateGraph()
        {
            using (var graph = Graph.FromProtocolBuffer<SimpleGraph>(Data.ReadStream("Tensorflow.SimpleGraph.pb")))
            {
                Assume.That(graph, Is.Not.Null);

                graph.X = new float[,,] {
                    {
                        { 111, 112, 113, 114, 115 },
                        { 121, 122, 123, 124, 125 },
                        { 131, 132, 133, 134, 135 }
                    },
                    {
                        { 211, 212, 213, 214, 215 },
                        { 221, 222, 223, 224, 225 },
                        { 231, 232, 233, 234, 235 }
                    }
                };

                graph.Y = new float[,,] {
                    {
                        { 6111, 6112, 6113, 6114, 6115 },
                        { 6121, 6122, 6123, 6124, 6125 },
                        { 6131, 6132, 6133, 6134, 6135 }
                    },
                    {
                        { 6211, 6212, 6213, 6214, 6215 },
                        { 6221, 6222, 6223, 6224, 6225 },
                        { 6231, 6232, 6233, 6234, 6235 }
                    }
                };

                graph.Run();

                Assert.That(graph.Z, Is.EqualTo(new float[,,] {
                    {
                        { 12333, 12336, 12339, 12342, 12345 },
                        { 12363, 12366, 12369, 12372, 12375 },
                        { 12393, 12396, 12399, 12402, 12405 }
                    },
                    {
                        { 12633, 12636, 12639, 12642, 12645 },
                        { 12663, 12666, 12669, 12672, 12675 },
                        { 12693, 12696, 12699, 12702, 12705 }
                    }
                }));

            }
        }

        [Test]
        public void WrongInputType()
        {
            Assert.Throws<Exception>(() => {
                Graph.FromProtocolBuffer<WrongInputType>(Data.ReadStream("Tensorflow.SimpleGraph.pb"));
            });
        }

        [Test]
        public void WrongOutputType()
        {
            Assert.Throws<Exception>(() => {
                Graph.FromProtocolBuffer<WrongOutputType>(Data.ReadStream("Tensorflow.SimpleGraph.pb"));
            });
        }

        [Test]
        public void WrongInputShape()
        {
            Assert.Throws<Exception>(() => {
                Graph.FromProtocolBuffer<WrongInputShape>(Data.ReadStream("Tensorflow.SimpleGraph.pb"));
            });
        }

        [Test]
        public void UnmappedInput()
        {
            Assert.Throws<Exception>(() => {
                Graph.FromProtocolBuffer<UnmappedInput>(Data.ReadStream("Tensorflow.SimpleGraph.pb"));
            });
        }

    }

    internal class SimpleGraph : Graph
    {
        [Input("x")]
        public float[,,] X { get; set; }

        [Input("y")]
        public float[,,] Y { get; set; }

        [Output("z")]
        public float[,,] Z { get; private set; }

    }

    internal class WrongInputType : Graph
    {
        [Input("x")]
        public double[,,] X { get; set; }

        [Input("y")]
        public float[,,] Y { get; set; }

        [Output("z")]
        public float[,,] Z { get; private set; }

    }

    internal class WrongOutputType : Graph
    {
        [Input("x")]
        public float[,,] X { get; set; }

        [Input("y")]
        public float[,,] Y { get; set; }

        [Output("z")]
        public int[,,] Z { get; private set; }

    }

    internal class WrongInputShape : Graph
    {
        [Input("x")]
        public float[,,] X { get; set; }

        [Input("y")]
        public float[,] Y { get; set; }

        [Output("z")]
        public float[,,] Z { get; private set; }

    }

    internal class UnmappedInput : Graph
    {
        [Input("x")]
        public float[,,] X { get; set; }

        [Output("z")]
        public float[,,] Z { get; private set; }

    }

}
