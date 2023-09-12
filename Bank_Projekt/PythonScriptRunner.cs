using System.Diagnostics;

class PythonScriptRunner
{
    public static void RunPythonScript(string scriptPath, string datapath)
    {
        Process pythonProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{scriptPath} \"{datapath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        pythonProcess.Start();

        string output = pythonProcess.StandardOutput.ReadToEnd();
        Console.WriteLine(output);

        pythonProcess.WaitForExit();
    }
}
