using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D
{
    public sealed record EGa3Trivector :
        IGeometricElement
    {
        public static EGa3Trivector Zero { get; }
            = new EGa3Trivector(0d);

        public static EGa3Trivector PseudoScalar { get; }
            = new EGa3Trivector(1d);

        public static EGa3Trivector PseudoScalarNegative { get; }
            = new EGa3Trivector(-1d);

        public static EGa3Trivector PseudoScalarInverse { get; }
            = new EGa3Trivector(-1d);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator EGa3Trivector(double value)
        {
            return new EGa3Trivector(value);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector operator +(EGa3Trivector mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector operator -(EGa3Trivector mv)
        {
            return mv.IsZero
                ? mv
                : new EGa3Trivector(-mv.Scalar123);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector operator +(EGa3Trivector mv1, EGa3Trivector mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3Trivector(mv1.Scalar123 + mv2.Scalar123);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector operator -(EGa3Trivector mv1, EGa3Trivector mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3Trivector(mv1.Scalar123 - mv2.Scalar123);
        }


        public double ScalarValue 
            => Scalar123;

        public double Scalar123 { get; }
        
        public bool IsZero { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EGa3Trivector(double value)
        {
            Scalar123 = value;

            IsZero = value == 0d;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Scalar123.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({Scalar123}) e123";
        }
    }
}