using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public class CommandLineHandler
{
    public static void Parse(string[] args, object commandLine)
    {
        if(!TryParse(args, commandLine, out string? errorMessage))
        {
            throw new InvalidOperationException(errorMessage);
        }
    }

    public static bool TryParse(string[] args, object commandLine, out string? errorMessage)
    {
        bool success = false;
        errorMessage = null;
        foreach (string arg in args)
        {
            string option;
            if (arg[0] == '/' || arg[0] == '-')
            {
                string[] optionParts = arg.Split(new char[] { ':' }, 2);

                // Usuwanie ukośnika lub dywizu
                option = optionParts[0].Remove(0, 1);
                PropertyInfo? property =
                    commandLine.GetType().GetProperty(option,
                        BindingFlags.IgnoreCase |
                        BindingFlags.Instance |
                        BindingFlags.Public);

                if (property != null)
                {
                    if (property.PropertyType == typeof(bool))
                    {
                        // Parametry do obsługi indekserów
                        property.SetValue(
                            commandLine, true, null);
                        success = true;
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(
                            commandLine, optionParts[1], null);
                        success = true;
                    }
                    else if (
                        // Dostępne jest też wywołanie property.PropertyType.IsEnum
                        property.PropertyType ==
                            typeof(ProcessPriorityClass))
                    {
                        try
                        {
                            property.SetValue(commandLine,
                                Enum.Parse(
                                    typeof(ProcessPriorityClass),
                                    optionParts[1], true),
                                null);
                            success = true;
                        }
                        catch (ArgumentException)
                        {
                            success = false;
                            errorMessage =
                                $@"Opcja '{optionParts[1]}' jest nieprawidłowa dla '{option}'";
                        }
                    }
                    else
                    {
                        success = false;
                        errorMessage =
                            $@"Typ danych '{property.PropertyType}' dla {commandLine.GetType()} nie jest obsługiwany.";
                    }
                }
                else
                {
                    success = false;
                    errorMessage =
                       $"Opcja '{option}' nie jest obsługiwana.";
                }
            }
        }
        return success;
    }
}