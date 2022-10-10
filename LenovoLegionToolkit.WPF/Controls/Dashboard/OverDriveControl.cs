using LenovoLegionToolkit.Lib;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class OverDriveControl : AbstractToggleFeatureCardControl<OverDriveState>
    {
        protected override OverDriveState OnState => OverDriveState.On;

        protected override OverDriveState OffState => OverDriveState.Off;

        public OverDriveControl()
        {
            Icon = SymbolRegular.TopSpeed24;
            Title = "Over Drive";
            Subtitle = "提高屏幕响应速度。";
        }
    }
}
