using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
    public static class RGaFloat64KVectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64HigherKVector CreateZeroHigherKVector(this RGaFloat64Processor metric, int grade)
        {
            return new RGaFloat64HigherKVector(metric, grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64HigherKVector CreateHigherKVector(this RGaFloat64Processor metric, ulong id)
        {
            var grade = id.Grade();

            return new RGaFloat64HigherKVector(
                metric,
                grade,
                new SingleItemDictionary<ulong, double>(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64HigherKVector CreateHigherKVector(this RGaFloat64Processor metric, ulong id, double scalar)
        {
            var grade = id.Grade();

            return scalar.IsZero()
                ? new RGaFloat64HigherKVector(metric, grade)
                : new RGaFloat64HigherKVector(metric, grade, new SingleItemDictionary<ulong, double>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64HigherKVector CreateHigherKVector(this RGaFloat64Processor metric, KeyValuePair<ulong, double> term)
        {
            var (id, scalar) = term;

            var grade = id.Grade();

            return scalar.IsZero()
                ? new RGaFloat64HigherKVector(metric, grade)
                : new RGaFloat64HigherKVector(metric, grade, new SingleItemDictionary<ulong, double>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64HigherKVector CreateHigherKVector(this RGaFloat64Processor metric, int grade, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
                return metric.CreateZeroHigherKVector(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
                return metric.CreateHigherKVector(basisScalarDictionary.First());

            return new RGaFloat64HigherKVector(
                metric,
                grade,
                basisScalarDictionary
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreateZeroKVector(this RGaFloat64Processor metric, int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return grade switch
            {
                0 => metric.CreateZeroScalar(),
                1 => metric.CreateZeroVector(),
                2 => metric.CreateZeroBivector(),
                _ => new RGaFloat64HigherKVector(metric, grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreateKVector(this RGaFloat64Processor metric, KeyValuePair<ulong, double> term)
        {
            var grade = term.Key.Grade();

            return grade switch
            {
                0 => new RGaFloat64Scalar(metric, term.Value),
                1 => new RGaFloat64Vector(metric, term),
                2 => new RGaFloat64Bivector(metric, term),
                _ => new RGaFloat64HigherKVector(metric, term)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreateKVector(this RGaFloat64Processor metric, int grade, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
                return metric.CreateZeroKVector(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
                return metric.CreateKVector(basisScalarDictionary.First());

            return grade switch
            {
                0 => new RGaFloat64Scalar(metric, basisScalarDictionary),
                1 => new RGaFloat64Vector(metric, basisScalarDictionary),
                2 => new RGaFloat64Bivector(metric, basisScalarDictionary),
                _ => new RGaFloat64HigherKVector(metric, grade, basisScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreateKVector(this RGaFloat64Processor metric, ulong basisBlade)
        {
            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(basisBlade, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreateKVector(this RGaFloat64Processor metric, ulong basisBlade, double scalar)
        {
            var grade = basisBlade.Grade();

            if (scalar.IsZero())
                return metric.CreateZeroKVector(grade);

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(basisBlade, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreatePseudoScalar(this RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector CreatePseudoScalar(this RGaFloat64Processor metric, int vSpaceDimensions, double scalarValue)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(id, scalarValue)
            );
        }

        public static RGaFloat64KVector CreatePseudoScalarReverse(this RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var id =
                metric.GetBasisPseudoScalarId(vSpaceDimensions);

            var scalar =
                vSpaceDimensions.ReverseIsNegativeOfGrade()
                    ? -1d : 1d;

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(id, scalar)
            );
        }

        public static RGaFloat64KVector CreatePseudoScalarConjugate(this RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var id =
                metric.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                metric.ConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToFloat64();

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(id, scalar)
            );
        }

        public static RGaFloat64KVector CreatePseudoScalarEInverse(this RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var id =
                metric.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                metric.EGpSquaredSign(id);

            var scalar = sign.ToFloat64();

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(id, scalar)
            );
        }

        public static RGaFloat64KVector CreatePseudoScalarInverse(this RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var id =
                metric.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                metric.GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToFloat64();

            return metric.CreateKVector(
                new KeyValuePair<ulong, double>(id, scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector ToKVector(this RGaBasisBlade basisBlade)
        {
            var processor = (RGaFloat64Processor)basisBlade.Metric;

            return processor.CreateKVector(
                basisBlade.Id,
                1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector ToKVector(this RGaSignedBasisBlade basisBlade)
        {
            var processor = (RGaFloat64Processor)basisBlade.Metric;

            return processor.CreateKVector(
                basisBlade.Id,
                basisBlade.Sign.ToFloat64()
            );
        }

    }
}