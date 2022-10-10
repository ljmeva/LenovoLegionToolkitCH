using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Listeners;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class WinKeyControl : AbstractToggleFeatureCardControl<WinKeyState>
    {
        private readonly WinKeyListener _listener = IoCContainer.Resolve<WinKeyListener>();

        protected override WinKeyState OnState => WinKeyState.On;

        protected override WinKeyState OffState => WinKeyState.Off;

        protected override bool DisablesWhileRefreshing => false;

        public WinKeyControl()
        {
            Icon = SymbolRegular.Keyboard24;
            Title = "锁定Windows键";
            Subtitle = "禁用键盘上的Windows徽标键";

            _listener.Changed += Listener_Changed;
        }

        private void Listener_Changed(object? sender, WinKeyChanged e) => Dispatcher.Invoke(async () =>
        {
            if (!IsLoaded || !IsVisible)
                return;

            await RefreshAsync();
        });
    }
}
