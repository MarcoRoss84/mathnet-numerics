#if NET40 || NET461

using NUnit.Framework;
using MathNet.Numerics.TestData;


namespace MathNet.Numerics.Tests
{
    [TestFixture, Category("HDF5")]
    public class Hdf5ReaderTests
    {
        [Test]
        public void ReadHdf5Test()
        {
            var values = new[] { 726.243, 768.023, 206.033, 906.027, 977.55, 291.906, 632.659, 982.151, 862.37, 677.181 };
            var values2 = new[] { 81.733f, 55.816f, 55.888f, 44.218f, 27.37f, 46.731f, 46.951f, 3.037f, 99.535f, 31.459f };

            using(var tmpFile = new TempFile("hdf.testData.h5"))
            {
                var reader = new Hdf5Reader(tmpFile.FileName);
                Assert.IsTrue(reader.readHdf5("/data/test1", out double[] data));
                Assert.That(data, Is.EquivalentTo(values));

                Assert.IsTrue(reader.readHdf5("/data/test2", out float[] data2));
                Assert.That(data2, Is.EquivalentTo(values2));
            }      
        }

        [Test]
        public void ReadHdf5FailedTest()
        {
            using (var tmpFile = new TempFile("hdf.testData.h5"))
            {
                var reader = new Hdf5Reader(tmpFile.FileName);
                Assert.IsFalse(reader.readHdf5("/data/notExisting", out double[] data));
            }
        }
    }
}

#endif
