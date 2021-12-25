using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.GeometricAlgebra.Euclidean3D
{
    public sealed record Ega3KVector1 :
        ITuple3D
    {
        public static Ega3KVector1 Zero { get; }
            = new Ega3KVector1(0d, 0d, 0d);

        public static Ega3KVector1 UnitXAxis { get; }
            = new Ega3KVector1(1d, 0d, 0d);
        
        public static Ega3KVector1 UnitXAxisNegative { get; }
            = new Ega3KVector1(-1d, 0d, 0d);

        public static Ega3KVector1 UnitYAxis { get; }
            = new Ega3KVector1(0d, 1d, 0d);
        
        public static Ega3KVector1 UnitYAxisNegative { get; }
            = new Ega3KVector1(0d, -1d, 0d);

        public static Ega3KVector1 UnitZAxis { get; }
            = new Ega3KVector1(0d, 0d, 1d);
        
        public static Ega3KVector1 UnitZAxisNegative { get; }
            = new Ega3KVector1(0d, 0d, -1d);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator +(Ega3KVector1 mv1)
        {
            return mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator -(Ega3KVector1 mv1)
        {
            return mv1.IsZero
                ? mv1
                : new Ega3KVector1(-mv1.Scalar1, -mv1.Scalar2, -mv1.Scalar3);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(Ega3KVector1 mv1, double mv2)
        {
            return new Ega3Multivector(
                mv2,                
                mv1,
                Ega3KVector2.Zero, 
                Ega3KVector3.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(double mv1, Ega3KVector1 mv2)
        {
            return new Ega3Multivector(
                mv1,
                mv2,
                Ega3KVector2.Zero, 
                Ega3KVector3.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator +(Ega3KVector1 mv1, Ega3KVector1 mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector1(
                    mv1.Scalar1 + mv2.Scalar1,
                    mv1.Scalar2 + mv2.Scalar2,
                    mv1.Scalar3 + mv2.Scalar3
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(Ega3KVector1 mv1, double mv2)
        {
            return new Ega3Multivector(
                -mv2,                
                mv1,
                Ega3KVector2.Zero, 
                Ega3KVector3.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(double mv1, Ega3KVector1 mv2)
        {
            return new Ega3Multivector(
                mv1,
                -mv2,
                Ega3KVector2.Zero, 
                Ega3KVector3.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator -(Ega3KVector1 mv1, Ega3KVector1 mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector1(
                    mv1.Scalar1 - mv2.Scalar1,
                    mv1.Scalar2 - mv2.Scalar2,
                    mv1.Scalar3 - mv2.Scalar3
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator *(Ega3KVector1 mv1, double mv2)
        {
            return new Ega3KVector1(
                mv1.X * mv2,
                mv1.Y * mv2,
                mv1.Z * mv2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator *(double mv1, Ega3KVector1 mv2)
        {
            return new Ega3KVector1(
                mv1 * mv2.X,
                mv1 * mv2.Y,
                mv1 * mv2.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator *(Ega3KVector1 mv1, Ega3KVector1 mv2)
        {
            return new Ega3Multivector(
                mv1.Sp(mv2),
                Zero,
                mv1.Op(mv1),
                Ega3KVector3.Zero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator /(Ega3KVector1 mv1, double mv2)
        {
            return new Ega3KVector1(
                mv1.X / mv2,
                mv1.Y / mv2,
                mv1.Z / mv2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 operator /(double mv1, Ega3KVector1 mv2)
        {
            var s = mv1 / mv2.NormSquared();

            return new Ega3KVector1(
                s * mv2.X,
                s * mv2.Y,
                s * mv2.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator /(Ega3KVector1 mv1, Ega3KVector1 mv2)
        {
            var mv = mv2 / mv2.NormSquared();

            return new Ega3Multivector(
                mv1.Sp(mv),
                Zero,
                mv1.Op(mv),
                Ega3KVector3.Zero
            );
        }


        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public double X => Scalar1;
        
        public double Y => Scalar2;
        
        public double Z => Scalar3;

        public double Scalar1 { get; }

        public double Scalar2 { get; }

        public double Scalar3 { get; }

        public bool IsZero { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3KVector1(double x, double y, double z)
        {
            Scalar1 = x;
            Scalar2 = y;
            Scalar3 = z;

            IsZero = x == 0d && y == 0d && z == 0d;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return X.IsValid() && Y.IsValid() && Z.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3Multivector ToMultivector()
        {
            return new Ega3Multivector(0d, this, Ega3KVector2.Zero, 0d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X}) e1 + ({Y}) e2 + ({Z}) e3";
        }
    }
}
