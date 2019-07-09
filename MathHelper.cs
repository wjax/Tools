using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    public class MathHelper
    {
        public static double WrapAngle360(double angle)
        {
            return (angle + 360) % 360;
        }
        public static double WrapAngle180(double angle)
        {
            double newAngle = angle;
            while (newAngle <= -180) newAngle += 360;
            while (newAngle > 180) newAngle -= 360;
            return newAngle;
        }
    }
}
