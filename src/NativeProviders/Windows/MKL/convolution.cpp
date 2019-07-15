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

template <class T> int conv2d(
    const T* kernel,
    const int kernelLength1, const int kernelLength2,
    const T* x,
    const int xLength1, const int xLength2,
    const int firstX1, const int firstX2,
    T* result,
    const int resultLength1, const int resultLength2,
    int (*convTaskCreation)(VSLConvTaskPtr*, const MKL_INT, const MKL_INT, const int[], const int[], const int[]),
    int (*convTaskExecution)(VSLConvTaskPtr, const T[], const int[], const T[], const int[], T[], const int[])
)
{
    VSLConvTaskPtr task;
    int kernelSize [2] = { kernelLength1, kernelLength2 };
    int xSize [2] = { xLength1, xLength2 };
    int resultSize [2] = { resultLength1, resultLength2 };
    int xOffset [2] = { firstX1, firstX2 };
    int kernelStrides[2] = { kernelLength2, 1 };
    int xStrides[2] = { xLength2, 1 };
    int resultStrides[2] = { resultLength2, 1 };

    if (convTaskCreation(&task, VSL_CONV_MODE_AUTO, 2, kernelSize, xSize, resultSize) != VSL_STATUS_OK)
    {
        return ConvolutionStatusTaskCreationFailed;
    }
    vslConvSetStart(task, xOffset);
    auto executionStatus = convTaskExecution(task, kernel, kernelStrides, x, xStrides, result, resultStrides);
    if (vslConvDeleteTask(&task) != 0) return ConvolutionStatusTaskDestructionFailed;
    if (executionStatus != VSL_STATUS_OK) return ConvolutionStatusTaskExecutionFailed;
    return ConvolutionStatusSuccess;
}


