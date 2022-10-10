using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{

    public class WhiteKeyboardBacklightAutomationStepControl : AbstractComboBoxAutomationStepCardControl<WhiteKeyboardBacklightState>
    {
        public WhiteKeyboardBacklightAutomationStepControl(IAutomationStep<WhiteKeyboardBacklightState> step) : base(step)
        {
            Icon = SymbolRegular.Keyboard24;
            Title = "键盘背光";
            Subtitle = "更改键盘背光亮度。";
        }
    }
}
