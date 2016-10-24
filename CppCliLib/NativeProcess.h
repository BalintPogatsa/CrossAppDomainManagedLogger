#pragma once

#include "ILogger.h"

class NativeProcess
{
private:
	ILogger& mLogger;

public:
	NativeProcess(ILogger & logger);

private:
	void NativeMethod();
};

