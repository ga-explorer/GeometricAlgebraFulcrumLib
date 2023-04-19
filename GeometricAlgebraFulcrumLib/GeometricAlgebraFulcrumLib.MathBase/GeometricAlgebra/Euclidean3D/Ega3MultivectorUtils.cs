using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D
{
    public static class Ega3MultivectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector ToEGa3Vector(this ITriplet<double> vector)
        {
            return new EGa3Vector(vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm GradeInvolution(this EGa3ScalarTerm mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector GradeInvolution(this EGa3Vector mv)
        {
            return new EGa3Vector(
                -mv.Scalar1,
                -mv.Scalar2,
                -mv.Scalar3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector GradeInvolution(this EGa3Bivector mv)
        {
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector GradeInvolution(this EGa3Trivector mv)
        {
            return new EGa3Trivector(-mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Reverse(this EGa3ScalarTerm mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector Reverse(this EGa3Vector mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector Reverse(this EGa3Bivector mv)
        {
            return new EGa3Bivector(
                -mv.Scalar12,
                -mv.Scalar13,
                -mv.Scalar23
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector Reverse(this EGa3Trivector mv)
        {
            return new EGa3Trivector(-mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm CliffordConjugate(this EGa3ScalarTerm mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector CliffordConjugate(this EGa3Vector mv)
        {
            return new EGa3Vector(
                -mv.Scalar1,
                -mv.Scalar2,
                -mv.Scalar3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector CliffordConjugate(this EGa3Bivector mv)
        {
            return new EGa3Bivector(
                -mv.Scalar12,
                -mv.Scalar13,
                -mv.Scalar23
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector CliffordConjugate(this EGa3Trivector mv)
        {
            return mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this EGa3ScalarTerm mv)
        {
            return mv.Scalar0 * mv.Scalar0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this EGa3Vector mv)
        {
            return mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this EGa3Bivector mv)
        {
            return mv.Scalar12 * mv.Scalar12 + mv.Scalar13 * mv.Scalar13 + mv.Scalar23 * mv.Scalar23;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormSquared(this EGa3Trivector mv)
        {
            return mv.Scalar123 * mv.Scalar123;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this EGa3ScalarTerm mv)
        {
            return Math.Abs(mv.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this EGa3Vector mv)
        {
            return Math.Sqrt(mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this EGa3Bivector mv)
        {
            return Math.Sqrt(
                mv.Scalar12 * mv.Scalar12 +
                mv.Scalar13 * mv.Scalar13 + 
                mv.Scalar23 * mv.Scalar23
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm(this EGa3Trivector mv)
        {
            return Math.Abs(mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector Dual(this EGa3ScalarTerm mv)
        {
            return new EGa3Trivector(mv.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector Dual(this EGa3Vector mv)
        {
            return new EGa3Bivector(
                mv.Scalar3,
                -mv.Scalar2,
                mv.Scalar1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector Dual(this EGa3Bivector mv)
        {
            return new EGa3Vector(
                -mv.Scalar23, 
                mv.Scalar13, 
                -mv.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Dual(this EGa3Trivector mv)
        {
            return new EGa3ScalarTerm(-mv.Scalar123);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector UnDual(this EGa3ScalarTerm mv)
        {
            return new EGa3Trivector(mv.Scalar0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector UnDual(this EGa3Vector mv)
        {
            return new EGa3Bivector(
                -mv.Scalar3,
                mv.Scalar2,
                -mv.Scalar1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector UnDual(this EGa3Bivector mv)
        {
            return new EGa3Vector(
                mv.Scalar23,
                -mv.Scalar13,
                mv.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm UnDual(this EGa3Trivector mv)
        {
            return new EGa3ScalarTerm(mv.Scalar123);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3ScalarTerm mv1, EGa3ScalarTerm mv2)
        {
            return mv1.Scalar0 * mv2.Scalar0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Vector mv1, EGa3ScalarTerm mv2)
        {
            return 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3ScalarTerm mv1, EGa3Vector mv2)
        {
            return 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Vector mv1, EGa3Vector mv2)
        {
            return mv1.Scalar1 * mv2.Scalar1 + 
                   mv1.Scalar2 * mv2.Scalar2 + 
                   mv1.Scalar3 * mv2.Scalar3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Bivector mv1, EGa3ScalarTerm mv2)
        {
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3ScalarTerm mv1, EGa3Bivector mv2)
        {
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Bivector mv1, EGa3Vector mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Vector mv1, EGa3Bivector mv2)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Bivector mv1, EGa3Bivector mv2)
        {
            return -(mv1.Scalar12 * mv2.Scalar12 + 
                   mv1.Scalar13 * mv2.Scalar13 + 
                   mv1.Scalar23 * mv2.Scalar23);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Trivector mv1, EGa3ScalarTerm mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3ScalarTerm mv1, EGa3Trivector mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Trivector mv1, EGa3Vector mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Vector mv1, EGa3Trivector mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Trivector mv1, EGa3Bivector mv2)
        {
            return 0;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Bivector mv1, EGa3Trivector mv2)
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Trivector mv1, EGa3Trivector mv2)
        {
            return -(mv1.Scalar123 * mv2.Scalar123);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, EGa3ScalarTerm mv2)
        {
            var mv = 0d;

            if (!mv1.KVector0.IsZero && !mv2.IsZero)
                mv += mv1.KVector0.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3ScalarTerm mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector0.IsZero)
                mv += mv1.Sp(mv2.KVector0);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, EGa3Vector mv2)
        {
            var mv = 0d;

            if (!mv1.KVector1.IsZero && !mv2.IsZero)
                mv += mv1.KVector1.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Vector mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector1.IsZero)
                mv += mv1.Sp(mv2.KVector1);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, EGa3Bivector mv2)
        {
            var mv = 0d;

            if (!mv1.KVector2.IsZero && !mv2.IsZero)
                mv += mv1.KVector2.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Bivector mv1, Ega3Multivector mv2)
        {
            var mv = 0d;

            if (!mv1.IsZero && !mv2.KVector2.IsZero)
                mv += mv1.Sp(mv2.KVector2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this Ega3Multivector mv1, EGa3Trivector mv2)
        {
            var mv = 0d;

            if (!mv1.KVector3.IsZero && !mv2.IsZero)
                mv += mv1.KVector3.Sp(mv2);

            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this EGa3Trivector mv1, Ega3Multivector mv2)
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
        public static EGa3ScalarTerm Op(this EGa3ScalarTerm mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3ScalarTerm(
                mv1.Scalar0 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector Op(this EGa3Vector mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3Vector(
                mv1.Scalar1 * mv2.Scalar0,
                mv1.Scalar2 * mv2.Scalar0,
                mv1.Scalar3 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Vector Op(this EGa3ScalarTerm mv1, EGa3Vector mv2)
        {
            return new EGa3Vector(
                mv1.Scalar0 * mv2.Scalar1,
                mv1.Scalar0 * mv2.Scalar2,
                mv1.Scalar0 * mv2.Scalar3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector Op(this EGa3Vector mv1, EGa3Vector mv2)
        {
            return new EGa3Bivector(
                mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
                mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
                mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector Op(this EGa3ScalarTerm mv1, EGa3Bivector mv2)
        {
            return new EGa3Bivector(
                mv1.Scalar0 * mv2.Scalar12,
                mv1.Scalar0 * mv2.Scalar13,
                mv1.Scalar0 * mv2.Scalar23
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Bivector Op(this EGa3Bivector mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3Bivector(
                mv1.Scalar12 * mv2.Scalar0,
                mv1.Scalar13 * mv2.Scalar0,
                mv1.Scalar23 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector Op(this EGa3Vector mv1, EGa3Bivector mv2)
        {
            return new EGa3Trivector(
                mv1.Scalar1 * mv2.Scalar23 - mv1.Scalar2 * mv2.Scalar13 + mv1.Scalar3 * mv2.Scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector Op(this EGa3Bivector mv1, EGa3Vector mv2)
        {
            return new EGa3Trivector(
                mv1.Scalar12 * mv2.Scalar3 - mv1.Scalar13 * mv2.Scalar2 + mv1.Scalar23 * mv2.Scalar1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Op(this EGa3Bivector mv1, EGa3Bivector mv2)
        {
            return EGa3ScalarTerm.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector Op(this EGa3ScalarTerm mv1, EGa3Trivector mv2)
        {
            return new EGa3Trivector(
                mv1.Scalar0 * mv2.Scalar123
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3Trivector Op(this EGa3Trivector mv1, EGa3ScalarTerm mv2)
        {
            return new EGa3Trivector(
                mv1.Scalar123 * mv2.Scalar0
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Op(this EGa3Vector mv1, EGa3Trivector mv2)
        {
            return EGa3ScalarTerm.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Op(this EGa3Trivector mv1, EGa3Vector mv2)
        {
            return EGa3ScalarTerm.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Op(this EGa3Bivector mv1, EGa3Trivector mv2)
        {
            return EGa3ScalarTerm.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Op(this EGa3Trivector mv1, EGa3Bivector mv2)
        {
            return EGa3ScalarTerm.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EGa3ScalarTerm Op(this EGa3Trivector mv1, EGa3Trivector mv2)
        {
            return EGa3ScalarTerm.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, EGa3ScalarTerm mv2)
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
        public static Ega3Multivector Op(this EGa3ScalarTerm mv1, Ega3Multivector mv2)
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
        public static Ega3Multivector Op(this EGa3Vector mv1, Ega3Multivector mv2)
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
        public static Ega3Multivector Op(this Ega3Multivector mv1, EGa3Vector mv2)
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
        public static Ega3Multivector Op(this EGa3Bivector mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv2.KVector0.IsZero)
                mv += mv1.Op(mv2.KVector0);
            
            if (!mv2.KVector1.IsZero)
                mv += mv1.Op(mv2.KVector1);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, EGa3Bivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv1.KVector0.IsZero)
                mv += mv1.KVector0.Op(mv2);
            
            if (!mv1.KVector1.IsZero)
                mv += mv1.KVector1.Op(mv2);
            
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this EGa3Trivector mv1, Ega3Multivector mv2)
        {
            var mv = Ega3Multivector.Zero;

            if (!mv2.KVector0.IsZero)
                mv += mv1.Op(mv2.KVector0);
            
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ega3Multivector Op(this Ega3Multivector mv1, EGa3Trivector mv2)
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