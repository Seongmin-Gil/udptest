using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
[Serializable]
public struct ToServerPacket
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
    public string m_StringlVariable;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
[Serializable]
public struct ToClientPacket
{
    [MarshalAs(UnmanagedType.Bool, SizeConst = 7)]
    public bool m_BoolVariable;
}