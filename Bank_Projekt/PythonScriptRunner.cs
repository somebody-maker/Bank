using System.Diagnostics;

class PythonScriptRunner
{
    public static void RunCustomerScript(string customerScriptPath)
    {
        Process pythonProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{customerScriptPath}",
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

    public static void RunBalanceScript(string balanceScriptPath)
    {
        int userInputId;

        while (true)
        {
            Console.Write("Enter the Customer ID (integer): ");
            if (int.TryParse(Console.ReadLine(), out userInputId))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }

        Process pythonProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{balanceScriptPath}",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        pythonProcess.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);

        pythonProcess.Start();

        StreamWriter sw = pythonProcess.StandardInput;
        sw.WriteLine(userInputId);

        sw.Close();

        pythonProcess.BeginOutputReadLine();

        pythonProcess.WaitForExit();
    }

}
