using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MazeSolver
{
    /// <summary>
    /// A class for loading maze agents from a DLL file.
    /// </summary>
    public static class MazeAgentLoader
    {
        /// <summary>
        /// Loads a single maze agent from a DLL file.
        /// </summary>
        /// <param name="dllName">The name of the DLL file.</param>
        /// <returns>The loaded maze agent.</returns>
        public static MazeAgent LoadSingleAgent(string dllName)
        {
            // The base class name for maze agents.
            string mazeAgentBaseClassName = "MazeSolver.MazeAgent";

            // Load the assembly from the DLL file.
            Assembly agentAssembly = Assembly.LoadFile(dllName);

            // Find the first exported type that inherits from the base class.
            foreach (Type type in agentAssembly.ExportedTypes)
            {
                if (type.BaseType.FullName == mazeAgentBaseClassName)
                {
                    // Create an instance of the type and cast it to a MazeAgent.
                    return Activator.CreateInstance(type, null) as MazeAgent;
                }
            }

            // If no type was found, throw an exception.
            string msg = $"Module {dllName} does not implement {mazeAgentBaseClassName}";
            throw new NotSupportedException(msg);
        }

        /// <summary>
        /// Loads all maze agents from DLL files in a directory.
        /// </summary>
        /// <param name="pluginDirectory">The directory to search for DLL files.</param>
        /// <returns>A queue of loaded maze agents.</returns>
        public static Queue<MazeAgent> LoadAllAgents(string pluginDirectory = null)
        {
            // Create a queue to hold the loaded maze agents.
            Queue<MazeAgent> mazeAgents = new Queue<MazeAgent>();

            // Get a list of all DLL files in the directory.
            foreach (string module in MazeAgentLoader.GetModuleList(pluginDirectory))
            {
                try
                {
                    // Load the maze agent from the DLL file and add it to the queue.
                    mazeAgents.Enqueue(LoadSingleAgent(module));
                }
                catch (NotSupportedException)
                {
                    // If the DLL file does not contain a maze agent, ignore it.
                    continue;
                }
            }

            return mazeAgents;
        }

        /// <summary>
        /// Checks if a method name is specific to a class.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>True if the method is specific to a class, false otherwise.</returns>
        private static bool IsClassSpecificMethod(string methodName)
        {
            // The list of methods that are not specific to a class.
            string[] baseObjMethods = { "Equals", "GetType", "GetHashCode", "ToString" };

            // Check if the method name is in the list.
            foreach (string method in baseObjMethods)
            {
                if (string.Compare(methodName, method, true) == 0)
                {
                    // If the method is not specific to a class, return false.
                    return false;
                }
            }

            // If the method is specific to a class, return true.
            return true;
        }

        /// <summary>
        /// Gets a list of all DLL files in a directory.
        /// </summary>
        /// <param name="pluginDirectory">The directory to search for DLL files.</param>
        /// <returns>A list of DLL file names.</returns>
        private static List<string> GetModuleList(string pluginDirectory = null)
        {
            // If no directory is specified, use the current directory.
            if (pluginDirectory == null)
            {
                pluginDirectory = Directory.GetCurrentDirectory();
            }

            // Get a list of all DLL files in the directory.
            List<string> moduleList = Directory.GetFiles(pluginDirectory, "*.dll").ToList();

            return moduleList;
        }

        /// <summary>
        /// Returns an array of public method prototypes for the given type.
        /// </summary>
        /// <param name="type">The type to get the method prototypes for.</param>
        /// <returns>An array of public method prototypes.</returns>
        public static string[] GetPublicMethodPrototypeList(Type type)
        {
            // Define the parameter separators used in the method prototype string.
            char[] parameterSeparators = new char[] { ',', ' ' };

            // Get all methods for the given type.
            MethodInfo[] fullMethodList = type.GetMethods();

            // Create an array to hold the method prototypes.
            string[] prototypeList = new string[fullMethodList.Length];

            // Loop through each method in the full method list.
            int n = 0;
            foreach (MethodInfo method in fullMethodList)
            {
                // Only include public methods that are specific to the class.
                if (method.IsPublic && IsClassSpecificMethod(method.Name))
                {
                    // Start building the method prototype string with the access modifier.
                    string prototype = $"public ";

                    // Add the return type to the method prototype string.
                    if (method.ReturnType.GenericTypeArguments.Length > 1)
                    {
                        // If the return type has multiple generic type arguments, add them to the method prototype string.
                        prototype += "(";
                        foreach (Type t in method.ReturnType.GenericTypeArguments)
                        {
                            prototype += $"{t.Name}, ";
                        }
                        prototype = prototype.TrimEnd(parameterSeparators) + ") ";
                    }
                    else
                    {
                        // If the return type has only one generic type argument, add it to the method prototype string.
                        prototype += $"{method.ReturnType.Name} ";
                    }

                    // Add the method name to the method prototype string.
                    prototype += $"{method.Name}";

                    // Add the method parameters to the method prototype string.
                    prototype += "(";
                    foreach (ParameterInfo parm in method.GetParameters())
                    {
                        prototype += $"{parm.ParameterType.Name} {parm.Name}, ";
                    }
                    prototype = prototype.TrimEnd(parameterSeparators) + ")";

                    // Add the completed method prototype string to the prototype list.
                    prototypeList[n++] = prototype;
                }
            }

            // Return the completed prototype list.
            return prototypeList;
        }

        /// <summary>
        /// Returns an array of strings containing the names of all the modules in the specified plugin directory.
        /// </summary>
        /// <param name="pluginDirectory">The directory to search for plugin modules. If null or empty, the current directory is searched.</param>
        /// <returns>An array of strings containing the names of all the modules in the specified plugin directory.</returns>
        public static string[] GetModuleList(string pluginDirectory = null)
        {
            // Create a list to store the names of all the modules.
            List<string> moduleList = new List<string>();

            // If a plugin directory was specified, search only that directory.
            if (!string.IsNullOrWhiteSpace(pluginDirectory))
            {
                // If the specified directory does not exist, print an error message and return an empty array.
                if (!Directory.Exists(pluginDirectory))
                {
                    Console.WriteLine($"Error: Directory {pluginDirectory} does not exist");
                    return new string[0];
                }

                // Search for all DLL files in the specified directory and add them to the module list.
                moduleList.AddRange(Directory.EnumerateFiles(pluginDirectory, "*.dll"));
            }
            else
            {
                // If no plugin directory was specified, search the current directory for DLL files.
                string pwd = Directory.GetCurrentDirectory();
                moduleList.AddRange(Directory.EnumerateFiles(pwd, "*.dll"));

                // If this is a MSVC project, search for DLL files in the bin/debug/netstandard2.0 directory.
                string csharpProjectPath = pwd + @"\..\..\..\"; // Assumes the project is three directories up from the current directory.
                foreach (string dir in Directory.EnumerateDirectories(csharpProjectPath))
                {
                    string csharpPath = @"\bin\debug\netstandard2.0";
                    string pluginPath = Path.GetFullPath(dir + csharpPath);
                    if (Directory.Exists(pluginPath))
                    {
                        moduleList.AddRange(Directory.EnumerateFiles(pluginPath, "*.dll"));
                    }
                }
            }

            // Convert the list to an array and return it.
            return moduleList.ToArray();
        }
    }
}
