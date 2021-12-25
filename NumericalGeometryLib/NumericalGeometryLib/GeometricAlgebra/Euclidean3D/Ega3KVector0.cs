using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;

namespace NumericalGeometryLib.GeometricAlgebra.Euclidean3D
{
    public sealed record Ega3KVector0 :
        IGeometricElement
    {
        public static Ega3KVector0 Zero { get; }
            = new Ega3KVector0(0d);

        public static Ega3KVector0 One { get; }
            = new Ega3KVector0(1d);

        public static Ega3KVector0 NegativeOne { get; }
            = new Ega3KVector0(-1d);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ega3KVector0(double value)
        {
            return new Ega3KVector0(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(Ega3KVector0 mv)
        {
            return mv.Scalar0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator +(Ega3KVector0 mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator -(Ega3KVector0 mv)
        {
            return mv.IsZero
                ? mv
                : new Ega3KVector0(-mv.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator +(Ega3KVector0 mv1, double mv2)
        {
            return new Ega3KVector0(mv1.Scalar0 + mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator +(double mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(mv1 + mv2.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator +(Ega3KVector0 mv1, Ega3KVector0 mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector0(mv1.Scalar0 + mv2.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator -(Ega3KVector0 mv1, double mv2)
        {
            return new Ega3KVector0(mv1.Scalar0 - mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator -(double mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(mv1 - mv2.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator -(Ega3KVector0 mv1, Ega3KVector0 mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new Ega3KVector0(mv1.Scalar0 - mv2.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator *(Ega3KVector0 mv1, double mv2)
        {
            return new Ega3KVector0(mv1.Scalar0 * mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator *(double mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(mv1 * mv2.Scalar0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator *(Ega3KVector0 mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(mv1.Scalar0 * mv2.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator /(Ega3KVector0 mv1, double mv2)
        {
            return new Ega3KVector0(mv1.Scalar0 / mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator /(double mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(mv1 / mv2.Scalar0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 operator /(Ega3KVector0 mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(mv1.Scalar0 / mv2.Scalar0);
        }


        public double ScalarValue 
            => Scalar0;

        public double Scalar0 { get; }

        public bool IsZero { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ega3KVector0(double value)
        {
            Scalar0 = value;

            IsZero = value == 0d;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Scalar0.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return Scalar0.ToString("G");
        }
    }
}