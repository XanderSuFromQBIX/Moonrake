﻿using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Commands
{
    internal class MoveCommand : ICommand
    {
        public void Exceute(EngineInternal engine, List<string> extraWords)
        {
            var playerLoc = GameState.CurrentGameState.GetCharacterLocation("Player");
            var originPortalsDestinations = engine.GameData.Portals
                .Where(p => p.HasOriginLocation(playerLoc))
                .Select(p => p.GetDestination(playerLoc))
                .Where(d => d.Destination != null)
                .Select(d =>  d.Destination)
                .ToList();
            var placeToMove = originPortalsDestinations;
            placeToMove.Add("Cancel");
            var placeToMoveTo = Console.Choose("Where would you like to move to?", placeToMove);

            if (placeToMoveTo.Equals("Cancel"))
            {
                Console.WriteLine("Canceled Move");
                return;
            }
            GameState.CurrentGameState.SetCharacterLocation("Player", placeToMoveTo);

            // Make the player automatically look after they move to the new location
            Console.WriteLine();
            var lookCommand = CommandHelper.GetCommand("look");
            lookCommand.Exceute(engine, new List<string>());
        }

        public bool IsActivatedBy(string word)
        {
            var validWords = new List<string>() { "go", "move", "travel" };
            return validWords.Contains(word.ToLower());
        }
    }
}
