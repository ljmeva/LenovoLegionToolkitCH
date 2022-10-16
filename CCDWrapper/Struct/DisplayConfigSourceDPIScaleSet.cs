using System.Runtime.InteropServices;

namespace CCD.Struct
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DisplayConfigSourceDPIScaleSet : IDisplayConfigInfo
    {
        public DisplayConfigDeviceInfoHeader header;
        public int scaleRel;
    }
}