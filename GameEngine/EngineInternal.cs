﻿using GameEngine.Commands;
using System.Collections.Generic;

namespace GameEngine
{
    internal class EngineInternal
    {
        /// <summary>
        /// Indicates if the main game loop should keep running game turns
        /// </summary>
        public bool RunGameLoop { get; set; } = true;

        /// <summary>
        /// If set to false, triggers the engine factory to stop generating new engines and exit.
        /// </summary>
        public bool RunFactory { get; set; } = true;

        public bool PlayerIsDead { get; set; } = false;
        public bool PlayerHasWon { get; set; } = false;

        public GameSourceData GameData { get; set; }

        public EngineInternal(GameSourceData gameData)
        {
            GameData = gameData;
        }

        /// <summary>
        /// Runs the game until they win, die or exit.
        /// </summary>
        public void StartEngine()
        {
            // Ask the player to pick to load a saved game if there are any
            var loadCommand = CommandHelper.GetCommand("load");
            loadCommand.Exceute(this, new List<string>());

            // Main game loop goes 1 loop for 1 game turn.
            while (RunGameLoop)
            {
                ProcessUserInput();

                GameData.TryGetCharacter("Player", out Character player);
                if (player.Hp <= 0)
                {
                    PlayerIsDead = true;
                }
                if (PlayerIsDead)
                {
                    Console.WriteLine();
                    Console.WriteLine("You have died. Please press a key.");
                    Console.ReadKey();
                    RunGameLoop = false;
                }
                // TODO: Find a way to figure out when the player has won.
                else if (PlayerHasWon)
                {
                    Console.WriteLine(GameData.GameEndingText);
                    Console.WriteLine("             |--The End--|             ");
                    Console.ReadLine();
                    RunGameLoop = false;
                }
            }
        }

        private void ProcessUserInput()
        {
            string input;
            Console.Write(">");
            input = Console.ReadLine();
            Console.WriteLine();
            var partsOfInput = new List<string>(input.Split(' '));
            var firstWord = partsOfInput[0];

            var commandToRun = CommandHelper.GetCommand(firstWord);
            if (commandToRun == null)
            {
                Console.WriteLine($"I don't know what you mean by '{firstWord}'.");
                return;
            }

            // The command is a real command if we got this far
            partsOfInput.RemoveAt(0);
            commandToRun.Exceute(this, partsOfInput);
        }

        /// <summary>
        /// Sets up everything to start a new game and shows the game introduction text
        /// </summary>
        public void StartNewGame()
        {
            GameState.CreateNewGameState();

            // Transfer all defaults from the game data to game state
            GameState.CurrentGameState.PlayerName = GameData.DefaultPlayerName;

            // Set the default locations for each character
            foreach (var characterName in GameData.DefaultCharacterLocations.Keys)
            {
                string locationName = GameData.DefaultCharacterLocations[characterName];
                GameState.CurrentGameState.SetCharacterLocation(characterName, locationName);
            }

            // Add game vars that represent the inital game state
            foreach (var gv in GameData.DefaultGameVars)
            {
                GameState.CurrentGameState.SetGameVarValue(gv.Key, gv.Value);
            }

            // Set the default locations of where trade posts exist at
            foreach (var tp in GameData.DefaultTradePostLocations)
            {
                GameState.CurrentGameState.SetTradePostLocation(tp.Key, tp.Value);
            }

            // Set the default items that all characters have
            foreach (var characterName in GameData.DefaultCharacterItems.Keys)
            {
                foreach (var itemTrackingName in GameData.DefaultCharacterItems[characterName].Keys)
                {
                    GameState.CurrentGameState.TryAddCharacterItemCount(characterName, itemTrackingName, GameData.DefaultCharacterItems[characterName][itemTrackingName], GameData);
                }
            }

            foreach (var locationName in GameData.DefaultLocationItems.Keys)
            {
                foreach (var itemTrackingName in GameData.DefaultLocationItems[locationName].Keys)
                {
                    GameState.CurrentGameState.TryAddLocationItemCount(locationName, itemTrackingName, GameData.DefaultLocationItems[locationName][itemTrackingName], GameData);
                }
            }

            // Show the intro
            Console.Clear();
            Console.WriteLine(GameData.GameIntroductionText);
            Console.WriteLine();

            var lookCommand = CommandHelper.GetCommand("look");
            lookCommand.Exceute(this, new List<string>());
        }
    }
}
