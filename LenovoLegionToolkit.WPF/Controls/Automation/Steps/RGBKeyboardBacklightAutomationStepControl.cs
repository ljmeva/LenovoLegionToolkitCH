using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class RGBKeyboardBacklightAutomationStepControl : AbstractComboBoxAutomationStepCardControl<RGBKeyboardBacklightPreset>
    {
        public RGBKeyboardBacklightAutomationStepControl(IAutomationStep<RGBKeyboardBacklightPreset> step) : base(step)
        {
            Icon = SymbolRegular.Keyboard24;
            Title = "键盘RGB";
            Subtitle = "更改键盘RGB亮度";
        }
    }
}
