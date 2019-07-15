// <copyright file="DenseVector.cs" company="Math.NET">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MathNet.Numerics.LinearAlgebra.Logical
{
    public partial class LogicalVector : IEnumerable<bool>
    {

        private bool[] _storage;

        public LogicalVector(bool[] storage)
        {
            _storage = storage;
        }

        public LogicalVector(int n)
        {
            _storage = new bool[n];
        }

        public int Count => _storage.Length;

        public bool this[int idx]
        {
            get { return _storage[idx]; }
            set { _storage[idx] = value; }
        }

        public LogicalVector this[Indexer indexer]
        {
            get
            {
                indexer.Init(Count);
                var result = new LogicalVector(indexer.Count);
                for (int i = 0; i < indexer.Count; i++)
                {
                    result[i] = this[indexer[i]];
                }
                return result;
            }
            set
            {
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

        public IEnumerator<bool> GetEnumerator()
        {
            return _storage.Cast<bool>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            var other = obj as LogicalVector;
            if (other == null) return false;
            return ElementwiseBinaryLogical(this, other, (v1, v2) => v1 == v2).All;
        }

        public override int GetHashCode()
        {
            return "LogicalVector".GetHashCode() ^ _storage.GetHashCode();
        }

        public static implicit operator bool[] (LogicalVector lv)
        {
            return lv._storage;
        }

        public static implicit operator LogicalVector(bool[] array)
        {
            return new LogicalVector(array);
        }

        public static implicit operator LogicalVector(bool v)
        {
            return new LogicalVector(new[] { v });
        }

        private static LogicalVector ElementwiseBinaryLogical(LogicalVector v1, LogicalVector v2, Func<bool, bool, bool> op)
        {
            var n = v1.Count;
            if (v2.Count != n) throw new ArgumentException("Both logical vectors must have the same length");
            var target = new bool[n];
            var s1 = v1._storage;
            var s2 = v2._storage;
            for (int i = 0; i < n; i++)
            {
                target[i] = op(s1[i], s2[i]);
            }
            return target;
        }

        public static LogicalVector operator &(LogicalVector v1, LogicalVector v2)
        {
            return ElementwiseBinaryLogical(v1, v2, (b1, b2) => b1 && b2);
        }

        public static LogicalVector operator  |(LogicalVector v1, LogicalVector v2)
        {
            return ElementwiseBinaryLogical(v1, v2, (b1, b2) => b1 || b2);
        }

        public static LogicalVector operator ^(LogicalVector v1, LogicalVector v2)
        {
            return ElementwiseBinaryLogical(v1, v2, (b1, b2) => b1 != b2);
        }

        public static LogicalVector operator ~(LogicalVector v)
        {
            var n = v.Count;
            var target = new bool[n];
            for (int i = 0; i < n; i++)
            {
                target[i] = !v[i];
            }
            return target;
        }

        public bool All
        {
            get { return _storage.All(b => b); }
        }

        public bool Any
        {
            get { return _storage.Any(b => b); }
        }

    }
}
