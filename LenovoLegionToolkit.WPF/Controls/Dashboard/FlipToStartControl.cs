using LenovoLegionToolkit.Lib;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class FlipToStartControl : AbstractToggleFeatureCardControl<FlipToStartState>
    {
        protected override FlipToStartState OnState => FlipToStartState.On;

        protected override FlipToStartState OffState => FlipToStartState.Off;

        public FlipToStartControl()
        {
            Icon = SymbolRegular.Power24;
            Title = "开盖启动";
            Subtitle = "翻开笔记本盖子自动开机。";
        }
    }
}
