using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Util
{
    public static bool FloatEquals(float f1, float f2)
    {
        return Math.Abs(f1 - f2) < 0.01f;
    }
}

