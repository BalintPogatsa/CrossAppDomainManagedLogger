#pragma once

#include <string>

class ILogger
{
public:
	virtual ~ILogger() {}
	virtual void Log(std::string const& message) = 0;
};