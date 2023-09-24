using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Combinations;

public sealed class XGaUnilinearCombinationTerm<T>
{
    public static XGaUnilinearCombinationTerm<T> Create(XGaProcessor<T> metric, XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        return new XGaUnilinearCombinationTerm<T>(
            outputBasisBlade.Sign.CreateScalar(metric.ScalarProcessor),
            metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaUnilinearCombinationTerm<T> Create(Scalar<T> inputScalar, XGaProcessor<T> metric, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaUnilinearCombinationTerm<T>(
            inputScalar,
            metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaUnilinearCombinationTerm<T> Create(Scalar<T> inputScalar, XGaProcessor<T> metric, IIndexSet inputBasisBladeId, IIndexSet outputBasisBladeId)
    {
        return new XGaUnilinearCombinationTerm<T>(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );
    }


    public XGaProcessor<T> Processor { get; }

    public IIndexSet InputBasisBladeId { get; }

    public IIndexSet OutputBasisBladeId { get; }

    public Scalar<T> InputScalar { get; internal set; }
    
    public XGaBasisBlade InputBasisBlade
        => Processor.CreateBasisBlade(InputBasisBladeId);

    public XGaBasisBlade OutputBasisBlade
        => Processor.CreateBasisBlade(OutputBasisBladeId);

    public int InputBasisBladeGrade
        => InputBasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();
    
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


    private XGaUnilinearCombinationTerm(Scalar<T> inputScalar, XGaProcessor<T> metric, IIndexSet inputBasisBladeId, IIndexSet outputBasisBladeId)
    {
        Processor = metric;
        InputScalar = inputScalar;
        InputBasisBladeId = inputBasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }


}