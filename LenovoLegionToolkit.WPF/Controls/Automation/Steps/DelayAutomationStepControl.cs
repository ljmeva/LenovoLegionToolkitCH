using LenovoLegionToolkit.Lib.Automation;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class DelayAutomationStepControl : AbstractComboBoxAutomationStepCardControl<Delay>
    {
        public DelayAutomationStepControl(IAutomationStep<Delay> step) : base(step)
        {
            Icon = SymbolRegular.Clock24;
            Title = "延迟";
            Subtitle = "在操作之间增添间隔。";
        }
    }
}
