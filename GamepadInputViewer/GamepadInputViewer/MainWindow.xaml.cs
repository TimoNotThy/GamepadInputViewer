using System.Windows;
using SharpDX.XInput;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using System;
using System.Windows.Controls;
using SharpDX.DirectInput;
using GamepadInputViewer.Controllers;
using GamepadInputViewer.Model;

namespace GamepadInputViewer
{
    public partial class MainWindow : Window
    {
        GamepadBase gamepad = null;
        Controller? controller = null;
        DeviceManagerXInput deviceManager = new DeviceManagerXInput();
        State currentState;
        State previousState;
        short triggerThreshold = 5000;
        public bool Checked { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();
            gamepad = new GamePadXInput(deviceManager.GetController());
            Autoconnect.DataContext = this;
            Task.Run(async () =>
            {

                await inputPolling();
            });
        }

        static void MainForJoystick()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Trace.WriteLine("No joystick/Gamepad found.");
            }

            // Instantiate the joystick
            var joystick = new Joystick(directInput, joystickGuid);

            Trace.WriteLine("Found Joystick/Gamepad with GUID: " + joystickGuid);

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Trace.WriteLine("Effect available " + effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();

            // Poll events from joystick
            while (true)
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                var buttons = joystick.GetCurrentState().Buttons;
                Trace.WriteLine(joystick.GetCurrentState().PointOfViewControllers.GetValue(0));
                /*                foreach (var data in datas)
                                {
                                    Trace.WriteLine(data);
                                }*/
                /*                for (int i = 0; i < buttons.Length; i++)
                                {
                                    if (buttons[i] == true)
                                        Trace.WriteLine(i);
                                }*/

            }
        }

        public async Task inputPolling()
        {
            while (controller == null)
            {
                await Task.Delay(100);
                gamepad = (GamepadBase)new GamePadXInput(deviceManager.GetController());
                refreshDevices();
            }
            if (controller != null)
            {
                previousState = controller.GetState();
            }
            while (true)
            {
                Trace.WriteLine(currentState.Gamepad.Buttons);
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
            if (deviceManager.isControllerConnected(0))
            {
                paintElipse(Device1, Colors.Green);
            }
            else
            {
                paintElipse(Device1, Colors.Red);
            }
            if (deviceManager.isControllerConnected(1))
            {
                paintElipse(Device2, Colors.Green);
            }
            else
            {
                paintElipse(Device2, Colors.Red);
            }
            if (deviceManager.isControllerConnected(2))
            {
                paintElipse(Device3, Colors.Green);
            }
            else
            {
                paintElipse(Device3, Colors.Red);
            }
            if (deviceManager.isControllerConnected(3))
            {
                paintElipse(Device4, Colors.Green);
            }
            else
            {
                paintElipse(Device4, Colors.Red);
            }
        }

        public void updateGamepadView()
        {
            //buttons
            if (gamepad.isTopButtonPressed())
            {
                paintElipse(ButtonY, Colors.Green);
            } else
            {
                paintElipse(ButtonY, Colors.Gray);
            }

            if (gamepad.isRightButtonPressed())
            {
                paintElipse(ButtonB, Colors.Green);
            }
            else
            {
                paintElipse(ButtonB, Colors.Gray);
            }

            if (gamepad.isBottomButtonPressed())
            {
                paintElipse(ButtonA, Colors.Green);
            }
            else
            {
                paintElipse(ButtonA, Colors.Gray);
            }

            if (gamepad.isLeftButtonPressed())
            {
                paintElipse(ButtonX, Colors.Green);
            }
            else
            {
                paintElipse(ButtonX, Colors.Gray);
            }

            if (currentState.Gamepad.Buttons == GamepadButtonFlags.B)
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
                    gamepad = new GamePadXInput(deviceManager.GetController());
                    controller = deviceManager.GetController();
                }
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

        private void paintElipse(System.Windows.Shapes.Ellipse elipse, System.Windows.Media.Color color)
        {
            elipse.Dispatcher.BeginInvoke((Action)(() => elipse.Fill = new SolidColorBrush(color)));
        }
    }
}
