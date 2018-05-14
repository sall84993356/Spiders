/*
* =======================================
* Version: 1.0
* Created: 11 Apirl 2018
* Compiler: Visual Studio 2017
*
* Author: Shaun Shi
* Company: ISee Ltd.
*
* =======================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.Common
{
    /// <summary>
    /// Process class reflection
    /// </summary>
    public static class ReflectionInvoke
    {
        /// <summary>
        /// Get Return Value by assemblyName,className,functionName,param,paramFunction
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        /// <param name="param"></param>
        /// <param name="paramFunction"></param>
        /// <returns></returns>
        public static object Invoke(string assemblyName, string className, string functionName, object[] param, object[] paramFunction)
        {
            // Namespace + '.' + class name
            string name = string.Format("{0}.{1}", assemblyName, className);
            // get assembly by assembly name
            Assembly assembly = Assembly.Load(assemblyName);
            // Get type
            Type type = assembly.GetType(name);
            // Get instance
            object instance = assembly.CreateInstance(name, true, System.Reflection.BindingFlags.Default, null, param, null, null);
            // Invoke function and return object
            return type.GetMethod(functionName).Invoke(instance, paramFunction);
        }

        /// <summary>
        /// Get Instance
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object GetInstance(string assemblyName, string className, object[] param)
        {
            // Namespace + '.' + class name
            string name = string.Format("{0}.{1}", assemblyName, className);
            // get assembly by assembly name
            Assembly assembly = Assembly.Load(assemblyName);
            // Get type
            Type type = assembly.GetType(name);
            // Get instance
            return assembly.CreateInstance(name, true, System.Reflection.BindingFlags.Default, null, param, null, null);
        }
    }
}
