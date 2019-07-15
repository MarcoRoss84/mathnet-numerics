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

using MathNet.Numerics.Convolutions;
using NUnit.Framework;
using System;
using System.Numerics;

namespace MathNet.Numerics.Tests.ConvolutionsTests
{
    [TestFixture, Category("Convolution")]
    public class TestConvolution2D
    {

        [Test]
        public void Conv2DValidOutArraySingle()
        {
            var x = new float[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new float[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new float[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = new float[3, 2];

            Convolution.Conv2D(h, x, actual);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidReturnArraySingle()
        {
            var x = new float[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new float[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new float[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = Convolution.Conv2D(h, x);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameOutArraySingle()
        {
            var x = new float[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new float[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new float[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = new float[5, 4];

            Convolution.Conv2D(h, x, actual, Padding2D.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameReturnArraySingle()
        {
            var x = new float[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new float[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new float[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = Convolution.Conv2D(h, x, Padding2D.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidOutArrayDouble()
        {
            var x = new double[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new double[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new double[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = new double[3, 2];

            Convolution.Conv2D(h, x, actual);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidReturnArrayDouble()
        {
            var x = new double[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new double[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new double[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = Convolution.Conv2D(h, x);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameOutArrayDouble()
        {
            var x = new double[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new double[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new double[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = new double[5, 4];

            Convolution.Conv2D(h, x, actual, Padding2D.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameReturnArrayDouble()
        {
            var x = new double[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new double[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new double[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = Convolution.Conv2D(h, x, Padding2D.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidOutArrayComplex32()
        {
            var x = new Complex32[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex32[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new Complex32[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = new Complex32[3, 2];

            Convolution.Conv2D(h, x, actual);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidReturnArrayComplex32()
        {
            var x = new Complex32[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex32[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new Complex32[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = Convolution.Conv2D(h, x);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameOutArrayComplex32()
        {
            var x = new Complex32[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex32[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new Complex32[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = new Complex32[5, 4];

            Convolution.Conv2D(h, x, actual, Padding2D.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameReturnArrayComplex32()
        {
            var x = new Complex32[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex32[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new Complex32[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = Convolution.Conv2D(h, x, Padding2D.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidOutArrayComplex()
        {
            var x = new Complex[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new Complex[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = new Complex[3, 2];

            Convolution.Conv2D(h, x, actual);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DValidReturnArrayComplex()
        {
            var x = new Complex[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f }
            };
            var expected = new Complex[,] {
                { 4.8f, 5.1f },
                { 0.7f, 2.8f },
                { 3.0f, 2.9f }
            };
            var actual = Convolution.Conv2D(h, x);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameOutArrayComplex()
        {
            var x = new Complex[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new Complex[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = new Complex[5, 4];

            Convolution.Conv2D(h, x, actual, Padding2D.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void Conv2DSameReturnArrayComplex()
        {
            var x = new Complex[,] {
                { 6, 1, 0, 0 },
                { 7, 7, 8, 4 },
                { 7, 0, 6, 3 },
                { 3, 2, 3, 7 },
                { 6, 0, 9, 7 }
            };
            var h = new Complex[,] {
                {  0.2f,  0.2f, -0.4f },
                {  0.3f,  0.2f,  0.0f },
                { -0.2f, -0.4f,  0.5f },
                { -0.1f,  0.6f,  0.3f }
            };
            var expected = new Complex[,] {
                {  2.3f,  4.8f,  5.1f, -1.0f },
                {  1.7f,  3.1f,  3.1f,  3.2f },
                {  3.1f,  8.5f,  9.4f,  5.8f },
                {  3.8f,  4.3f,  5.6f,  3.7f },
                { -0.8f,  3.0f, -3.3f,  6.8f }
            };
            var actual = Convolution.Conv2D(h, x, Padding2D.Same);

            Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
            Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
        }

        [Test]
        public void MiniExampleSame()
        {
            var x = new float[,] {
                { 1, 2 },
                { 4, 8 }
            };

            var h = new float[,]
            {
                { 1, 1 },
                { 1, 1 }
            };

            var expected = new float[,]
            {
                { 15, 10 },
                { 12,  8 }
            };

            var actual = Convolution.Conv2D(h, x, Padding2D.Same);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void MiniExampleValid()
        {
            var x = new float[,] {
                { 1, 2 },
                { 4, 8 }
            };

            var h = new float[,]
            {
                { 1, 1 },
                { 1, 1 }
            };

            var expected = new float[,]
            {
                { 15 }
            };

            var actual = Convolution.Conv2D(h, x);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        private double[,] Imag(Complex[,] actual)
        {
            return Convert2D(actual, v => v.Imaginary);
        }

        private double[,] Real(Complex[,] actual)
        {
            return Convert2D(actual, v => v.Real);
        }

        private float[,] Imag(Complex32[,] actual)
        {
            return Convert2D(actual, v => v.Imaginary);
        }

        private float[,] Real(Complex32[,] actual)
        {
            return Convert2D(actual, v => v.Real);
        }

        private TOut[,] Convert2D<TIn, TOut>(TIn[,] input, Func<TIn, TOut> map)
        {
            var rows = input.GetLength(0);
            var cols = input.GetLength(1);
            var result = new TOut[rows, cols];
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    result[row, col] = map(input[row, col]);
                }
            }
            return result;
        }


    }
}
