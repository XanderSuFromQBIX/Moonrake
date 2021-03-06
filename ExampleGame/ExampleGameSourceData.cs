﻿using GameEngine;

namespace ExampleGame
{
    /// <summary>
    /// This class gives an example of how to implement all the features available to the game data
    /// </summary>
    public class ExampleGameSourceData : GameSourceData
    {
        public ExampleGameVariables GameVariables { get; private set; }
        public ExampleLocations Locations { get; private set; }
        public ExampleCharacters Characters { get; private set; }
        public ExampleItems Items { get; private set; }

        /// <summary>
        /// Constructor that fills in the data
        /// </summary>
        public ExampleGameSourceData()
        {
            // Note that the properties should be defined in this order due to how they reference each other
            GameVariables = new ExampleGameVariables(this);
            Locations = new ExampleLocations(this);
            Characters = new ExampleCharacters(this);
            Items = new ExampleItems(this);

            DefaultPlayerName = "Sally";
            GameIntroductionText = "There once was an example game.";

            // TODO: From the cemetary theatre you'll need to set the numbers on a combination lock to 1234 through the use command.
            // TODO: When a new game starts the combination should be initially set to 8734.
            // TODO: getting the combination right should lead to the warped hall

            // TODO: Chanage the dull bronze key in the start room to be discovered like this:
            //         1. The Start Room initially has no location items in it.
            //         2. The player pulls a lever on the wall (use lever).
            //         3. The lever interact code calls
            //              AddLocationItem("Start", "Key", 1);
            //              And Console.WriteLine  "You see a shiny key fall from a now open crack in the wall"
            //              Plus it keeps track if this has already happened in a gamevariable so it doesn't spit out multiple keys
            //              The lever stays visible and can be moved up and down still, but the 2nd attempt produces no result.
        }
    }
}
