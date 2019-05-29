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

using System;
using System.Collections.Generic;
using System.Linq;

namespace MathNet.Numerics
{

    /// <summary>
    /// This class provides methods to find locations of samples in
    /// an enumerable satisfying certian conditions
    /// </summary>
    public static class Find
    {

        /// <summary>
        /// Find all elements of the given enumerable where the conditional
        /// value is true.
        /// </summary>
        /// <param name="booleans">Enumerable of booleans</param>
        /// <returns>Indices of elements set to true</returns>
        public static IEnumerable<int> All(IEnumerable<bool> booleans)
        {
            var idx = 0;
            foreach (var b in booleans)
            {
                if (b) yield return idx;
                idx++;
            }
        }

        /// <summary>
        /// Find the first element of the given enumerable where the conditional
        /// value is true, if any.
        /// </summary>
        /// <param name="booleans">Enumerable of booleans</param>
        /// <returns>Index of the first element set to true, or null if there isn't any.</returns>
        public static int? First(IEnumerable<bool> booleans)
        {
            var idx = 0;
            foreach (var b in booleans)
            {
                if (b) return idx;
                idx++;
            }
            return null;
        }

        /// <summary>
        /// Find the first n elements of the given enumerable where the conditional
        /// value is true, if any. Note that the resulting enumerable may contain
        /// less than n elements.
        /// </summary>
        /// <param name="booleans">Enumerable of booleans</param>
        /// <param name="n">The maximum number of elements to find</param>
        /// <returns>Indices of the first n elements set to true</returns>
        public static IEnumerable<int> First(IEnumerable<bool> booleans, int n)
        {
            return All(booleans).Take(n);
        }

        /// <summary>
        /// Find the last element of the given enumerable where the conditional
        /// value is true, if any.
        /// </summary>
        /// <param name="booleans">Enumerable of booleans</param>
        /// <returns>Index of the last element set to true, or null if there isn't any.</returns>
        public static int? Last(IEnumerable<bool> booleans)
        {
            var list = booleans as IList<bool> ?? booleans.ToList();
            for (var idx = list.Count-1; idx >= 0; idx--)
            {
                if (list[idx]) return idx;
            }
            return null;
        }

        /// <summary>
        /// Find the last n elements of the given enumerable where the conditional
        /// value is true, if any. Note that the resulting enumerable may contain
        /// less than n elements.
        /// </summary>
        /// <param name="booleans">Enumerable of booleans</param>
        /// <param name="n">The maximum number of elements to find</param>
        /// <returns>Indices of the last n elements set to true</returns>
        public static IEnumerable<int> Last(IEnumerable<bool> booleans, int n)
        {
            var list = booleans as IList<bool> ?? booleans.ToList();
            var counter = 0;
            for (var idx = list.Count - 1; idx >= 0; idx--)
            {
                if (list[idx])
                {
                    yield return idx;
                    counter++;
                }
                if (counter == n) yield break;
            }
        }

        /// <summary>
        /// Find peaks (local maxima) in the input enumerable.
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="x">Stream of input values</param>
        /// <param name="minPeakHeight">Minimum height for local maxima to be detected (optional)</param>
        /// <returns>The positions of local maxima in the input sequence</returns>
        public static IEnumerable<int> Peaks<T>(IEnumerable<T> x, T? minPeakHeight = null) where T : struct, IComparable<T>, IEquatable<T>
        {
            return PeaksAndValues(x, (idx, v) => idx, minPeakHeight);
        }

        /// <summary>
        /// Find peaks (local maxima) in the input enumerable.
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="x">Stream of input values</param>
        /// <param name="minPeakHeight">Minimum height for local maxima to be detected (optional)</param>
        /// <returns>The positions and values of local maxima in the input sequence</returns>
        public static IEnumerable<Tuple<int,T>> PeaksAndValues<T>(IEnumerable<T> x, T? minPeakHeight = null) where T : struct, IComparable<T>, IEquatable<T>
        {
            return PeaksAndValues(x, (idx, v) => Tuple.Create(idx, v), minPeakHeight);
        }

        /// <summary>
        /// Find peaks (local maxima) in the input enumerable.
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="x">Stream of input values</param>
        /// <param name="minPeakDistance">Minimum distance between local maxima</param>
        /// <param name="minPeakHeight">Minimum height for local maxima to be detected (optional)</param>
        /// <returns>The positions of local maxima in the input sequence</returns>
        public static IEnumerable<int> Peaks<T>(IEnumerable<T> x, int minPeakDistance, T? minPeakHeight = null) where T : struct, IComparable<T>, IEquatable<T>
        {
            return PeaksAndValues(x, minPeakDistance, minPeakHeight)
                .Select(tuple => tuple.Item1);
        }

        /// <summary>
        /// Find peaks (local maxima) in the input enumerable.
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="x">Stream of input values</param>
        /// <param name="minPeakDistance">Minimum distance between local maxima</param>
        /// <param name="minPeakHeight">Minimum height for local maxima to be detected (optional)</param>
        /// <returns>The positions and values of local maxima in the input sequence</returns>
        public static IEnumerable<Tuple<int, T>> PeaksAndValues<T>(IEnumerable<T> x, int minPeakDistance, T? minPeakHeight = null) where T : struct, IComparable<T>, IEquatable<T>
        {
            // get peaks in descending peak height order
            var peaks = PeaksAndValues(x, (idx, v) => Tuple.Create(idx, v), minPeakHeight)
                .OrderByDescending(peak => peak.Item2)
                .ToList();
            // keep only the peaks where all the previous (higher) peaks are at least minPeakDistance away
            return peaks
                .Where((p, i) => peaks.GetRange(0, i).All(o => Math.Abs(o.Item1-p.Item1) >= minPeakDistance))
                .OrderBy(p => p.Item1);
        }


        private static IEnumerable<R> PeaksAndValues<T,R>(IEnumerable<T> x, Func<int,T,R> resultMap, T? minPeakHeight = null) where T : struct, IComparable<T>, IEquatable<T>
        {
            if (!minPeakHeight.HasValue)
            {
                var increasing = false;
                var idx = 0;
                var previous_x = default(T);
                foreach (var current_x in x)
                {
                    if (increasing && current_x.CompareTo(previous_x) < 0 && idx > 0)
                    {
                        increasing = false;
                        if (idx > 1) yield return resultMap(idx - 1, previous_x);
                    }
                    else if (current_x.CompareTo(previous_x) > 0)
                    {
                        increasing = true;
                    }
                    previous_x = current_x;
                    idx++;
                }
            }
            else
            {
                var increasing = false;
                var idx = 0;
                var previous_x = default(T);
                foreach (var current_x in x)
                {
                    if (increasing && current_x.CompareTo(previous_x) < 0 && idx > 0)
                    {
                        increasing = false;
                        if (previous_x.CompareTo(minPeakHeight.Value) >= 0 && idx > 1) yield return resultMap(idx - 1, previous_x);
                    }
                    else if (current_x.CompareTo(previous_x) > 0)
                    {
                        increasing = true;
                    }
                    previous_x = current_x;
                    idx++;
                }
            }
        }



    }
}
