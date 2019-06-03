using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.DigitalFilters;
using MathNet.Numerics.Providers.Common.Mkl;

namespace MathNet.Numerics.Providers.DigitalFilter.Mkl
{
    public class MklDigitalFilterProvider : IDigitalFilterProvider
    {
        private string v;
        private MklConsistency consistency;
        private MklPrecision precision;
        private MklAccuracy accuracy;

        public MklDigitalFilterProvider(string v, MklConsistency consistency, MklPrecision precision, MklAccuracy accuracy)
        {
            this.v = v;
            this.consistency = consistency;
            this.precision = precision;
            this.accuracy = accuracy;
        }

        public IEnumerable<float> ApplyFilter(Filter filter, IEnumerable<float> x)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> ApplyFilter(Filter filter, IEnumerable<double> x)
        {
            throw new NotImplementedException();
        }

        public void FreeResources()
        {
            throw new NotImplementedException();
        }

        public void InitializeVerify()
        {
            throw new NotImplementedException();
        }

        public bool IsAvailable()
        {
            return false;
        }
    }
}
