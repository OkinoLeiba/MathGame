using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Microsoft.VisualStudio.TestTools.UnitTesting.Parallel; 

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
