using System;
using System.Diagnostics;

public partial class Program
{
    private class CommandLineInfo
    {
        [CommandLineSwitchAlias("?")]
        public bool Help { get; set; }
        [CommandLineSwitchRequired]
        public string? Out { get; set; }
        public ProcessPriorityClass Priority { get; set; } = ProcessPriorityClass.Normal;
    }
    internal class CommandLineSwitchRequiredAttribute : Attribute
    {
        // Bez implementacji
    }
    internal class CommandLineSwitchAliasAttribute : Attribute
    {
        public CommandLineSwitchAliasAttribute(string _)
        {
            // Bez implementacji
        }
    }
}