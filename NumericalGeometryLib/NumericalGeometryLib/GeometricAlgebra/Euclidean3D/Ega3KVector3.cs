using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;

namespace NumericalGeometryLib.GeometricAlgebra.Euclidean3D
{
    public sealed record Ega3KVector3 :
        IGeometricElement
    {
        public static Ega3KVector3 Zero { get; }
            = new Ega3KVector3(0d);

        public static Ega3KVector3 PseudoScalar { get; }
            = new Ega3KVector3(1d);

        public static Ega3KVector3 PseudoScalarNegative { get; }
            = new Ega3KVector3(-1d);

        public static Ega3KVector3 PseudoScalarInverse { get; }
            = new Ega3KVector3(-1d);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3KVector3(double value)
        {
            return new Ega3KVector3(value);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 operator +(Ega3KVector3 mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 operator -(Ega3KVector3 mv)
        {
            return mv.IsZero
                ? mv
                : new Ega3KVector3(-mv.Scalar123);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 operator +(Ega3KVector3 mv1, Ega3KVector3 mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector3(mv1.Scalar123 + mv2.Scalar123);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 operator -(Ega3KVector3 mv1, Ega3KVector3 mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector3(mv1.Scalar123 - mv2.Scalar123);
        }


        public double ScalarValue 
            => Scalar123;

        public double Scalar123 { get; }
        
        public bool IsZero { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3KVector3(double value)
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