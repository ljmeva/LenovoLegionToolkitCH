using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Common;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace LenovoLegionToolkit.WPF.Utils
{
    public static class MessageBoxHelper
    {
        public static Task<bool> ShowAsync(
            DependencyObject dependencyObject,
            string title,
            string message,
            string leftButton = "是",
            string rightButton = "否"
        )
        {
            return ShowAsync(Window.GetWindow(dependencyObject), title, message, leftButton, rightButton);
        }

        public static Task<bool> ShowAsync(
            Window window,
            string title,
            string message,
            string primaryButton = "是",
            string secondaryButton = "否"
        )
        {
            var tcs = new TaskCompletionSource<bool>();

            var messageBox = new MessageBox
            {
                Owner = window,
                Title = title,
                Content = new TextBlock
                {
                    Text = message,
                    TextWrapping = TextWrapping.Wrap,
                },
                ButtonLeftName = primaryButton,
                ButtonRightName = secondaryButton,
                ShowInTaskbar = false,
                Topmost = false,
                ResizeMode = ResizeMode.NoResize,
            };
            messageBox.ButtonLeftClick += (s, e) =>
            {
                tcs.SetResult(true);
                messageBox.Close();
            };
            messageBox.ButtonRightClick += (s, e) =>
            {
                tcs.SetResult(false);
                messageBox.Close();
            };
            messageBox.Closing += (s, e) =>
            {
                tcs.TrySetResult(false);
            };
            messageBox.Show();

            return tcs.Task;
        }

        public static Task<string?> ShowInputAsync(
            DependencyObject dependencyObject,
            string title,
            string? placeholder = null,
            string? text = null,
            string primaryButton = "OK",
            string secondaryButton = "取消",
            bool allowEmpty = false
        )
        {
            return ShowInputAsync(Window.GetWindow(dependencyObject), title, placeholder, text, primaryButton, secondaryButton, allowEmpty);
        }

        public static Task<string?> ShowInputAsync(
            Window window,
            string title,
            string? placeholder = null,
            string? text = null,
            string primaryButton = "OK",
            string secondaryButton = "取消",
            bool allowEmpty = false
        )
        {
            var tcs = new TaskCompletionSource<string?>();

            var textBox = new TextBox
            {
                MaxLines = 1,
                MaxLength = 50,
                PlaceholderText = placeholder,
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                SelectionStart = text?.Length ?? 0,
                SelectionLength = 0
            };
            var messageBox = new MessageBox
            {
                Owner = window,
                Title = title,
                Content = textBox,
                ButtonLeftAppearance = ControlAppearance.Transparent,
                ButtonLeftName = primaryButton,
                ButtonRightName = secondaryButton,
                ShowInTaskbar = false,
                Topmost = false,
                MinHeight = 160,
                MaxHeight = 160,
                ResizeMode = ResizeMode.NoResize,
            };

            textBox.TextChanged += (s, e) =>
            {
                var isEmpty = !allowEmpty && string.IsNullOrWhiteSpace(textBox.Text);
                messageBox.ButtonLeftAppearance = isEmpty ? ControlAppearance.Transparent : ControlAppearance.Primary;
            };
            messageBox.ButtonLeftClick += (s, e) =>
            {
                var content = textBox.Text?.Trim();
                var text = string.IsNullOrWhiteSpace(content) ? null : content;
                if (!allowEmpty && text is null)
                    return;
                tcs.SetResult(text);
                messageBox.Close();
            };
            messageBox.ButtonRightClick += (s, e) =>
            {
                tcs.SetResult("");
                messageBox.Close();
            };
            messageBox.Closing += (s, e) =>
            {
                tcs.TrySetResult("");
            };
            messageBox.Show();

            FocusManager.SetFocusedElement(window, textBox);

            return tcs.Task;
        }
    }
}
