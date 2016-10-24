#include "stdafx.h"
#include "ProcessWrapper.h"

#include "ILogger.h"
#include "LoggerBridge.h"
#include "CrossDomainLoggerBridge.h"
#include "NativeProcess.h"
#include "NativeProcessWithThread.h"

ProcessWrapper::ProcessWrapper()
{
	
}

void ProcessWrapper::TestSimpleManagedLogger()
{
	ManagedTools::IManagedLogger^ managedLogger = gcnew ManagedTools::SimpleManagedLogger();
	LoggerBridge loggerBridge = LoggerBridge(managedLogger);
	NativeProcess nativeProcess = NativeProcess(loggerBridge);
}

void ProcessWrapper::TestCrossAppDomainManagedLogger()
{
	ManagedTools::IManagedLogger^ managedLogger = ManagedTools::CrossAppDomainManagedLogger::Instance;
	CrossDomainLoggerBridge loggerBridge = CrossDomainLoggerBridge();
	NativeProcess nativeProcess = NativeProcess(loggerBridge);
}

void ProcessWrapper::TestSimpleManagedLoggerFromThread()
{
	ManagedTools::IManagedLogger^ managedLogger = gcnew ManagedTools::SimpleManagedLogger();
	LoggerBridge loggerBridge = LoggerBridge(managedLogger);
	NativeProcessWithThread nativeProcess = NativeProcessWithThread(loggerBridge);
	// The problem here is that the loggerBridge can be destructed before the thread calls the callback method - this can result in ExecutionEngineException...
	System::Threading::Thread::Sleep(1000);
}

void ProcessWrapper::TestCrossAppDomainManagedLoggerFromThread()
{
	ManagedTools::IManagedLogger^ managedLogger = ManagedTools::CrossAppDomainManagedLogger::Instance;
	CrossDomainLoggerBridge loggerBridge = CrossDomainLoggerBridge();
	NativeProcessWithThread nativeProcess = NativeProcessWithThread(loggerBridge);
}