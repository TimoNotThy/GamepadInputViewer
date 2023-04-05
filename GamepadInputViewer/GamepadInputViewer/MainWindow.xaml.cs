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
using System.Windows.Shapes;

namespace GamepadInputViewer
{
    public partial class MainWindow : Window
    {
        Color BUTTON_PRESSED = Colors.Green;
        Color BUTTON_NOT_PRESSED = Colors.Gray;
        Color DEVICE_ACTIVE = Colors.Green;
        Color DEVICE_NOT_ACTIVE = Colors.Red;
        int MAX_AXIS = 32768;
        int THUMB_PADDING = 22;
        Tuple<double, double> leftThumbPosition;
        Tuple<double, double> rightThumbPosition;
        GamepadBase gamepad = null;
        Controller? controller = null;
        DeviceManagerXInput deviceManager = new DeviceManagerXInput();
        State currentState;
        State previousState;
        public bool Checked { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();
            leftThumbPosition = new Tuple<double, double>(LeftThumbPos.Margin.Top, LeftThumbPos.Margin.Left);
            rightThumbPosition = new Tuple<double, double>(RightThumbPos.Margin.Top, RightThumbPos.Margin.Left);
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
            updateDeviceColor(0, Device1);
            updateDeviceColor(1, Device2);
            updateDeviceColor(2, Device3);
            updateDeviceColor(3, Device4);
        }

        public void updateGamepadView()
        {
            updateButtonColor(gamepad.isTopButtonPressed(), ButtonY);
            updateButtonColor(gamepad.isRightButtonPressed(), ButtonB);
            updateButtonColor(gamepad.isBottomButtonPressed(), ButtonA);
            updateButtonColor(gamepad.isLeftButtonPressed(), ButtonX);
            updateButtonColor(gamepad.isStartButtonPressed(), Start);
            updateButtonColor(gamepad.isBackButtonPressed(), Back);
            updateButtonColor(gamepad.isDPadDownPressed(), DPadDown);
            updateButtonColor(gamepad.isDPadLeftPressed(), DPadLeft);
            updateButtonColor(gamepad.isDPadRightPressed(), DPadRight);
            updateButtonColor(gamepad.isDPadUpPressed(), DPadUp);
            updateButtonColor(gamepad.isLeftJoystickPressed(), LeftThumb);
            updateButtonColor(gamepad.isRightJoystickPressed(), RightThumb);
            updateButtonColor(gamepad.isLeftBumperPressed(), LeftShoulder);
            updateButtonColor(gamepad.isRightBumperPressed(), RightShoulder);
            updateTriggerColor(gamepad.getLeftTrigger(), LeftTrigger); 
            updateTriggerColor(gamepad.getRightTrigger(), RightTrigger);

            updateLeftThumbPos();
            updateRightThumbPos();

        }

        private void updateLeftThumbPos()
        {
            int devider = MAX_AXIS / THUMB_PADDING;
            var thumbAxes = gamepad.getLeftJoystickAxes();
            int displacementXAxis = thumbAxes.Item1 / devider;
            displacementXAxis = (displacementXAxis > THUMB_PADDING) ? THUMB_PADDING : displacementXAxis;
            int displacementYAxis = thumbAxes.Item2 / devider;
            displacementYAxis = (displacementYAxis > THUMB_PADDING) ? THUMB_PADDING : displacementYAxis;
            LeftThumbPos.Dispatcher.BeginInvoke((Action)(() => LeftThumbPos.Margin = new Thickness(leftThumbPosition.Item1 + displacementXAxis, leftThumbPosition.Item2 - displacementYAxis, 0, 0)));
        }

        private void updateRightThumbPos()
        {
            int devider = MAX_AXIS / THUMB_PADDING;
            var thumbAxes = gamepad.getRightJoystickAxes();
            int displacementXAxis = thumbAxes.Item1 / devider;
            displacementXAxis = (displacementXAxis > THUMB_PADDING) ? THUMB_PADDING : displacementXAxis;
            int displacementYAxis = thumbAxes.Item2 / devider;
            displacementYAxis = (displacementYAxis > THUMB_PADDING) ? THUMB_PADDING : displacementYAxis;
            RightThumbPos.Dispatcher.BeginInvoke((Action)(() => RightThumbPos.Margin = new Thickness(rightThumbPosition.Item2 + displacementXAxis, rightThumbPosition.Item1 - displacementYAxis, 0, 0)));
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

        private void paintElipse (Ellipse elipse, Color color)
        {
            elipse.Dispatcher.BeginInvoke((Action)(() => elipse.Fill = new SolidColorBrush(color)));
        }

        private void updateButtonColor(bool isActive, Ellipse elipse)
        {
            if (isActive)
            {
                paintElipse(elipse, BUTTON_PRESSED);
            }
            else
            {
                paintElipse(elipse, BUTTON_NOT_PRESSED);
            }
        }

        private void updateTriggerColor(int value, Ellipse elipse)
        {
            if (value > Gamepad.TriggerThreshold)
            {
                paintElipse(elipse, BUTTON_PRESSED);
            }
            else
            {
                paintElipse(elipse, BUTTON_NOT_PRESSED);
            }
        }

        private void updateDeviceColor(int deviceId, Ellipse elipse)
        {
            if (deviceManager.isControllerConnected(deviceId))
            {
                paintElipse(elipse, DEVICE_ACTIVE);
            }
            else
            {
                paintElipse(elipse, DEVICE_NOT_ACTIVE);
            }
        }
    }
}
