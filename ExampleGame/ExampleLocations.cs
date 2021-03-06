﻿using GameEngine.Locations;

namespace ExampleGame
{
    public class ExampleLocations
    {
        public string Start { get; private set; }

        public string BanquetHall { get; private set; }

        public string CemetaryTheatre { get; private set; }

        public string BanquetElevator { get; private set; }

        public string SecretWarpedHall { get; private set; }

        public ExampleLocations(ExampleGameSourceData gameData)
        {
            Start = gameData.AddLocation(new Location("Starting Area", "an open area with a small marble colleseum",
                    "You stand in the midsts of a miniture marble colleseum. " +
                    "The pillars appear to have been carved by hand and you sense that this area is very old."
                    ));

            BanquetHall = gameData.AddLocation(new Location("Banquet Hall", "a large hall filled with several tables",
                    "Around you are several tables full of delicious looking food. " +
                    "The hall is bustling with many visitors of different nationality"
                    ));

            CemetaryTheatre = gameData.AddLocation(new Location("Cemetary Theatre", "a frightful looking cemetary",
                    "Although the dirt in this area is real, the multitudes of tombstones are " +
                    "obvious fakes. Upon further inspection you discover that they all have the " +
                    "same name on them and have been made from a mold."
                    ));

            BanquetElevator = gameData.AddLocation(new Location("Banquet Elevator", "an ornately designed elevator",
                    "From within the elevator you view what could have only taken ages to craft. " +
                    "The elevator walls are hand crafted from a giant, once whole, block of marble. " +
                    "Although the design is from a historical era, the electronic components appear to " +
                    "be more advanced than what you are familiar with."
                    ));
            SecretWarpedHall = gameData.AddLocation(new Location("Warped Hallway", "a hallway that isn't quite straight",
                    "As you peer down the hallway the walls curve in and out in a haphazardly manner."));

            // This describes that the starting area is always connected to the banquet hall
            // and that the banquet hall is always connected to the starting area
            // Start <--> Banquet Hall
            gameData.AddPortal(
                new PortalAlwaysOpenRule(Start, BanquetHall, "To the west you see"),
                new PortalAlwaysOpenRule(BanquetHall, Start, "To the east you see")
                );

            // Another 2 way portal
            // Start <--> Cemetary
            gameData.AddPortal(
                new PortalAlwaysOpenRule(Start, CemetaryTheatre, "To the east you see"),
                new PortalAlwaysOpenRule(CemetaryTheatre, Start, "To the west you see")
                );

            // Banquet Hall <--> Elevator
            gameData.AddPortal(
                // These 2 rules will only be considered if the player is in the banquet hall.
                // If the game variable "BanquestElevatorFloor" is set to 1 then the first rule will match, otherwise it'll match the 2nd
                new PortalOpenGameVarRule(BanquetHall, BanquetElevator, "Through an open elevator door you see", gameData.GameVariables.BanquetElevatorFloor, "1"),
                new PortalAlwaysClosedRule(BanquetHall, null, "You see a closed elevator door"),

                new PortalOpenGameVarRule(BanquetElevator, BanquetHall, "From within the elevator you peer through the door to see", gameData.GameVariables.BanquetElevatorFloor, "1"),
                new PortalAlwaysClosedRule(BanquetElevator, null, "The elevator door is closed")
                );

            // Banquet Hall <--> Warped Hall
            gameData.AddPortal(
                new PortalOpenGameVarRule(BanquetHall, SecretWarpedHall, "Through the now open passageway you see", gameData.GameVariables.BanquetToSecretWarpedHallDoorOpen, "true"),
                new PortalAlwaysClosedRule(BanquetHall, null, "You see a large closed stone door with a keyhole. The door is locked."),

                new PortalOpenGameVarRule(SecretWarpedHall, BanquetHall, "Through the now open passageway you see", gameData.GameVariables.BanquetToSecretWarpedHallDoorOpen, "true"),
                new PortalAlwaysClosedRule(SecretWarpedHall, null, "You see the end of the hallway. There is currently no way through in this direction.")
                );
        }
    }
}
