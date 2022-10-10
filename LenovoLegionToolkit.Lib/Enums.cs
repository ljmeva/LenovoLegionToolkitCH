using System.ComponentModel.DataAnnotations;

namespace LenovoLegionToolkit.Lib
{
    public enum AlwaysOnUSBState
    {
        [Display(Name = "禁用")]
        Off,
        [Display(Name = "睡眠时可用")]
        OnWhenSleeping,
        [Display(Name = "睡眠和关机时可用")]
        OnAlways
    }


    public enum BatteryState
    {
        [Display(Name = "养护模式")]
        Conservation,
        [Display(Name = "正常模式")]
        Normal,
        [Display(Name = "快充模式")]
        RapidCharge
    }

    public enum DriverKey
    {
        Fn_F4 = 256,
        Fn_F8 = 8192,
        Fn_F10 = 32
    }

    public enum FanTableType
    {
        [Display(Name = "未知")]
        Unknown,
        CPU,
        GPU,
        CPUSensor
    }

    public enum FlipToStartState
    {
        [Display(Name = "关闭")]
        Off,
        [Display(Name = "打开")]
        On
    }

    public enum FnLockState
    {
        [Display(Name = "关闭")]
        Off,
        [Display(Name = "打开")]
        On
    }

    public enum GSyncState
    {
        [Display(Name = "打开")]
        On,
        [Display(Name = "关闭")]
        Off
    }

    public enum HybridModeState
    {
        [Display(Name = "混合模式", Description = "自动切换独显和核显")]
        On,
        [Display(Name = "核显直连", Description = "仅使用核显")]
        OnIGPUOnly,
        [Display(Name = "自动模式", Description = "按需自动打开混合模式，不使用显卡时打开核显模式")]
        OnAuto,
        [Display(Name = "独显直连", Description = "仅使用独显，需重启")]
        Off
    }

    public enum IGPUModeState
    {
        [Display(Name = "默认")]
        Default,
        [Display(Name = "仅使用核显")]
        IGPUOnly,
        [Display(Name = "自动")]
        Auto
    }

    public enum KnownFolder
    {
        Contacts,
        Downloads,
        Favorites,
        Links,
        SavedGames,
        SavedSearches
    }

    public enum NotificationDuration
    {
        Short,
        Long
    }

    public enum NotificationIcon
    {
        [Display(Name = "麦克风已禁用")]
        MicrophoneOff,
        [Display(Name = "麦克风已启用")]
        MicrophoneOn,
        [Display(Name = "刷新率")]
        RefreshRate,
        [Display(Name = "触控板已启用")]
        TouchpadOn,
        [Display(Name = "触控板已禁用")]
        TouchpadOff,
        [Display(Name = "摄像头已启用")]
        CameraOn,
        [Display(Name = "摄像头已禁用")]
        CameraOff
    }

    public enum OS
    {
        [Display(Name = "Windows 11")]
        Windows11,
        [Display(Name = "Windows 10")]
        Windows10,
        [Display(Name = "Windows 8")]
        Windows8,
        [Display(Name = "Windows 7")]
        Windows7
    }

    public enum OverDriveState
    {
        [Display(Name = "关闭")]
        Off,
        [Display(Name = "打开")]
        On
    }

    public enum PowerAdapterStatus
    {
        [Display(Name = "已连接")]
        Connected,
        [Display(Name = "低功率连接")]
        ConnectedLowWattage,
        [Display(Name = "未连接")]
        Disconnected
    }

    public enum PowerModeState
    {
        [Display(Name = "安静模式")]
        Quiet,
        [Display(Name = "均衡模式")]
        Balance,
        [Display(Name = "野兽模式")]
        Performance,
        [Display(Name = "自定义模式")]
        GodMode = 254
    }

    public enum ProcessEventInfoType
    {
        [Display(Name = "开启")]
        Started,
        [Display(Name = "禁用")]
        Stopped
    }

    public enum RGBKeyboardBacklightChanged { }

    public enum RGBKeyboardBrightness
    {
        [Display(Name = "低亮度")]
        Low,
        [Display(Name = "高亮度")]
        High
    }

    public enum RGBKeyboardEffect
    {
        [Display(Name = "静态")]
        Static,
        [Display(Name = "呼吸")]
        Breath,
        [Display(Name = "平滑")]
        Smooth,
        [Display(Name = "从右往做")]
        WaveRTL,
        [Display(Name = "从左往右")]
        WaveLTR
    }

    public enum RGBKeyboardBacklightPreset
    {
        Off = -1,
        [Display(Name = "预设 1")]
        One = 0,
        [Display(Name = "预设 2")]
        Two = 1,
        [Display(Name = "预设 3")]
        Three = 2
    }

    public enum RBGKeyboardSpeed
    {
        [Display(Name = "最慢")]
        Slowest,
        [Display(Name = "慢")]
        Slow,
        [Display(Name = "快")]
        Fast,
        [Display(Name = "最快")]
        Fastest
    }

    public enum SoftwareStatus
    {
        [Display(Name = "启动")]
        Enabled,
        [Display(Name = "禁用")]
        Disabled,
        [Display(Name = "未找到")]
        NotFound
    }

    public enum SpecialKey
    {
        Unknown = 0,
        Fn_F9 = 1,
        Fn_LockOn = 2,
        Fn_LockOff = 3,
        Fn_PrtSc = 4,
        CameraOn = 12,
        CameraOff = 13,
        Fn_R = 16,
        Fn_R_2 = 0x0041002A
    }

    public enum Theme
    {
        [Display(Name = "跟随系统")]
        System,
        [Display(Name = "浅色模式")]
        Light,
        [Display(Name = "深色模式")]
        Dark
    }

    public enum TemperatureUnit
    {
        C,
        F
    }

    public enum TouchpadLockState
    {
        [Display(Name = "关闭")]
        Off,
        [Display(Name = "打开")]
        On
    }

    public enum WhiteKeyboardBacklightChanged { }

    public enum WhiteKeyboardBacklightState
    {
        [Display(Name = "关闭")]
        Off,
        [Display(Name = "低亮度")]
        Low,
        [Display(Name = "高亮度")]
        High
    }

    public enum WinKeyState
    {
        [Display(Name = "关闭")]
        Off,
        [Display(Name = "打开")]
        On
    }

    public enum WinKeyChanged { }
}
