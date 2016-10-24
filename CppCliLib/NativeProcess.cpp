#include "stdafx.h"
#include <thread>
#include "NativeProcess.h"

#include "ILogger.h"


NativeProcess::NativeProcess(ILogger & logger) : mLogger(logger)
{
	NativeMethod();
}

void NativeProcess::NativeMethod()
{
	mLogger.Log("Native callback - same thread");
}