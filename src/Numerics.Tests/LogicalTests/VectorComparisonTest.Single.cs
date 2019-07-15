using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra.Logical;
using System;

namespace MathNet.Numerics.Tests.LogicalTests
{
    [TestFixture, Category("Logicals")]
    public class VectorComparisonTestSingle
    {
        [Test]
        public void VectorLessThanVector()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f, 18.1f });

            var result = v1 < v2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, false, true, false, true })));

        }

        [Test]
        public void VectorLessThanScalar()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            
            var result = v < 13;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, false, true, true, true })));

        }

        [Test]
        public void ScalarLessThanVector()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = 13 < v;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, true, false, false, false })));

        }

        [Test]
        public void LessThanVectorLengthInvalid()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = v1 < v2;
            });
        }

        [Test]
        public void VectorLessThanOrEqualToVector()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f, 18.1f });

            var result = v1 <= v2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, true, true, false, true })));

        }

        [Test]
        public void VectorLessThanOrEqualToScalar()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = v <= 13;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, false, true, true, true })));

        }

        [Test]
        public void ScalarLessThanOrEqualToVector()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = 13 <= v;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, true, false, false, false })));

        }

        [Test]
        public void LessThanOrEqualToVectorLengthInvalid()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = v1 <= v2;
            });
        }

        [Test]
        public void VectorGreaterThanVector()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f, 18.1f });

            var result = v1 > v2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, false, false, true, false })));

        }

        [Test]
        public void VectorGreaterThanScalar()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = v > 13;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, true, false, false, false })));

        }

        [Test]
        public void ScalarGreaterThanVector()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = 13 > v;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { false, false, true, true, true })));

        }

        [Test]
        public void GreaterThanVectorLengthInvalid()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = v1 > v2;
            });
        }

        [Test]
        public void VectorGreaterThanOrEqualToVector()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f, 18.1f });

            var result = v1 >= v2;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, true, false, true, false })));

        }

        [Test]
        public void VectorGreaterThanOrEqualToScalar()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = v >= 13;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, true, false, false, false })));

        }

        [Test]
        public void ScalarGreaterThanOrEqualToVector()
        {
            var v = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });

            var result = 13 >= v;

            Assert.That(result, Is.EqualTo(new LogicalVector(new[] { true, false, true, true, true })));

        }

        [Test]
        public void GreaterThanOrEqualToVectorLengthInvalid()
        {
            var v1 = DenseVector.OfArray(new float[] { 13f, 15.6f, 0.8f, -3.1f, 5.2f });
            var v2 = DenseVector.OfArray(new float[] { 12f, 15.6f, 0.9f, -3.2f });

            Assert.Throws<ArgumentException>(() =>
            {
                var result = v1 >= v2;
            });
        }

    }
}
