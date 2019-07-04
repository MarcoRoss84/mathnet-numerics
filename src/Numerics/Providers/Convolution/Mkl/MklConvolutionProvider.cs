using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using MathNet.Numerics.Providers.Common.Mkl;



namespace MathNet.Numerics.Providers.Convolution.Mkl
{

#if NATIVE

    internal class MklConvolutionProvider : IConvolutionProvider, IDisposable
    {

        const int MinimumCompatibleRevision = 11;
        private readonly string _hintPath;

        /// <param name="hintPath">Hint path where to look for the native binaries</param>
        internal MklConvolutionProvider(string hintPath)
        {
            _hintPath = hintPath;
        }

        /// <summary>
        /// Try to find out whether the provider is available, at least in principle.
        /// Verification may still fail if available, but it will certainly fail if unavailable.
        /// </summary>
        public bool IsAvailable()
        {
            return MklProvider.IsAvailable(hintPath: _hintPath);
        }

        /// <summary>
        /// Initialize and verify that the provided is indeed available. If not, fall back to alternatives like the managed provider
        /// </summary>
        public void InitializeVerify()
        {
            int revision = MklProvider.Load(hintPath: _hintPath);
            if (revision < MinimumCompatibleRevision)
            {
                throw new NotSupportedException($"MKL Native Provider revision r{revision} is too old. Consider upgrading to a newer version. Revision r{MinimumCompatibleRevision} and newer are supported.");
            }

            // we only support exactly one major version, since major version changes imply a breaking change.
            int fftMajor = SafeNativeMethods.query_capability((int)ProviderCapability.FourierTransformMajor);
            int fftMinor = SafeNativeMethods.query_capability((int)ProviderCapability.FourierTransformMinor);
            if (!(fftMajor == 1 && fftMinor >= 0))
            {
                throw new NotSupportedException(string.Format("MKL Native Provider not compatible. Expecting Fourier transform v1 but provider implements v{0}.", fftMajor));
            }
        }

        /// <summary>
        /// Frees memory buffers, caches and handles allocated in or to the provider.
        /// Does not unload the provider itself, it is still usable afterwards.
        /// </summary>
        public virtual void FreeResources()
        {
            MklProvider.FreeResources();
        }

        public override string ToString()
        {
            return MklProvider.Describe();
        }

        public void Conv1D(float[] kernel, float[] x, int xOffset, float[] y, int yOffset, int length)
        {
            SafeNativeMethods.s_conv1d(kernel, kernel.Length, x, x.Length, xOffset, y, length);
        }

        public void Conv1D(double[] kernel, double[] x, int xOffset, double[] y, int yOffset, int length)
        {
            SafeNativeMethods.d_conv1d(kernel, kernel.Length, x, x.Length, xOffset, y, length);
        }

        public void Conv1D(Complex32[] kernel, Complex32[] x, int xOffset, Complex32[] y, int yOffset, int length)
        {
            SafeNativeMethods.c_conv1d(kernel, kernel.Length, x, x.Length, xOffset, y, length);
        }

        public void Conv1D(Complex[] kernel, Complex[] x, int xOffset, Complex[] y, int yOffset, int length)
        {
            SafeNativeMethods.z_conv1d(kernel, kernel.Length, x, x.Length, xOffset, y, length);
        }

        public void Conv2D(float[,] kernel, float[,] x, Tuple<int, int> xOffset, float[,] y, Tuple<int, int> targetShape)
        {
            SafeNativeMethods.s_conv2d(kernel, kernel.GetLength(0), kernel.GetLength(1), x, x.GetLength(0), x.GetLength(1), xOffset.Item1, xOffset.Item2, y, targetShape.Item1, targetShape.Item2);
        }
        
        public void Conv2D(double[,] kernel, double[,] x, Tuple<int, int> xOffset, double[,] y, Tuple<int, int> targetShape)
        {
            SafeNativeMethods.d_conv2d(kernel, kernel.GetLength(0), kernel.GetLength(1), x, x.GetLength(0), x.GetLength(1), xOffset.Item1, xOffset.Item2, y, targetShape.Item1, targetShape.Item2);
        }

        public void Conv2D(Complex[,] kernel, Complex[,] x, Tuple<int, int> xOffset, Complex[,] y, Tuple<int, int> targetShape)
        {
            SafeNativeMethods.z_conv2d(kernel, kernel.GetLength(0), kernel.GetLength(1), x, x.GetLength(0), x.GetLength(1), xOffset.Item1, xOffset.Item2, y, targetShape.Item1, targetShape.Item2);
        }

        public void Conv2D(Complex32[,] kernel, Complex32[,] x, Tuple<int, int> xOffset, Complex32[,] y, Tuple<int, int> targetShape)
        {
            SafeNativeMethods.c_conv2d(kernel, kernel.GetLength(0), kernel.GetLength(1), x, x.GetLength(0), x.GetLength(1), xOffset.Item1, xOffset.Item2, y, targetShape.Item1, targetShape.Item2);
        }

        public void Dispose()
        {
            FreeResources();
        }

        
    }

#endif
}
