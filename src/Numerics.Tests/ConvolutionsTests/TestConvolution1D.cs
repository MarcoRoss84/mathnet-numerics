// <copyright file="TestConvolution1D.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2019 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using System;
using NUnit.Framework;
using MathNet.Numerics.Convolutions;
using System.Numerics;

namespace MathNet.Numerics.Tests.ConvolutionsTests
{

    [TestFixture, Category("Convolution")]
    public class TestConvolution1D
    {

        
        [Test]
        public void Conv1DValidOutArraySingle()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };
            var actual = new float[expected.Length];

            Convolution.Conv1D(h, x, actual);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidReturnArraySingle()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

            var actual = Convolution.Conv1D(h, x);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameOddKernelSingle()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f, -4.4f };

            var actual = Convolution.Conv1D(h, x, Padding.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameEvenKernelSingle()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f, 0.8f };
            var expected = new float[] { -1.9f, 23.5f, -11.1f, -8.2f, 16.2f, 7.3f, -0.2f, -1.4f, -5.2f, 3.1f };
            var actual = new float[expected.Length];

            Convolution.Conv1D(h, x, actual, Padding.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DCausalSingle()
        {
            var x = new float[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new float[] { 0.5f, -1f, 0.3f };
            var expected = new float[] { 6f, -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

            var actual = Convolution.Conv1D(h, x, Padding.Causal);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidOutArrayDouble()
        {
            var x = new double[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new double[] { 0.5f, -1f, 0.3f };
            var expected = new double[] { -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };
            var actual = new double[expected.Length];

            Convolution.Conv1D(h, x, actual);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidReturnArrayDouble()
        {
            var x = new double[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new double[] { 0.5f, -1f, 0.3f };
            var expected = new double[] { -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

            var actual = Convolution.Conv1D(h, x);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameOddKernelDouble()
        {
            var x = new double[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new double[] { 0.5f, -1f, 0.3f };
            var expected = new double[] { -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f, -4.4f };

            var actual = Convolution.Conv1D(h, x, Padding.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameEvenKernelDouble()
        {
            var x = new double[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new double[] { 0.5f, -1f, 0.3f, 0.8f };
            var expected = new double[] { -1.9f, 23.5f, -11.1f, -8.2f, 16.2f, 7.3f, -0.2f, -1.4f, -5.2f, 3.1f };
            var actual = new double[expected.Length];

            Convolution.Conv1D(h, x, actual, Padding.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DCausalDouble()
        {
            var x = new double[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
            var h = new double[] { 0.5f, -1f, 0.3f };
            var expected = new double[] { 6f, -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

            var actual = Convolution.Conv1D(h, x, Padding.Causal);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidOutArrayComplex32()
        {
            var x = Generate.Map2<float, float, Complex32>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2},
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1},
                (a, b) => new Complex32(a, b)
                );
            var h = Generate.Map2<float, float, Complex32>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex32(a, b)
                );
            var expected = Generate.Map2<float, float, Complex32>(
                new float[] { 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex32(a, b)
                );
            var actual = new Complex32[expected.Length];

            Convolution.Conv1D(h, x, actual);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidReturnArrayComplex32()
        {
            var x = Generate.Map2<float, float, Complex32>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex32(a, b)
                );
            var h = Generate.Map2<float, float, Complex32>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex32(a, b)
                );
            var expected = Generate.Map2<float, float, Complex32>(
                new float[] { 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex32(a, b)
                );

            var actual = Convolution.Conv1D(h, x);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameOddKernelComplex32()
        {
            var x = Generate.Map2<float, float, Complex32>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex32(a, b)
                );
            var h = Generate.Map2<float, float, Complex32>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex32(a, b)
                );
            var expected = Generate.Map2<float, float, Complex32>(
                new float[] { 6.5f, 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f, 8.4f },
                new float[] { 1f, -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f, 4.5f },
                (a, b) => new Complex32(a, b)
                );

            var actual = Convolution.Conv1D(h, x, Padding.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameEvenKernelComplex32()
        {
            var x = Generate.Map2<float, float, Complex32>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex32(a, b)
                );
            var h = Generate.Map2<float, float, Complex32>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex32(a, b)
                );
            var expected = Generate.Map2<float, float, Complex32>(
                new float[] { 6.5f, 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f, 8.4f },
                new float[] { 1f, -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f, 4.5f },
                (a, b) => new Complex32(a, b)
                );
            var actual = new Complex32[expected.Length];

            Convolution.Conv1D(h, x, actual, Padding.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DCausalComplex32()
        {
            var x = Generate.Map2<float, float, Complex32>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex32(a, b)
                );
            var h = Generate.Map2<float, float, Complex32>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex32(a, b)
                );
            var expected = Generate.Map2<float, float, Complex32>(
                new float[] { -5.4f, 6.5f, 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { 0.3f, 1f, -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex32(a, b)
                );
            var actual = Convolution.Conv1D(h, x, Padding.Causal);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidOutArrayComplex()
        {
            var x = Generate.Map2<float, float, Complex>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex(a, b)
                );
            var h = Generate.Map2<float, float, Complex>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex(a, b)
                );
            var expected = Generate.Map2<float, float, Complex>(
                new float[] { 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex(a, b)
                );
            var actual = new Complex[expected.Length];

            Convolution.Conv1D(h, x, actual);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DValidReturnArrayComplex()
        {
            var x = Generate.Map2<float, float, Complex>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex(a, b)
                );
            var h = Generate.Map2<float, float, Complex>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex(a, b)
                );
            var expected = Generate.Map2<float, float, Complex>(
                new float[] { 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex(a, b)
                );

            var actual = Convolution.Conv1D(h, x);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameOddKernelComplex()
        {
            var x = Generate.Map2<float, float, Complex>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex(a, b)
                );
            var h = Generate.Map2<float, float, Complex>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex(a, b)
                );
            var expected = Generate.Map2<float, float, Complex>(
                new float[] { 6.5f, 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f, 8.4f },
                new float[] { 1f, -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f, 4.5f },
                (a, b) => new Complex(a, b)
                );

            var actual = Convolution.Conv1D(h, x, Padding.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DSameEvenKernelComplex()
        {
            var x = Generate.Map2<float, float, Complex>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex(a, b)
                );
            var h = Generate.Map2<float, float, Complex>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex(a, b)
                );
            var expected = Generate.Map2<float, float, Complex>(
                new float[] { 6.5f, 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f, 8.4f },
                new float[] { 1f, -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f, 4.5f },
                (a, b) => new Complex(a, b)
                );
            var actual = new Complex[expected.Length];

            Convolution.Conv1D(h, x, actual, Padding.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv1DCausalComplex()
        {
            var x = Generate.Map2<float, float, Complex>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex(a, b)
                );
            var h = Generate.Map2<float, float, Complex>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex(a, b)
                );
            var expected = Generate.Map2<float, float, Complex>(
                new float[] { -5.4f, 6.5f, 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { 0.3f, 1f, -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex(a, b)
                );
            var actual = Convolution.Conv1D(h, x, Padding.Causal);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        public float[] Real(Complex32[] c)
        {
            return Generate.Map(c, v => v.Real);
        }

        public float[] Imag(Complex32[] c)
        {
            return Generate.Map(c, v => v.Imaginary);
        }

        public double[] Real(Complex[] c)
        {
            return Generate.Map(c, v => v.Real);
        }

        public double[] Imag(Complex[] c)
        {
            return Generate.Map(c, v => v.Imaginary);
        }

    }
}
