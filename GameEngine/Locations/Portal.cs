﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Locations
{
    /// <summary>
    /// A portal is something that can lead to another location.
    /// The location that a portal leads to is based on a set of rules.
    /// Example portals:
    ///     A standard door - This portal always leads to the same target location
    ///     A stairway - This portal always leads to the same target location
    ///     An elevator door - This portal leads to different locations depending on what floor the elevator is on.
    /// </summary>
    public class Portal
    {
        private List<PortalRule> Rules { get; set; }

        public Portal(List<PortalRule> rules)
        {
            Rules = rules;
        }

        /// <summary>
        /// Indicates if the portal has any rules that originate from the specified location
        /// </summary>
        /// <param name="locationName">The location to check for</param>
        /// <returns>True / False</returns>
        public bool HasOriginLocation(string locationName)
        {
            return Rules.Any(r => r.Origin.Equals(locationName));
        }

        /// <summary>
        /// Looks at all the rules that originate from the specified location and returns the first
        /// one that matches as a PortalDestinationDetails
        /// </summary>
        /// <param name="origin">The source location name</param>
        /// <returns>The first rule that matches</returns>
        public PortalDestinationDetails GetDestination(string origin)
        {
            var destDetails = new PortalDestinationDetails();

            // Filter the list of rules down to just those that start from the specified origin
            var originRules = Rules.Where(r => r.Origin.Equals(origin));

            foreach (var destRule in originRules)
            {
                if (destRule is PortalAlwaysClosedRule || destRule is PortalAlwaysOpenRule)
                {
                    destDetails.Description = destRule.Description;
                    destDetails.Destination = destRule.Destination;
                    return destDetails;
                }
                else if (destRule is PortalOpenGameVarRule)
                {
                    var gameVarRule = destRule as PortalOpenGameVarRule;
                    var gameVarRuleValue = GameState.CurrentGameState.GetGameVarValue(gameVarRule.GameVarName);
                    if (gameVarRuleValue != null &&
                        gameVarRuleValue.Equals(gameVarRule.ExpectedValue, StringComparison.OrdinalIgnoreCase))
                    {
                        destDetails.Description = destRule.Description;
                        destDetails.Destination = destRule.Destination;
                        return destDetails;
                    }
                }
            }

            return destDetails;
        }
    }
}
