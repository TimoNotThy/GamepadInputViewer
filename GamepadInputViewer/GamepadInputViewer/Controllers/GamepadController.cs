using GamepadInputViewer.GamePadData;
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
        private GamepadInputData gamepadInputData;

        public GamepadController(GamepadInputData gamepadInputData)
        {
            this.gamepadInputData = gamepadInputData;
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
                    return new GamepadRawInput(deviceManagerRawInput.GetController(), gamepadInputData);
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
                    var rawGamepad = new GamepadRawInput(deviceManagerRawInput.GetController(deviceId), gamepadInputData);
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
