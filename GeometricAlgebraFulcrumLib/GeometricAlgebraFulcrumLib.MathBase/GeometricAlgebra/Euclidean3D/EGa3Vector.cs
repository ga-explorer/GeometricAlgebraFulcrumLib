using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D
{
    public sealed record EGa3Vector :
        IFloat64Tuple3D
    {
        public static EGa3Vector Zero { get; }
            = new EGa3Vector(0d, 0d, 0d);

        public static EGa3Vector UnitXAxis { get; }
            = new EGa3Vector(1d, 0d, 0d);
        
        public static EGa3Vector UnitXAxisNegative { get; }
            = new EGa3Vector(-1d, 0d, 0d);

        public static EGa3Vector UnitYAxis { get; }
            = new EGa3Vector(0d, 1d, 0d);
        
        public static EGa3Vector UnitYAxisNegative { get; }
            = new EGa3Vector(0d, -1d, 0d);

        public static EGa3Vector UnitZAxis { get; }
            = new EGa3Vector(0d, 0d, 1d);
        
        public static EGa3Vector UnitZAxisNegative { get; }
            = new EGa3Vector(0d, 0d, -1d);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator +(EGa3Vector mv1)
        {
            return mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator -(EGa3Vector mv1)
        {
            return mv1.IsZero
                ? mv1
                : new EGa3Vector(-mv1.Scalar1, -mv1.Scalar2, -mv1.Scalar3);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(EGa3Vector mv1, double mv2)
        {
            return new Ega3Multivector(
                mv2,                
                mv1,
                EGa3Bivector.Zero, 
                EGa3Trivector.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator +(double mv1, EGa3Vector mv2)
        {
            return new Ega3Multivector(
                mv1,
                mv2,
                EGa3Bivector.Zero, 
                EGa3Trivector.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator +(EGa3Vector mv1, EGa3Vector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3Vector(
                    mv1.Scalar1 + mv2.Scalar1,
                    mv1.Scalar2 + mv2.Scalar2,
                    mv1.Scalar3 + mv2.Scalar3
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(EGa3Vector mv1, double mv2)
        {
            return new Ega3Multivector(
                -mv2,                
                mv1,
                EGa3Bivector.Zero, 
                EGa3Trivector.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator -(double mv1, EGa3Vector mv2)
        {
            return new Ega3Multivector(
                mv1,
                -mv2,
                EGa3Bivector.Zero, 
                EGa3Trivector.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator -(EGa3Vector mv1, EGa3Vector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3Vector(
                    mv1.Scalar1 - mv2.Scalar1,
                    mv1.Scalar2 - mv2.Scalar2,
                    mv1.Scalar3 - mv2.Scalar3
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator *(EGa3Vector mv1, double mv2)
        {
            return new EGa3Vector(
                mv1.X * mv2,
                mv1.Y * mv2,
                mv1.Z * mv2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator *(double mv1, EGa3Vector mv2)
        {
            return new EGa3Vector(
                mv1 * mv2.X,
                mv1 * mv2.Y,
                mv1 * mv2.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator *(EGa3Vector mv1, EGa3Vector mv2)
        {
            return new Ega3Multivector(
                mv1.Sp(mv2),
                Zero,
                mv1.Op(mv1),
                EGa3Trivector.Zero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator /(EGa3Vector mv1, double mv2)
        {
            return new EGa3Vector(
                mv1.X / mv2,
                mv1.Y / mv2,
                mv1.Z / mv2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector operator /(double mv1, EGa3Vector mv2)
        {
            var s = mv1 / mv2.NormSquared();

            return new EGa3Vector(
                s * mv2.X,
                s * mv2.Y,
                s * mv2.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector operator /(EGa3Vector mv1, EGa3Vector mv2)
        {
            var mv = mv2 / mv2.NormSquared();

            return new Ega3Multivector(
                mv1.Sp(mv),
                Zero,
                mv1.Op(mv),
                EGa3Trivector.Zero
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
        public EGa3Vector(double x, double y, double z)
        {
            Scalar1 = x;
            Scalar2 = y;
            Scalar3 = z;

            IsZero = x == 0d && y == 0d && z == 0d;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EGa3Vector(ITriplet<double> vector)
        {
            Scalar1 = vector.Item1;
            Scalar2 = vector.Item2;
            Scalar3 = vector.Item3;

            IsZero = vector.Item1 == 0d && vector.Item2 == 0d && vector.Item3 == 0d;

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
            return new Ega3Multivector(0d, this, EGa3Bivector.Zero, 0d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X}) e1 + ({Y}) e2 + ({Z}) e3";
        }
    }
}
