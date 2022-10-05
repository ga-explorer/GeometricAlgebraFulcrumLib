using System;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace NumericalGeometryLib.GeometricAlgebra.Euclidean3D
{
    public static class Ega3MultivectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 ToEga3KVector1(this ITriplet<double> vector)
        {
            return new Ega3KVector1(vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 GradeInvolution(this Ega3KVector0 mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 GradeInvolution(this Ega3KVector1 mv)
        {
            return new Ega3KVector1(
                -mv.Scalar1,
                -mv.Scalar2,
                -mv.Scalar3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 GradeInvolution(this Ega3KVector2 mv)
        {
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 GradeInvolution(this Ega3KVector3 mv)
        {
            return new Ega3KVector3(-mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Reverse(this Ega3KVector0 mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 Reverse(this Ega3KVector1 mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 Reverse(this Ega3KVector2 mv)
        {
            return new Ega3KVector2(
                -mv.Scalar12,
                -mv.Scalar13,
                -mv.Scalar23
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 Reverse(this Ega3KVector3 mv)
        {
            return new Ega3KVector3(-mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 CliffordConjugate(this Ega3KVector0 mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 CliffordConjugate(this Ega3KVector1 mv)
        {
            return new Ega3KVector1(
                -mv.Scalar1,
                -mv.Scalar2,
                -mv.Scalar3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 CliffordConjugate(this Ega3KVector2 mv)
        {
            return new Ega3KVector2(
                -mv.Scalar12,
                -mv.Scalar13,
                -mv.Scalar23
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 CliffordConjugate(this Ega3KVector3 mv)
        {
            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this Ega3KVector0 mv)
        {
            return mv.Scalar0 * mv.Scalar0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this Ega3KVector1 mv)
        {
            return mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this Ega3KVector2 mv)
        {
            return mv.Scalar12 * mv.Scalar12 + mv.Scalar13 * mv.Scalar13 + mv.Scalar23 * mv.Scalar23;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this Ega3KVector3 mv)
        {
            return mv.Scalar123 * mv.Scalar123;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this Ega3KVector0 mv)
        {
            return Math.Abs(mv.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this Ega3KVector1 mv)
        {
            return Math.Sqrt(mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this Ega3KVector2 mv)
        {
            return Math.Sqrt(
                mv.Scalar12 * mv.Scalar12 +
                mv.Scalar13 * mv.Scalar13 + 
                mv.Scalar23 * mv.Scalar23
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this Ega3KVector3 mv)
        {
            return Math.Abs(mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 Dual(this Ega3KVector0 mv)
        {
            return new Ega3KVector3(mv.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 Dual(this Ega3KVector1 mv)
        {
            return new Ega3KVector2(
                mv.Scalar3,
                -mv.Scalar2,
                mv.Scalar1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 Dual(this Ega3KVector2 mv)
        {
            return new Ega3KVector1(
                -mv.Scalar23, 
                mv.Scalar13, 
                -mv.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Dual(this Ega3KVector3 mv)
        {
            return new Ega3KVector0(-mv.Scalar123);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 UnDual(this Ega3KVector0 mv)
        {
            return new Ega3KVector3(mv.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 UnDual(this Ega3KVector1 mv)
        {
            return new Ega3KVector2(
                -mv.Scalar3,
                mv.Scalar2,
                -mv.Scalar1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 UnDual(this Ega3KVector2 mv)
        {
            return new Ega3KVector1(
                mv.Scalar23,
                -mv.Scalar13,
                mv.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 UnDual(this Ega3KVector3 mv)
        {
            return new Ega3KVector0(mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector0 mv1, Ega3KVector0 mv2)
        {
            return mv1.Scalar0 * mv2.Scalar0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector1 mv1, Ega3KVector0 mv2)
        {
            return 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector0 mv1, Ega3KVector1 mv2)
        {
            return 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector1 mv1, Ega3KVector1 mv2)
        {
            return mv1.Scalar1 * mv2.Scalar1 + 
                   mv1.Scalar2 * mv2.Scalar2 + 
                   mv1.Scalar3 * mv2.Scalar3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector2 mv1, Ega3KVector0 mv2)
        {
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector0 mv1, Ega3KVector2 mv2)
        {
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector2 mv1, Ega3KVector1 mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector1 mv1, Ega3KVector2 mv2)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector2 mv1, Ega3KVector2 mv2)
        {
            return -(mv1.Scalar12 * mv2.Scalar12 + 
                   mv1.Scalar13 * mv2.Scalar13 + 
                   mv1.Scalar23 * mv2.Scalar23);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector3 mv1, Ega3KVector0 mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector0 mv1, Ega3KVector3 mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector3 mv1, Ega3KVector1 mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector1 mv1, Ega3KVector3 mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector3 mv1, Ega3KVector2 mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector2 mv1, Ega3KVector3 mv2)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector3 mv1, Ega3KVector3 mv2)
        {
            return -(mv1.Scalar123 * mv2.Scalar123);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, Ega3KVector0 mv2)
        {
            var mv = 0d;

            if (!mv1.KVector0.IsZero && !mv2.IsZero)
                mv += mv1.KVector0.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector0 mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector0.IsZero)
                mv += mv1.Sp(mv2.KVector0);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, Ega3KVector1 mv2)
        {
            var mv = 0d;

            if (!mv1.KVector1.IsZero && !mv2.IsZero)
                mv += mv1.KVector1.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector1 mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector1.IsZero)
                mv += mv1.Sp(mv2.KVector1);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, Ega3KVector2 mv2)
        {
            var mv = 0d;

            if (!mv1.KVector2.IsZero && !mv2.IsZero)
                mv += mv1.KVector2.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector2 mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector2.IsZero)
                mv += mv1.Sp(mv2.KVector2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, Ega3KVector3 mv2)
        {
            var mv = 0d;

            if (!mv1.KVector3.IsZero && !mv2.IsZero)
                mv += mv1.KVector3.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3KVector3 mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector3.IsZero)
                mv += mv1.Sp(mv2.KVector3);

            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.KVector0.IsZero && !mv2.KVector0.IsZero)
                mv += mv1.KVector0.Sp(mv2.KVector0);
        
            if (!mv1.KVector1.IsZero && !mv2.KVector1.IsZero)
                mv += mv1.KVector1.Sp(mv2.KVector1);

            if (!mv1.KVector2.IsZero && !mv2.KVector2.IsZero)
                mv += mv1.KVector2.Sp(mv2.KVector2);

            if (!mv1.KVector3.IsZero && !mv2.KVector3.IsZero)
                mv += mv1.KVector3.Sp(mv2.KVector3);

            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector0 mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector0(
                mv1.Scalar0 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 Op(this Ega3KVector1 mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector1(
                mv1.Scalar1 * mv2.Scalar0,
                mv1.Scalar2 * mv2.Scalar0,
                mv1.Scalar3 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector1 Op(this Ega3KVector0 mv1, Ega3KVector1 mv2)
        {
            return new Ega3KVector1(
                mv1.Scalar0 * mv2.Scalar1,
                mv1.Scalar0 * mv2.Scalar2,
                mv1.Scalar0 * mv2.Scalar3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 Op(this Ega3KVector1 mv1, Ega3KVector1 mv2)
        {
            return new Ega3KVector2(
                mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
                mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
                mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 Op(this Ega3KVector0 mv1, Ega3KVector2 mv2)
        {
            return new Ega3KVector2(
                mv1.Scalar0 * mv2.Scalar12,
                mv1.Scalar0 * mv2.Scalar13,
                mv1.Scalar0 * mv2.Scalar23
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector2 Op(this Ega3KVector2 mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector2(
                mv1.Scalar12 * mv2.Scalar0,
                mv1.Scalar13 * mv2.Scalar0,
                mv1.Scalar23 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 Op(this Ega3KVector1 mv1, Ega3KVector2 mv2)
        {
            return new Ega3KVector3(
                mv1.Scalar1 * mv2.Scalar23 - mv1.Scalar2 * mv2.Scalar13 + mv1.Scalar3 * mv2.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 Op(this Ega3KVector2 mv1, Ega3KVector1 mv2)
        {
            return new Ega3KVector3(
                mv1.Scalar12 * mv2.Scalar3 - mv1.Scalar13 * mv2.Scalar2 + mv1.Scalar23 * mv2.Scalar1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector2 mv1, Ega3KVector2 mv2)
        {
            return Ega3KVector0.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 Op(this Ega3KVector0 mv1, Ega3KVector3 mv2)
        {
            return new Ega3KVector3(
                mv1.Scalar0 * mv2.Scalar123
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector3 Op(this Ega3KVector3 mv1, Ega3KVector0 mv2)
        {
            return new Ega3KVector3(
                mv1.Scalar123 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector1 mv1, Ega3KVector3 mv2)
        {
            return Ega3KVector0.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector3 mv1, Ega3KVector1 mv2)
        {
            return Ega3KVector0.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector2 mv1, Ega3KVector3 mv2)
        {
            return Ega3KVector0.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector3 mv1, Ega3KVector2 mv2)
        {
            return Ega3KVector0.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3KVector0 Op(this Ega3KVector3 mv1, Ega3KVector3 mv2)
        {
            return Ega3KVector0.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, Ega3KVector0 mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv1.KVector0.IsZero)
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero)
                mv += mv1.KVector1.Op(mv2);
            
            if (!mv1.KVector2.IsZero)
                mv += mv1.KVector2.Op(mv2);
            
            if (!mv1.KVector3.IsZero)
                mv += mv1.KVector3.Op(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3KVector0 mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv2.KVector0.IsZero)
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero)
                mv += mv1.Op(mv2.KVector1);
            
            if (!mv2.KVector2.IsZero)
                mv += mv1.Op(mv2.KVector2);
            
            if (!mv2.KVector3.IsZero)
                mv += mv1.Op(mv2.KVector3);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3KVector1 mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv2.KVector0.IsZero)
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero)
                mv += mv1.Op(mv2.KVector1);
            
            if (!mv2.KVector2.IsZero)
                mv += mv1.Op(mv2.KVector2);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, Ega3KVector1 mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv1.KVector0.IsZero)
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero)
                mv += mv1.KVector1.Op(mv2);
            
            if (!mv1.KVector2.IsZero)
                mv += mv1.KVector2.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3KVector2 mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv2.KVector0.IsZero)
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero)
                mv += mv1.Op(mv2.KVector1);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, Ega3KVector2 mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv1.KVector0.IsZero)
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero)
                mv += mv1.KVector1.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3KVector3 mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv2.KVector0.IsZero)
                mv += mv1.Op(mv2.KVector0);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, Ega3KVector3 mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv1.KVector0.IsZero)
                mv += mv1.KVector0.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv1.KVector0.IsZero)
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero)
                mv += mv1.KVector1.Op(mv2);

            if (!mv1.KVector2.IsZero)
                mv += mv1.KVector2.Op(mv2);

            if (!mv1.KVector3.IsZero)
                mv += mv1.KVector3.Op(mv2);

            return mv;
        }
    }
}