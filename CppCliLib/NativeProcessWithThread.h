#pragma once

#include "ILogger.h"

class NativeProcessWithThread
{
private:
	ILogger& mLogger;

public:
	NativeProcessWithThread(ILogger & logger);

private:
	void NativeMethod();
};