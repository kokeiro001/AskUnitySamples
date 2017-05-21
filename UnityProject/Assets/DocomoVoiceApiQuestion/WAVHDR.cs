using System;
using System.Runtime.InteropServices;

namespace DocomoVoiceApiQuestion
{
    [StructLayout(LayoutKind.Sequential)]
    public class WAVHDR
    {
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 riff = 0x46464952; /* RIFF */
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 fileSize;
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 wave = 0x45564157; /* WAVE */
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 fmt = 0x20746D66; /* fmt  */
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 fmtbytes = 16;
        [MarshalAs(UnmanagedType.I2)]
        public UInt16 formatid;
        [MarshalAs(UnmanagedType.I2)]
        public UInt16 channel;
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 fs;
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 bytespersec;
        [MarshalAs(UnmanagedType.I2)]
        public UInt16 blocksize;
        [MarshalAs(UnmanagedType.I2)]
        public UInt16 bitspersample;
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 data = 0x61746164; /* data */
        [MarshalAs(UnmanagedType.I4)]
        public UInt32 size;

        //convert the struct to byte array
        public byte[] getByteArray()
        {
            int len = Marshal.SizeOf(this);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(this, ptr, true/*or false*/);
            Marshal.Copy(ptr, arr, 0, len /*or arr.Length*/);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }

}