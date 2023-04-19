using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D
{
    public sealed record EGa3ScalarTerm :
        IGeometricElement
    {
        public static EGa3ScalarTerm Zero { get; }
            = new EGa3ScalarTerm(0d);

        public static EGa3ScalarTerm One { get; }
            = new EGa3ScalarTerm(1d);

        public static EGa3ScalarTerm NegativeOne { get; }
            = new EGa3ScalarTerm(-1d);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator EGa3ScalarTerm(double value)
        {
            return new EGa3ScalarTerm(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(EGa3ScalarTerm mv)
        {
            return mv.Scalar0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator +(EGa3ScalarTerm mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator -(EGa3ScalarTerm mv)
        {
            return mv.IsZero
                ? mv
                : new EGa3ScalarTerm(-mv.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator +(EGa3ScalarTerm mv1, double mv2)
        {
            return new EGa3ScalarTerm(mv1.Scalar0 + mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator +(double mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(mv1 + mv2.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator +(EGa3ScalarTerm mv1, EGa3ScalarTerm mv2)
        {
            if (mv1.IsZero) return mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3ScalarTerm(mv1.Scalar0 + mv2.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator -(EGa3ScalarTerm mv1, double mv2)
        {
            return new EGa3ScalarTerm(mv1.Scalar0 - mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator -(double mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(mv1 - mv2.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator -(EGa3ScalarTerm mv1, EGa3ScalarTerm mv2)
        {
            if (mv1.IsZero) return -mv2;

            return mv2.IsZero
                ? mv1
                : new EGa3ScalarTerm(mv1.Scalar0 - mv2.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator *(EGa3ScalarTerm mv1, double mv2)
        {
            return new EGa3ScalarTerm(mv1.Scalar0 * mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator *(double mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(mv1 * mv2.Scalar0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator *(EGa3ScalarTerm mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(mv1.Scalar0 * mv2.Scalar0);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator /(EGa3ScalarTerm mv1, double mv2)
        {
            return new EGa3ScalarTerm(mv1.Scalar0 / mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator /(double mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(mv1 / mv2.Scalar0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm operator /(EGa3ScalarTerm mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(mv1.Scalar0 / mv2.Scalar0);
        }


        public double ScalarValue 
            => Scalar0;

        public double Scalar0 { get; }

        public bool IsZero { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EGa3ScalarTerm(double value)
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