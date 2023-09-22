using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase que invoca los comandos y puede deshacerlos
public class CommandInvoker: MonoBehaviour
{
    private List<ICommand> commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (var command in commands)
        {
            command.Execute();
        }
    }

    public void ClearCommands()
    {
        commands.Clear();
    }
}
