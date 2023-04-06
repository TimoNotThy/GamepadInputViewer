using SharpDX.XInput;

namespace GamepadInputViewer
{
    public class DeviceManagerXInput
    {

        Controller[] controllers;

        Controller currentControllerInUse;

        int MAX_AMOUNT_OF_DEVICES = 4;
        public DeviceManagerXInput()
        {
            controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two),
                new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
            currentControllerInUse = controllers[0];

        }

        public Controller GetController(int number)
        {
            currentControllerInUse = controllers[number];
            return controllers[number];

        }

        public Controller GetController()
        {
            int i = 0;
            foreach (var controller in controllers)
            {
                if (controller.IsConnected)
                {
                    currentControllerInUse = controllers[i];
                    return controller;
                }
                i++;
            }
            currentControllerInUse = controllers[0];
            return controllers[0];
        }

        public int getAmountOfDevices()
        {
            return MAX_AMOUNT_OF_DEVICES;
        }

        public bool isControllerConnected(int index)
        {
            return GetController(index) is not null && GetController(index).IsConnected;
        }

/*        public int GetCurrentControllerInUse()
        {
            return currentControllerInUse.;
        }*/
    }
}
