using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk3D
{
    public static class IcantMath
    {
        public static float DegreeToRadian(float degree)
        {
            return degree * (float)Math.PI / 180f;
        }
    }
}
