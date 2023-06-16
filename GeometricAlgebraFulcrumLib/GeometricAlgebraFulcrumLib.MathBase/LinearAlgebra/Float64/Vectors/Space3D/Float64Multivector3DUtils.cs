using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public static class Float64Multivector3DUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Scalar3D mv1, Float64Scalar3D mv2)
        {
            return mv1.Scalar * mv2.Scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Scalar3D mv1, Float64Vector3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Scalar3D mv1, Float64Bivector3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Scalar3D mv1, Float64Trivector3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Scalar3D mv1, Float64Multivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero() && !mv2.KVector0.IsZero())
                mv += mv1.Sp(mv2.KVector0);

            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Vector3D mv1, Float64Scalar3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Vector3D mv1, Float64Vector3D mv2)
        {
            return mv1.Scalar1 * mv2.Scalar1 + 
                   mv1.Scalar2 * mv2.Scalar2 + 
                   mv1.Scalar3 * mv2.Scalar3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Vector3D mv1, Float64Bivector3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Vector3D mv1, Float64Trivector3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Vector3D mv1, Float64Multivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero() && !mv2.KVector1.IsZero())
                mv += mv1.Sp(mv2.KVector1);

            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Bivector3D mv1, Float64Scalar3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Bivector3D mv1, Float64Vector3D mv2)
        {
            return Float64Scalar.Zero;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Bivector3D mv1, Float64Bivector3D mv2)
        {
            return -(mv1.Scalar12 * mv2.Scalar12 + 
                   mv1.Scalar13 * mv2.Scalar13 + 
                   mv1.Scalar23 * mv2.Scalar23);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Bivector3D mv1, Float64Trivector3D mv2)
        {
            return Float64Scalar.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Bivector3D mv1, Float64Multivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero() && !mv2.KVector2.IsZero())
                mv += mv1.Sp(mv2.KVector2);

            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Trivector3D mv1, Float64Scalar3D mv2)
        {
            return Float64Scalar.Zero;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Trivector3D mv1, Float64Vector3D mv2)
        {
            return Float64Scalar.Zero;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Trivector3D mv1, Float64Bivector3D mv2)
        {
            return Float64Scalar.Zero;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Trivector3D mv1, Float64Trivector3D mv2)
        {
            return -(mv1.Scalar123 * mv2.Scalar123);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Trivector3D mv1, Float64Multivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero() && !mv2.KVector3.IsZero())
                mv += mv1.Sp(mv2.KVector3);

            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Multivector3D mv1, Float64Scalar3D mv2)
        {
            var mv = 0d;

            if (!mv1.KVector0.IsZero() && !mv2.IsZero())
                mv += mv1.KVector0.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Multivector3D mv1, Float64Vector3D mv2)
        {
            var mv = 0d;

            if (!mv1.KVector1.IsZero() && !mv2.IsZero())
                mv += mv1.KVector1.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Multivector3D mv1, Float64Bivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.KVector2.IsZero() && !mv2.IsZero())
                mv += mv1.KVector2.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Multivector3D mv1, Float64Trivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.KVector3.IsZero() && !mv2.IsZero())
                mv += mv1.KVector3.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar Sp(this Float64Multivector3D mv1, Float64Multivector3D mv2)
        {
            var mv = 0d;

            if (!mv1.KVector0.IsZero() && !mv2.KVector0.IsZero())
                mv += mv1.KVector0.Sp(mv2.KVector0);
        
            if (!mv1.KVector1.IsZero() && !mv2.KVector1.IsZero())
                mv += mv1.KVector1.Sp(mv2.KVector1);

            if (!mv1.KVector2.IsZero() && !mv2.KVector2.IsZero())
                mv += mv1.KVector2.Sp(mv2.KVector2);

            if (!mv1.KVector3.IsZero() && !mv2.KVector3.IsZero())
                mv += mv1.KVector3.Sp(mv2.KVector3);

            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Scalar3D mv1, Float64Scalar3D mv2)
        {
            return Float64Scalar3D.Create(
                mv1.Scalar * mv2.Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D Op(this Float64Vector3D mv1, Float64Scalar3D mv2)
        {
            return Float64Vector3D.Create(
                mv1.Scalar1 * mv2.Scalar,
                mv1.Scalar2 * mv2.Scalar,
                mv1.Scalar3 * mv2.Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D Op(this Float64Scalar3D mv1, Float64Vector3D mv2)
        {
            return Float64Vector3D.Create(
                mv1.Scalar * mv2.Scalar1,
                mv1.Scalar * mv2.Scalar2,
                mv1.Scalar * mv2.Scalar3
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector3D VectorOp(this ITriplet<double> mv1, ITriplet<double> mv2)
        {
            return Float64Bivector3D.Create(
                mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1,
                mv1.Item1 * mv2.Item3 - mv1.Item3 * mv2.Item1,
                mv1.Item2 * mv2.Item3 - mv1.Item3 * mv2.Item2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector3D Op(this Float64Vector3D mv1, Float64Vector3D mv2)
        {
            return Float64Bivector3D.Create(
                mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
                mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
                mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector3D Op(this Float64Scalar3D mv1, Float64Bivector3D mv2)
        {
            return Float64Bivector3D.Create(
                mv1.Scalar * mv2.Scalar12,
                mv1.Scalar * mv2.Scalar13,
                mv1.Scalar * mv2.Scalar23
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector3D Op(this Float64Bivector3D mv1, Float64Scalar3D mv2)
        {
            return Float64Bivector3D.Create(
                mv1.Scalar12 * mv2.Scalar,
                mv1.Scalar13 * mv2.Scalar,
                mv1.Scalar23 * mv2.Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Trivector3D Op(this Float64Vector3D mv1, Float64Bivector3D mv2)
        {
            return Float64Trivector3D.Create(
                mv1.Scalar1 * mv2.Scalar23 - 
                mv1.Scalar2 * mv2.Scalar13 + 
                mv1.Scalar3 * mv2.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Trivector3D Op(this Float64Bivector3D mv1, Float64Vector3D mv2)
        {
            return Float64Trivector3D.Create(
                mv1.Scalar12 * mv2.Scalar3 - 
                mv1.Scalar13 * mv2.Scalar2 + 
                mv1.Scalar23 * mv2.Scalar1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Bivector3D mv1, Float64Bivector3D mv2)
        {
            return Float64Scalar3D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Trivector3D Op(this Float64Scalar3D mv1, Float64Trivector3D mv2)
        {
            return Float64Trivector3D.Create(
                mv1.Scalar * mv2.Scalar123
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Trivector3D Op(this Float64Trivector3D mv1, Float64Scalar3D mv2)
        {
            return Float64Trivector3D.Create(
                mv1.Scalar123 * mv2.Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Vector3D mv1, Float64Trivector3D mv2)
        {
            return Float64Scalar3D.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Trivector3D mv1, Float64Vector3D mv2)
        {
            return Float64Scalar3D.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Bivector3D mv1, Float64Trivector3D mv2)
        {
            return Float64Scalar3D.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Trivector3D mv1, Float64Bivector3D mv2)
        {
            return Float64Scalar3D.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Scalar3D Op(this Float64Trivector3D mv1, Float64Trivector3D mv2)
        {
            return Float64Scalar3D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Multivector3D mv1, Float64Scalar3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv1.KVector0.IsZero())
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero())
                mv += mv1.KVector1.Op(mv2);
            
            if (!mv1.KVector2.IsZero())
                mv += mv1.KVector2.Op(mv2);
            
            if (!mv1.KVector3.IsZero())
                mv += mv1.KVector3.Op(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Scalar3D mv1, Float64Multivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv2.KVector0.IsZero())
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero())
                mv += mv1.Op(mv2.KVector1);
            
            if (!mv2.KVector2.IsZero())
                mv += mv1.Op(mv2.KVector2);
            
            if (!mv2.KVector3.IsZero())
                mv += mv1.Op(mv2.KVector3);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Vector3D mv1, Float64Multivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv2.KVector0.IsZero())
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero())
                mv += mv1.Op(mv2.KVector1);
            
            if (!mv2.KVector2.IsZero())
                mv += mv1.Op(mv2.KVector2);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Multivector3D mv1, Float64Vector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv1.KVector0.IsZero())
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero())
                mv += mv1.KVector1.Op(mv2);
            
            if (!mv1.KVector2.IsZero())
                mv += mv1.KVector2.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Bivector3D mv1, Float64Multivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv2.KVector0.IsZero())
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero())
                mv += mv1.Op(mv2.KVector1);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Multivector3D mv1, Float64Bivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv1.KVector0.IsZero())
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero())
                mv += mv1.KVector1.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Trivector3D mv1, Float64Multivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv2.KVector0.IsZero())
                mv += mv1.Op(mv2.KVector0);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Multivector3D mv1, Float64Trivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv1.KVector0.IsZero())
                mv += mv1.KVector0.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Multivector3D Op(this Float64Multivector3D mv1, Float64Multivector3D mv2)
        {
            var mv = Float64Multivector3D.Zero;

            if (!mv1.KVector0.IsZero())
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero())
                mv += mv1.KVector1.Op(mv2);

            if (!mv1.KVector2.IsZero())
                mv += mv1.KVector2.Op(mv2);

            if (!mv1.KVector3.IsZero())
                mv += mv1.KVector3.Op(mv2);

            return mv;
        }
    }
}