using System.Diagnostics;

internal class PythonScriptRunnerBase
{
    public static void RunPythonScript(string scriptPath, string jsonFilePath)
    {
        Process pythonProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{scriptPath} \"{jsonFilePath}\"",
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