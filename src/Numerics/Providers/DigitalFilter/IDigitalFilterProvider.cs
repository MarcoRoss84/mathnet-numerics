using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.DigitalFilters;

namespace MathNet.Numerics.Providers.DigitalFilter
{
    public interface IDigitalFilterProvider
    {
        void InitializeVerify();
        bool IsAvailable();
        void FreeResources();
        IEnumerable<float> ApplyFilter(Filter filter, IEnumerable<float> x);

        IEnumerable<double> ApplyFilter(Filter filter, IEnumerable<double> x);
    }
}
