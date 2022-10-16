
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CCD.Enum;
using CCD.Struct;

namespace CCD
{
    public static class CCDHelpers
    {
        static uint[] DpiVals = { 100, 125, 150, 175, 200, 225, 250, 300, 350, 400, 450, 500 };

        /// <summary>
        ///     This method can be used in order to filter out specific paths that we are interested,
        ///     a long with their corresponding paths.
        /// </summary>
        /// <param name="pathType"></param>
        /// <param name="topologyId"></param>
        /// <returns></returns>
        public static IEnumerable<DisplayConfigPathWrap> GetPathWraps(
            QueryDisplayFlags pathType,
            out DisplayConfigTopologyId topologyId)
        {
            topologyId = DisplayConfigTopologyId.Zero;

            int numPathArrayElements;
            int numModeInfoArrayElements;

            var status = Wrapper.GetDisplayConfigBufferSizes(
                pathType,
                out numPathArrayElements,
                out numModeInfoArrayElements);

            if (status != StatusCode.Success)
            {
                // TODO; POSSIBLY HANDLE SOME OF THE CASES.
                var reason = string.Format("GetDisplayConfigBufferSizesFailed() failed. Status: {0}", status);
                throw new Exception(reason);
            }

            var pathInfoArray = new DisplayConfigPathInfo[numPathArrayElements];
            var modeInfoArray = new DisplayConfigModeInfo[numModeInfoArrayElements];

            // topology ID only valid with QDC_DATABASE_CURRENT
            var queryDisplayStatus = pathType == QueryDisplayFlags.DatabaseCurrent
                ? Wrapper.QueryDisplayConfig(
                    pathType,
                    ref numPathArrayElements, pathInfoArray,
                    ref numModeInfoArrayElements, modeInfoArray, out topologyId)
                : Wrapper.QueryDisplayConfig(
                    pathType,
                    ref numPathArrayElements, pathInfoArray,
                    ref numModeInfoArrayElements, modeInfoArray);
            //////////////////////

            if (queryDisplayStatus != StatusCode.Success)
            {
                // TODO; POSSIBLY HANDLE SOME OF THE CASES.
                var reason = string.Format("QueryDisplayConfig() failed. Status: {0}", queryDisplayStatus);
                throw new Exception(reason);
            }

            var list = new List<DisplayConfigPathWrap>();
            foreach (var path in pathInfoArray)
            {
                var outputModes = new List<DisplayConfigModeInfo>();
                foreach (var modeIndex in new[]
                {
                    path.sourceInfo.modeInfoIdx,
                    path.targetInfo.modeInfoIdx
                })
                {
                    if (modeIndex < modeInfoArray.Length)
                        outputModes.Add(modeInfoArray[modeIndex]);
                }

                list.Add(new DisplayConfigPathWrap(path, outputModes));
            }
            return list;
        }

        /// <summary>
        ///     This method give you access to monitor device name.
        ///     Such as "\\DISPLAY1"
        /// </summary>
        /// <param name="modeInfo"></param>
        /// <param name="displayConfigSourceDeviceName"></param>
        /// <returns></returns>
        public static String GetSourceDeviceName(DisplayConfigPathSourceInfo modeInfo)
        {
            var deviceName = new DisplayConfigSourceDeviceName
            {
                header = new DisplayConfigDeviceInfoHeader
                {
                    adapterId = modeInfo.adapterId,
                    id = modeInfo.id,
                    size =
                        Marshal.SizeOf(
                            typeof (DisplayConfigSourceDeviceName)),
                    type = DisplayConfigDeviceInfoType.GetSourceName
                }
            };

            Wrapper.DisplayConfigGetDeviceInfo(ref deviceName);
            return String.IsNullOrEmpty(deviceName.viewGdiDeviceName) ? "(internal display)" : deviceName.viewGdiDeviceName;
        }
        
