﻿using GameEngine.Locations;
using System.Collections.Generic;

namespace GameEngine
{
    public interface IGameSourceData
    {
        string StartingLocationName { get; }

        string DefaultPlayerName { get; }

        string GameIntroductionText { get; }

        List<Character> Characters { get; }

        List<Location> Locations { get; }
    }
}