//
// IActionable.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Questar.Maps;
using Questar.Primitives;

namespace Questar.Actors
{
    public interface IActionable : IEntity
    {
        void Move (Point p);
    }
}

