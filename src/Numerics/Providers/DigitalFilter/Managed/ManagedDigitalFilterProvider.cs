using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.DigitalFilters;

namespace MathNet.Numerics.Providers.DigitalFilter.Managed
{
    public class ManagedDigitalFilterProvider : IDigitalFilterProvider
    {
        public IEnumerable<float> ApplyFilter(Filter filter, IEnumerable<float> x)
        {
            return ApplyFilter(filter, x.Select(xi => (double)xi)).Select(yi => (float)yi);
        }

        public IEnumerable<double> ApplyFilter(Filter filter, IEnumerable<double> x)
        {
            var warmup = Math.Max(filter.PastX.Length, filter.PastY.Length);
            var xDiff = warmup - filter.PastX.Length;
            var yDiff = warmup - filter.PastY.Length;
            var allX = new double[xDiff].Concat(filter.PastX).Concat(x).ToArray();
            var allY = new double[yDiff].Concat(filter.PastY).Concat(new double[allX.Length - filter.PastX.Length]).ToArray();
            
            for (int i = warmup; i < allX.Length; i++)
            {
                allY[i] = ReverseDotProduct(filter.B, allX, i) - ReverseDotProduct(filter.A, allY, i);
            }
            return allY.Skip(warmup);
        }

        private double ReverseDotProduct(double[] b, double[] x, int xi0)
        {
            var val = 0.0;
            for (int i = 0; i < b.Length; i++)
            {
                val += b[i] * x[xi0 - i];
            }
            return val;
        }

        public void FreeResources()
        {
        }

        public void InitializeVerify()
        {
        }

        public bool IsAvailable()
        {
            return true;
        }
    }
}
