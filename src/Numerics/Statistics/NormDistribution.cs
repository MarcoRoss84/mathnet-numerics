// <copyright file="NormDistribution.cs" company="Math.NET">
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

namespace MathNet.Numerics.Statistics
{
    public static class NormDistribution
    {

        private const double CdfFactor = 0.707106781186548;

        /// <summary>
        /// Probability density function for a normal distribution
        /// </summary>
        /// <param name="x">sample where the probability density function shall be evaluated</param>
        /// <param name="mu">mean of the normal distribution (default: 0)</param>
        /// <param name="sigma">standard deviation of the normal distribution (default: 1)</param>
        /// <returns>The probability density function of the given normal distribution at 'x'</returns>
        public static double NormPDF(double x, double mu = 0, double sigma = 1)
        {
            var z = GetZ(x, mu, sigma);
            return 0.398942280401433 / sigma * Math.Exp(-0.5 * z * z);
        }

        /// <summary>
        /// Cumulative density function for a normal distribution
        /// </summary>
        /// <param name="x">sample where the cumulative density function shall be evaluated</param>
        /// <param name="mu">mean of the normal distribution (default: 0)</param>
        /// <param name="sigma">standard deviation of the normal distribution (default: 1)</param>
        /// <returns>The cumulative density function of the given normal distribution at 'x'</returns>
        public static double NormCDF(double x, double mu = 0, double sigma = 1)
        {
            var z = GetZ(x, mu, sigma);
            return Math.Max(double.Epsilon, 0.5 * SpecialFunctions.Erfc(-z * CdfFactor));
        }

        private static double GetZ(double x, double mu, double sigma)
        {
            return (x - mu) / sigma;
        }

    }
}
