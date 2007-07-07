////////////////////////////////////////////////////////////////////////////////
//  Potions.cs: All potions are defined here.
//
//  Copyright (C) 2007
//  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
////////////////////////////////////////////////////////////////////////////////

namespace Questar.Content.Items

import Questar.Items from Questar.exe
import Questar.Actors from Questar.exe

class Potion (Item):
    pass

// This should actually subclass Potion.
class HealPotion (Item):
    amount = 0

    protected Amount:
        set:
            amount = value

    override def Use (target as Actor):
        target.HitPoints.Current += amount;

class HealLightWounds (HealPotion):
    def constructor ():
        Amount = 20

class HealSeriousWounds (HealPotion):
    def constructor ():
        Amount = 50

