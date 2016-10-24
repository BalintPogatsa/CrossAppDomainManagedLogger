#include "stdafx.h"
#include "CrossDomainLoggerBridge.h"

using namespace System;
using namespace System::Runtime::InteropServices;

CrossDomainLoggerBridge::CrossDomainLoggerBridge()
{
	Console::WriteLine("Unmanaged object created in domain: " + AppDomain::CurrentDomain->FriendlyName + " (" + AppDomain::CurrentDomain->Id + ")");
}

void CrossDomainLoggerBridge::Log(std::string const& message)
{
	Console::WriteLine("Callback from domain: " + AppDomain::CurrentDomain->FriendlyName + " (" + AppDomain::CurrentDomain->Id + ")");
	String^ msg = gcnew String(message.c_str());

	try
	{
		// unwrapping a gcroot object and calling it's method doesn't work from different app domain,
		// so a proxy object has to be created
		//ManagedTools::LogProxy^ logProxy = gcnew ManagedTools::LogProxy();
		//logProxy->LogFromUnmanagedThread(msg);

		ManagedTools::CrossAppDomainManagedLogger::Instance->Log(msg);

		// This throws System.ArgumentException: Cannot pass a GCHandle across AppDomains 
		// (if the LoggerBridge was not created in the default app domain)
		//gcroot<ManagedTools::IManagedLogger^>& loggerRoot = *((gcroot<ManagedTools::IManagedLogger^>*)mManagedLogger);
		//loggerRoot->Log(msg);
	}
	catch (Exception^ ex)
	{
		Console::WriteLine("Exception when handling callback: {0} {1}", ex->ToString(), ex->Message);
	}
	catch (...)
	{
		Console::WriteLine("Unmanaged exception when handling callback");
	}
}