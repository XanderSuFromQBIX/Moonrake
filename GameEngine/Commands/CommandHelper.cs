﻿using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Commands
{
    internal static class CommandHelper
    {
        private static List<ICommand> AllCommands { get; set; }

        static CommandHelper()
        {
            AllCommands = new List<ICommand>()
            {
                new MoveCommand(),
                new SaveCommand(),
                new LoadCommand(),
                new ExitCommand(),
                new LetPlayerChangeTheirNameCommand(),
                new LookCommand(),
                new ClearCommand(),
                new InventoryCommand(),
                new GrabCommand(),
                new DropCommand(),
                new AttackCommand(),
                new StatsCommand(),
                new InteractCommand()
            };
        }

        public static ICommand GetCommand(string word)
        {
            var commandToRun = AllCommands.FirstOrDefault(c => c.IsActivatedBy(word));
            return commandToRun;
        }
    }
}
