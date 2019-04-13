﻿using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamsAndWhatTheyMean
{
    class DragonKittyCharacters
    {
        public string Player { get; private set; }
        public string MomCharacter { get; private set; }
        public string DadCharacter { get; private set; }
        public string BlackSmithCharacter { get; private set; }

        public DragonKittyCharacters(TheTaleOfTheDragonKittySourceData gameData)
        {
            Player = gameData.AddCharacter(new Character("Player", 50, 40));
            MomCharacter = gameData.AddCharacter(new Character("Mom", 400, 150));
            DadCharacter = gameData.AddCharacter(new Character("Dad", 500, 250));
            BlackSmithCharacter = gameData.AddCharacter(new Character("The Black-Smith", 1000, 700));
        }
    }
}