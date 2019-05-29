# Find

This class provides static functions to find positions (indices) in an enumerable where certain conditions are met.

## Find conditionals

The following methods return indices of elements in an enumerable of boolean values that are true: `Find.All(...)`,  `Find.First(...)`, `Find.Last(...)`.

### Examples

Find all elements where a condition is met:

```c#
// Define conditional input
var v = new bool[] {
    true, true, false, false, true, false, true, false
};
// Find all elements that are 'true'
var result = Find.All(v);
// The indices 0, 1, 4, 6 are returned
Assert.That(result, Is.EqualTo(new[] { 0, 1, 4, 6}));
```

Find the first 2 elements where a condition is met:

```C#
// Define conditional input
var v = new bool[] {
    true, true, false, false, true, false, true, false
};
// Find all elements that are 'true'
var result = Find.All(v);
// The indices 0, 1 are returned
Assert.That(result, Is.EqualTo(new[] { 0, 1}));
```

## Find peaks

The `Find.Peaks(...)` methods return indices of local maxima in the given input sequence whereas `Find.PeaksAndValues(...)` returns tuples containing indices and values of local maxima in the input sequence.

### Examples

Find a peak in a short input sequence containing a short plateau:

```c#
// define input sequence, we expect the local maxima
// to be detected at the end of plateau's if any
var x = new[] { 0F, 0F, 2F, 2F, 1F, 1F, 0F };
// find peaks
var peaks = Find.Peaks(x);
// One peak at position 3 has been detected
Assert.That(peaks, Is.EqualTo(new[] { 3 }));
```

Find peaks and values given a minimum peak height:

```c#
// define input sequence
var x = new[] { 0F, 0.9F, 0F, 2F, 0F, 1F, 0F };
// find peaks with a minimal height of 1
var peaks = Find.PeaksAndValues(x, minPeakHeight: 1);
// the local maxima at positions 3 and 5 have been detected
// whereas the maximum at position 1 has been discared
// because its height is less than 1.
Assert.That(peaks, Is.EqualTo(new[] { 
    Tuple.Create(3, 2f), 
    Tuple.Create(5, 1f) 
}));
```

Find peaks with a minimal peak distance:

```c#
// define input sequence
var x = new[] { 0F, 1F, 0F, 2F, 0F, 2F, 0F, 3F, 0F, 1F, 2F, 1F };
// find peaks with a minimal distance of 3 samples
var peaks = Find.Peaks(x, minPeakDistance: 3);
// the maxima at positions 3, 7, and 10 are detected. There are also
// local maxima at positiosn 1 and 5 which are discared because they
// are too close to larger local maxima.
Assert.That(peaks, Is.EqualTo(new[] { 3, 7, 10 }));
```



