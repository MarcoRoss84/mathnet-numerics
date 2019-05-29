using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Logical;
using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;

namespace MathNet.Numerics.Tests.LogicalTests
{
    [TestFixture, Category("Logicals")]
    public class LogicalTest
    {

        [Test]
        public void ElementWiseAnd()
        {

            var logical1 = new LogicalVector(new[] { true, true, false, false });
            var logical2 = new LogicalVector(new[] { true, false, true, false });

            var result = logical1 & logical2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, false, false, false })));

        }

        [Test]
        public void ElementWiseOr()
        {

            var logical1 = new LogicalVector(new[] { true, true, false, false });
            var logical2 = new LogicalVector(new[] { true, false, true, false });

            var result = logical1 | logical2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, true, true, false })));

        }

        [Test]
        public void ElementWiseXOr()
        {

            var logical1 = new LogicalVector(new[] { true, true, false, false });
            var logical2 = new LogicalVector(new[] { true, false, true, false });

            var result = logical1 ^ logical2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, true, true, false })));

        }

        [Test]
        public void ElementWiseNot()
        {

            var logical = new LogicalVector(new[] { true, true, false, false });
            
            var result = ~logical;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, false, true, true })));

        }

        [Test]
        public void IndexingIntoLogicals()
        {
            var logical = new LogicalVector(new[] { true, true, false, false });
            var result = logical[Indexer.FromRange(1, 3)];
            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, false })));
        }

        [Test]
        public void ElementwiseEqualsVector()
        {
            var v1 = DenseVector.OfArray(new double[] { 13, 15.6, 0.8, -3.1, 5.2 });
            var v2 = DenseVector.OfArray(new double[] { 12, 15.6, 0.9, -3.2, 5.2 });

            var result = Vector.op_ElementwiseEquals(v1, v2);

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, true, false, false, true })));
        }

        [Test]
        public void ElementwiseEqualsScalar1()
        {
            var v = DenseVector.OfArray(new double[] { 13, 15.6, 0.8, -3.1, 5.2 });
            
            var result = Vector.op_ElementwiseEquals(v, 0.8);

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, false, true, false, false })));
        }

        [Test]
        public void ElementwiseEqualsScalar2()
        {
            var v = DenseVector.OfArray(new double[] { 13, 15.6, 0.8, -3.1, 5.2 });

            var result = Vector.op_ElementwiseEquals(13, v);

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, false, false, false, false })));
        }

    }
}
