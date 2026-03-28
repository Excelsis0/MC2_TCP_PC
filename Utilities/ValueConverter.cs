using System;

namespace MC2_TCP_PC.Utilities
{
    public static class ValueConverter
    {
        public static bool StringToTypedValue(string value)
        {
            // Implement your conversion logic here
            return bool.Parse(value);
        }

        public static int ToBool(bool value)
        {
            return value ? 1 : 0;
        }

        public static int ToInt(string value)
        {
            // Implement your conversion logic here
            return int.Parse(value);
        }

        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public static float ParseAndClamp(string value, float min, float max)
        {
            float result;
            if (float.TryParse(value, out result))
            {
                return Math.Clamp(result, min, max);
            }
            return min; // or handle as desired
        }

        public static float Normalize(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}