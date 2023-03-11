using System.Windows;
using SharpDX.XInput;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using System;

namespace GamepadInputViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller? controller = null;
        State currentState;
        State previousState;
        short triggerThreshold = 5000;
        public MainWindow()
        {
            InitializeComponent();

            Trace.WriteLine("Start XGamepadApp");
            // Initialize XInput
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
            // Get 1st controller available
            updateDeviceView(controllers);

            foreach (var selectControler in controllers)
            {
                if (selectControler.IsConnected)
                {
                    controller = selectControler;
                    break;
                }
            }
            Task.Factory.StartNew(() => inputPolling());
            Trace.WriteLine("End XGamepadApp");

        }


        public void inputPolling()
        {
            if (controller == null)
            {
                Trace.WriteLine("No XInput controller installed");
            }
            else
            {
                Trace.WriteLine("Found a XInput controller available");
                Trace.WriteLine("Press buttons on the controller to display events or escape key to exit... ");
                previousState = controller.GetState();
                while (controller.IsConnected)
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

        private void updateDeviceView(Controller?[] controllers)
        {
                if (controllers[0].IsConnected)
                {
                    Device1.Dispatcher.BeginInvoke((Action)(() => Device1.Fill = new SolidColorBrush(Colors.Green)));
                }
            if (controllers[1].IsConnected)
            {
                Device2.Dispatcher.BeginInvoke((Action)(() => Device2.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (controllers[2].IsConnected)
            {
                Device3.Dispatcher.BeginInvoke((Action)(() => Device3.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (controllers[3].IsConnected)
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

    }
}
