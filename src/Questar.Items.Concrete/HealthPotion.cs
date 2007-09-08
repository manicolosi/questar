using System;

using Questar.Actors;

namespace Questar.Items.Concrete
{
	public class HealthPotion : AbstractItem, IDrinkable
	{
		private int recover_amount;

		public HealthPotion ()
		{
		}
		
		public int RecoverAmount
		{
			get { return recover_amount; }
			protected set { recover_amount = value; }
		}
		
		public void Drink (Actor actor)
		{
			Console.WriteLine ("{0} is drinking {1}. {2} HP recovered!",
			                   actor.Name, base.Name, RecoverAmount);
		}
	}
}
