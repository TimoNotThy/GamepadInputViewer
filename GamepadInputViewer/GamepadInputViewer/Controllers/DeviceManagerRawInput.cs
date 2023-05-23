using System.Collections.Generic;
using System.Diagnostics;
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
            if (rawInputGamepads.Count > 0)
            {
                return rawInputGamepads[0];
            }
            else
            {
                return null;
            }
        }

        public bool isControllerConnected(int index)
        {
            //Trace.WriteLine(rawInputGamepads[0]?.IsConnected+ "rawInputGamepads[1]?.IsConnected+ rawInputGamepads[0]?.IsConnected+ rawInputGamepads[0]?.IsConnected)
            //if (rawInputGamepads[index]?.IsConnected == true) {
            if (true) { 
                return true;
            }
            else
            {

                return false;
            }
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

    }


}
