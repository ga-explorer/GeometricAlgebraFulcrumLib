using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Combinations;

public sealed class XGaFloat64TrilinearCombinationTerm
{
    public enum InputsKind
    {
        Distinct,
        EqualFirstSecond,
        EqualFirstThird,
        EqualSecondThird,
        Equal
    }

    public static XGaFloat64TrilinearCombinationTerm Create(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        return new XGaFloat64TrilinearCombinationTerm(
            outputBasisBlade.Sign.ToFloat64(),
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            input3BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64TrilinearCombinationTerm Create(Float64Scalar inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaFloat64TrilinearCombinationTerm(
            inputScalar,
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            input3BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaFloat64TrilinearCombinationTerm Create(Float64Scalar inputScalar, XGaMetric metric, IIndexSet input1BasisBladeId, IIndexSet input2BasisBladeId, IIndexSet input3BasisBladeId, IIndexSet outputBasisBladeId)
    {
        return new XGaFloat64TrilinearCombinationTerm(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            input3BasisBladeId,
            outputBasisBladeId
        );
    }


    public XGaMetric Metric { get; }

    public IIndexSet Input1BasisBladeId { get; }

    public IIndexSet Input2BasisBladeId { get; }
    
    public IIndexSet Input3BasisBladeId { get; }

    public IIndexSet OutputBasisBladeId { get; }

    public Float64Scalar InputScalar { get; internal set; }

    public IntegerSign InputScalarSign
        => InputScalar.Sign();

    public Float64Scalar InputScalarMagnitude
        => InputScalar.Abs();

    public XGaBasisBlade Input1BasisBlade
        => Metric.CreateBasisBlade(Input1BasisBladeId);
    
    public XGaBasisBlade Input2BasisBlade
        => Metric.CreateBasisBlade(Input2BasisBladeId);

    public XGaBasisBlade Input3BasisBlade
        => Metric.CreateBasisBlade(Input3BasisBladeId);

    public XGaBasisBlade OutputBasisBlade
        => Metric.CreateBasisBlade(OutputBasisBladeId);

    public int Input1BasisBladeGrade
        => Input1BasisBladeId.Grade();

    public int Input2BasisBladeGrade
        => Input2BasisBladeId.Grade();
    
    public int Input3BasisBladeGrade
        => Input3BasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();
    
    public bool IsPositiveIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           OutputBasisBladeId.Equals(Input3BasisBladeId) &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           OutputBasisBladeId.Equals(Input3BasisBladeId) &&
           InputScalar.IsNegativeOne();

    public bool IsGradePreserving
        => OutputBasisBladeGrade == Input1BasisBladeGrade &&
           OutputBasisBladeGrade == Input2BasisBladeGrade &&
           OutputBasisBladeGrade == Input3BasisBladeGrade &&
           !InputScalar.IsZero();

    public bool IsInputScalarZero
        => InputScalar.IsZero();

    public bool IsInputScalarPositive
        => InputScalar.IsPositive();

    public bool IsInputScalarNegative
        => InputScalar.IsNegative();


    private XGaFloat64TrilinearCombinationTerm(Float64Scalar inputScalar, XGaMetric metric, IIndexSet input1BasisBladeId, IIndexSet input2BasisBladeId, IIndexSet input3BasisBladeId, IIndexSet outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        Input1BasisBladeId = input1BasisBladeId;
        Input2BasisBladeId = input2BasisBladeId;
        Input3BasisBladeId = input3BasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }

    
    public Quad<IIndexSet> GetUniqueKey(InputsKind inputsKind)
    {
        if (inputsKind == InputsKind.Equal)
        {
            var (id1, id2, id3) = IndexSetUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId, 
                Input3BasisBladeId
            );

            return new Quad<IIndexSet>(
                id1, 
                id2, 
                id3,
                OutputBasisBladeId
            );
        }
        
        if (inputsKind == InputsKind.EqualFirstSecond)
        {
            var (id1, id2) = IndexSetUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId
            );

            return new Quad<IIndexSet>(
                id1,
                id2,
                Input3BasisBladeId, 
                OutputBasisBladeId
            );
        }

        if (inputsKind == InputsKind.EqualFirstThird)
        {
            var (id1, id3) = IndexSetUtils.Sort(
                Input1BasisBladeId, 
                Input3BasisBladeId
            );

            return new Quad<IIndexSet>(
                id1,
                Input2BasisBladeId, 
                id3,
                OutputBasisBladeId
            );
        }

        if (inputsKind == InputsKind.EqualSecondThird)
        {
            var (id2, id3) = IndexSetUtils.Sort(
                Input2BasisBladeId, 
                Input3BasisBladeId
            );

            return new Quad<IIndexSet>(
                Input1BasisBladeId, 
                id2, 
                id3,
                OutputBasisBladeId
            );
        }

        return new Quad<IIndexSet>(
            Input1BasisBladeId, 
            Input2BasisBladeId, 
            Input3BasisBladeId,
            OutputBasisBladeId
        );
    }
}