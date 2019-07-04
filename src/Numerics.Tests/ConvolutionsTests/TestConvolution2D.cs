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

    }
}
