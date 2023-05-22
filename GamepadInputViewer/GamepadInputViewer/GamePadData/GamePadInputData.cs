using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.GamePadData
{
    internal class GamepadInputData
    {
        public GamepadInputData(RawInputButton1Flags button1, RawInputButton2Flags button2, sbyte x, sbyte y, sbyte z, sbyte Rx, sbyte Ry, sbyte Rz)
        {
            this.button1 = button1; 
            this.button2 = button2;
            this.x = x;
            this.y = y;
            this.z = z;
            this.Rx = Rx;
            this.Ry = Ry;
            this.Rz = Rz;
        } 
        public RawInputButton1Flags button1 { get;set;}
        public RawInputButton2Flags button2 { get;set;}
        public sbyte x { get;set;}
        public sbyte y { get; set; }
        public sbyte z { get; set; }
        public sbyte Rx { get; set; }
        public sbyte Ry { get; set; }
        public sbyte Rz { get; set; }

    }
}
