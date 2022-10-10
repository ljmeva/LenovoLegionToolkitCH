using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Listeners;
using Wpf.Ui.Common;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class FnLockControl : AbstractToggleFeatureCardControl<FnLockState>
    {
        private readonly SpecialKeyListener _listener = IoCContainer.Resolve<SpecialKeyListener>();

        protected override FnLockState OnState => FnLockState.On;

        protected override FnLockState OffState => FnLockState.Off;

        public FnLockControl()
        {
            Icon = SymbolRegular.Keyboard24;
            Title = "Fn锁";
            Subtitle = "锁定功能键。开启后无需按下Fn可直接使用功能键。\n可使用Fn+Esc快速切换。";

            _listener.Changed += Listener_Changed;
        }

        private void Listener_Changed(object? sender, SpecialKey e) => Dispatcher.Invoke(async () =>
        {
            if (!IsLoaded || !IsVisible)
                return;

            if (e == SpecialKey.Fn_LockOn || e == SpecialKey.Fn_LockOff)
                await RefreshAsync();
        });
    }
}
