﻿using System;

namespace GameEngine
{
    public static class EngineFactory
    {
        /// <summary>
        /// Starts the factory.
        /// The factory will create games and start them over and over until stopped.
        /// </summary>
        /// <param name="gameDataFactory">A method that returns a GameData</param>
        public static void Start(Func<GameSourceData> gameDataFactory)
        {
            EngineInternal engine;
            do
            {
                // Create a new game data instance
                var gameData = gameDataFactory();

                // Create a new engine 
                engine = new EngineInternal(gameData);

                // Start the game
                engine.StartEngine();

            } while (engine.RunFactory);
        }
    }
}
