//
// NativeMethods.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Questar.Base
{
    public static class NativeMethods
    {
        [DllImport ("libc")]
        public static extern int prctl (int option, string name,
            ulong arg3, ulong arg4, ulong arg5);
    }
}
