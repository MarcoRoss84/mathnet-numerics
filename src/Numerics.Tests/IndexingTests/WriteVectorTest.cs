using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra;
using System;

namespace MathNet.Numerics.Tests.IndexingTests
{
    [TestFixture, Category("Indexing")]
    public class WriteVectorTest
    {

        [Test]
        public void SetSimpleIntegerIndex()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[3] = -8;
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, 2f, -8f, 4f, 5f })));
        }

        [Test]
        public void SetIntegerEnumerableIndex()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[new[] { 1, 5, 0 }] = DenseVector.OfArray(new[] { -1f, -5f, -10f }); ;
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { -10f, -1f, 2f, 3f, 4f, -5f })));
        }

        [Test]
        public void SetIntegerEnumerableIndexVectorWithOneElement()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[new[] { 1, 5, 0 }] = DenseVector.OfArray(new[] { -1f});
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { -1f, -1f, 2f, 3f, 4f, -1f })));
        }

        [Test]
        public void SetIntegerEnumerableIndexScalar()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[new[] { 1, 5, 0 }] = -1;
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { -1f, -1f, 2f, 3f, 4f, -1f })));
        }

        [Test]
        public void SetIntegerEnumerableIndexWrongNumberOfElements()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                v[new[] { 1, 5, 0 }] = DenseVector.OfArray(new[] { -1f, -5f, -10f, 8f }); ;
            });
        }

        [Test]
        public void SetLogicalIndex()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[new[] { false, false, true, true, false, true }] = new float[] { -2, -3, -5 };
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, -2f, -3f, 4f, -5f })));
        }

        [Test]
        public void SetLogicalIndexVectorWithOneElement()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[new[] { false, false, true, true, false, true }] = DenseVector.OfArray(new float[] { 10 });
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, 10f, 10f, 4f, 10f })));
        }

        [Test]
        public void SetLogicalIndexVectorScalar()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[new[] { false, false, true, true, false, true }] = 10;
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, 10f, 10f, 4f, 10f })));
        }

        [Test]
        public void SetLogicalIndexWrongNumberOfElements()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var r = v[new[] { false, false, true, true, false, true }] = new float[] { -2, -3 };
            });
        }

        [Test]
        public void SetLogicalIndexWrongNumberOfIndices()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var r = v[new[] { false, false, true, true, false }] = new float[] { -2, -3 };
            });
        }

        [Test]
        public void SetRange()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[Indexer.FromRange(2, 4)] = DenseVector.OfArray(new[] { -2f, -3f });
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, -2f, -3f, 4f, 5f })));
        }

        [Test]
        public void SetRangeVectorOfLength1()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[Indexer.FromRange(2, 4)] = DenseVector.OfArray(new[] { 8f });
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, 8f, 8f, 4f, 5f })));
        }

        [Test]
        public void SetRangeScalar()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            v[Indexer.FromRange(2, 4)] = 8;
            Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, 8f, 8f, 4f, 5f })));
        }

        [Test]
        public void SetRangeInvalidLength()
        {
            var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                v[Indexer.FromRange(2, 5)] = new[] { -2f, -3f };
            });
        }
    }
}
