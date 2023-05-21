using GamepadInputViewer.Model;
using System;
using System.Collections.Generic;

namespace GamepadInputViewer.Controllers
{
    internal class GamepadController
    {
        private DeviceManagerXInput deviceManagerXInput;
        private DeviceManagerDirectInput deviceManagerDirectInput;
        private DeviceManagerRawInput deviceManagerRawInput;
        private InputType inputType;
        private List<sbyte> rawInputData;

        public GamepadController(List<sbyte> rawInputData)
        {
            this.rawInputData = rawInputData;
            this.deviceManagerXInput = new DeviceManagerXInput();
            this.deviceManagerDirectInput = new DeviceManagerDirectInput();
            this.deviceManagerRawInput = new DeviceManagerRawInput();
            inputType = InputType.XInput;
            
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
                    return new GamepadRawInput(deviceManagerRawInput.GetController(), rawInputData);
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
                    var rawGamepad = new GamepadRawInput(deviceManagerRawInput.GetController(deviceId), rawInputData);
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
    }
}
