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

        public HitPoints (int max) : this (max, max)
        {
        }

        public int Current
        {
            get { return current; }
            set {
                int new_current = Math.Min (value, max);
                if (new_current == current)
                    return;

                OnChanged ((HitPoints) Clone ());
                current = new_current;
            }
        }

        public int Max
        {
            get { return max; }
            set {
                if (value == max)
                    return;

                OnChanged ((HitPoints) Clone ());
                max = value;
            }
        }

        private void OnChanged (HitPoints old)
        {
            EventHelper.Raise<HitPointsEventArgs> (this, Changed,
                delegate (HitPointsEventArgs args) {
                    args.OldHitPoints = old;
                });
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

