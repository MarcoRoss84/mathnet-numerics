using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Providers.DigitalFilter;

namespace MathNet.Numerics.DigitalFilters
{
    public class Filter
    {
        private readonly double[] _b;
        private readonly double[] _a;

        private readonly double[] _pastX;
        private readonly double[] _pastY;

        public double[] B => _b;

        public double[] A => _a;

        internal double[] PastX => _pastX;

        internal double[] PastY => _pastY;


        private bool IsLowPass { get; }

        public Filter(double[] b, bool isLowPass) : this(b, new[] { 1.0 }, isLowPass)
        { }

        public Filter(double[] b, double[] a, bool isLowPass)
        {

            if (b.Length == 0 || a.Length == 0) throw new ArgumentException("Filter coefficients cannot be empty");
            if (b.Length < 2) throw new ArgumentException("Expect filter numerator to be of length 2 or higher");
            if (a.Length < 1) throw new ArgumentException("Expect filter denominator not to be empty");
            var a0 = a[0];
            _b = b.Select(bi => bi / a0).ToArray();
            _a = a.Select(ai => ai / a0).ToArray();
            _pastX = new double[_b.Length - 1];
            _pastY = new double[_a.Length - 1];
            IsLowPass = isLowPass;
        }

        public Filter Clone()
        {
            return new Filter(_b, _a, IsLowPass);
        }

        public IEnumerable<float> FilterSignal(IEnumerable<float> x)
        {
            return DigitalFilterControl.Provider.ApplyFilter(this, x);
        }

        public IEnumerable<double> FilterSignal(IEnumerable<double> x)
        {
            return DigitalFilterControl.Provider.ApplyFilter(this, x);
        }

        public void ResetState()
        {
            for (int i = 0; i < _pastX.Length; i++)
            {
                _pastX[i] = 0;
            }
            for (int i = 0; i < _pastY.Length; i++)
            {
                _pastY[i] = 0;
            }
        }

    }
}
