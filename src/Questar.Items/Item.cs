using System;

using Questar.Actors;
using Questar.Primitives;

namespace Questar.Items
{
	public interface Item
	{
		string Name { get; }
		string Description { get; }
        Location Location { get; }

        Actor Owner { get; set; }
        bool IsOwned { get; }
	}
}
