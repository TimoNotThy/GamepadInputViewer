using GamepadInputViewer.GamePadData;
using GamepadInputViewer.Model;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Interop;

namespace GamepadInputViewer.Controllers
{
    internal class GamepadController
    {
        private DeviceManagerXInput deviceManagerXInput;
        private DeviceManagerDirectInput deviceManagerDirectInput;
        private DeviceManagerRawInput deviceManagerRawInput;
        private InputType inputType;
        private GamepadInputData gamepadInputData;
        private IntPtr hwnd;
        RawInputDeviceHandle selectedDevice;

        public GamepadController(IntPtr hwnd)
        {
            this.hwnd = hwnd;
            gamepadInputData = new GamepadInputData(0, 0, 0, 0, 0, 0, 0);
            this.deviceManagerXInput = new DeviceManagerXInput();
            this.deviceManagerDirectInput = new DeviceManagerDirectInput();
            this.deviceManagerRawInput = new DeviceManagerRawInput();
            inputType = InputType.RawInput;

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
                RawInputData data = RawInputData.FromHandle(lparam);
                var sourceDeviceHandle = data.Header.DeviceHandle;
                if (sourceDeviceHandle == selectedDevice)
                {
                    switch (data)
                    {
                        case RawInputHidData hid:
                            var tempArray = hid.Hid.ToStructure();

                            ushort button = BitConverter.ToUInt16(new byte[2] { tempArray[10], tempArray[9] }, 0);
                            gamepadInputData.button = (RawInputButtonFlags)button;
                            gamepadInputData.x = (sbyte)tempArray[11];
                            gamepadInputData.y = (sbyte)tempArray[12];
                            gamepadInputData.z = (sbyte)tempArray[13];
                            gamepadInputData.Rx = (sbyte)tempArray[14];
                            gamepadInputData.Ry = (sbyte)tempArray[15];
                            gamepadInputData.Rz = (sbyte)tempArray[16];

                            break;
                    }
                }
                
            }
            if (msg == 0x0219)
            {
                gamepadInputData.reset();
                deviceManagerRawInput.refreshDevices();
            }


            return IntPtr.Zero;
        }

        public GamepadBase? getGamepad(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    return new GamepadXInput(deviceManagerXInput.GetController());
                case InputType.DirectInput:
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
                case InputType.RawInput:
                    var gamepad = deviceManagerRawInput.GetController();
                    if (gamepad is not null)
                    {
                        selectedDevice = gamepad.Handle;
                    }
                    return new GamepadRawInput(gamepad, gamepadInputData);
                default:
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
            }
        }

        public GamepadBase? getGamepad(InputType inputType, int deviceId)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    return new GamepadXInput(deviceManagerXInput.GetController(deviceId));
                case InputType.DirectInput:
                    var gamepad = new GamepadDirectInput(deviceManagerDirectInput.getController(deviceId));
                    gamepad.setId(deviceId);
                    return gamepad;
                case InputType.RawInput:
                    var optionalGamepad = deviceManagerRawInput.GetController(deviceId);
                    if(optionalGamepad is not null)
                    {
                        selectedDevice = optionalGamepad.Handle;
                    }
                    var rawGamepad = new GamepadRawInput(optionalGamepad, gamepadInputData);
                    rawGamepad.setId(deviceId);
                    return rawGamepad;
                default:
                    var gamepaddef = new GamepadDirectInput(deviceManagerDirectInput.getController(deviceId));
                    gamepaddef.setId(deviceId);
                    return gamepaddef;
            }
        }

        public void setInputType(InputType type)
        {
            inputType = type;
        }

        public InputType getInputType()
        {
            return inputType;
        }

        public bool isControllerConnected(int deviceId)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    return deviceManagerXInput.isControllerConnected(deviceId);
                case InputType.DirectInput:
                    return deviceManagerDirectInput.isControllerConnected(deviceId);
                case InputType.RawInput:
                    return deviceManagerRawInput.isControllerConnected(deviceId);
            }
            return false;
        }

        public void resetRawInputData()
        {
            gamepadInputData.reset();
        }
    }
}
