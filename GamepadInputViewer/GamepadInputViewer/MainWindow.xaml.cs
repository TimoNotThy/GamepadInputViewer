using System.Windows;
using SharpDX.XInput;
using System.Threading.Tasks;
using System.Windows.Media;
using System;
using System.Windows.Controls;
using GamepadInputViewer.Controllers;
using GamepadInputViewer.Model;
using System.Windows.Shapes;
using SharpDX;
using System.Windows.Interop;
using Linearstar.Windows.RawInput;
using GamepadInputViewer.GamePadData;

namespace GamepadInputViewer
{
    public partial class MainWindow : Window
    {
        Color BUTTON_PRESSED = Colors.Green;
        Color BUTTON_NOT_PRESSED = Colors.Gray;
        Color DEVICE_ACTIVE = Colors.Green;
        Color DEVICE_NOT_ACTIVE = Colors.Red;
        int MAX_AXIS = 32768;
        int THUMB_PADDING = 20;
        Tuple<double, double> leftThumbPosition;
        Tuple<double, double> rightThumbPosition;
        GamepadBase? gamepad;
        GamepadController gamepadController;
        bool selectionChanged = false;
        public bool Checked { get; set; } = true;
        GamepadInputData gamePadInputData;
        public MainWindow()
        {
            gamePadInputData = new GamepadInputData(0,0,0,0,0,0,0,0);
            gamepadController = new GamepadController(gamePadInputData);
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            leftThumbPosition = new Tuple<double, double>(LeftThumbPos.Margin.Left, LeftThumbPos.Margin.Top);
            rightThumbPosition = new Tuple<double, double>(RightThumbPos.Margin.Left, RightThumbPos.Margin.Top);
            gamepad = gamepadController.getGamepad(gamepadController.getInputType());
            Autoconnect.DataContext = this;
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(25);
                    if (selectionChanged)
                    {
                        if (Checked == true)
                        {
                            gamepad = gamepadController.getGamepad(gamepadController.getInputType());
                        }
                        else if (Checked == false)
                        {
                            var selectedIndex = 0;
                            DeviceSelector.Dispatcher.Invoke(() => selectedIndex = DeviceSelector.SelectedIndex);
                            gamepad = gamepadController.getGamepad(gamepadController.getInputType(), selectedIndex);
                        }
                        selectionChanged = false;
                    }
                    updateGamepadView();
                    updateDeviceView();
                }
            });
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            var windowInteropHelper = new WindowInteropHelper(this);
            var hwnd = windowInteropHelper.Handle;

            RawInputDevice.RegisterDevice(HidUsageAndPage.GamePad,
                RawInputDeviceFlags.InputSink, hwnd);

            HwndSource source = HwndSource.FromHwnd(hwnd);
            source.AddHook(Hook);
        }

        private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            const int WM_INPUT = 0x00FF;

            if (msg == WM_INPUT)
            {
                var data = RawInputData.FromHandle(lparam);

                var sourceDeviceHandle = data.Header.DeviceHandle;
                var sourceDevice = data.Device;

                switch (data)
                {
                    case RawInputHidData hid:
                        var tempArray = (sbyte[])(Array)hid.Hid.ToStructure();
                        gamePadInputData.button1 = (RawInputButton1Flags)tempArray[9];
                        gamePadInputData.button2 = (RawInputButton2Flags)tempArray[10];
                        gamePadInputData.x = tempArray[11];
                        gamePadInputData.y = tempArray[12];
                        gamePadInputData.z = tempArray[13];
                        gamePadInputData.Rx = tempArray[14];
                        gamePadInputData.Ry = tempArray[15];
                        gamePadInputData.Rz = tempArray[16];
                        
                        break;
                }
            }

            return IntPtr.Zero;
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
            if (gamepad != null)
            {
                if (gamepadController.isControllerConnected(gamepad.getId()))
                {
                    try
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
                        updateLeftThumbPosition(gamepad.getLeftJoystickAxes());
                        updateRightThumbPosition(gamepad.getRightJoystickAxes());

                    }
                    catch (SharpDXException e)
                    {
                        selectionChanged = true;
                    }
                }
                else
                {
                    selectionChanged = true;
                }

            }
        }

        private void updateLeftThumbPosition(Tuple<int, int> axes)
        {
            if (Math.Abs(axes.Item1) > Gamepad.LeftThumbDeadZone || Math.Abs(axes.Item2) > Gamepad.LeftThumbDeadZone)
            {
                int devider = (MAX_AXIS - Gamepad.LeftThumbDeadZone) / THUMB_PADDING;
                int displacementXAxis = axes.Item1 / devider;
                int displacementYAxis = axes.Item2 / devider;
                LeftThumbPos.Dispatcher.BeginInvoke((Action)(() =>
                LeftThumbPos.Margin = new Thickness(leftThumbPosition.Item1 + displacementXAxis, leftThumbPosition.Item2 - displacementYAxis, 0, 0)));
            }
            else
            {
                LeftThumbPos.Dispatcher.BeginInvoke((Action)(() =>
                LeftThumbPos.Margin = new Thickness(leftThumbPosition.Item1, leftThumbPosition.Item2, 0, 0)));
            }
        }

        private void updateRightThumbPosition(Tuple<int, int> axes)
        {
            if (Math.Abs(axes.Item1) > Gamepad.RightThumbDeadZone || Math.Abs(axes.Item2) > Gamepad.RightThumbDeadZone)
            {
                int devider = (MAX_AXIS - Gamepad.RightThumbDeadZone) / THUMB_PADDING;
                int displacementXAxis = axes.Item1 / devider;
                int displacementYAxis = axes.Item2 / devider;
                RightThumbPos.Dispatcher.BeginInvoke((Action)(() =>
                RightThumbPos.Margin = new Thickness(rightThumbPosition.Item1 + displacementXAxis, rightThumbPosition.Item2 - displacementYAxis, 0, 0)));
            }
            else
            {
                RightThumbPos.Dispatcher.BeginInvoke((Action)(() =>
                    RightThumbPos.Margin = new Thickness(rightThumbPosition.Item1, rightThumbPosition.Item2, 0, 0)));
            }
        }

        private void DeviceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged = true;
        }
        private void CheckBox_Unchecked(object sender, EventArgs e)
        {
            selectionChanged = true;
            DeviceSelector.IsEnabled = true;
        }

        private void CheckBox_Checked(object sender, EventArgs e)
        {
            selectionChanged = true;
            DeviceSelector.IsEnabled = false;
        }

        private void paintElipse(Ellipse elipse, Color color)
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
            if (gamepadController.isControllerConnected(deviceId))
            {
                paintElipse(elipse, DEVICE_ACTIVE);
            }
            else
            {
                paintElipse(elipse, DEVICE_NOT_ACTIVE);
            }
        }

        private void InputSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged = true;
            if (((ComboBox)sender).SelectedIndex == 1)
            {
                gamepadController.setInputType(InputType.XInput);
            }
            else if (((ComboBox)sender).SelectedIndex == 0)
            {
                gamepadController.setInputType(InputType.DirectInput);
            }
            else
            {
                gamepadController.setInputType(InputType.RawInput);
            }

        }
    }
}
