# Convolutions

The class `MathNet.Numerics.Convolutions.Convolution` provides an interface to perform convolutions on arrays of the basic numeric types `float`, `double`, `Complex32`, and `Complex`.  Currently, only one-dimensional convolution is supported, implemented by two methods:

```C#
public static void Conv1D(float[] kernel, float[] x, float[] y, Padding padding = Padding.Valid)
```

 where the user provides an array where the results are written to and

```c#
public static float[] Conv1D(float[] kernel, float[] x, Padding padding = Padding.Valid)
```

where the results array is created and returned by the function.

The logic itself is implemented in providers, where a managed reference implementation as well as an optimized Intel MKL implementation are available.

## Examples:

The following snippet shows how a causal convolution is performed with the results array being created by the convolution function itself:

```C#
// Data to be convoluted
var x = new double[] { 12, 3, -5, 16, 8, -2, -2, -1, 2, 5 };
// convolution kernel
var h = new double[] { 0.5f, -1f, 0.3f };
// expected result when using a 'Causal' padding
var expected = new double[] { 6f, -10.5f, -1.9f, 13.9f, -13.5f, -4.2f, 3.4f, 0.9f, 1.4f, 0.2f };

// perform convolution with the results array begin created and returned by the convolution function.
var actual = Convolution.Conv1D(h, x, Padding.Causal);

// The results array contains the expected values
Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
```

The next example the results of a convolution with "valid" padding written to an existing target array:

```C#
// Generate complex test data and kernel
var x = Generate.Map2<float, float, Complex>(
                new float[] { 3, -3, 7, 1, -3, 9, 8, 1, 3, 2 },
                new float[] { -6, -4, -1, -6, 7, -6, -6, -7, -6, -1 },
                (a, b) => new Complex(a, b)
                );
var h = Generate.Map2<float, float, Complex>(
                new float[] { -0.4f, 0.9f, -0.1f },
                new float[] { -0.7f, 0.9f, 1f },
                (a, b) => new Complex(a, b)
                );
var expected = Generate.Map2<float, float, Complex>(
                new float[] { 3.1f, 6.9f, 12.7f, -10.9f, -0.6f, 12.4f, 7f, 13.5f },
                new float[] { -7.2f, 4.5f, 1.9f, 1.3f, -4.2f, 13.5f, 3.5f, -2f },
                (a, b) => new Complex(a, b)
                );

// Initialize target array for the results
var actual = new Complex[expected.Length];

// perform the convolution
Convolution.Conv1D(h, x, actual);

// The target array contains the expected values
Assert.That(Real(actual), Is.EqualTo(Real(expected)).Within(0.0001f));
Assert.That(Imag(actual), Is.EqualTo(Imag(expected)).Within(0.0001f));
```

