﻿namespace GameEngine.Locations
{
    /// <summary>
    /// A portal rule is tested to see if a portal links to the specified destination.
    /// A location can have multiple rules to determine what the actual destination is.
    /// </summary>
    public abstract class PortalRule
    {
        /// <summary>
        /// The location where the portal starts at
        /// </summary>
        public string Origin { get; private set; }

        /// <summary>
        /// The destination of where the portal leads to if the rule matches.
        /// This may be null if the portal is closed.
        /// </summary>
        public string Destination { get; private set; }

        /// <summary>
        /// A description of what the portal looks like when the rule matches.
        /// This is a description as if the user is standing in the origin location.
        /// This description should only define what the portal portion looks like
        /// and not the room beyond.
        /// e.g. Beyond an open elevator door you see
        /// </summary>
        public string Description { get; private set; }

        public PortalRule(string origin, string destination, string description)
        {
            Origin = origin;
            Destination = destination;
            Description = description;
        }
    }
}
