using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Combinations;

public sealed class RGaUnilinearCombinationTerm<T>
{
    public static RGaUnilinearCombinationTerm<T> Create(RGaProcessor<T> metric, RGaBasisBlade inputBasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        return new RGaUnilinearCombinationTerm<T>(
            outputBasisBlade.Sign.ScalarFromNumber(metric.ScalarProcessor),
            metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaUnilinearCombinationTerm<T> Create(Scalar<T> inputScalar, RGaProcessor<T> metric, RGaBasisBlade inputBasisBlade, RGaBasisBlade outputBasisBlade)
    {
        return new RGaUnilinearCombinationTerm<T>(
            inputScalar,
            metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaUnilinearCombinationTerm<T> Create(Scalar<T> inputScalar, RGaProcessor<T> metric, ulong inputBasisBladeId, ulong outputBasisBladeId)
    {
        return new RGaUnilinearCombinationTerm<T>(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );
    }


    public RGaProcessor<T> Processor { get; }

    public ulong InputBasisBladeId { get; }

    public ulong OutputBasisBladeId { get; }

    public Scalar<T> InputScalar { get; internal set; }
    
    public RGaBasisBlade InputBasisBlade
        => Processor.CreateBasisBlade(InputBasisBladeId);

    public RGaBasisBlade OutputBasisBlade
        => Processor.CreateBasisBlade(OutputBasisBladeId);

    public int InputBasisBladeGrade
        => InputBasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();
    
    public ulong InputBasisBladeIndex
        => InputBasisBladeId.BasisBladeIdToIndex();

    public ulong OutputBasisBladeIndex
        => OutputBasisBladeId.BasisBladeIdToIndex();

    public bool IsPositiveIdentity
        => OutputBasisBladeId.Equals(InputBasisBladeId) &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId.Equals(InputBasisBladeId) &&
           InputScalar.IsMinusOne();

    public bool IsGradePreserving
        => OutputBasisBladeGrade == InputBasisBladeGrade &&
           !InputScalar.IsZero();

    public bool IsInputScalarZero
        => InputScalar.IsZero();

    public bool IsInputScalarPositive
        => InputScalar.IsPositive();

    public bool IsInputScalarNegative
        => InputScalar.IsNegative();


    private RGaUnilinearCombinationTerm(Scalar<T> inputScalar, RGaProcessor<T> metric, ulong inputBasisBladeId, ulong outputBasisBladeId)
    {
        Processor = metric;
        InputScalar = inputScalar;
        InputBasisBladeId = inputBasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }


}