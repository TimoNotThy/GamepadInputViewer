using SharpDX.XInput;

namespace GamepadInputViewer
{
    public class DeviceManagerXInput
    {

        Controller[] controllers;

        public DeviceManagerXInput()
        {
            controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two),
                new Controller(UserIndex.Three), new Controller(UserIndex.Four) };

        }

        public Controller? GetController(int number)
        {
            if (controllers.Length > number)
            {
                return controllers[number];
            }
            return null;
        }

        public Controller? GetController()
        {
            int i = 0;
            foreach (var controller in controllers)
            {
                if (controller.IsConnected)
                {
                    return controller;
                }
                i++;
            }
            return null;
        }

        public bool isControllerConnected(int index)
        {
            if (controllers.Length > index)
            {
                var gamepad = GetController(index);
                if (gamepad is not null)
                {
                    return gamepad.IsConnected;
                }
                else return false;
            }
            return false;
        }

    }
}
