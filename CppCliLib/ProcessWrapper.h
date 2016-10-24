#pragma once
public ref class ProcessWrapper : public System::MarshalByRefObject
{
public:
	ProcessWrapper();
	void TestSimpleManagedLogger();
	void TestCrossAppDomainManagedLogger();
	void TestSimpleManagedLoggerFromThread();
	void TestCrossAppDomainManagedLoggerFromThread();
};

