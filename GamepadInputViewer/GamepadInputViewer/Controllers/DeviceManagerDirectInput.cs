using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace GamepadInputViewer.Controllers
{
    internal class DeviceManagerDirectInput
    {
        DirectInput directInput;
        List<Guid> joysticks;

        public DeviceManagerDirectInput()
        {
            directInput = new DirectInput();
            joysticks = new List<Guid>(4);   
        }

        public Joystick? getController()
        {
            joysticks.Clear();
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                joysticks.Add(deviceInstance.InstanceGuid);

            }
            while(joysticks.Count < 4)
            {
                joysticks.Add(Guid.Empty); 
            }
            if (joystickGuid != Guid.Empty)
            {
                return new Joystick(directInput, joystickGuid);
            }
                return null;
        }

        public Joystick? getController(int number)
        {
            joysticks.Clear();
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joysticks.Add(deviceInstance.InstanceGuid);
            }
            while (joysticks.Count < 4)
            {
                joysticks.Add(Guid.Empty);
            }
            if (joysticks[number] != Guid.Empty)
            {
                return new Joystick(directInput, joysticks[number]);
            }
            return null;
        }

        public bool isControllerConnected(int index)
        {
            if (joysticks[index] != Guid.Empty)
            {
                return true;
            }
            return false;
        }

    }
}
