using Microsoft.VisualStudio.TestTools;
using Microsoft.VisualStudio.UnitTesting;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
