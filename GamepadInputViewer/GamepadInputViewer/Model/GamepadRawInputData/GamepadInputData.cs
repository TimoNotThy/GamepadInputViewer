

namespace GamepadInputViewer.GamePadData
{
    internal class GamepadInputData
    {
        public GamepadInputData(RawInputButtonFlags button, sbyte x, sbyte y, sbyte z, sbyte Rx, sbyte Ry, sbyte Rz)
        {
            this.button = button; 
            this.x = x;
            this.y = y;
            this.z = z;
            this.Rx = Rx;
            this.Ry = Ry;
            this.Rz = Rz;
        } 
        public RawInputButtonFlags button { get;set;}
        public sbyte x { get;set;}
        public sbyte y { get; set; }
        public sbyte z { get; set; }
        public sbyte Rx { get; set; }
        public sbyte Ry { get; set; }
        public sbyte Rz { get; set; }

        public void reset()
        {
            button = 0;
            x = 0;
            y = 0;
            z = 0;
            Rx = 0;
            Ry = 0;
            Rz = 0;
        }

    }
}
