using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NewTek
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class NDIlib
    {
        // The creation structure that is used when you are creating a finder
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct find_create_t
        {
            // Do we want to incluide the list of NDI sources that are running
            // on the local machine ?
            // If TRUE then local sources will be visible, if FALSE then they
            // will not.
            [MarshalAsAttribute(UnmanagedType.U1)]
            public bool show_local_sources;

            // Which groups do you want to search in for sources
            public IntPtr   p_groups;

            // The list of additional IP addresses that exist that we should query for
            // sources on. For instance, if you want to find the sources on a remote machine
            // that is not on your local sub-net then you can put a comma seperated list of
            // those IP addresses here and those sources will be available locally even though
            // they are not mDNS discoverable. An example might be "12.0.0.8,13.0.12.8".
            // When none is specified the registry is used.
            // Default = NULL;
            public IntPtr   p_extra_ips;
        }

        //**************************************************************************************************************************
        // Create a new finder instance. This will return NULL if it fails.
        // This function is deprecated, please use NDIlib_find_create_v2 if you can. This function
        // ignores the p_extra_ips member and sets it to the default.
        [DllImport(_dllName, EntryPoint = "NDIlib_find_create_v2", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr find_create_v2(ref find_create_t p_create_settings);

        // This will destroy an existing finder instance.
        [DllImport(_dllName, EntryPoint = "NDIlib_find_destroy", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void find_destroy(IntPtr p_instance);

        // This function will recover the current set of sources (i.e. the ones that exist right this second).
        // The char* memory buffers returned in NDIlib_source_t are valid until the next call to NDIlib_find_get_current_sources or a call to NDIlib_find_destroy.
        // For a given NDIlib_find_instance_t, do not call NDIlib_find_get_current_sources asynchronously.
        [DllImport(_dllName, EntryPoint = "NDIlib_find_get_current_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr find_get_current_sources(IntPtr p_instance, ref UInt32 p_no_sources);

        // This will allow you to wait until the number of online sources have changed.
        [DllImport(_dllName, EntryPoint = "NDIlib_find_wait_for_sources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.U1)]
        public static extern bool find_wait_for_sources(IntPtr p_instance, UInt32 timeout_in_ms);

    } // class NDIlib

} // namespace NewTek
