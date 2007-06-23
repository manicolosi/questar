//
// IEntity.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Questar.Primitives;

namespace Questar.Maps
{
    public interface IEntity
    {
        Map Map { get; }
        Point Location { get; }

        string Tile { get; }
    }
}

