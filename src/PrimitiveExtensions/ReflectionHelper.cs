using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PrimitiveExtensions
{
    /// <summary>
    ///     Provides static methods that aid in performing reflection related operations.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Retruns Fully qualified method name
        /// </summary>
        /// <returns>string of fully qualified methodName</returns>
        public static string GetThisFullyQualifiedMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            Type type = method.DeclaringType;
            string fullyQualifiedMethodName = string.Format("{0}.{1}", type.FullName, method.Name);
            return fullyQualifiedMethodName;
        }

        /// <summary>
        /// Returns method name
        /// </summary>
        /// <returns>string of method name </returns>
        public static string GetThisMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            Type type = method.DeclaringType;
            return method.Name;
        }

        /// <summary>
        /// Returns caller qualified method name
        /// </summary>
        /// <returns>string of caller qualified method name</returns>
        public static string GetThisCallersFullyQualifiedMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(2).GetMethod();
            Type type = method.DeclaringType;
            string fullyQualifiedMethodName = string.Format("{0}.{1}", type.FullName, method.Name);
            return fullyQualifiedMethodName;
        }
        /// <summary>
        /// Returns caller method name
        /// </summary>
        /// <returns>string of caller method name</returns>
        public static string GetThisCallersMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(2).GetMethod();
            Type type = method.DeclaringType;
            return method.Name;
        }

        /// <summary>
        /// Returns Entry Software Name
        /// </summary>
        /// <returns>string of entry software name</returns>
        public static string GetEntrySoftwareName()
        {
            try
            {
                try
                {
                    return Assembly.GetEntryAssembly().GetName().Name;
                }
                catch (Exception)
                {
                    return Assembly.GetExecutingAssembly().GetName().Name;
                }
            }
            catch (Exception)
            {
                return "Unknown";
            }

        }

        /// <summary>
        /// Returns class stack trace
        /// </summary>
        /// <returns>string of class stact trace</returns>
        public static string GetThisCallersCallersFullyQualifiedMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(3).GetMethod();
            Type type = method.DeclaringType;
            string fullyQualifiedMethodName = string.Format("{0}.{1}", type.FullName, method.Name);
            return fullyQualifiedMethodName;
        }

        /// <summary>
        ///  Returns program folder name
        /// </summary>
        /// <returns>string of program folder name</returns>
        public static String GetProgramFolder()
        {
      
            var s = (Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
            return s;
            
        }
    }
}
