﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Settings;
using LenovoLegionToolkit.Lib.System;
using LenovoLegionToolkit.Lib.Utils;
using Wpf.Ui.Common;

#pragma warning disable IDE0052 // Remove unread private members

namespace LenovoLegionToolkit.WPF.Pages
{
    public partial class BatteryPage
    {
        private readonly ApplicationSettings _settings = IoCContainer.Resolve<ApplicationSettings>();

        private CancellationTokenSource? _cts;
        private Task? _refreshTask;

        public BatteryPage()
        {
            InitializeComponent();

            Loaded += BatteryPage_Loaded;
            IsVisibleChanged += BatteryPage_IsVisibleChanged;
        }

        private void BatteryPage_Loaded(object sender, RoutedEventArgs e) => Refresh();

        private void BatteryPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (IsVisible)
                Refresh();
            else
                _cts?.Cancel();
        }

        private void Refresh()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            var token = _cts.Token;

            _refreshTask = Task.Run(async () =>
            {
                if (Log.Instance.IsTraceEnabled)
                    Log.Instance.Trace($"正在更新电池信息...");

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var batteryInfo = Battery.GetBatteryInformation();
                        var powerAdapterStatus = await Power.IsPowerAdapterConnectedAsync().ConfigureAwait(false);
                        Dispatcher.Invoke(() => Set(batteryInfo, powerAdapterStatus));

                        await Task.Delay(2000, token);
                    }
                    catch (TaskCanceledException) { }
                    catch (Exception ex)
                    {
                        if (Log.Instance.IsTraceEnabled)
                            Log.Instance.Trace($"电池信息更新失败。", ex);
                    }
                }

                if (Log.Instance.IsTraceEnabled)
                    Log.Instance.Trace($"电池信息更新终止。");
            }, token);
        }

        private void Set(BatteryInformation batteryInfo, PowerAdapterStatus powerAdapterStatus)
        {
            var number = (int)Math.Round(batteryInfo.BatteryPercentage / 10.0);
            _batteryIcon.Symbol = number switch
            {
                10 => SymbolRegular.Battery1024,
                9 => SymbolRegular.Battery924,
                8 => SymbolRegular.Battery824,
                7 => SymbolRegular.Battery724,
                6 => SymbolRegular.Battery624,
                5 => SymbolRegular.Battery524,
                4 => SymbolRegular.Battery424,
                3 => SymbolRegular.Battery324,
                2 => SymbolRegular.Battery224,
                1 => SymbolRegular.Battery124,
                _ => SymbolRegular.Battery024,
            };

            _precentRemaining.Text = $"{batteryInfo.BatteryPercentage}%";
            _status.Text = GetStatusText(batteryInfo);
            _lowWattageCharger.Visibility = powerAdapterStatus == PowerAdapterStatus.ConnectedLowWattage ? Visibility.Visible : Visibility.Hidden;

            if (batteryInfo.BatteryTemperatureC is not null)
                _batteryTemperatureText.Text = GetTemperatureText(batteryInfo.BatteryTemperatureC);
            else
                _batteryTemperatureCardControl.Visibility = Visibility.Collapsed;

            if (!batteryInfo.IsCharging && batteryInfo.OnBatterySince.HasValue)
            {
                var onBatterySince = batteryInfo.OnBatterySince.Value;
                var dateText = onBatterySince.ToString("G");
                var duration = DateTime.Now.Subtract(onBatterySince);
                _onBatterySinceText.Text = $"{dateText} ({duration.Hours}h {duration.Minutes}m {duration.Seconds}s)";
            }
            else
            {
                _onBatterySinceText.Text = "-";
            }

            _batteryDischargeRateText.Text = $"{batteryInfo.DischargeRate / 1000.0:+0.00;-0.00;0.00} W";
            _batteryCapacityText.Text = $"{batteryInfo.EstimateChargeRemaining / 1000.0:0.00} Wh";
            _batteryFullChargeCapacityText.Text = $"{batteryInfo.FullChargeCapacity / 1000.0:0.00} Wh";
            _batteryDesignCapacityText.Text = $"{batteryInfo.DesignCapacity / 1000.0:0.00} Wh";

            if (batteryInfo.ManufactureDate is not null)
                _batteryManufactureDateText.Text = batteryInfo.ManufactureDate?.ToString("d") ?? "-";
            else
                _batteryManufactureDateCardControl.Visibility = Visibility.Collapsed;

            if (batteryInfo.FirstUseDate is not null)
                _batteryFirstUseDateText.Text = batteryInfo.FirstUseDate?.ToString("d") ?? "-";
            else
                _batteryFirstUseDateCardControl.Visibility = Visibility.Collapsed;

            _batteryCycleCountText.Text = $"{batteryInfo.CycleCount}";
        }

        private string GetStatusText(BatteryInformation batteryInfo)
        {
            if (batteryInfo.IsCharging)
            {
                if (batteryInfo.DischargeRate > 0)
                    return "已连接, 充电中...";

                return "已连接, 不在充电";
            }

            if (batteryInfo.BatteryLifeRemaining < 0)
                return "正在估算剩余使用时间...";

            return $"估计剩余使用时间: {GetTimeString(batteryInfo.BatteryLifeRemaining)}";
        }

        private static string GetTimeString(int seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            var result = string.Empty;

            var hours = timeSpan.Hours;
            if (hours > 0)
                result += $"{hours}h ";

            var minutes = timeSpan.Minutes;
            result += $"{minutes}min";

            return result;
        }

        private string GetTemperatureText(double? temperature)
        {
            _batteryTemperatureCardControl.Tag = temperature;

            if (temperature is null)
                return "—";

            if (_settings.Store.TemperatureUnit == TemperatureUnit.F)
            {
                temperature *= 9.0 / 5.0;
                temperature += 32;
                return $"{temperature:0.0} °F";
            }


            return $"{temperature:0.0} °C";
        }

        private void BatteryTemperatureCardControl_Click(object sender, RoutedEventArgs e)
        {
            _settings.Store.TemperatureUnit = _settings.Store.TemperatureUnit == TemperatureUnit.C ? TemperatureUnit.F : TemperatureUnit.C;
            _settings.SynchronizeStore();

            var temperature = (sender as FrameworkElement)?.Tag as double?;
            _batteryTemperatureText.Text = GetTemperatureText(temperature);
        }
    }
}
