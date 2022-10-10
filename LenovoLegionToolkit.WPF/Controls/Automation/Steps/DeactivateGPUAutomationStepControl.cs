using LenovoLegionToolkit.Lib.Automation;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class DeactivateGPUAutomationStepControl : AbstractComboBoxAutomationStepCardControl<DeactivateGPUAutomationStepState>
    {
        public DeactivateGPUAutomationStepControl(DeactivateGPUAutomationStep step) : base(step)
        {
            Icon = SymbolRegular.DeveloperBoard24;
            Title = "禁用独显";
            Subtitle = "不用时禁用独显。\n\n警告：该功能只在独显未工作时生效。";
        }
    }
}
