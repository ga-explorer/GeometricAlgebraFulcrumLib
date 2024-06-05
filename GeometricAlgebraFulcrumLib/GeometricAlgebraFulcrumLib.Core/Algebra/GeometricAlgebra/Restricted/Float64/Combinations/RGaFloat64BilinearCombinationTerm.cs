using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Combinations;

public sealed class RGaFloat64BilinearCombinationTerm
{
    public static RGaFloat64BilinearCombinationTerm Create(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        return new RGaFloat64BilinearCombinationTerm(
            outputBasisBlade.Sign.ToFloat64(),
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaFloat64BilinearCombinationTerm Create(Float64Scalar inputScalar, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        return new RGaFloat64BilinearCombinationTerm(
            inputScalar,
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaFloat64BilinearCombinationTerm Create(Float64Scalar inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong outputBasisBladeId)
    {
        return new RGaFloat64BilinearCombinationTerm(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );
    }


    public RGaMetric Metric { get; }

    public ulong Input1BasisBladeId { get; }

    public ulong Input2BasisBladeId { get; }

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

    public RGaBasisBlade OutputBasisBlade
        => Metric.CreateBasisBlade(OutputBasisBladeId);

    public int Input1BasisBladeGrade
        => Input1BasisBladeId.Grade();

    public int Input2BasisBladeGrade
        => Input2BasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();

    public ulong Input1BasisBladeIndex
        => Input1BasisBladeId.BasisBladeIdToIndex();

    public ulong Input2BasisBladeIndex
        => Input2BasisBladeId.BasisBladeIdToIndex();

    public ulong OutputBasisBladeIndex
        => OutputBasisBladeId.BasisBladeIdToIndex();

    public bool IsPositiveIdentity
        => OutputBasisBladeId == Input1BasisBladeId &&
           OutputBasisBladeId == Input2BasisBladeId &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId == Input1BasisBladeId &&
           OutputBasisBladeId == Input2BasisBladeId &&
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


    private RGaFloat64BilinearCombinationTerm(Float64Scalar inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        Input1BasisBladeId = input1BasisBladeId;
        Input2BasisBladeId = input2BasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }

    
    public Triplet<ulong> GetUniqueKey(bool assumeEqualInputs)
    {
        if (assumeEqualInputs)
        {
            var (id1, id2) = UInt64BitUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId
            );

            return new Triplet<ulong>(
                id1, 
                id2, 
                OutputBasisBladeId
            );
        }

        return new Triplet<ulong>(
            Input1BasisBladeId, 
            Input2BasisBladeId, 
            OutputBasisBladeId
        );
    }
}