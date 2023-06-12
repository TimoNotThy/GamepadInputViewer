using GamepadInputViewer.GamePadData;
using GamepadInputViewer.Model;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;
using System;
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


                            if (data.Device?.ManufacturerName == "BtGamepad") { 
                            var tempArray = hid.Hid.ToStructure();
                            ushort button = BitConverter.ToUInt16(new byte[2] { tempArray[9], tempArray[10] }, 0);
                            gamepadInputData.button = (RawInputButtonFlags)button;
                            gamepadInputData.x = (sbyte)tempArray[11];
                            gamepadInputData.y = (sbyte)tempArray[12];
                            gamepadInputData.z = (sbyte)tempArray[13];
                            gamepadInputData.Rx = (sbyte)tempArray[14];
                            gamepadInputData.Ry = (sbyte)tempArray[15];
                            gamepadInputData.Rz = (sbyte)tempArray[16];
                            }
                            else if (data.Device?.ProductId == 654)
                            {
                                var tempArray = hid.Hid.ToStructure();
                                byte tempButtons = (byte)(tempArray[20] & 0x3);
                                if (tempArray[18] < 127)
                                {
                                    tempButtons = (byte)(tempButtons ^ 0x8);
                                }
                                if (tempArray[18] > 129)
                                {
                                    tempButtons = (byte)(tempButtons ^ 0x4);
                                }

                                if ((tempArray[20] & 0x4) == 0x4)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x10);
                                }
                                if ((tempArray[20] & 0x8) == 0x8)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x90);
                                }
                                if ((tempArray[20] & 0xC) == 0xC)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x80);
                                }
                                if ((tempArray[20] & 0x10) == 0x10)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0xA0);
                                }
                                if ((tempArray[20] & 0x14) == 0x14)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x20);
                                }
                                if ((tempArray[20] & 0x18) == 0x18)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x60);
                                }
                                if ((tempArray[20] & 0x1C) == 0x1C)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x40);
                                }
                                if ((tempArray[20] & 0x20) == 0x20)
                                {
                                    tempButtons = (byte)(tempButtons & 0x00);
                                    tempButtons = (byte)(tempButtons ^ 0x50);
                                }
                                ushort button = BitConverter.ToUInt16(new byte[2] { tempArray[19], tempButtons }, 0);
                                gamepadInputData.button = (RawInputButtonFlags)button;
                                ushort leftX = BitConverter.ToUInt16(new byte[2] { tempArray[9], tempArray[10] }, 0);
                                ushort leftY = BitConverter.ToUInt16(new byte[2] { tempArray[11], tempArray[12] }, 0);
                                ushort rightX = BitConverter.ToUInt16(new byte[2] { tempArray[13], tempArray[14] }, 0);
                                ushort rightY = BitConverter.ToUInt16(new byte[2] { tempArray[15], tempArray[16] }, 0);
                                gamepadInputData.x = (sbyte)(leftX/256 +128);
                                gamepadInputData.y = (sbyte)(leftY/256 + 128);
                                gamepadInputData.z = (sbyte)(rightX/256 + 128);
                                gamepadInputData.Rx = (sbyte)(rightY/256 + 128);
                                gamepadInputData.Ry = 0;
                                gamepadInputData.Rz = 0;

                            }
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

        public IGamepad? getGamepad(InputType inputType)
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

        public IGamepad? getGamepad(InputType inputType, int deviceId)
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
