using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;

namespace NumericalGeometryLib.GeometricAlgebra.Euclidean3D
{
    public sealed record Ega3KVector2
        : ITriplet<double>, IGeometricElement
    {
        public static Ega3KVector2 Zero { get; }
            = new Ega3KVector2(0d, 0d, 0d);

        public static Ega3KVector2 XyBivector { get; }
            = new Ega3KVector2(1d, 0d, 0d);

        public static Ega3KVector2 YxBivector { get; }
            = new Ega3KVector2(-1d, 0d, 0d);

        public static Ega3KVector2 YzBivector { get; }
            = new Ega3KVector2(0d, 1d, 0d);

        public static Ega3KVector2 ZyBivector { get; }
            = new Ega3KVector2(0d, -1d, 0d);

        public static Ega3KVector2 ZxBivector { get; }
            = new Ega3KVector2(0d, 0d, 1d);

        public static Ega3KVector2 XzBivector { get; }
            = new Ega3KVector2(0d, 0d, -1d);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 operator +(Ega3KVector2 mv1)
        {
            return mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 operator -(Ega3KVector2 mv1)
        {
            return mv1.IsZero
                ? mv1
                : new Ega3KVector2(-mv1.Scalar12, -mv1.Scalar13, -mv1.Scalar23);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 operator +(Ega3KVector2 mv1, Ega3KVector2 mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector2(
                    mv1.Scalar12 + mv2.Scalar12,
                    mv1.Scalar13 + mv2.Scalar13,
                    mv1.Scalar23 + mv2.Scalar23
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 operator -(Ega3KVector2 mv1, Ega3KVector2 mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector2(
                    mv1.Scalar12 - mv2.Scalar12,
                    mv1.Scalar13 - mv2.Scalar13,
                    mv1.Scalar23 - mv2.Scalar23
                );
        }


        public double Item1 => Scalar12;

        public double Item2 => Scalar13;

        public double Item3 => Scalar23;

        public double Xy => Scalar12;

        public double Yx => -Scalar12;

        public double Xz => Scalar13;

        public double Zx => -Scalar13;

        public double Yz => Scalar23;

        public double Zy => -Scalar23;

        public double Scalar12 { get; }

        public double Scalar13 { get; }

        public double Scalar23 { get; }
        
        public bool IsZero { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3KVector2(double xy, double xz, double yz)
        {
            Scalar12 = xy;
            Scalar13 = xz;
            Scalar23 = yz;

            IsZero = xy == 0d && 
                     xz == 0d && 
                     yz == 0d;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Scalar12.IsValid() && 
                   Scalar13.IsValid() && 
                   Scalar23.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector ToMultivector()
        {
            return new Ega3Multivector(0d, Ega3KVector1.Zero, this, 0d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({Xy}) e12 + ({Yz}) e13 + ({Zx}) e23";
        }
    }
}