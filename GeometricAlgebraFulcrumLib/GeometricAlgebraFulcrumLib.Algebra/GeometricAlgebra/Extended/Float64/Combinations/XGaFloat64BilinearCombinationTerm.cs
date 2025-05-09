using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Combinations;

public sealed class XGaFloat64BilinearCombinationTerm
{
    public static XGaFloat64BilinearCombinationTerm Create(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        return new XGaFloat64BilinearCombinationTerm(
            outputBasisBlade.Sign.ToFloat64(),
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64BilinearCombinationTerm Create(Float64Scalar inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaFloat64BilinearCombinationTerm(
            inputScalar,
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64BilinearCombinationTerm Create(Float64Scalar inputScalar, XGaMetric metric, IndexSet input1BasisBladeId, IndexSet input2BasisBladeId, IndexSet outputBasisBladeId)
    {
        return new XGaFloat64BilinearCombinationTerm(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );
    }


    public XGaMetric Metric { get; }

    public IndexSet Input1BasisBladeId { get; }

    public IndexSet Input2BasisBladeId { get; }

    public IndexSet OutputBasisBladeId { get; }

    public Float64Scalar InputScalar { get; internal set; }

    public IntegerSign InputScalarSign
        => InputScalar.Sign();

    public Float64Scalar InputScalarMagnitude
        => InputScalar.Abs();

    public XGaBasisBlade Input1BasisBlade
        => Metric.CreateBasisBlade(Input1BasisBladeId);

    public XGaBasisBlade Input2BasisBlade
        => Metric.CreateBasisBlade(Input2BasisBladeId);

    public XGaBasisBlade OutputBasisBlade
        => Metric.CreateBasisBlade(OutputBasisBladeId);

    public int Input1BasisBladeGrade
        => Input1BasisBladeId.Grade();

    public int Input2BasisBladeGrade
        => Input2BasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();
    
    public bool IsPositiveIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           InputScalar.IsNegativeOne();

    public bool IsGradePreserving
        => OutputBasisBladeGrade == Input1BasisBladeGrade &&
           OutputBasisBladeGrade == Input2BasisBladeGrade &&
           !InputScalar.IsZero();

    public bool IsInputScalarZero
        => InputScalar.IsZero();

    public bool IsInputScalarPositive
        => InputScalar.IsPositive();

    public bool IsInputScalarNegative
        => InputScalar.IsNegative();


    private XGaFloat64BilinearCombinationTerm(Float64Scalar inputScalar, XGaMetric metric, IndexSet input1BasisBladeId, IndexSet input2BasisBladeId, IndexSet outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        Input1BasisBladeId = input1BasisBladeId;
        Input2BasisBladeId = input2BasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }

    
    public Triplet<IndexSet> GetUniqueKey(bool assumeEqualInputs)
    {
        if (assumeEqualInputs)
        {
            var (id1, id2) = IndexSetUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId
            );

            return new Triplet<IndexSet>(
                id1, 
                id2, 
                OutputBasisBladeId
            );
        }

        return new Triplet<IndexSet>(
            Input1BasisBladeId, 
            Input2BasisBladeId, 
            OutputBasisBladeId
        );
    }
}