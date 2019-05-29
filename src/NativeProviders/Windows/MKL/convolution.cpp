#include "convolution.h"

template <class T> int conv1d(
    const T* kernel, const int kernelLength,
    const T* x, const int xLength, const int firstX,
    T* result, const int resultLength,
    int (*convTaskCreation)(VSLConvTaskPtr*, const MKL_INT, const MKL_INT, const MKL_INT, const MKL_INT),
    int (*convTaskExecution)(VSLConvTaskPtr, const T[], const MKL_INT, const T[], const MKL_INT, T[], const MKL_INT)
)
{
    VSLConvTaskPtr task;
    if (convTaskCreation(&task, VSL_CONV_MODE_AUTO, kernelLength, xLength, resultLength) != VSL_STATUS_OK)
    {
        return ConvolutionStatusTaskCreationFailed;
    }
    vslConvSetStart(task, &firstX);
    auto executionStatus = convTaskExecution(task, kernel, 1, x, 1, result, 1);
    if (vslConvDeleteTask(&task) != 0) return ConvolutionStatusTaskDestructionFailed;
    if (executionStatus != VSL_STATUS_OK) return ConvolutionStatusTaskExecutionFailed;
    return ConvolutionStatusSuccess;
}


