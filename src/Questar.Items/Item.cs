using System;

using Questar.Actors;
using Questar.Primitives;

namespace Questar.Items
{
	public interface Item : Entity
	{
        Actor Owner { get; set; }
        bool IsOwned { get; }
	}
}
