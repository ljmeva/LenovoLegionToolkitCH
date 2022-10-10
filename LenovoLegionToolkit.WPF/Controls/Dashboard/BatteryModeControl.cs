using LenovoLegionToolkit.Lib;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class BatteryModeControl : AbstractComboBoxFeatureCardControl<BatteryState>
    {
        public BatteryModeControl()
        {
            Icon = SymbolRegular.BatteryCharge24;
            Title = "充电模式";
            Subtitle = "选择充电模式。";
        }
    }
}
