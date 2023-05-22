﻿using System.Collections.Generic;
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
                for(int i = rawInputGamepads.Count; i < 4; i++)
                {
                    rawInputGamepads.Add(null);
                }
            }

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
            if (rawInputGamepads[index] == null) {
                return false;
            }
            else
            {
                return true;
            }
        }


    }


}
