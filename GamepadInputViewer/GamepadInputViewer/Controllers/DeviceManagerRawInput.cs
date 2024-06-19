using System.Collections.Generic;
using Linearstar.Windows.RawInput;

namespace GamepadInputViewer.Controllers
{
    internal class DeviceManagerRawInput
    {
        List<RawInputDevice?> rawInputGamepads;
        public DeviceManagerRawInput()
        {
            rawInputGamepads = new List<RawInputDevice?>();
            addConnectedDevices();
        }

        public RawInputDevice? GetController(int number)
        {
            if (number < rawInputGamepads.Count)
            {
                return rawInputGamepads[number];
            }
            else
            {
                return null;
            }
        }

        public RawInputDevice? GetController()
        {
            foreach(var gamepad in rawInputGamepads)
            {
                if(gamepad != null)
                {
                    return gamepad;
                }
            }
            return null;
        }

        public bool isControllerConnected(int index)
        {
            return rawInputGamepads[index] != null;
        }

        private void addConnectedDevices()
        {
            var devices = RawInputDevice.GetDevices();
            foreach (var device in devices)
            {
                if (device.UsageAndPage == HidUsageAndPage.GamePad)
                {
                    rawInputGamepads.Add(device);
                }
            }
            if (rawInputGamepads.Count < 4)
            {
                for (int i = rawInputGamepads.Count; i < 4; i++)
                {
                    rawInputGamepads.Add(null);
                }
            }
        }

        internal void refreshDevices()
        {
            rawInputGamepads.Clear();
            addConnectedDevices();
        }

    }


}
