using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)] // Use sequential layout and pack of 1 byte
public struct SendPacket
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] // Array of 5 integers
    public byte[] Data;

    public static byte[] Serialize(SendPacket packet)
    {
        int size = Marshal.SizeOf(packet);
        byte[] data = new byte[size];

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(packet, ptr, true);
        Marshal.Copy(ptr, data, 0, size);
        Marshal.FreeHGlobal(ptr);

        return data;
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)] // Use sequential layout and pack of 1 byte
public struct ReceivePacket
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] // Array of 7 integers
    public byte[] Data;

    public static ReceivePacket Deserialize(byte[] data)
    {
        ReceivePacket packet = new ReceivePacket();
        int size = Marshal.SizeOf(packet);

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(data, 0, ptr, size);
        packet = (ReceivePacket)Marshal.PtrToStructure(ptr, typeof(ReceivePacket));
        Marshal.FreeHGlobal(ptr);

        return packet;
    }
}