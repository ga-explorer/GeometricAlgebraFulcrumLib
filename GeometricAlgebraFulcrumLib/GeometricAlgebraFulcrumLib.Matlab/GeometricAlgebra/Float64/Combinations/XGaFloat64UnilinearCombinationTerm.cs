using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Combinations;

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

    public static XGaFloat64UnilinearCombinationTerm Create(double inputScalar, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaFloat64UnilinearCombinationTerm(
            inputScalar,
            inputBasisBlade.Metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64UnilinearCombinationTerm Create(double inputScalar, XGaMetric metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
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

    public double InputScalar { get; internal set; }

    public IntegerSign InputScalarSign
        => InputScalar.Sign();

    public double InputScalarMagnitude
        => InputScalar.Abs();

    public XGaBasisBlade InputBasisBlade
        => Metric.BasisBlade(InputBasisBladeId);

    public XGaBasisBlade OutputBasisBlade
        => Metric.BasisBlade(OutputBasisBladeId);

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


    private XGaFloat64UnilinearCombinationTerm(double inputScalar, XGaMetric metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        InputBasisBladeId = inputBasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }


}