using Python.Runtime;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cuku.Pythonity
{
    public static class Pythonity
    {
#if !UNITY_EDITOR
        static Pythonity()
        {
            string[] pyvenvs = Directory.GetFiles(Application.streamingAssetsPath, "pyvenv.cfg", SearchOption.AllDirectories);
            foreach (var pyvenv in pyvenvs)
            {
                var lines = File.ReadAllLines(pyvenv);
                foreach (var line in lines)
                {
                    // Reference python.dll and initialize PythonEngine
                    if (line.StartsWith("home ="))
                    {
                        var home = line.Substring("home =".Length).Trim();
                        Runtime.PythonDLL = Path.Combine(home, new DirectoryInfo(home).Name + ".dll");
                        PythonEngine.Initialize();
                    }
                    // Add site-packages path where libraries are installed
                    else if (line.StartsWith("command ="))
                    {
                        var site_packages = Path.Combine(line.Substring(line.IndexOf("-m venv") + "-m venv".Length).Trim(), @"Lib\site-packages");
                        using (Py.GIL())
                        {
                            dynamic py_sys = Py.Import("sys");
                            py_sys.path.insert(0, site_packages);
                        }
                    }
                }
            }
        }
#endif

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