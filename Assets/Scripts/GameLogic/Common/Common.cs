using System;

namespace Common
{
    public class Common
    {
        public static float Negify(float number)
        {
            return (Math.Abs(number) * -1);
        }

        public static float Posify(float number)
        {
            return Math.Abs(number);
        }
        
        public static float Distance(float x1, float x2)
        {
            return Math.Abs(x1 - x2);
        }
    }
}
