using Cuku.Provider;
using Cysharp.Threading.Tasks;
using Python.Runtime;
using System.Collections.Generic;
using System.IO;

namespace Cuku.Pythonity
{
	public static class Pythonity
	{
		static Pythonity()
		{
			foreach (var pyvenv in "pyvenv.cfg".GetStreamingAssets())
			{
				foreach (var line in File.ReadAllLines(pyvenv))
					// Reference python.dll and initialize PythonEngine
					if (line.StartsWith("home ="))
					{
						var home = line.Substring("home =".Length).Trim();
						Runtime.PythonDLL = Path.Combine(home, new DirectoryInfo(home).Name + ".dll");
						PythonEngine.Initialize();
					}

				// Add site-packages path where libraries are installed
				var site_packages = Path.Combine(Path.GetDirectoryName(pyvenv), @"Lib\site-packages");
				using (Py.GIL())
				{
					dynamic py_sys = Py.Import("sys");
					py_sys.path.insert(0, site_packages);
				}
			}
		}

		/// <summary>
		/// The Python code of a Package is located in "StreamingAssets/Py[PackageName]".
		/// </summary>
		public static string PythonPackage(this string package) => $"Py{package}";

		/// <summary>
		/// Execute python code from specified script with optional input parameters and receive optional output data.
		/// </summary>
		/// <param name="script">Properly formated Python code.</param>
		/// <param name="directory">Optional directory inside StreamingAssets where to search for the script file.</param>
		/// <param name="output">Optional output object returned by the Python code.
		/// <para>IMPORTANT: Is the developers responsability to cast the object to the correct type!</para></param>
		/// <param name="input">Optional input parameters passed to the python code.</param>
		/// <returns></returns>
		public static async UniTask<object> Execute(this string script, string directory = "", string output = null, params KeyValuePair<string, object>[] input)
		{
			// Load script code from StreamingAssets
			var code = await System.IO.File.ReadAllTextAsync($"{script}.py"
				.GetStreamingAssets(directory)[0]).AsUniTask();

			using (Py.GIL())
			{
				using (var scope = Py.CreateScope())
				{
					// Set input parameters
					foreach (var parameter in input)
						scope.Set(parameter.Key, parameter.Value.ToPython());

					// Execute python code
					scope.Exec(code);

					// Cache object returned by the python code and return it at the end
					if (!string.IsNullOrEmpty(output))
						return scope.Get<object>(output);

					return new object();
				}
			}
		}

		public static void Shutdown() => PythonEngine.Shutdown();
	}
}