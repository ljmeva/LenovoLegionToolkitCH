using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class OverDriveAutomationStepControl : AbstractComboBoxAutomationStepCardControl<OverDriveState>
    {
        public OverDriveAutomationStepControl(IAutomationStep<OverDriveState> step) : base(step)
        {
            Icon = SymbolRegular.TopSpeed24;
            Title = "Over Drive";
            Subtitle = "提高屏幕反应速度。";
        }
    }
}
