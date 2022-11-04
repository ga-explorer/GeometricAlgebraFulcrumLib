namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems
{
    public static class SkrMapUtils
    {
        public static double[] SkrRotate(this double[] uVector)
        {
            var n = uVector.Length;
            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a = 0d;
            for (var i = 0; i < n - 1; i++)
                a += uVector[i];
            a /= 1d + nSqrt;

            var un = uVector[^1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            for (var i = 0; i < n; i++)
                vVector[i] = uVector[i] + k;
            vVector[^1] -= m;

            return vVector;
        }

        public static double[] SkrRotate3D(this double[] uVector)
        {
            const int n = 3;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k - m;

            return vVector;
        }

        public static double[] SkrRotate4D(this double[] uVector)
        {
            const int n = 4;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k - m;

            return vVector;
        }

        public static double[] SkrRotate5D(this double[] uVector)
        {
            const int n = 5;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k - m;

            return vVector;
        }

        public static double[] SkrRotate6D(this double[] uVector)
        {
            const int n = 6;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k - m;

            return vVector;
        }

        public static double[] SkrRotate7D(this double[] uVector)
        {
            const int n = 7;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k - m;

            return vVector;
        }

        public static double[] SkrRotate8D(this double[] uVector)
        {
            const int n = 8;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k - m;

            return vVector;
        }

        public static double[] SkrRotate9D(this double[] uVector)
        {
            const int n = 9;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k - m;

            return vVector;
        }

        public static double[] SkrRotate10D(this double[] uVector)
        {
            const int n = 10;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k - m;

            return vVector;
        }

        public static double[] SkrRotate11D(this double[] uVector)
        {
            const int n = 11;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k - m;

            return vVector;
        }

        public static double[] SkrRotate12D(this double[] uVector)
        {
            const int n = 12;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k - m;

            return vVector;
        }

        public static double[] SkrRotate13D(this double[] uVector)
        {
            const int n = 13;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k - m;

            return vVector;
        }

        public static double[] SkrRotate14D(this double[] uVector)
        {
            const int n = 14;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k - m;

            return vVector;
        }

        public static double[] SkrRotate15D(this double[] uVector)
        {
            const int n = 15;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k - m;

            return vVector;
        }

        public static double[] SkrRotate16D(this double[] uVector)
        {
            const int n = 16;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k - m;

            return vVector;
        }

        public static double[] SkrRotate17D(this double[] uVector)
        {
            const int n = 17;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k - m;

            return vVector;
        }

        public static double[] SkrRotate18D(this double[] uVector)
        {
            const int n = 18;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k - m;

            return vVector;
        }

        public static double[] SkrRotate19D(this double[] uVector)
        {
            const int n = 19;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k - m;

            return vVector;
        }

        public static double[] SkrRotate20D(this double[] uVector)
        {
            const int n = 20;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k - m;

            return vVector;
        }

        public static double[] SkrRotate21D(this double[] uVector)
        {
            const int n = 21;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k - m;

            return vVector;
        }

        public static double[] SkrRotate22D(this double[] uVector)
        {
            const int n = 22;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k - m;

            return vVector;
        }

        public static double[] SkrRotate23D(this double[] uVector)
        {
            const int n = 23;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k - m;

            return vVector;
        }

        public static double[] SkrRotate24D(this double[] uVector)
        {
            const int n = 24;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k - m;

            return vVector;
        }

        public static double[] SkrRotate25D(this double[] uVector)
        {
            const int n = 25;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k - m;

            return vVector;
        }

        public static double[] SkrRotate26D(this double[] uVector)
        {
            const int n = 26;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k - m;

            return vVector;
        }

        public static double[] SkrRotate27D(this double[] uVector)
        {
            const int n = 27;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k - m;

            return vVector;
        }

        public static double[] SkrRotate28D(this double[] uVector)
        {
            const int n = 28;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k - m;

            return vVector;
        }

        public static double[] SkrRotate29D(this double[] uVector)
        {
            const int n = 29;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k - m;

            return vVector;
        }

        public static double[] SkrRotate30D(this double[] uVector)
        {
            const int n = 30;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k - m;

            return vVector;
        }

        public static double[] SkrRotate31D(this double[] uVector)
        {
            const int n = 31;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k - m;

            return vVector;
        }

        public static double[] SkrRotate32D(this double[] uVector)
        {
            const int n = 32;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k - m;

            return vVector;
        }

        public static double[] SkrRotate33D(this double[] uVector)
        {
            const int n = 33;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k - m;

            return vVector;
        }

        public static double[] SkrRotate34D(this double[] uVector)
        {
            const int n = 34;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k - m;

            return vVector;
        }

        public static double[] SkrRotate35D(this double[] uVector)
        {
            const int n = 35;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k - m;

            return vVector;
        }

        public static double[] SkrRotate36D(this double[] uVector)
        {
            const int n = 36;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k - m;

            return vVector;
        }

        public static double[] SkrRotate37D(this double[] uVector)
        {
            const int n = 37;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k - m;

            return vVector;
        }

        public static double[] SkrRotate38D(this double[] uVector)
        {
            const int n = 38;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k - m;

            return vVector;
        }

        public static double[] SkrRotate39D(this double[] uVector)
        {
            const int n = 39;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k - m;

            return vVector;
        }

        public static double[] SkrRotate40D(this double[] uVector)
        {
            const int n = 40;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k - m;

            return vVector;
        }

        public static double[] SkrRotate41D(this double[] uVector)
        {
            const int n = 41;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k - m;

            return vVector;
        }

        public static double[] SkrRotate42D(this double[] uVector)
        {
            const int n = 42;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k - m;

            return vVector;
        }

        public static double[] SkrRotate43D(this double[] uVector)
        {
            const int n = 43;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41] +
                uVector[42];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k;
            vVector[42] = uVector[42] + k - m;

            return vVector;
        }

        public static double[] SkrRotate44D(this double[] uVector)
        {
            const int n = 44;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41] +
                uVector[42] +
                uVector[43];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k;
            vVector[42] = uVector[42] + k;
            vVector[43] = uVector[43] + k - m;

            return vVector;
        }

        public static double[] SkrRotate45D(this double[] uVector)
        {
            const int n = 45;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41] +
                uVector[42] +
                uVector[43] +
                uVector[44];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k;
            vVector[42] = uVector[42] + k;
            vVector[43] = uVector[43] + k;
            vVector[44] = uVector[44] + k - m;

            return vVector;
        }

        public static double[] SkrRotate46D(this double[] uVector)
        {
            const int n = 46;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41] +
                uVector[42] +
                uVector[43] +
                uVector[44] +
                uVector[45];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k;
            vVector[42] = uVector[42] + k;
            vVector[43] = uVector[43] + k;
            vVector[44] = uVector[44] + k;
            vVector[45] = uVector[45] + k - m;

            return vVector;
        }

        public static double[] SkrRotate47D(this double[] uVector)
        {
            const int n = 47;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41] +
                uVector[42] +
                uVector[43] +
                uVector[44] +
                uVector[45] +
                uVector[46];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k;
            vVector[42] = uVector[42] + k;
            vVector[43] = uVector[43] + k;
            vVector[44] = uVector[44] + k;
            vVector[45] = uVector[45] + k;
            vVector[46] = uVector[46] + k - m;

            return vVector;
        }

        public static double[] SkrRotate48D(this double[] uVector)
        {
            const int n = 48;

            var nSqrt = Math.Sqrt(n);
            var vVector = new double[n];

            var a =
                uVector[0] +
                uVector[1] +
                uVector[2] +
                uVector[3] +
                uVector[4] +
                uVector[5] +
                uVector[6] +
                uVector[7] +
                uVector[8] +
                uVector[9] +
                uVector[10] +
                uVector[11] +
                uVector[12] +
                uVector[13] +
                uVector[14] +
                uVector[15] +
                uVector[16] +
                uVector[17] +
                uVector[18] +
                uVector[19] +
                uVector[20] +
                uVector[21] +
                uVector[22] +
                uVector[23] +
                uVector[24] +
                uVector[25] +
                uVector[26] +
                uVector[27] +
                uVector[28] +
                uVector[29] +
                uVector[30] +
                uVector[31] +
                uVector[32] +
                uVector[33] +
                uVector[34] +
                uVector[35] +
                uVector[36] +
                uVector[37] +
                uVector[38] +
                uVector[39] +
                uVector[40] +
                uVector[41] +
                uVector[42] +
                uVector[43] +
                uVector[44] +
                uVector[45] +
                uVector[46] +
                uVector[47];
            a /= 1d + nSqrt;

            var un = uVector[n - 1];
            var k = (un - a) / nSqrt;
            var m = un + a;

            vVector[0] = uVector[0] + k;
            vVector[1] = uVector[1] + k;
            vVector[2] = uVector[2] + k;
            vVector[3] = uVector[3] + k;
            vVector[4] = uVector[4] + k;
            vVector[5] = uVector[5] + k;
            vVector[6] = uVector[6] + k;
            vVector[7] = uVector[7] + k;
            vVector[8] = uVector[8] + k;
            vVector[9] = uVector[9] + k;
            vVector[10] = uVector[10] + k;
            vVector[11] = uVector[11] + k;
            vVector[12] = uVector[12] + k;
            vVector[13] = uVector[13] + k;
            vVector[14] = uVector[14] + k;
            vVector[15] = uVector[15] + k;
            vVector[16] = uVector[16] + k;
            vVector[17] = uVector[17] + k;
            vVector[18] = uVector[18] + k;
            vVector[19] = uVector[19] + k;
            vVector[20] = uVector[20] + k;
            vVector[21] = uVector[21] + k;
            vVector[22] = uVector[22] + k;
            vVector[23] = uVector[23] + k;
            vVector[24] = uVector[24] + k;
            vVector[25] = uVector[25] + k;
            vVector[26] = uVector[26] + k;
            vVector[27] = uVector[27] + k;
            vVector[28] = uVector[28] + k;
            vVector[29] = uVector[29] + k;
            vVector[30] = uVector[30] + k;
            vVector[31] = uVector[31] + k;
            vVector[32] = uVector[32] + k;
            vVector[33] = uVector[33] + k;
            vVector[34] = uVector[34] + k;
            vVector[35] = uVector[35] + k;
            vVector[36] = uVector[36] + k;
            vVector[37] = uVector[37] + k;
            vVector[38] = uVector[38] + k;
            vVector[39] = uVector[39] + k;
            vVector[40] = uVector[40] + k;
            vVector[41] = uVector[41] + k;
            vVector[42] = uVector[42] + k;
            vVector[43] = uVector[43] + k;
            vVector[44] = uVector[44] + k;
            vVector[45] = uVector[45] + k;
            vVector[46] = uVector[46] + k;
            vVector[47] = uVector[47] + k - m;

            return vVector;
        }
    }
}
