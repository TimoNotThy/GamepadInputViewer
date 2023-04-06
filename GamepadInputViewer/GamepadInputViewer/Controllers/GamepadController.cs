using GamepadInputViewer.Model;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace GamepadInputViewer.Controllers
{
    internal class GamepadController
    {
        private DeviceManagerXInput deviceManagerXInput = new DeviceManagerXInput();
        private DeviceManagerDirectInput deviceManagerDirectInput = new DeviceManagerDirectInput();
        private InputType inputType;

        public GamepadController()
        {
            this.deviceManagerXInput = new DeviceManagerXInput();
            this.deviceManagerDirectInput = new DeviceManagerDirectInput();
            inputType = InputType.XInput;
        }

        public GamepadBase getGamepad(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    return new GamepadXInput(deviceManagerXInput.GetController());
                case InputType.DirectInput:
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
                default:
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
            }
        }

        public GamepadBase getGamepad(InputType inputType, int deviceId)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    return new GamepadXInput(deviceManagerXInput.GetController(deviceId));
                case InputType.DirectInput:
                    var gamepad = new GamepadDirectInput(deviceManagerDirectInput.getController(deviceId));
                    gamepad.setId(deviceId);
                    return gamepad;
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
            }
            return false;
        }
    }
}
