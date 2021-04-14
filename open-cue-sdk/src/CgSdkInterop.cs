using System.Runtime.InteropServices;

namespace OpenCue.Sdk
{
    internal class CgSdkInterop
    {
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkClearAllEvents", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool ClearAllEvents();
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkClearAllStates", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool ClearAllStates();
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkClearState", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool ClearState(string gameName);
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkGetLastError", CallingConvention=CallingConvention.Cdecl)]
        internal static extern int GetLastError();
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkPerformProtocolHandshake", CallingConvention=CallingConvention.Cdecl)]
        internal static extern _CorsairProtocolDetails PerformProtocolHandshake();
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkReleaseControl", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool ReleaseControl();
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkRequestControl", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool RequestControl();
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkSetEvent", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool SetEvent(string gameName);
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkSetGame", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool SetGame(string gameName);
        [DllImport("lib/CgSDK.x64_2015.dll", EntryPoint="CgSdkSetState", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool SetState(string gameName);
    }
}