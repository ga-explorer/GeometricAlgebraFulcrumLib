using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Combinations;

public sealed class XGaFloat64UnilinearCombinationTerm
{
    public static XGaFloat64UnilinearCombinationTerm Create(XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        return new XGaFloat64UnilinearCombinationTerm(
            outputBasisBlade.Sign.ToFloat64(),
            inputBasisBlade.Metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64UnilinearCombinationTerm Create(Float64Scalar inputScalar, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaFloat64UnilinearCombinationTerm(
            inputScalar,
            inputBasisBlade.Metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64UnilinearCombinationTerm Create(Float64Scalar inputScalar, XGaMetric metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
    {
        return new XGaFloat64UnilinearCombinationTerm(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );
    }


    public XGaMetric Metric { get; }

    public IndexSet InputBasisBladeId { get; }

    public IndexSet OutputBasisBladeId { get; }

    public Float64Scalar InputScalar { get; internal set; }

    public IntegerSign InputScalarSign
        => InputScalar.Sign();

    public Float64Scalar InputScalarMagnitude
        => InputScalar.Abs();

    public XGaBasisBlade InputBasisBlade
        => Metric.CreateBasisBlade(InputBasisBladeId);

    public XGaBasisBlade OutputBasisBlade
        => Metric.CreateBasisBlade(OutputBasisBladeId);

    public int InputBasisBladeGrade
        => InputBasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();
    
    public bool IsPositiveIdentity
        => OutputBasisBladeId.Equals(InputBasisBladeId) &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId.Equals(InputBasisBladeId) &&
           InputScalar.IsNegativeOne();

    public bool IsGradePreserving
        => OutputBasisBladeGrade == InputBasisBladeGrade &&
           !InputScalar.IsZero();

    public bool IsInputScalarZero
        => InputScalar.IsZero();

    public bool IsInputScalarPositive
        => InputScalar.IsPositive();

    public bool IsInputScalarNegative
        => InputScalar.IsNegative();


    private XGaFloat64UnilinearCombinationTerm(Float64Scalar inputScalar, XGaMetric metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        InputBasisBladeId = inputBasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }


}