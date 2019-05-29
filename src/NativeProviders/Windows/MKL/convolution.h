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



