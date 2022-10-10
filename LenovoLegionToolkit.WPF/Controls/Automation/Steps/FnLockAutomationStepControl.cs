using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class FnLockAutomationStepControl : AbstractComboBoxAutomationStepCardControl<FnLockState>
    {
        public FnLockAutomationStepControl(IAutomationStep<FnLockState> step) : base(step)
        {
            Icon = SymbolRegular.Keyboard24;
            Title = "Fn锁";
            Subtitle = "切换Fn键的功能。";
        }
    }
}
