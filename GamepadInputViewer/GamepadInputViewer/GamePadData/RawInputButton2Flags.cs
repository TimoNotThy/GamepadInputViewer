using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.GamePadData
{
    public enum RawInputButton2Flags : byte
    {
        LeftThumb = 0x1,
        RightThumb = 0x2,
        LeftTrigger = 0x4,
        RightTrigger = 0x8,
        DPadUp = 0x10,
        DPadDown = 0x20,
        DPadLeft = 0x40,
        DPadRight = 0x80
    }
}
