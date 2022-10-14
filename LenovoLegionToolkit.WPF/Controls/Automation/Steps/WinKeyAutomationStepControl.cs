using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class WinKeyAutomationStepControl : AbstractComboBoxAutomationStepCardControl<WinKeyState>
    {
        public WinKeyAutomationStepControl(IAutomationStep<WinKeyState> step) : base(step)
        {
            Icon = SymbolRegular.Keyboard24;
            Title = "禁用Windows键";
            Subtitle = "启用或禁用Windows键。";
        }
    }
}
