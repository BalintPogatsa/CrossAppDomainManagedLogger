#pragma once

#include "ILogger.h"
#include <vcclr.h>

public class CrossDomainLoggerBridge : public ILogger
{

public:
	CrossDomainLoggerBridge();

public:
	virtual void Log(std::string const& message);
};