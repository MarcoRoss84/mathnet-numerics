// <copyright file="Vector.FancyIndexing.cs" company="Math.NET">
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

namespace MathNet.Numerics.LinearAlgebra
{
    public abstract partial class Vector<T>
    {

        /// <summary>
        /// Gets/sets a subvector of this vector using the given indexer.
        /// </summary>
        /// <param name="indexer">
        /// Indexer into the given vector
        /// </param>
        public Vector<T> this[Indexer indexer]
        {
            get
            {
                indexer.Init(Count);
                var target = Build.SameAs(this, indexer.Count);
                for (var i = 0; i < indexer.Count; i++)
                {
                    target[i] = this[indexer[i]];
                }
                return target;
            }
            set {
                indexer.Init(this.Count);
                if (value.Count != indexer.Count && value.Count != 1)
                {
                    throw new ArgumentOutOfRangeException("The value passed to a subarray must either be scalar or have the same number as elements as the indexed subarray");
                }
                if (value.Count == 1)
                {
                    var v = value[0];
                    foreach (int idx in indexer) this[idx] = v;
                }
                else
                {
                    for (int i = 0; i < indexer.Count; i++)
                    {
                        this[indexer[i]] = value[i];
                    }
                }
            }
        }

    }
}
