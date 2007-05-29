using Questar.Base;

namespace Questar.Maps
{
    public interface IEntity
    {
        Map Map { get; }
        Point Location { get; }

        string Tile { get; }
    }
}

