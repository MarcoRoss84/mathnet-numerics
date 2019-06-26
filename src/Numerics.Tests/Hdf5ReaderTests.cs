#if NET40 || NET461

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MathNet.Numerics.TestData;


namespace MathNet.Numerics.Tests
{
    [TestFixture]
    public class Hdf5ReaderTests
    {
        [Test]
        public void ReadHdf5InfoTest()
        {
            var rand = new System.Random(0);
            var values = new[] { 726.243, 768.023, 206.033, 906.027, 977.55, 291.906, 632.659, 982.151, 862.37, 677.181 };
            var values2 = new[] { 81.733f, 55.816f, 55.888f, 44.218f, 27.37f, 46.731f, 46.951f, 3.037f, 99.535f, 31.459f };

            using(var tmpFile = new TempFile("hdf.testData.h5"))
            {
                //var reader = new Hdf5Reader(@"C:\Users\310154330\Desktop\TestData\testData.h5");
                var reader = new Hdf5Reader(tmpFile.FileName);
                reader.readHdf5("/data/test1", out double[] data);
                Assert.That(data, Is.EquivalentTo(values));

                reader.readHdf5("/data/test2", out float[] data2);
                Assert.That(data2, Is.EquivalentTo(values2));
            }      

        }
    }
}

#endif
