### Adds advanced indexing to vectors and matrices:

Additionally to the conventional indexing of single elements, advanced indexers allow for indexing a whole range of elements in a vector or a range of rows/columns in matrices:

- Indexing all elements in a vector or all rows/columns in a matrix with `Indexer.All`
- Indexing a regularly sampled ranged of elements from a vector or rows/columns in a matrix with `Indexer.FromRange(startIncl, endExcl, step)`
- Indexing an arbitrary selection of elements in a vector or rows/columns in a matrix in any order with `Indexer.FromIndices(IEnumerable<int> indices)`. 
_Note: There is also an implicit conversion from an int[] to this indexer_
- Indexing an arbitrary selection of elements in a vector or rows/columns in a matrix with booleans `Indexer.FromLogical(IEnumerable<bool> indices)`.
_Note: There is also an implicit conversion from a bool[] to this inexer_

In order to improve readability of source code, implicit conversions have been defined for converting scalars and native one- or two-dimensional arrays to vectors and matrices.

#### Examples
Getting elements from a vector:
```C#
var v = DenseVector.OfArray(new[] { 0f, 2f, 4f, 6f, 8f, 10f });
// select the elements and indices 1, 5, and 0 in that order
var r = v[new [] { 1, 5, 0 }];
// r contains [2f, 10f, 0f]
Assert.That(r, Is.EqualTo(DenseVector.OfArray(new [] { 2f, 10f, 0f })));
```
Setting elements in a vector:
```C#
var v = DenseVector.OfArray(new[] { 0f, 1f, 2f, 3f, 4f, 5f });
// make use of the range indexer and the implicit conversion of scalars to vectors
v[Indexer.FromRange(2, 4)] = 8; 
// elements of v have been updated
Assert.That(v, Is.EqualTo(DenseVector.OfArray(new[] { 0f, 1f, 8f, 8f, 4f, 5f }))); 
```
Setting an entire row in a matrix:
```C#
var m = DenseMatrix.OfArray(new float[,] {
    { 11, 12, 13 },
    { 21, 22, 23 },
    { 31, 32, 33 },
    { 41, 42, 43 }
});
// replace the entire first row, make use of the implicit conversion of
// arrays to vectors
m[0, Indexer.All] = new float[] { 51, 52, 53 };
// The matrix has been updated accordingly
Assert.That(m, Is.EqualTo(DenseMatrix.OfArray(new float[,] {
    { 51, 52, 53 },
    { 21, 22, 23 },
    { 31, 32, 33 },
    { 41, 42, 43 }
})));
```