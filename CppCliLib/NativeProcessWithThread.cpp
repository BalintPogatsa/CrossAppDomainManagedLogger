#include "stdafx.h"
#include <thread>
#include "NativeProcessWithThread.h"

#include "ILogger.h"


NativeProcessWithThread::NativeProcessWithThread(ILogger & logger) : mLogger(logger)
{
	std::thread t1(&NativeProcessWithThread::NativeMethod, this);
	t1.detach();
}

void NativeProcessWithThread::NativeMethod()
{
	mLogger.Log("Native callback - another thread");
}