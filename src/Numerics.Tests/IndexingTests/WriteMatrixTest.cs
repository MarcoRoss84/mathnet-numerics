using System;
using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace MathNet.Numerics.Tests.IndexingTests
{
    [TestFixture, Category("Indexing")]
    public class WriteMatrixTest
    {
        [Test]
        public void SetRow()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[0, Indexer.All] = new float[] { 51, 52, 53 };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 51, 52, 53 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            })));
        }

        [Test]
        public void SetSubRow()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[0, new[] { false, true, true }] = new float[] { 52, 53 };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 52, 53 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            })));
        }

        [Test]
        public void SetSubRowVectorWithOneElement()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[0, new[] { false, true, true }] = new float[] { 52 };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 52, 52 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            })));
        }

        [Test]
        public void SetSubRowScalar()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[0, new[] { false, true, true }] = 52;
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 52, 52 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            })));
        }

        [Test]
        public void SetSubRowInvalidLength()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                m[0, new[] { false, true, true }] = new float[] { 52, 53, 54 };
            });
        }

        [Test]
        public void SetSubColumn()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[new[] { 1, 3 }, 1] = new float[] { 24, 44 };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 12, 13 },
                { 21, 24, 23 },
                { 31, 32, 33 },
                { 41, 44, 43 }
            })));
        }

        [Test]
        public void SetSubColumnVectorWithOneElement()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[new[] { 1, 3 }, 1] = new float[] { 24 };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 12, 13 },
                { 21, 24, 23 },
                { 31, 32, 33 },
                { 41, 24, 43 }
            })));
        }

        [Test]
        public void SetColumnScalar()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[Indexer.All, 1] = 55;
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 55, 13 },
                { 21, 55, 23 },
                { 31, 55, 33 },
                { 41, 55, 43 }
            })));
        }

        [Test]
        public void SetColumnInvalidLength()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                m[Indexer.All, 1] = new float[] { 52, 53, 54 };
            });
        }

        [Test]
        public void SetSubMatrix()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[new[] { 1, 3 }, Indexer.FromRange(2,3)] = new float[,] {
                { 24 },
                { 44 }
            };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 12, 13 },
                { 21, 22, 24 },
                { 31, 32, 33 },
                { 41, 42, 44 }
            })));
        }

        [Test]
        public void SetSubMatrixMatrixWithOneElement()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[new[] { 1, 3 }, Indexer.FromRange(1, 3)] = new float[,] {
                { 55 }
            };
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 12, 13 },
                { 21, 55, 55 },
                { 31, 32, 33 },
                { 41, 55, 55 }
            })));
        }

        [Test]
        public void SetSubMatrixScalar()
        {
            var m = DenseMatrix.OfArray(new float[,] {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 },
                { 41, 42, 43 }
            });
            m[new[] { 1, 3 }, Indexer.FromRange(1, 3)] = 55;
            Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,]
            {
                { 11, 12, 13 },
                { 21, 55, 55 },
                { 31, 32, 33 },
                { 41, 55, 55 }
            })));
        }
    }
}
