using System;
using System.Diagnostics;

public partial class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        CommandLineInfo commandLine = new CommandLineInfo();

        if (!CommandLineHandler.TryParse(args, commandLine, out string? errorMessage))
        {
            Console.WriteLine(errorMessage);
            DisplayHelp();
        }
        if (commandLine.Help)
        {
            DisplayHelp();
        }
        else
        {
            if (commandLine.Priority != ProcessPriorityClass.Normal)
            {
                //zmienianie priorytetu watku
            }
        }
    }

    private static void DisplayHelp()
    {
        Console.WriteLine(
            "Compress.exe / Out:< nazwa pliku > / Help \n"
            + "/ Priority:RealRime | High |"
            + "AboveNormal | Normal | BelowNormal | Idle");
    }
}