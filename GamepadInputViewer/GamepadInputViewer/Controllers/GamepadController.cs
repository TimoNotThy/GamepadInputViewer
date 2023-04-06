using GamepadInputViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.Controllers
{
    internal class GamepadController
    {
        private DeviceManagerXInput deviceManagerXInput = new DeviceManagerXInput();
        private DeviceManagerDirectInput deviceManagerDirectInput = new DeviceManagerDirectInput();
        private InputType selectedInputType;

        public GamepadController() 
        {
            this.deviceManagerXInput = new DeviceManagerXInput();
            this.deviceManagerDirectInput = new DeviceManagerDirectInput();
            selectedInputType = InputType.DirectInput;
        }

        public GamepadBase getGamepad(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    selectedInputType = InputType.XInput;
                    return new GamepadXInput(deviceManagerXInput.GetController());
                case InputType.DirectInput:
                    selectedInputType = InputType.DirectInput;
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
                default:
                    selectedInputType = InputType.DirectInput;
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
            }
        }

        public GamepadBase getGamepad(InputType inputType, int deviceId)
        {
            switch (inputType)
            {
                case InputType.XInput:
                    selectedInputType = InputType.XInput;
                    return new GamepadXInput(deviceManagerXInput.GetController(deviceId));
                case InputType.DirectInput:
                    selectedInputType = InputType.DirectInput;
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
                default:
                    selectedInputType = InputType.DirectInput;
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
            }
        }

        public bool isControllerConnected(int deviceId)
        {
            return deviceManagerXInput.isControllerConnected(deviceId);
        }

    }
}
