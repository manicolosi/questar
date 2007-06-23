//
// HitPoints.cs: Represents an actor's HP.
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;

using Questar.Helpers;

namespace Questar.Actors
{
    public class HitPointsEventArgs : EventArgs
    {
        public HitPoints OldHitPoints;

        public HitPointsEventArgs ()
        {
        }
    }

    public class HitPoints : ICloneable
    {
        public int current, max;

        public event EventHandler<HitPointsEventArgs> Changed;

        public HitPoints (int current, int max)
        {
            Max = max;
            Current = current;
        }

        public int Current
        {
            get { return current; }
            set {
                if (value > max)
                    throw new ArgumentException (
                        "Current must be less than Max.");

                HitPoints old = Clone () as HitPoints;
                current = value;

                EventHelper.Raise<HitPointsEventArgs> (this, Changed,
                    delegate (HitPointsEventArgs args) {
                        args.OldHitPoints = old;
                    });
            }
        }

        public int Max
        {
            get { return max; }
            set {
                if (value < current)
                    throw new ArgumentException (
                        "Max must be greater than Current.");

                HitPoints old = Clone () as HitPoints;
                max = value;

                EventHelper.Raise<HitPointsEventArgs> (this, Changed,
                    delegate (HitPointsEventArgs args) {
                        args.OldHitPoints = old;
                    });
            }
        }

        public bool IsEmpty
        {
            get { return current == 0; }
        }

        public bool IsFull
        {
            get { return current == max; }
        }

        public override string ToString ()
        {
            return String.Format ("{0}/{1}", Current, Max);
        }

        public object Clone ()
        {
            return base.MemberwiseClone ();
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            HitPoints hp = (HitPoints) o;

            if (Current != hp.Current)
                return false;
            if (Max != hp.Max)
                return false;

            return true;
        }

        public static bool operator == (HitPoints a, HitPoints b)
        {
            return a.Equals (b);
        }

        public static bool operator != (HitPoints a, HitPoints b)
        {
            return !a.Equals (b);
        }
    }
}

