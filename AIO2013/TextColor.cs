using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIO2013
{
    static class TextColor
    {
        internal static int Counter = 0;
        public static Color CurrentColor;

        internal static Color[] ColorPalet = new Color[] { Color.Yellow, Color.Lime, Color.Orange, Color.Aqua };
        

        public static void NextColor()
        {

            if (Counter < 4)
            {
                CurrentColor = ColorPalet[Counter];
                Counter++;
            }
            else
            {
                Counter = 0;
                CurrentColor = ColorPalet[Counter];
                
            }
            
        }


    }
}
