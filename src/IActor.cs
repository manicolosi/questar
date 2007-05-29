using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Maps;

namespace Questar.Actors
{
    public class ActorMovedEventArgs : EventArgs
    {
        public Point OldLocation;
    }

    public interface IActor : IEntity, IActionable
    {
        event EventHandler<ActorMovedEventArgs> Moved;

        string Name { get; }
        bool IsTurnReady { get; }
        IAction Action { get; }
    }
}

