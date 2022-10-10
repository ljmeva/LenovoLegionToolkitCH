using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Extensions;
using LenovoLegionToolkit.Lib.Features;
using LenovoLegionToolkit.Lib.System;
using LenovoLegionToolkit.Lib.Utils;
using LenovoLegionToolkit.WPF.Utils;
using LenovoLegionToolkit.WPF.Windows.Dashboard;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Button = Wpf.Ui.Controls.Button;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class HybridModeControl : ContentControl
    {
        public HybridModeControl()
        {
            Initialized += HybridModeControl_Initialized;
        }

        private async void HybridModeControl_Initialized(object? sender, EventArgs e)
        {
            var mi = await Compatibility.GetMachineInformation();
            if (mi.Properties.SupportsExtendedHybridMode)
                Content = new ComboBoxHybridModeControl();
            else
                Content = new ToggleHybridModeControl();
        }
    }

    public class ComboBoxHybridModeControl : AbstractComboBoxFeatureCardControl<HybridModeState>
    {
        private readonly Button _infoButton = new()
        {
            Icon = SymbolRegular.Info24,
            FontSize = 20,
            Margin = new(8, 0, 0, 0),
        };

        public ComboBoxHybridModeControl()
        {
            Icon = SymbolRegular.LeafOne24;
            Title = "GPU工作模式";
            Subtitle = "根据使用情况选择GPU工作模式。\n开关独显直连需要重启。";
        }

        protected override FrameworkElement? GetAccessory(ComboBox comboBox)
        {
            comboBox.Width = 180;

            _infoButton.Click += InfoButton_Click;

            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(_infoButton);

            return stackPanel;
        }

        protected override async Task OnStateChange(ComboBox comboBox, IFeature<HybridModeState> feature, HybridModeState? newValue, HybridModeState? oldValue)
        {
            if (newValue is null || oldValue is null)
                return;

            await base.OnStateChange(comboBox, feature, newValue, oldValue);

            if (newValue != HybridModeState.Off && oldValue != HybridModeState.Off)
                return;

            var result = await MessageBoxHelper.ShowAsync(
                this,
                "需要重启",
                $"应用 {newValue.GetDisplayName()} 需要重启。是否立即重启？",
                "重启",
                "稍后");

            if (result)
                await Power.RestartAsync();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ExtendedHybridModeInfoWindow
            {
                Owner = Window.GetWindow(this),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ShowInTaskbar = false,
            };
            window.ShowDialog();
        }
    }

    public class ToggleHybridModeControl : AbstractToggleFeatureCardControl<HybridModeState>
    {
        protected override HybridModeState OnState => HybridModeState.On;

        protected override HybridModeState OffState => HybridModeState.Off;

        public ToggleHybridModeControl()
        {
            Icon = SymbolRegular.LeafOne24;
            Title = "混合模式";
            Subtitle = "独显核显混合使用。\n需要重启。";
        }

        protected override async Task OnStateChange(ToggleSwitch toggle, IFeature<HybridModeState> feature)
        {
            await base.OnStateChange(toggle, feature);

            var result = await MessageBoxHelper.ShowAsync(
                this,
                "需要重启",
                "更改到混合模式需要重启。是否立即重启？",
                "重启",
                "稍后");

            if (result)
                await Power.RestartAsync();
        }
    }
}
