using System.Windows;
using SharpDX.XInput;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using System;
using System.Windows.Controls;

namespace GamepadInputViewer
{
    public partial class MainWindow : Window
    {
        Controller? controller = null;
        DeviceManager deviceManager = new DeviceManager();
        State currentState;
        State previousState;
        short triggerThreshold = 5000;
        public bool Checked { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();
            Autoconnect.DataContext = this;
            Task.Run(async () =>
            {
                await inputPolling();
            });
        }

        public async Task inputPolling()
        {
            while (controller == null)
            {
                await Task.Delay(100);
                refreshDevices();
            }
            if (controller != null)
            {
                previousState = controller.GetState();
            }
            while (true)
            {
                await Task.Delay(50);
                refreshDevices();
                if (controller != null && controller.IsConnected)
                {
                    currentState = controller.GetState();
                    if (previousState.PacketNumber != currentState.PacketNumber)
                    {
                        Trace.WriteLine(currentState.Gamepad);

                        updateGamepadView();
                    }

                    previousState = currentState;
                }
            }
        }

        private void updateDeviceView()
        {
            Device1.Dispatcher.BeginInvoke((Action)(() => Device1.Fill = new SolidColorBrush(Colors.Red)));
            Device2.Dispatcher.BeginInvoke((Action)(() => Device2.Fill = new SolidColorBrush(Colors.Red)));
            Device3.Dispatcher.BeginInvoke((Action)(() => Device3.Fill = new SolidColorBrush(Colors.Red)));
            Device4.Dispatcher.BeginInvoke((Action)(() => Device4.Fill = new SolidColorBrush(Colors.Red)));
            if (deviceManager.GetController(0) is not null && deviceManager.GetController(0)!.IsConnected)
            {
                Device1.Dispatcher.BeginInvoke((Action)(() => Device1.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (deviceManager.GetController(1) is not null && deviceManager.GetController(1)!.IsConnected)
            {
                Device2.Dispatcher.BeginInvoke((Action)(() => Device2.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (deviceManager.GetController(2) is not null && deviceManager.GetController(2)!.IsConnected)
            {
                Device3.Dispatcher.BeginInvoke((Action)(() => Device3.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (deviceManager.GetController(3) is not null && deviceManager.GetController(3)!.IsConnected)
            {
                Device4.Dispatcher.BeginInvoke((Action)(() => Device4.Fill = new SolidColorBrush(Colors.Green)));
            }
        }

        public void updateGamepadView()
        {
            //buttons
            if (currentState.Gamepad.Buttons == GamepadButtonFlags.None)
            {
                ButtonY.Dispatcher.BeginInvoke((Action)(() => ButtonY.Fill = new SolidColorBrush(Colors.Gray)));
                ButtonB.Dispatcher.BeginInvoke((Action)(() => ButtonB.Fill = new SolidColorBrush(Colors.Gray)));
                ButtonA.Dispatcher.BeginInvoke((Action)(() => ButtonA.Fill = new SolidColorBrush(Colors.Gray)));
                ButtonX.Dispatcher.BeginInvoke((Action)(() => ButtonX.Fill = new SolidColorBrush(Colors.Gray)));
                RightThumb.Dispatcher.BeginInvoke((Action)(() => RightThumb.Fill = new SolidColorBrush(Colors.Gray)));
                LeftThumb.Dispatcher.BeginInvoke((Action)(() => LeftThumb.Fill = new SolidColorBrush(Colors.Gray)));
                Start.Dispatcher.BeginInvoke((Action)(() => Start.Fill = new SolidColorBrush(Colors.Gray)));
                Back.Dispatcher.BeginInvoke((Action)(() => Back.Fill = new SolidColorBrush(Colors.Gray)));
                LeftShoulder.Dispatcher.BeginInvoke((Action)(() => LeftShoulder.Fill = new SolidColorBrush(Colors.Gray)));
                RightShoulder.Dispatcher.BeginInvoke((Action)(() => RightShoulder.Fill = new SolidColorBrush(Colors.Gray)));
                DPadDown.Dispatcher.BeginInvoke((Action)(() => DPadDown.Fill = new SolidColorBrush(Colors.Gray)));
                DPadLeft.Dispatcher.BeginInvoke((Action)(() => DPadLeft.Fill = new SolidColorBrush(Colors.Gray)));
                DPadRight.Dispatcher.BeginInvoke((Action)(() => DPadRight.Fill = new SolidColorBrush(Colors.Gray)));
                DPadUp.Dispatcher.BeginInvoke((Action)(() => DPadUp.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.Y)
            {
                ButtonY.Dispatcher.BeginInvoke((Action)(() => ButtonY.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.B)
            {
                ButtonB.Dispatcher.BeginInvoke((Action)(() => ButtonB.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.A)
            {
                ButtonA.Dispatcher.BeginInvoke((Action)(() => ButtonA.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.X)
            {
                ButtonX.Dispatcher.BeginInvoke((Action)(() => ButtonX.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.RightThumb)
            {
                RightThumb.Dispatcher.BeginInvoke((Action)(() => RightThumb.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.LeftThumb)
            {
                LeftThumb.Dispatcher.BeginInvoke((Action)(() => LeftThumb.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.Start)
            {
                Start.Dispatcher.BeginInvoke((Action)(() => Start.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.Back)
            {
                Back.Dispatcher.BeginInvoke((Action)(() => Back.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.LeftShoulder)
            {
                LeftShoulder.Dispatcher.BeginInvoke((Action)(() => LeftShoulder.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.RightShoulder)
            {
                RightShoulder.Dispatcher.BeginInvoke((Action)(() => RightShoulder.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.DPadUp)
            {
                DPadUp.Dispatcher.BeginInvoke((Action)(() => DPadUp.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.DPadRight)
            {
                DPadRight.Dispatcher.BeginInvoke((Action)(() => DPadRight.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.DPadDown)
            {
                DPadDown.Dispatcher.BeginInvoke((Action)(() => DPadDown.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.Buttons == GamepadButtonFlags.DPadLeft)
            {
                DPadLeft.Dispatcher.BeginInvoke((Action)(() => DPadLeft.Fill = new SolidColorBrush(Colors.Green)));
            }
            //triggers
            if (currentState.Gamepad.LeftTrigger == 0)
            {
                LeftTrigger.Dispatcher.BeginInvoke((Action)(() => LeftTrigger.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else
            {
                LeftTrigger.Dispatcher.BeginInvoke((Action)(() => LeftTrigger.Fill = new SolidColorBrush(Colors.Green)));
            }

            if (currentState.Gamepad.RightTrigger == 0)
            {
                RightTrigger.Dispatcher.BeginInvoke((Action)(() => RightTrigger.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else
            {
                RightTrigger.Dispatcher.BeginInvoke((Action)(() => RightTrigger.Fill = new SolidColorBrush(Colors.Green)));
            }
            //leftthumb
            if (currentState.Gamepad.LeftThumbX < triggerThreshold && currentState.Gamepad.LeftThumbX > -triggerThreshold)
            {
                LeftThumbLeft.Dispatcher.BeginInvoke((Action)(() => LeftThumbLeft.Fill = new SolidColorBrush(Colors.Gray)));
                LeftThumbRight.Dispatcher.BeginInvoke((Action)(() => LeftThumbRight.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.LeftThumbX > triggerThreshold)
            {
                LeftThumbRight.Dispatcher.BeginInvoke((Action)(() => LeftThumbRight.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.LeftThumbX < -triggerThreshold)
            {
                LeftThumbLeft.Dispatcher.BeginInvoke((Action)(() => LeftThumbLeft.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (currentState.Gamepad.LeftThumbY < triggerThreshold && currentState.Gamepad.LeftThumbY > -triggerThreshold)
            {
                LeftThumbDown.Dispatcher.BeginInvoke((Action)(() => LeftThumbDown.Fill = new SolidColorBrush(Colors.Gray)));
                LeftThumbUp.Dispatcher.BeginInvoke((Action)(() => LeftThumbUp.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.LeftThumbY > triggerThreshold)
            {
                LeftThumbUp.Dispatcher.BeginInvoke((Action)(() => LeftThumbUp.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.LeftThumbY < -triggerThreshold)
            {
                LeftThumbDown.Dispatcher.BeginInvoke((Action)(() => LeftThumbDown.Fill = new SolidColorBrush(Colors.Green)));
            }

            //rightthumb
            if (currentState.Gamepad.RightThumbX < triggerThreshold && currentState.Gamepad.RightThumbX > -triggerThreshold)
            {
                RightThumbLeft.Dispatcher.BeginInvoke((Action)(() => RightThumbLeft.Fill = new SolidColorBrush(Colors.Gray)));
                RightThumbRight.Dispatcher.BeginInvoke((Action)(() => RightThumbRight.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.RightThumbX > triggerThreshold)
            {
                RightThumbRight.Dispatcher.BeginInvoke((Action)(() => RightThumbRight.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.RightThumbX < -triggerThreshold)
            {
                RightThumbLeft.Dispatcher.BeginInvoke((Action)(() => RightThumbLeft.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (currentState.Gamepad.RightThumbY < triggerThreshold && currentState.Gamepad.RightThumbY > -triggerThreshold)
            {
                RightThumbDown.Dispatcher.BeginInvoke((Action)(() => RightThumbDown.Fill = new SolidColorBrush(Colors.Gray)));
                RightThumbUp.Dispatcher.BeginInvoke((Action)(() => RightThumbUp.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.RightThumbY > triggerThreshold)
            {
                RightThumbUp.Dispatcher.BeginInvoke((Action)(() => RightThumbUp.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.RightThumbY < -triggerThreshold)
            {
                RightThumbDown.Dispatcher.BeginInvoke((Action)(() => RightThumbDown.Fill = new SolidColorBrush(Colors.Green)));
            }
        }

        private void refreshDevices()
        {
            updateDeviceView();
            if (controller == null || controller.IsConnected == false)
            {
                if (Checked)
                {
                    controller = deviceManager.GetController();
                }
                /*                else
                                {
                                    int temp = 0;
                                    DeviceSelector.Dispatcher.BeginInvoke((Action)(() => temp = DeviceSelector.SelectedIndex));
                                    controller = deviceManager.GetController(temp);
                                }*/
            }
        }

        private void DeviceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                controller = deviceManager.GetController(((ComboBox)sender).SelectedIndex);
            }
        }
        private void CheckBox_Unchecked(object sender, EventArgs e)
        {
            controller = deviceManager.GetController(DeviceSelector.SelectedIndex);
        }
    }
}
