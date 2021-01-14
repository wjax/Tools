using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    public class MathHelper
    {
        public static double WrapAngle360(double angle)
        {
            return ModuloAbs((angle + 360) , 360);
        }
        
        public static double WrapAngle180(double angle)
        {
            double newAngle = angle;
            while (newAngle <= -180) newAngle += 360;
            while (newAngle > 180) newAngle -= 360;
            return newAngle;
        }

        public static double ModuloAbs(double a, double n)
        {
            return (a % n + n) % n;
        }

        public static double DeltaAngles(double a, double b)
        {
            double diff = a - b;
            diff = ModuloAbs((diff + 180), 360) - 180;

            return -diff;
        }
    }
}
