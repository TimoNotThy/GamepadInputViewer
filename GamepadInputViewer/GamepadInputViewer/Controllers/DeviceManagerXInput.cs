using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;

namespace GamepadInputViewer
{
    public class DeviceManagerXInput
    {

        Controller[] controllers;

        int? currentControllerInUse = null;

        int MAX_AMOUNT_OF_DEVICES = 4;
        public DeviceManagerXInput()
        {
            controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two),
                new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
        }

        public Controller? GetController(int number)
        {
            if (controllers != null && number >= 0 && number <= 3)
            {
                if (controllers[number].IsConnected)
                {
                    currentControllerInUse = number;
                    return controllers[number];
                }
            }
            currentControllerInUse = null;
            return null;
        }

        public Controller? GetController()
        {
            if (controllers != null)
            {
                int i = 0;
                foreach (var controller in controllers)
                {
                    if (controller.IsConnected)
                    {
                        currentControllerInUse = i;
                        return controller;
                    }
                    i++;
                }
            }
            currentControllerInUse = null;
            return null;
        }

        public int getAmountOfDevices()
        {
            return MAX_AMOUNT_OF_DEVICES;
        }

        public bool isControllerConnected(int index)
        {
            return GetController(index) is not null && GetController(index).IsConnected;
        }

        public int? GetCurrentControllerInUse()
        {
            return currentControllerInUse;
        }
    }
}
