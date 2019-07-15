// <copyright file="Euclid.cs" company="Math.NET">
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

using NUnit.Framework;
using MathNet.Numerics;
using System.Linq;
using System.Collections.Generic;
using System;

namespace MathNet.Numerics.Tests
{
    [TestFixture, Category("Find")]
    public class FindTest
    {

        [Test]
        public void FindAll()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.All(v);
            Assert.That(result, Is.EqualTo(new[] { 0, 1, 4, 6}));
        }

        [Test]
        public void FindFirst()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.First(v);
            Assert.That(result.HasValue);
            Assert.That(result.Value, Is.EqualTo(0));
        }

        [Test]
        public void FindFirstNoneAvailable()
        {
            var v = new bool[] {
                false, false, false, false
            };
            var result = Find.First(v);
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void FindFirstNMoreAvailable()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.First(v, 2);
            Assert.That(result, Is.EqualTo(new[] { 0, 1 }));
        }

        [Test]
        public void FindFirstNLessAvailable()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.First(v, 6);
            Assert.That(result, Is.EqualTo(new[] { 0, 1, 4, 6 }));
        }

        [Test]
        public void FindLast()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.Last(v);
            Assert.That(result.HasValue);
            Assert.That(result.Value, Is.EqualTo(6));
        }

        [Test]
        public void FindLastNoneAvailable()
        {
            var v = new bool[] {
                false, false, false, false
            };
            var result = Find.Last(v);
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void FindLastNMoreAvailable()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.Last(v, 2);
            Assert.That(result, Is.EqualTo(new[] { 6, 4 }));
        }

        [Test]
        public void FindLastNLessAvailable()
        {
            var v = new bool[] {
                true, true, false, false, true, false, true, false
            };
            var result = Find.Last(v, 6);
            Assert.That(result, Is.EqualTo(new[] { 6, 4, 1, 0 }));
        }

        [Test]
        public void FindPeaksEmptyInput()
        {
            var x = new float[0];
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksSingleValueInput()
        {
            var x = new[] { 3F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksPeakAtFirstSample()
        {
            var x = new[] { 1F, 0F, 0F, 0F, 0F, 0F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksPeakAtLastSample()
        {
            var x = new[] { 0F, 0F, 0F, 0F, 0F, 1F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksPeakAtMiddleSample()
        {
            var x = new[] { 0F, 0F, 0F, 1F, 0F, 0F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.EqualTo(new[] { 3 }));
        }

        [Test]
        public void FindPeaksTwoPeaksInMiddleSamples()
        {
            var x = new[] { 0F, 1F, 0F, 2F, 0F, 0F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.EqualTo(new[] { 1, 3 }));
        }

        [Test]
        public void FindPeaksPeaksAtFirstandLastSample()
        {
            var x = new[] { 3F, 1F, 0F, 0F, 0F, 1F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksPlateauInMiddleSamples()
        {
            var x = new[] { 0F, 0F, 1F, 1F, 1F, 0F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.EqualTo(new[] { 4 }));
        }

        [Test]
        public void FindPeaksPlateauInMiddleAndStepSamplesRight()
        {
            var x = new[] { 0F, 0F, 2F, 2F, 1F, 1F, 0F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.EqualTo(new[] { 3 }));
        }

        [Test]
        public void FindPeaksPlateauInMiddleAndStepSamplesLeft()
        {
            var x = new[] { 0F, 0F, 1F, 1F, 2F, 2F, 0F };
            var peaks = Find.Peaks(x);
            Assert.That(peaks, Is.EqualTo(new[] { 5 }));
        }

        [Test]
        public void FindPeaksMinPeakDistance()
        {
            var x = new[] { 0F, 1F, 0F, 2F, 0F, 2F, 0F, 3F, 0F, 1F, 2F, 1F };
            var peaks = Find.Peaks(x, minPeakDistance: 3);
            Assert.That(peaks, Is.EqualTo(new[] { 3, 7, 10 }));
        }

        [Test]
        public void FindPeaksMinPeakHeight()
        {
            var x = new[] { 0F, 0.9F, 0F, 2F, 0F, 1F, 0F };
            var peaks = Find.Peaks(x, minPeakHeight: 1);
            Assert.That(peaks, Is.EqualTo(new[] { 3, 5 }));
        }

        [Test]
        public void FindPeaksAndValuesEmptyInput()
        {
            var x = new float[0];
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksAndValuesSingleValueInput()
        {
            var x = new[] { 3F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksAndValuesPeakAtFirstSample()
        {
            var x = new[] { 1F, 0F, 0F, 0F, 0F, 0F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksAndValuesPeakAtLastSample()
        {
            var x = new[] { 0F, 0F, 0F, 0F, 0F, 1F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksAndValuesPeakAtMiddleSample()
        {
            var x = new[] { 0F, 0F, 0F, 1F, 0F, 0F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(3, 1f) }));
        }

        [Test]
        public void FindPeaksAndValuesTwoPeaksInMiddleSamples()
        {
            var x = new[] { 0F, 1F, 0F, 2F, 0F, 0F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(1, 1f), Tuple.Create(3, 2f) }));
        }

        [Test]
        public void FindPeaksAndValuesPeaksAtFirstandLastSample()
        {
            var x = new[] { 3F, 1F, 0F, 0F, 0F, 1F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.Empty);
        }

        [Test]
        public void FindPeaksAndValuesPlateauInMiddleSamples()
        {
            var x = new[] { 0F, 0F, 1F, 1F, 1F, 0F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(4, 1f) }));
        }

        [Test]
        public void FindPeaksAndValuesPlateauInMiddleAndStepSamplesRight()
        {
            var x = new[] { 0F, 0F, 2F, 2F, 1F, 1F, 0F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(3, 2f) }));
        }

        [Test]
        public void FindPeaksAndValuesPlateauInMiddleAndStepSamplesLeft()
        {
            var x = new[] { 0F, 0F, 1F, 1F, 2F, 2F, 0F };
            var peaks = Find.PeaksAndValues(x);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(5, 2f) }));
        }

        [Test]
        public void FindPeaksAndValuesMinPeakDistance()
        {
            var x = new[] { 0F, 1F, 0F, 2F, 0F, 2F, 0F, 3F, 0F, 1F, 2F, 1F };
            var peaks = Find.PeaksAndValues(x, minPeakDistance: 3);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(3, 2f), Tuple.Create(7, 3f), Tuple.Create(10, 2f) }));
        }

        [Test]
        public void FindPeaksAndValuesMinPeakHeight()
        {
            var x = new[] { 0F, 0.9F, 0F, 2F, 0F, 1F, 0F };
            var peaks = Find.PeaksAndValues(x, minPeakHeight: 1);
            Assert.That(peaks, Is.EqualTo(new[] { Tuple.Create(3, 2f), Tuple.Create(5, 1f) }));
        }
    }
}
