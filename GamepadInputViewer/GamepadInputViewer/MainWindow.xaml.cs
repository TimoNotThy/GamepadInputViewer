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
        public MainWindow()
        {
            InitializeComponent();

            Trace.WriteLine("Start XGamepadApp");
            // Initialize XInput
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
            // Get 1st controller available

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

                        updateView();
                    }

                    previousState = currentState;
                }
            }
        }

        public void updateView()
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
            if (currentState.Gamepad.LeftThumbX < 5000 && currentState.Gamepad.LeftThumbX > -5000)
            {
                LeftThumbLeft.Dispatcher.BeginInvoke((Action)(() => LeftThumbLeft.Fill = new SolidColorBrush(Colors.Gray)));
                LeftThumbRight.Dispatcher.BeginInvoke((Action)(() => LeftThumbRight.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.LeftThumbX > 5000)
            {
                LeftThumbRight.Dispatcher.BeginInvoke((Action)(() => LeftThumbRight.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.LeftThumbX < -5000)
            {
                LeftThumbLeft.Dispatcher.BeginInvoke((Action)(() => LeftThumbLeft.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (currentState.Gamepad.LeftThumbY < 5000 && currentState.Gamepad.LeftThumbY > -5000)
            {
                LeftThumbDown.Dispatcher.BeginInvoke((Action)(() => LeftThumbDown.Fill = new SolidColorBrush(Colors.Gray)));
                LeftThumbUp.Dispatcher.BeginInvoke((Action)(() => LeftThumbUp.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.LeftThumbY > 5000)
            {
                LeftThumbUp.Dispatcher.BeginInvoke((Action)(() => LeftThumbUp.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.LeftThumbY < -5000)
            {
                LeftThumbDown.Dispatcher.BeginInvoke((Action)(() => LeftThumbDown.Fill = new SolidColorBrush(Colors.Green)));
            }

            //rightthumb
            if (currentState.Gamepad.RightThumbX < 5000 && currentState.Gamepad.RightThumbX > -5000)
            {
                RightThumbLeft.Dispatcher.BeginInvoke((Action)(() => RightThumbLeft.Fill = new SolidColorBrush(Colors.Gray)));
                RightThumbRight.Dispatcher.BeginInvoke((Action)(() => RightThumbRight.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.RightThumbX > 5000)
            {
                RightThumbRight.Dispatcher.BeginInvoke((Action)(() => RightThumbRight.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.RightThumbX < -5000)
            {
                RightThumbLeft.Dispatcher.BeginInvoke((Action)(() => RightThumbLeft.Fill = new SolidColorBrush(Colors.Green)));
            }
            if (currentState.Gamepad.RightThumbY < 5000 && currentState.Gamepad.RightThumbY > -5000)
            {
                RightThumbDown.Dispatcher.BeginInvoke((Action)(() => RightThumbDown.Fill = new SolidColorBrush(Colors.Gray)));
                RightThumbUp.Dispatcher.BeginInvoke((Action)(() => RightThumbUp.Fill = new SolidColorBrush(Colors.Gray)));
            }
            else if (currentState.Gamepad.RightThumbY > 5000)
            {
                RightThumbUp.Dispatcher.BeginInvoke((Action)(() => RightThumbUp.Fill = new SolidColorBrush(Colors.Green)));
            }
            else if (currentState.Gamepad.RightThumbY < -5000)
            {
                RightThumbDown.Dispatcher.BeginInvoke((Action)(() => RightThumbDown.Fill = new SolidColorBrush(Colors.Green)));
            }
        }

    }
}
