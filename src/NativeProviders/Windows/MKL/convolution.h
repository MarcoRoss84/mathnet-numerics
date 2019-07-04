#pragma once
#include "mkl_vsl.h"

#define API extern "C" __declspec(dllexport)

#define ConvolutionStatusSuccess 0;
#define ConvolutionStatusTaskCreationFailed 1;
#define ConvolutionStatusTaskExecutionFailed 2;
#define ConvolutionStatusTaskDestructionFailed 3;

template <class T> int conv1d(
    const T* kernel,
    const int kernelLength,
    const T* x,
    const int xLength,
    const int firstX,
    T* result,
    const int resultLength,
    int (*convTaskCreation)(VSLConvTaskPtr*, const MKL_INT, const MKL_INT, const MKL_INT, const MKL_INT),
    int (*convTaskExecution)(VSLConvTaskPtr, const T[], const MKL_INT, const T[], const MKL_INT, T[], const MKL_INT)
);

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
);

API inline int s_conv1d(const float* kernel, const int kernelLength, const float* x, const int xLength, const int firstX, float* result, const int resultLength)
{
    return conv1d<float>(kernel, kernelLength, x, xLength, firstX, result, resultLength, vslsConvNewTask1D, vslsConvExec1D);
}
API inline int d_conv1d(const double* kernel, const int kernelLength, const double* x, const int xLength, const int firstX, double* result, const int resultLength)
{
    return conv1d<double>(kernel, kernelLength, x, xLength, firstX, result, resultLength, vsldConvNewTask1D, vsldConvExec1D);
}
API inline int c_conv1d(const MKL_Complex8* kernel, const int kernelLength, const MKL_Complex8* x, const int xLength, const int firstX, MKL_Complex8* result, const int resultLength)
{
    return conv1d<MKL_Complex8>(kernel, kernelLength, x, xLength, firstX, result, resultLength, vslcConvNewTask1D, vslcConvExec1D);
}
API inline int z_conv1d(const MKL_Complex16* kernel, const int kernelLength, const MKL_Complex16* x, const int xLength, const int firstX, MKL_Complex16* result, const int resultLength)
{
    return conv1d<MKL_Complex16>(kernel, kernelLength, x, xLength, firstX, result, resultLength, vslzConvNewTask1D, vslzConvExec1D);
}

API inline int s_conv2d(const float* kernel, const int kernelLength1, const int kernelLength2, const float* x, const int xLength1, const int xLength2, const int firstX1, const int firstX2, float* result, const int resultLength1, const int resultLength2)
{
    return conv2d<float>(kernel, kernelLength1, kernelLength2, x, xLength1, xLength2, firstX1, firstX2, result, resultLength1, resultLength2, vslsConvNewTask, vslsConvExec);
}

API inline int d_conv2d(const double* kernel, const int kernelLength1, const int kernelLength2, const double* x, const int xLength1, const int xLength2, const int firstX1, const int firstX2, double* result, const int resultLength1, const int resultLength2)
{
    return conv2d<double>(kernel, kernelLength1, kernelLength2, x, xLength1, xLength2, firstX1, firstX2, result, resultLength1, resultLength2, vsldConvNewTask, vsldConvExec);
}

API inline int c_conv2d(const MKL_Complex8* kernel, const int kernelLength1, const int kernelLength2, const MKL_Complex8* x, const int xLength1, const int xLength2, const int firstX1, const int firstX2, MKL_Complex8* result, const int resultLength1, const int resultLength2)
{
    return conv2d<MKL_Complex8>(kernel, kernelLength1, kernelLength2, x, xLength1, xLength2, firstX1, firstX2, result, resultLength1, resultLength2, vslcConvNewTask, vslcConvExec);
}

API inline int z_conv2d(const MKL_Complex16* kernel, const int kernelLength1, const int kernelLength2, const MKL_Complex16* x, const int xLength1, const int xLength2, const int firstX1, const int firstX2, MKL_Complex16* result, const int resultLength1, const int resultLength2)
{
    return conv2d<MKL_Complex16>(kernel, kernelLength1, kernelLength2, x, xLength1, xLength2, firstX1, firstX2, result, resultLength1, resultLength2, vslzConvNewTask, vslzConvExec);
}



