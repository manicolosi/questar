using System;

using Questar.Actors;

namespace Questar.Items
{
	public interface IDrinkable : Item
	{
		void Drink (Actor actor);
	}
}
