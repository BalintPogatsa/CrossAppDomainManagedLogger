#include "stdafx.h"
#include "CrossDomainLoggerBridge.h"

using namespace System;
using namespace System::Runtime::InteropServices;

CrossDomainLoggerBridge::CrossDomainLoggerBridge()
{
	ManagedTools::FileLogger::WriteLine("Unmanaged object created in domain: " + AppDomain::CurrentDomain->FriendlyName + " (" + AppDomain::CurrentDomain->Id + ")");
}

void CrossDomainLoggerBridge::Log(std::string const& message)
{
	ManagedTools::FileLogger::WriteLine("Callback from domain: " + AppDomain::CurrentDomain->FriendlyName + " (" + AppDomain::CurrentDomain->Id + ")");
	String^ msg = gcnew String(message.c_str());

	try
	{
		// unwrapping a gcroot object and calling it's method doesn't work from different app domain,
		// so a proxy object has to be created
		//ManagedTools::LogProxy^ logProxy = gcnew ManagedTools::LogProxy();
		//logProxy->LogFromUnmanagedThread(msg);

		ManagedTools::CrossAppDomainManagedLogger::Instance->Log(msg);
	}
	catch (Exception^ ex)
	{
		ManagedTools::FileLogger::WriteLine("Exception when handling callback: {0} {1}", ex->ToString(), ex->Message);
	}
	catch (...)
	{
		ManagedTools::FileLogger::WriteLine("Unmanaged exception when handling callback");
	}
}