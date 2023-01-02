using System.Runtime.InteropServices;

namespace ImageFilters.CLI;

class Program
{
    /// <summary>
    /// The name and full path to the currently running executable.
    /// </summary>
    /// <summary>
    /// This is the name of the configuration file.
    /// </summary>

    [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow")]
    private static extern IntPtr _GetConsoleWindow();

    static void Main(string[]? args)
    {


        var firstParam = args is { Length: > 0 } ? args[0] : null;
        var fileToOpenOnStart = File.Exists(firstParam) ? firstParam : null;

        // execute CLI if arguments are given which are not forcing into gui or a valid filename
        var result = CommandLine.ParseCommandLineArguments(args);
        Environment.Exit((int)result);

    }
}
