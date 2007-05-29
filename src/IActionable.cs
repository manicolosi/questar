using Questar.Base;
using Questar.Maps;

namespace Questar.Actors
{
    public interface IActionable : IEntity
    {
        void Move (Point p);
    }
}

