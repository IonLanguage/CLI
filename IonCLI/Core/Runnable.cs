using System;
using System.Diagnostics;
using System.IO;
using IonCLI.Core;
using IonCLI.InterOperability;
using IonCLI.Tools;

namespace IonCLI.Core
{
    public class Runnable
    {
        /// <summary>
        /// The arguments to be passed onto the
        /// process' start information.
        /// </summary>
        public string[] Arguments { get; set; }

        /// <summary>
        /// The path to the executable file.
        /// </summary>
        public string Path { get; set; }

        public Runnable(string path)
        {
            this.Path = path;
        }

        public Runnable()
        {
            //
        }

        /// <summary>
        /// Execute the target file and return
        /// its output.
        /// </summary>
        public string Run()
        {
            // Create a new process instance.
            Process process = new Process();

            // Ensure tool path exists.
            if (!File.Exists(this.Path))
            {
                Log.Error($"Executable path does not exist: {this.Path}");
            }

            // Set the process' target file.
            process.StartInfo.FileName = this.Path;

            // Do not create a window.
            process.StartInfo.CreateNoWindow = true;

            // Do not execute from shell.
            process.StartInfo.UseShellExecute = false;

            // Fill all provided arguments.
            foreach (string arg in this.Arguments)
            {
                process.StartInfo.ArgumentList.Add(arg);
            }

            // Instruct process to redirect output.
            process.StartInfo.RedirectStandardOutput = true;

            // TODO: Handle error stream too?
            // Instruct process to redirect errors.
            process.StartInfo.RedirectStandardError = true;

            // Start the process.
            process.Start();

            // Capture the output of the tool.
            string output = process.StandardOutput.ReadToEnd();

            // Trim output.
            output = output.Trim();

            // Wait for completion.
            process.WaitForExit();

            // Return the resulting output.
            return output;
        }
    }
}
