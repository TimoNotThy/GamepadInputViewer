using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.GamePadData
{
    public enum RawInputButton1Flags : byte
    {
        A = 0x1,
        B = 0x2,
        X = 0x4,
        Y = 0x8,
        LeftBumper = 0x10,
        RightBumper = 0x20,
        Back = 0x40,
        Start = 0x80
    }
}
