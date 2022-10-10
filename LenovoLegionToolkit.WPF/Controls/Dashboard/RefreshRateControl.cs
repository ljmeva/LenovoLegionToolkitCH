using System;
using System.Threading.Tasks;
using System.Windows;
using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Listeners;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class RefreshRateControl : AbstractComboBoxFeatureCardControl<RefreshRate>
    {
        private readonly DisplayConfigurationListener _listener = IoCContainer.Resolve<DisplayConfigurationListener>();

        public RefreshRateControl()
        {
            Icon = SymbolRegular.Laptop24;
            Title = "屏幕刷新率";
            Subtitle = "更改屏幕刷新率。";

            _listener.Changed += Listener_Changed;
        }

        protected override async Task OnRefreshAsync()
        {
            await base.OnRefreshAsync();

            if (_comboBox.Items.Count < 2)
                Visibility = Visibility.Collapsed;
            else
                Visibility = Visibility.Visible;
        }

        private void Listener_Changed(object? sender, EventArgs e) => Dispatcher.Invoke(async () =>
        {
            if (IsLoaded)
                await RefreshAsync();
        });
    }
}
