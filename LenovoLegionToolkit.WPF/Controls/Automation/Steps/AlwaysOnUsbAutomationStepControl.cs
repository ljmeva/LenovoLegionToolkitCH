using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class AlwaysOnUsbAutomationStepControl : AbstractComboBoxAutomationStepCardControl<AlwaysOnUSBState>
    {
        public AlwaysOnUsbAutomationStepControl(IAutomationStep<AlwaysOnUSBState> step) : base(step)
        {
            Icon = SymbolRegular.UsbStick24;
            Title = "保持USB供电";
            Subtitle = "关机或睡眠保持USB向外供电";
        }
    }
}
