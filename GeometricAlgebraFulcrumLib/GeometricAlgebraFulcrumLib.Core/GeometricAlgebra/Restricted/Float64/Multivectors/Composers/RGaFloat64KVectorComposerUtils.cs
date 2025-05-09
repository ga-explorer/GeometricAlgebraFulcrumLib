using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

public static class RGaFloat64KVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorZero(this RGaFloat64Processor metric, int grade)
    {
        return new RGaFloat64HigherKVector(metric, grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorTerm(this RGaFloat64Processor metric, ulong id)
    {
        var grade = id.Grade();

        return new RGaFloat64HigherKVector(
            metric,
            grade,
            new SingleItemDictionary<ulong, double>(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorTerm(this RGaFloat64Processor metric, ulong id, double scalar)
    {
        var grade = id.Grade();

        return scalar.IsZero()
            ? new RGaFloat64HigherKVector(metric, grade)
            : new RGaFloat64HigherKVector(metric, grade, new SingleItemDictionary<ulong, double>(id, scalar));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorTerm(this RGaFloat64Processor metric, int grade, ulong index, double scalar)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return scalar.IsZero()
            ? new RGaFloat64HigherKVector(metric, grade)
            : new RGaFloat64HigherKVector(metric, grade, new SingleItemDictionary<ulong, double>(id, scalar));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorTerm(this RGaFloat64Processor metric, IReadOnlyList<int> basisVectorIndexList)
    {
        var id = basisVectorIndexList.BasisVectorIndicesToBasisBladeId();
        var grade = id.Grade();

        return new RGaFloat64HigherKVector(
            metric, 
            grade, 
            new SingleItemDictionary<ulong, double>(id, 1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorTerm(this RGaFloat64Processor metric, IReadOnlyList<int> basisVectorIndexList, double scalar)
    {
        var id = basisVectorIndexList.BasisVectorIndicesToBasisBladeId();
        var grade = id.Grade();

        return scalar.IsZero()
            ? new RGaFloat64HigherKVector(metric, grade)
            : new RGaFloat64HigherKVector(metric, grade, new SingleItemDictionary<ulong, double>(id, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVectorTerm(this RGaFloat64Processor metric, KeyValuePair<ulong, double> term)
    {
        var (id, scalar) = term;

        var grade = id.Grade();

        return scalar.IsZero()
            ? new RGaFloat64HigherKVector(metric, grade)
            : new RGaFloat64HigherKVector(metric, grade, new SingleItemDictionary<ulong, double>(id, scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector HigherKVector(this RGaFloat64Processor metric, int grade, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
            return metric.HigherKVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
            return metric.HigherKVectorTerm(basisScalarDictionary.First());

        return new RGaFloat64HigherKVector(
            metric,
            grade,
            basisScalarDictionary
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector KVectorZero(this RGaFloat64Processor metric, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return grade switch
        {
            0 => metric.ScalarZero,
            1 => metric.VectorZero,
            2 => metric.BivectorZero,
            _ => new RGaFloat64HigherKVector(metric, grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector KVectorTerm(this RGaFloat64Processor metric, KeyValuePair<ulong, double> term)
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
    public static RGaFloat64KVector KVectorTerm(this RGaFloat64Processor metric, ulong basisBlade)
    {
        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(basisBlade, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector KVectorTerm(this RGaFloat64Processor metric, ulong basisBlade, double scalar)
    {
        var grade = basisBlade.Grade();

        if (scalar.IsZero())
            return metric.KVectorZero(grade);

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(basisBlade, scalar)
        );
    }
    
    public static RGaFloat64KVector KVectorTerm(this RGaFloat64Processor metric, IReadOnlyList<int> basisVectorIndexList)
    {
        var id = basisVectorIndexList.BasisVectorIndicesToBasisBladeId();
        var grade = id.Grade();

        if (grade == 0)
            return metric.ScalarOne;

        var idScalarDictionary =
            new SingleItemDictionary<ulong, double>(id, 1);

        if (grade == 1)
            return metric.Vector(idScalarDictionary);
            
        if (grade == 2)
            return metric.Bivector(idScalarDictionary);

        return new RGaFloat64HigherKVector(
            metric, 
            grade, 
            idScalarDictionary
        );
    }

    public static RGaFloat64KVector KVectorTerm(this RGaFloat64Processor metric, IReadOnlyList<int> basisVectorIndexList, double scalar)
    {
        var id = basisVectorIndexList.BasisVectorIndicesToBasisBladeId();
        var grade = id.Grade();

        if (scalar.IsZero())
            return metric.ScalarZero;
            
        if (grade == 0)
            return metric.Scalar(scalar);

        var idScalarDictionary =
            new SingleItemDictionary<ulong, double>(id, scalar);

        if (grade == 1)
            return metric.Vector(idScalarDictionary);
            
        if (grade == 2)
            return metric.Bivector(idScalarDictionary);

        return new RGaFloat64HigherKVector(
            metric, 
            grade, 
            idScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector PseudoScalar(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector PseudoScalar(this RGaFloat64Processor metric, int vSpaceDimensions, double scalarValue)
    {
        var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(id, scalarValue)
        );
    }

    public static RGaFloat64KVector PseudoScalarReverse(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var id =
            metric.GetBasisPseudoScalarId(vSpaceDimensions);

        var scalar =
            vSpaceDimensions.ReverseIsNegativeOfGrade()
                ? -1d : 1d;

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(id, scalar)
        );
    }

    public static RGaFloat64KVector PseudoScalarConjugate(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var id =
            metric.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            metric.HermitianConjugateSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ToFloat64();

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(id, scalar)
        );
    }

    public static RGaFloat64KVector PseudoScalarEInverse(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var id =
            metric.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            metric.EGpSquaredSign(id);

        var scalar = sign.ToFloat64();

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(id, scalar)
        );
    }

    public static RGaFloat64KVector PseudoScalarInverse(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var id =
            metric.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            metric.GpSquaredSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ToFloat64();

        return metric.KVectorTerm(
            new KeyValuePair<ulong, double>(id, scalar)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector KVector(this RGaFloat64Processor metric, int grade, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
            return metric.KVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
            return metric.KVectorTerm(basisScalarDictionary.First());

        return grade switch
        {
            0 => new RGaFloat64Scalar(metric, basisScalarDictionary),
            1 => new RGaFloat64Vector(metric, basisScalarDictionary),
            2 => new RGaFloat64Bivector(metric, basisScalarDictionary),
            _ => new RGaFloat64HigherKVector(metric, grade, basisScalarDictionary)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ToKVector(this RGaBasisBlade basisBlade)
    {
        var processor = (RGaFloat64Processor)basisBlade.Metric;

        return processor.KVectorTerm(
            basisBlade.Id,
            1d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ToKVector(this RGaSignedBasisBlade basisBlade)
    {
        var processor = (RGaFloat64Processor)basisBlade.Metric;

        return processor.KVectorTerm(
            basisBlade.Id,
            basisBlade.Sign.ToFloat64()
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ToRGaFloat64Trivector(this LinFloat64Trivector3D trivector)
    {
        return RGaFloat64Processor
            .Euclidean
            .CreateComposer()
            .SetTrivectorTerm(0, 1, 2, trivector.Scalar123)
            .GetHigherKVector(3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ToRGaFloat64Trivector(this LinFloat64Trivector3D trivector, RGaFloat64Processor processor)
    {
        return processor
            .CreateComposer()
            .SetTrivectorTerm(0, 1, 2, trivector.Scalar123)
            .GetHigherKVector(3);
    }
}