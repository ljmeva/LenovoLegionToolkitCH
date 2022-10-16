using System.Runtime.InteropServices;

namespace CCD.Struct
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DisplayConfigSourceDPIScaleGet : IDisplayConfigInfo
    {
        public DisplayConfigDeviceInfoHeader header;
        public int minScaleRel;
        public int curScaleRel;
        public int maxScaleRel;
    }
}