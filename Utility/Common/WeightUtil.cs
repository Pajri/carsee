using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CarSee.Utility.Common
{
    public class WeightUtil
    {
        public static bool IsValueBetweenDouble(string valRange, double val)
        {
            string[] valSplit = valRange.Split('-');
            double valMin = double.Parse(valSplit[0], CultureInfo.InvariantCulture);
            double valMax = double.Parse(valSplit[1], CultureInfo.InvariantCulture);

            if (val >= valMin && val <= valMax) return true;
            return false;
        }

        public static bool IsValueBetweenLong(string valRange, long val)
        {
            string[] valSplit = valRange.Split('-');
            long valMin = long.Parse(valSplit[0]);
            long valMax = long.Parse(valSplit[1]);

            if (val >= valMin && val <= valMax) return true;
            return false;
        }

        public static bool IsValueBetweenInt(string valRange, int val)
        {
            string[] valSplit = valRange.Split('-');
            int valMin = int.Parse(valSplit[0]);
            int valMax = int.Parse(valSplit[1]);

            if (val >= valMin && val <= valMax) return true;
            return false;
        }

        public static bool IsValueBetweenFloat(string valRange, float val)
        {
            string[] valSplit = valRange.Split('-');
            float valMin = float.Parse(valSplit[0], CultureInfo.InvariantCulture);
            float valMax = float.Parse(valSplit[1], CultureInfo.InvariantCulture);

            if (val >= valMin && val <= valMax) return true;
            return false;
        }
    }
}
