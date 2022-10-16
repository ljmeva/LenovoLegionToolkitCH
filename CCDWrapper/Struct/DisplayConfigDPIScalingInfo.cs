using System.Runtime.InteropServices;

namespace CCD.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigDPIScalingInfo
    {
        public uint mininum;
        public uint maximum;
        public uint current;
        public uint recommended;
    }
}
