using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LenovoLegionToolkit.Lib.Automation.Steps;
using Wpf.Ui.Common;
using NumberBox = Wpf.Ui.Controls.NumberBox;

namespace LenovoLegionToolkit.WPF.Controls.Automation.Steps
{
    public class DisplayBrightnessAutomationStepControl : AbstractAutomationStepControl<DisplayBrightnessAutomationStep>
    {
        private readonly NumberBox _brightness = new()
        {
            Width = 150,
            IntegersOnly = true,
            ClearButtonEnabled = false,
            Min = 0,
            Max = 100,
            Step = 5,
        };

        private readonly Grid _grid = new();

        public DisplayBrightnessAutomationStepControl(DisplayBrightnessAutomationStep step) : base(step)
        {
            Icon = SymbolRegular.BrightnessHigh48;
            Title = "屏幕亮度";
            Subtitle = "更改屏幕亮度。\n切换性能模式也会改变点亮，请将该操作放到最后一部。\n\n警告：显示器关闭时可能无法更改亮度。";
        }

        public override IAutomationStep CreateAutomationStep() => new DisplayBrightnessAutomationStep((int)_brightness.Value);

        protected override UIElement? GetCustomControl()
        {
            _brightness.TextChanged += (s, e) => RaiseChanged();
            _grid.Children.Add(_brightness);
            return _grid;
        }

        protected override void OnFinishedLoading() { }

        protected override Task RefreshAsync()
        {
            _brightness.Value = AutomationStep.Brightness;
            return Task.CompletedTask;
        }
    }
}
