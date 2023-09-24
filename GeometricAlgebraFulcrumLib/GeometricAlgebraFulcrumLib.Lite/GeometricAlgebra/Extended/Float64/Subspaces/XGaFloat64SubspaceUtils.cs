using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Subspaces
{
    public static class XGaFloat64SubspaceUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IXGaFloat64Subspace subspace, XGaFloat64Vector vector, bool nearZeroFlag = false)
        {
            var processor = ScalarProcessorOfFloat64.DefaultProcessor;

            var mv2 = vector.Op(subspace.GetBlade());

            return subspace.SubspaceDimension >= 2 &&
                   (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IXGaFloat64Subspace subspace, XGaFloat64Bivector mv, bool nearZeroFlag = false)
        {
            var processor = ScalarProcessorOfFloat64.DefaultProcessor;

            var mv2 = mv - subspace.Project(mv);

            return subspace.SubspaceDimension >= 2 &&
                   (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IXGaFloat64Subspace subspace, XGaFloat64KVector mv, bool nearZeroFlag = false)
        {
            var processor = ScalarProcessorOfFloat64.DefaultProcessor;

            var mv2 = mv - subspace.Project(mv);

            return subspace.SubspaceDimension >= 2 &&
                   (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this IXGaFloat64Subspace subspace, IXGaFloat64Subspace mv, bool nearZeroFlag = false)
        {
            return subspace.Contains(mv.GetBlade(), nearZeroFlag);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector Project(this IXGaFloat64Subspace subspace, XGaFloat64Vector blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector Project(this IXGaFloat64Subspace subspace, XGaFloat64Bivector blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64KVector Project(this IXGaFloat64Subspace subspace, XGaFloat64KVector blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector Reflect(this IXGaFloat64Subspace subspace, XGaFloat64Vector blade)
        {
            return subspace
                .GetBlade()
                .Gp(-blade)
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector Reflect(this IXGaFloat64Subspace subspace, XGaFloat64Bivector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade)
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64KVector Reflect(this IXGaFloat64Subspace subspace, XGaFloat64KVector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector VersorProduct(this IXGaFloat64Subspace subspace, XGaFloat64Vector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector VersorProduct(this IXGaFloat64Subspace subspace, XGaFloat64Bivector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64KVector VersorProduct(this IXGaFloat64Subspace subspace, XGaFloat64KVector blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64KVector Complement(this IXGaFloat64Subspace subspace, XGaFloat64Vector blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64KVector Complement(this IXGaFloat64Subspace subspace, XGaFloat64Bivector blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64KVector Complement(this IXGaFloat64Subspace subspace, XGaFloat64KVector blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector Complement(this IXGaFloat64Subspace subspace, XGaFloat64Multivector blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }
    }
}