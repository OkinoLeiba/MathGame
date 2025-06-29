using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Ensure the correct namespace is used for ExecutionScope  
using Microsoft.Testing.Platform.MSBuild;

// This assembly attribute is used to configure the test execution settings for MSTest.  
// It specifies that tests should be run in parallel at the method level, with no limit on the number of workers.  
// This can help improve test execution speed by allowing multiple tests to run concurrently.  
// Consider the implications of parallel test execution, such as shared state or dependencies between tests that may lead to flaky tests  
// Confirm DLL and test framework versions are compatible with this setting  
// TODO: Confirm correct DLL that contains the test classes is referenced in the project file  

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
