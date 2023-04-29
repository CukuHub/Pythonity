using Python.Runtime;
using System;
using System.Collections.Generic;

namespace Cuku.Pythonity
{
    public static class Pythonity
    {
        static Pythonity()
        {
            Runtime.PythonDLL = @"C:\Python\Python311\python311.dll";
            PythonEngine.Initialize();

            // Add vsite-packages path where libraries are installed
            using (Py.GIL())
            {
                dynamic py_sys = Py.Import("sys");

                // IMPROVE: https://github.com/InfiniTwin/Pythonity/issues/2
                string site_pkg = "C:\\Python\\spacyvenv\\Lib\\site-packages";

                py_sys.path.insert(0, site_pkg);
            }
        }

		/// <summary>
		/// Execute python code with optional input parameters and receive option output data.
		/// </summary>
		/// <param name="code">Properly formated Python code.</param>
		/// <param name="output">Optional output object returned by Python code.
		/// Important: Is your responsability to cast the object to the correct type!</param>
		/// <param name="input">Option input parameters passed to the python code before executing it.</param>
		/// <returns></returns>
        public static object Execute(string code, string output = null, params KeyValuePair<string, object>[] input)
        {
            var outputObject = new object();

            using (Py.GIL())
            {
                using (var scope = Py.CreateScope())
                {
					// Set input parameters
                    foreach (var parameter in input)
                    {
                        scope.Set(parameter.Key, parameter.Value.ToPython());
                    }
					
					// Execute python code
                    scope.Exec(code);

					// Cache object returned by the python code and return it at the end
                    if (!string.IsNullOrEmpty(output))
                    {
                        outputObject = scope.Get<object>(output);
                    }
                }

                return outputObject;
            }
        }

        public static void Shutdown() => PythonEngine.Shutdown();
    }
}