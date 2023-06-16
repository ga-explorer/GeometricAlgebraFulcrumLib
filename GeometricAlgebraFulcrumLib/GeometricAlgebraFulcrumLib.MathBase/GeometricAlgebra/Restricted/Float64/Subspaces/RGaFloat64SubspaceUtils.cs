using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Subspaces
{
    public static class RGaFloat64SubspaceUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IRGaFloat64Subspace subspace, RGaFloat64Vector vector, bool nearZeroFlag = false)
        {
            var processor = ScalarProcessorFloat64.DefaultProcessor;

            var mv2 = vector.Op(subspace.GetBlade());

            return subspace.SubspaceDimension >= 2 &&
                   (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IRGaFloat64Subspace subspace, RGaFloat64Bivector mv, bool nearZeroFlag = false)
        {
            var processor = ScalarProcessorFloat64.DefaultProcessor;

            var mv2 = mv - subspace.Project(mv);

            return subspace.SubspaceDimension >= 2 &&
                   (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IRGaFloat64Subspace subspace, RGaFloat64KVector mv, bool nearZeroFlag = false)
        {
            var processor = ScalarProcessorFloat64.DefaultProcessor;

            var mv2 = mv - subspace.Project(mv);

            return subspace.SubspaceDimension >= 2 &&
                   (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IRGaFloat64Subspace subspace, IRGaFloat64Subspace mv, bool nearZeroFlag = false)
        {
            return subspace.Contains(mv.GetBlade(), nearZeroFlag);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector Project(this IRGaFloat64Subspace subspace, RGaFloat64Vector blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector Project(this IRGaFloat64Subspace subspace, RGaFloat64Bivector blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector Project(this IRGaFloat64Subspace subspace, RGaFloat64KVector blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector Reflect(this IRGaFloat64Subspace subspace, RGaFloat64Vector blade)
        {
            return subspace
                .GetBlade()
                .Gp(-blade)
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector Reflect(this IRGaFloat64Subspace subspace, RGaFloat64Bivector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade)
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector Reflect(this IRGaFloat64Subspace subspace, RGaFloat64KVector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector VersorProduct(this IRGaFloat64Subspace subspace, RGaFloat64Vector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector VersorProduct(this IRGaFloat64Subspace subspace, RGaFloat64Bivector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector VersorProduct(this IRGaFloat64Subspace subspace, RGaFloat64KVector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector Complement(this IRGaFloat64Subspace subspace, RGaFloat64Vector blade)
        {
            return blade.Lcp(subspace.GetBlade()).GetKVectorPart(blade.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector Complement(this IRGaFloat64Subspace subspace, RGaFloat64Bivector blade)
        {
            return blade.Lcp(subspace.GetBlade()).GetKVectorPart(blade.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector Complement(this IRGaFloat64Subspace subspace, RGaFloat64KVector blade)
        {
            return blade.Lcp(subspace.GetBlade()).GetKVectorPart(blade.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Multivector Complement(this IRGaFloat64Subspace subspace, RGaFloat64Multivector blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }
    }
}