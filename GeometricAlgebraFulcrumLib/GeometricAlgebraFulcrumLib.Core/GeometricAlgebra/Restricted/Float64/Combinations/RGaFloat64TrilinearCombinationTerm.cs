using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Combinations;

public sealed class RGaFloat64TrilinearCombinationTerm
{
    public enum InputsKind
    {
        Distinct,
        EqualFirstSecond,
        EqualFirstThird,
        EqualSecondThird,
        Equal
    }

    public static RGaFloat64TrilinearCombinationTerm Create(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        return new RGaFloat64TrilinearCombinationTerm(
            outputBasisBlade.Sign.ToFloat64(),
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            input3BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaFloat64TrilinearCombinationTerm Create(Float64Scalar inputScalar, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        return new RGaFloat64TrilinearCombinationTerm(
            inputScalar,
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            input3BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaFloat64TrilinearCombinationTerm Create(Float64Scalar inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong input3BasisBladeId, ulong outputBasisBladeId)
    {
        return new RGaFloat64TrilinearCombinationTerm(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            input3BasisBladeId,
            outputBasisBladeId
        );
    }


    public RGaMetric Metric { get; }

    public ulong Input1BasisBladeId { get; }

    public ulong Input2BasisBladeId { get; }
    
    public ulong Input3BasisBladeId { get; }

    public ulong OutputBasisBladeId { get; }

    public Float64Scalar InputScalar { get; internal set; }

    public IntegerSign InputScalarSign
        => InputScalar.Sign();

    public Float64Scalar InputScalarMagnitude
        => InputScalar.Abs();

    public RGaBasisBlade Input1BasisBlade
        => Metric.CreateBasisBlade(Input1BasisBladeId);
    
    public RGaBasisBlade Input2BasisBlade
        => Metric.CreateBasisBlade(Input2BasisBladeId);

    public RGaBasisBlade Input3BasisBlade
        => Metric.CreateBasisBlade(Input3BasisBladeId);

    public RGaBasisBlade OutputBasisBlade
        => Metric.CreateBasisBlade(OutputBasisBladeId);

    public int Input1BasisBladeGrade
        => Input1BasisBladeId.Grade();

    public int Input2BasisBladeGrade
        => Input2BasisBladeId.Grade();
    
    public int Input3BasisBladeGrade
        => Input3BasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();

    public ulong Input1BasisBladeIndex
        => Input1BasisBladeId.BasisBladeIdToIndex();

    public ulong Input2BasisBladeIndex
        => Input2BasisBladeId.BasisBladeIdToIndex();
    
    public ulong Input3BasisBladeIndex
        => Input3BasisBladeId.BasisBladeIdToIndex();

    public ulong OutputBasisBladeIndex
        => OutputBasisBladeId.BasisBladeIdToIndex();

    public bool IsPositiveIdentity
        => OutputBasisBladeId == Input1BasisBladeId &&
           OutputBasisBladeId == Input2BasisBladeId &&
           OutputBasisBladeId == Input3BasisBladeId &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId == Input1BasisBladeId &&
           OutputBasisBladeId == Input2BasisBladeId &&
           OutputBasisBladeId == Input3BasisBladeId &&
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


    private RGaFloat64TrilinearCombinationTerm(Float64Scalar inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong input3BasisBladeId, ulong outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        Input1BasisBladeId = input1BasisBladeId;
        Input2BasisBladeId = input2BasisBladeId;
        Input3BasisBladeId = input3BasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }

    
    public Quad<ulong> GetUniqueKey(InputsKind inputsKind)
    {
        if (inputsKind == InputsKind.Equal)
        {
            var (id1, id2, id3) = UInt64BitUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId, 
                Input3BasisBladeId
            );

            return new Quad<ulong>(
                id1, 
                id2, 
                id3,
                OutputBasisBladeId
            );
        }
        
        if (inputsKind == InputsKind.EqualFirstSecond)
        {
            var (id1, id2) = UInt64BitUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId
            );

            return new Quad<ulong>(
                id1,
                id2,
                Input3BasisBladeId, 
                OutputBasisBladeId
            );
        }

        if (inputsKind == InputsKind.EqualFirstThird)
        {
            var (id1, id3) = UInt64BitUtils.Sort(
                Input1BasisBladeId, 
                Input3BasisBladeId
            );

            return new Quad<ulong>(
                id1,
                Input2BasisBladeId, 
                id3,
                OutputBasisBladeId
            );
        }

        if (inputsKind == InputsKind.EqualSecondThird)
        {
            var (id2, id3) = UInt64BitUtils.Sort(
                Input2BasisBladeId, 
                Input3BasisBladeId
            );

            return new Quad<ulong>(
                Input1BasisBladeId, 
                id2, 
                id3,
                OutputBasisBladeId
            );
        }

        return new Quad<ulong>(
            Input1BasisBladeId, 
            Input2BasisBladeId, 
            Input3BasisBladeId,
            OutputBasisBladeId
        );
    }
}