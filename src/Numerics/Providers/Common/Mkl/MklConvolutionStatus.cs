using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathNet.Numerics.Providers.Common.Mkl
{
    internal enum MklConvolutionStatus
    {
        Success = 0,
        TaskCreationFailed = 1,
        TaskExecutionFailed = 2,
        TaskDestructionFailed = 3,
    }
}
