using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.GamePadData
{
    public enum RawInputButtonFlags : ushort
    {

        A = 0x1,
        B = 0x2,
        X = 0x4,
        Y = 0x8,
        LeftBumper = 0x10,
        RightBumper = 0x20,
        Back = 0x40,
        Start = 0x80,
        LeftThumb = 0x100,
        RightThumb = 0x200,
        LeftTrigger = 0x400,
        RightTrigger = 0x800,
        DPadUp = 0x1000,
        DPadDown = 0x2000,
        DPadLeft = 0x4000,
        DPadRight = 0x8000
    }
}
