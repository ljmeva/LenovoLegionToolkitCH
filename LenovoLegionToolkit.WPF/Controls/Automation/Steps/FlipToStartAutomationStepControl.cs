using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class FlipToStartAutomationStepControl : AbstractComboBoxAutomationStepCardControl<FlipToStartState>
    {
        public FlipToStartAutomationStepControl(IAutomationStep<FlipToStartState> step) : base(step)
        {
            Icon = SymbolRegular.Power24;
            Title = "开盖自启";
            Subtitle = "打开笔记本盖子时自动启动电脑。";
        }
    }
}
