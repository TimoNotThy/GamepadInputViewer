using GamepadInputViewer.Model;

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
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
                default:
                    return new GamepadDirectInput(deviceManagerDirectInput.getController());
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
            return deviceManagerXInput.isControllerConnected(deviceId);
        }

    }
}
