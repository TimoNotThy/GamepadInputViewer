using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace GamepadInputViewer.Controllers
{
    internal class DeviceManagerDirectInput
    {
        DirectInput directInput;

        public DeviceManagerDirectInput()
        {
            directInput = new DirectInput();
        }

        public Joystick? getController()
        {
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;

            }
            if (joystickGuid != Guid.Empty)
            {
                return new Joystick(directInput, joystickGuid);
            }
                return null;
        }

        public Joystick? getController(int number)
        {
            if (directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices).Count > number)
            {
                var joystickGuid = directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices)[number].InstanceGuid;
                return new Joystick(directInput, joystickGuid);
            }
            return null;
        }

        public bool isControllerConnected(int index)
        {
            if (directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices).Count > index)
            {
                return true;
            }
            return false;
        }

    }
}