        public static String GetTargetDeviceName(DisplayConfigPathTargetInfo modeInfo)
        {
            var deviceName = new DisplayConfigTargetDeviceName
            {
                header = new DisplayConfigDeviceInfoHeader
                {
                    adapterId = modeInfo.adapterId,
                    id = modeInfo.id,
                    size =
                        Marshal.SizeOf(
                            typeof(DisplayConfigTargetDeviceName)),
                    type = DisplayConfigDeviceInfoType.GetTargetName
                }
            };

            Wrapper.DisplayConfigGetDeviceInfo(ref deviceName);
            return String.IsNullOrEmpty(deviceName.monitorFriendlyDeviceName) ? "(internal display)" : deviceName.monitorFriendlyDeviceName;
        }

        public static DisplayConfigDPIScalingInfo GetDPIScalingInfo(Luid adapterID, uint sourceID)
        {

            DisplayConfigDPIScalingInfo dpiInfo = new DisplayConfigDPIScalingInfo();

            var requestPacket = new DisplayConfigSourceDPIScaleGet();
            requestPacket.header.type = DisplayConfigDeviceInfoType.GetDPIScale;
            requestPacket.header.size = Marshal.SizeOf(typeof(DisplayConfigSourceDPIScaleGet));
            requestPacket.header.adapterId = adapterID;
            requestPacket.header.id = sourceID;

            var res = Wrapper.DisplayConfigGetDeviceInfo(ref requestPacket);
            if (StatusCode.Success == res)
            {//success
                if (requestPacket.curScaleRel < requestPacket.minScaleRel)
                {
                    requestPacket.curScaleRel = requestPacket.minScaleRel;
                }
                else if (requestPacket.curScaleRel > requestPacket.maxScaleRel)
                {
                    requestPacket.curScaleRel = requestPacket.maxScaleRel;
                }

                int minAbs = Math.Abs(requestPacket.minScaleRel);
                if (DpiVals.Length >= (minAbs + requestPacket.maxScaleRel + 1))
                {//all ok
                    dpiInfo.current = DpiVals[minAbs + requestPacket.curScaleRel];
                    dpiInfo.recommended = DpiVals[minAbs];
                    dpiInfo.maximum = DpiVals[minAbs + requestPacket.maxScaleRel];
                }
                else
                {
                    //Error! Probably DpiVals array is outdated
                    return dpiInfo;
                }
            }
            else
            {
                //DisplayConfigGetDeviceInfo() failed
                return dpiInfo;
            }
            return dpiInfo;
        }

        public static bool SetDPIScaling(Luid adapterID, uint sourceID, uint dpiPercentToSet)
        {
            var dPIScalingInfo = GetDPIScalingInfo(adapterID, sourceID);

            if (dpiPercentToSet == dPIScalingInfo.current)
            {
                return true;
            }

            if (dpiPercentToSet < dPIScalingInfo.mininum)
            {
                dpiPercentToSet = dPIScalingInfo.mininum;
            }
            else if (dpiPercentToSet > dPIScalingInfo.maximum)
            {
                dpiPercentToSet = dPIScalingInfo.maximum;
            }

            int idx1 = -1, idx2 = -1;

            int i = 0;
            foreach (var val in DpiVals)
    {
                if (val == dpiPercentToSet)
                {
                    idx1 = i;
                }

                if (val == dPIScalingInfo.recommended)
                {
                    idx2 = i;
                }
                i++;
            }

            if ((idx1 == -1) || (idx2 == -1))
            {
                //Error cannot find dpi value
                return false;
            }

            int dpiRelativeVal = idx1 - idx2;

            var setPacket = new DisplayConfigSourceDPIScaleSet();
            setPacket.header.type = DisplayConfigDeviceInfoType.SetDPIScale;
            setPacket.header.size = Marshal.SizeOf(typeof(DisplayConfigSourceDPIScaleSet));
            setPacket.header.adapterId = adapterID;
            setPacket.header.id = sourceID;
            setPacket.scaleRel = dpiRelativeVal;

            var res = Wrapper.DisplayConfigSetDeviceInfo(ref setPacket);
            return StatusCode.Success == res;
        }
    }
}