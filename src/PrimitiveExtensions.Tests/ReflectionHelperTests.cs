using Xunit;

namespace PrimitiveExtensions.Tests
{

    public class ReflectionHelperTests
    {
        [Fact]
        public void GetThisMethodName_GivenThis_ExpectThisMethodName()
        {
            Assert.Equal("GetThisMethodName_GivenThis_ExpectThisMethodName",ReflectionHelper.GetThisMethodName());
        }

        [Fact]
        public void GetThisCallersMethodName_GivenThis_ExpectThisMethodName()
        {
            Assert.Equal("InvokeMethod",ReflectionHelper.GetThisCallersMethodName());
        }

        [Fact]
        public void GetThisFullyQualifiedMethodName_GivenThis_ExpectThisMethodName()
        {
            Assert.Equal("PrimitiveExtensions.Tests.ReflectionHelperTests.GetThisFullyQualifiedMethodName_GivenThis_ExpectThisMethodName", ReflectionHelper.GetThisFullyQualifiedMethodName());
        }

        [Fact]
        public void GetThisCallersFullyQualifiedMethodName_GivenThis_ExpectThisMethodName()
        {
            Assert.Equal("System.RuntimeMethodHandle.InvokeMethod",ReflectionHelper.GetThisCallersFullyQualifiedMethodName());
        }

        //GetEntrySoftwareName
        [Fact]
        public void GetThisCallersMethodName_GivenNoparm_ExpectInvokeMethod()
        {
            string result = ReflectionHelper.GetThisCallersMethodName();
            Assert.Equal("InvokeMethod",result);
        }

        //GetEntrySoftwareName
        [Fact]
        public void GetEntrySoftwareName_GivenNoparm_ExpectTesthost()
        {
            string result = ReflectionHelper.GetEntrySoftwareName();
            Assert.Equal("testhost",result );
        }

        //GetThisCallersCallersFullyQualifiedMethodName
        [Fact]
        public void GetThisCallersCallersFullyQualifiedMethodName_GivenNoparm_ExpectSystemReflectionRuntimeMethodInfoUnsafeInvokeInternal()
        {
            string result = ReflectionHelper.GetThisCallersCallersFullyQualifiedMethodName();
            Assert.Equal( "System.Reflection.RuntimeMethodInfo.Invoke",result);
        }

    }
}

