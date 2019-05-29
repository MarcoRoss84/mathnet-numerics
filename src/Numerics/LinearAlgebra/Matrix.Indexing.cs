using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathNet.Numerics.LinearAlgebra
{
    public abstract partial class Matrix<T>
    {

        public Vector<T> this[int row, Indexer columns]
        {
            get {
                columns.Init(ColumnCount);
                var target = Vector<T>.Build.Dense(columns.Count);
                for (var i = 0; i < columns.Count; i++)
                {
                    target[i] = this[row, columns[i]];
                }
                return target;
            }
            set
            {
                columns.Init(ColumnCount);
                if (value.Count != 1 && value.Count != columns.Count)
                {
                    throw new ArgumentOutOfRangeException("The number of elements must match the number of indexed columns");
                }
                if (value.Count == 1)
                {
                    var v = value[0];
                    foreach (var column in columns)
                    {
                        this[row, column] = v;
                    }
                }
                else
                {
                    for (var i = 0; i < columns.Count; i++)
                    {
                        this[row, columns[i]] = value[i];
                    }
                }
                
            }
        }

        public Vector<T> this[Indexer rows, int column]
        {
            get
            {
                rows.Init(RowCount);
                var target = Vector<T>.Build.Dense(rows.Count);
                for (var i = 0; i < rows.Count; i++)
                {
                    target[i] = this[rows[i], column];
                }
                return target;
            }
            set
            {
                rows.Init(RowCount);
                if (value.Count != 1 && value.Count != rows.Count)
                {
                    throw new ArgumentOutOfRangeException("The number of elements must match the number of indexed rows");
                }
                if (value.Count == 1)
                {
                    var v = value[0];
                    foreach (var row in rows)
                    {
                        this[row, column] = v;
                    }
                }
                else
                {
                    for (var i = 0; i < rows.Count; i++)
                    {
                        this[rows[i], column] = value[i];
                    }
                }

            }
        }

        public Matrix<T> this[Indexer rows, Indexer columns]
        {
            get
            {
                rows.Init(RowCount);
                columns.Init(ColumnCount);
                var target = Build.Dense(rows.Count, columns.Count);
                for (var col = 0; col < columns.Count; col++)
                {
                    for (var row = 0; row < rows.Count; row++)
                    {
                        target[row, col] = this[rows[row], columns[col]];
                    }
                }
                return target;
            }
            set
            {
                rows.Init(RowCount);
                columns.Init(ColumnCount);
                if (value.ColumnCount != 1 && value.ColumnCount != columns.Count)
                {
                    throw new ArgumentOutOfRangeException("The number of elements must match the number of indexed columns");
                }
                if (value.RowCount != 1 && value.RowCount != rows.Count)
                {
                    throw new ArgumentOutOfRangeException("The number of elements must match the number of indexed rows");
                }
                if (value.RowCount == 1 && value.ColumnCount == 1)
                {
                    var v = value[0, 0];
                    foreach (var row in rows)
                    {
                        foreach (var column in columns)

                        {
                            this[row, column] = v;
                        }
                    }
                }
                else
                {
                    for (var ci = 0; ci < columns.Count; ci++)
                    {
                        for (var ri = 0; ri < rows.Count; ri++)
                        {
                            this[rows[ri], columns[ci]] = value[ri, ci];
                        }
                    }
                }

            }
        }

    }
}
