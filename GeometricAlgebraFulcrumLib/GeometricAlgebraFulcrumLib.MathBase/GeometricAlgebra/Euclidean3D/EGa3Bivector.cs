using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D
{
    public sealed record EGa3Bivector : 
        ITriplet<double>, 
        IGeometricElement
    {
        public static EGa3Bivector Zero { get; }
            = new EGa3Bivector(0d, 0d, 0d);

        public static EGa3Bivector XyBivector { get; }
            = new EGa3Bivector(1d, 0d, 0d);

        public static EGa3Bivector YxBivector { get; }
            = new EGa3Bivector(-1d, 0d, 0d);

        public static EGa3Bivector YzBivector { get; }
            = new EGa3Bivector(0d, 1d, 0d);

        public static EGa3Bivector ZyBivector { get; }
            = new EGa3Bivector(0d, -1d, 0d);

        public static EGa3Bivector ZxBivector { get; }
            = new EGa3Bivector(0d, 0d, 1d);

        public static EGa3Bivector XzBivector { get; }
            = new EGa3Bivector(0d, 0d, -1d);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator +(EGa3Bivector mv1)
        {
            return mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator -(EGa3Bivector mv1)
        {
            return mv1.IsZero
                ? mv1
                : new EGa3Bivector(-mv1.Scalar12, -mv1.Scalar13, -mv1.Scalar23);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator +(EGa3Bivector mv1, EGa3Bivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3Bivector(
                    mv1.Scalar12 + mv2.Scalar12,
                    mv1.Scalar13 + mv2.Scalar13,
                    mv1.Scalar23 + mv2.Scalar23
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator -(EGa3Bivector mv1, EGa3Bivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3Bivector(
                    mv1.Scalar12 - mv2.Scalar12,
                    mv1.Scalar13 - mv2.Scalar13,
                    mv1.Scalar23 - mv2.Scalar23
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator *(EGa3Bivector mv1, double mv2)
        {
            if (mv1.IsZero || mv2.IsZero()) return Zero;

            return new EGa3Bivector(
                mv1.Scalar12 * mv2,
                mv1.Scalar13 * mv2,
                mv1.Scalar23 * mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator *(double mv1, EGa3Bivector mv2)
        {
            if (mv1.IsZero() || mv2.IsZero) return Zero;

            return new EGa3Bivector(
                mv1 * mv2.Scalar12,
                mv1 * mv2.Scalar13,
                mv1 * mv2.Scalar23
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector operator /(EGa3Bivector mv1, double mv2)
        {
            return mv1 * (1d / mv2);
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
        public EGa3Bivector(double xy, double xz, double yz)
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
            return new Ega3Multivector(0d, EGa3Vector.Zero, this, 0d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({Xy})<1,2> + ({Xz})<1,3> + ({Yz})<2,3>";
        }
    }
}