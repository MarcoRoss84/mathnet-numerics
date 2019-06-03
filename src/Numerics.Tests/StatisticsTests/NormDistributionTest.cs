using NUnit.Framework;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;

namespace MathNet.Numerics.Tests.StatisticsTests
{
    [TestFixture, Category("Statistics")]
    public class NormDistributionTest
    {

        [Test]
        public void TestStandardNormPdf()
        {

            var expected = new double[] { 0.0009, 0.3989, 0.3814, 0.0000 };
            var result = new[] { -3.5, 0, 0.3, 5 }.NormPDF();
            Assert.That(result, Is.EqualTo(expected).Within(0.00005));

        }

        [Test]
        public void TestGeneralNormPdf()
        {

            var expected = new float[] { 0.0180F, 0.0940F, 0.1016F, 0.0940F };
            var result = new[] { -3.5F, 0, 0.3F, 5 }.NormPDF(mu: 2.5F, sigma: 3);
            Assert.That(result, Is.EqualTo(expected).Within(0.00005F));

        }

        [Test]
        public void TestStandardNormCdf()
        {
            var expected = new double[] { 0.0002, 0.5, 0.6179, 1 };
            var result = new[] { -3.5, 0, 0.3, 5 }.NormCDF();
            Assert.That(result, Is.EqualTo(expected).Within(0.00005));
        }

        [Test]
        public void TestGeneralNormCdf()
        {
            var expected = new float[] { 0.0228F, 0.2023F, 0.2317F, 0.7977F };
            var result = new[] { -3.5F, 0, 0.3F, 5 }.NormCDF(mu: 2.5F, sigma: 3);
            Assert.That(result, Is.EqualTo(expected).Within(0.00005F));
        }

        [Test]
        public void TestErfSingle()
        {
            var expected = new float[] { -1, 0, 0.3286F, 1 };
            var result = new[] { -3.5F, 0, 0.3F, 5}.Erf();
            Assert.That(result, Is.EqualTo(expected).Within(0.00005F));
        }

        [Test]
        public void TestErfcSingle()
        {
            var expected = new float[] { 2, 1, 0.6714F, 0 };
            var result = new[] { -3.5F, 0, 0.3F, 5 }.Erfc();
            Assert.That(result, Is.EqualTo(expected).Within(0.00005F));
        }

        [Test]
        public void TestErfDouble()
        {
            var expected = new double[] { -1, 0, 0.3286, 1 };
            var result = new[] { -3.5, 0, 0.3, 5 }.Erf();
            Assert.That(result, Is.EqualTo(expected).Within(0.00005));
        }

        [Test]
        public void TestErfcDouble()
        {
            var expected = new double[] { 2, 1, 0.6714, 0 };
            var result = new[] { -3.5, 0, 0.3, 5 }.Erfc();
            Assert.That(result, Is.EqualTo(expected).Within(0.00005));
        }

    }
}
