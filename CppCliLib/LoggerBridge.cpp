#include "stdafx.h"
#include "LoggerBridge.h"

using namespace System;
using namespace System::Runtime::InteropServices;

LoggerBridge::LoggerBridge(ManagedTools::IManagedLogger^ managedLogger)
{
	Console::WriteLine("Unmanaged object created in domain: " + AppDomain::CurrentDomain->FriendlyName + " (" + AppDomain::CurrentDomain->Id + ")");
	
	// A void pointer is used to store the gcroot
	// with gcroot as a member variable the callback from unmanaged thread would throw an InvalidOperationException: handle is not initialized
	mManagedLogger = new gcroot<ManagedTools::IManagedLogger^>();
	*mManagedLogger = managedLogger;
}

void LoggerBridge::Log(std::string const& message)
{
	Console::WriteLine("Callback from domain: " + AppDomain::CurrentDomain->FriendlyName + " (" + AppDomain::CurrentDomain->Id + ")");
	String^ msg = gcnew String(message.c_str());

	try
	{
		if (mManagedLogger == nullptr)
		{
			Console::WriteLine("gcroot doesn't not exist anymore");
			return;
		}

		if (static_cast<ManagedTools::IManagedLogger^>(*mManagedLogger) == nullptr)
		{
			Console::WriteLine("managed logger doesn't not exist anymore");
			return;
		}

		// This throws System.ArgumentException: Cannot pass a GCHandle across AppDomains 
		// if the LoggerBridge was not created in the default app domain
		(*mManagedLogger)->Log(msg);
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

std::string LoggerBridge::ToUnManagedString(System::String ^ s)
{
	// convert .NET System::String to std::string
	const char* cstr = (const char*)(Marshal::StringToHGlobalAnsi(s)).ToPointer();
	std::string sstr = cstr;
	Marshal::FreeHGlobal(System::IntPtr((void*)cstr));
	return sstr;
}