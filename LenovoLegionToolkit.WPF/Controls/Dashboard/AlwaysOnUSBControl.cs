using LenovoLegionToolkit.Lib;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class AlwaysOnUSBControl : AbstractComboBoxFeatureCardControl<AlwaysOnUSBState>
    {
        public AlwaysOnUSBControl()
        {
            Icon = SymbolRegular.UsbStick24;
            Title = "保持USB供电";
            Subtitle = "当笔记本关闭或睡眠时，仍可使用USB供电。";
        }
    }
}
