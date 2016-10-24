#pragma once

#include "ILogger.h"
#include <vcclr.h>

public class LoggerBridge : public ILogger
{
private:
	gcroot<ManagedTools::IManagedLogger^>* mManagedLogger;

private:
	static std::string LoggerBridge::ToUnManagedString(System::String^ s);

public:
	LoggerBridge(ManagedTools::IManagedLogger^ managedLogger);

public:
	virtual void Log(std::string const& message);
};

