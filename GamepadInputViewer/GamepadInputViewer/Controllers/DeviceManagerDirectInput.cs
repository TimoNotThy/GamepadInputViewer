using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.Controllers
{
    internal class DeviceManagerDirectInput
    {
        DirectInput directInput;

        public DeviceManagerDirectInput()
        {
            directInput = new DirectInput();
        }

        public Joystick getController()
        {
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            return new Joystick(directInput, joystickGuid);
        }
        
        public void MainForJoystick()
        {
            // Initialize DirectInput

            // Find a Joystick Guid
/*            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;*/

            // If Gamepad not found, look for a Joystick
/*            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;*/

            // If Joystick not found, throws an error
 /*           if (joystickGuid == Guid.Empty)
            {
                Trace.WriteLine("No joystick/Gamepad found.");
            }*/

            // Instantiate the joystick
            var joystick = new Joystick(directInput, Guid.Empty);

/*            Trace.WriteLine("Found Joystick/Gamepad with GUID: " + joystickGuid);*/

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Trace.WriteLine("Effect available " + effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            //joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            //joystick.Acquire();

            // Poll events from joystick
            while (true)
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                var buttons = joystick.GetCurrentState().Buttons;
                Trace.WriteLine(joystick.GetCurrentState().PointOfViewControllers.GetValue(0));
                foreach (var data in datas)
                {
                    Trace.WriteLine(data);
                }

                /*                for (int i = 0; i < buttons.Length; i++)
                                {
                                    if (buttons[i] == true)
                                        Trace.WriteLine(i);
                                }*/

            }
        }
    }
}
