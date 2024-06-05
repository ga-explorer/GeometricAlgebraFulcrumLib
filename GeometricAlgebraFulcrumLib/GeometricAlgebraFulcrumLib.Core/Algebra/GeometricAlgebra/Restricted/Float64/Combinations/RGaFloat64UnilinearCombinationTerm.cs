using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Combinations;

public sealed class RGaFloat64UnilinearCombinationTerm
{
    public static RGaFloat64UnilinearCombinationTerm Create(RGaBasisBlade inputBasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        return new RGaFloat64UnilinearCombinationTerm(
            outputBasisBlade.Sign.ToFloat64(),
            inputBasisBlade.Metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaFloat64UnilinearCombinationTerm Create(Float64Scalar inputScalar, RGaBasisBlade inputBasisBlade, RGaBasisBlade outputBasisBlade)
    {
        return new RGaFloat64UnilinearCombinationTerm(
            inputScalar,
            inputBasisBlade.Metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaFloat64UnilinearCombinationTerm Create(Float64Scalar inputScalar, RGaMetric metric, ulong inputBasisBladeId, ulong outputBasisBladeId)
    {
        return new RGaFloat64UnilinearCombinationTerm(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );
    }


    public RGaMetric Metric { get; }

    public ulong InputBasisBladeId { get; }

    public ulong OutputBasisBladeId { get; }

    public Float64Scalar InputScalar { get; internal set; }

    public IntegerSign InputScalarSign
        => InputScalar.Sign();

    public Float64Scalar InputScalarMagnitude
        => InputScalar.Abs();

    public RGaBasisBlade InputBasisBlade
        => Metric.CreateBasisBlade(InputBasisBladeId);

    public RGaBasisBlade OutputBasisBlade
        => Metric.CreateBasisBlade(OutputBasisBladeId);

    public int InputBasisBladeGrade
        => InputBasisBladeId.Grade();

    public int OutputBasisBladeGrade
        => OutputBasisBladeId.Grade();

    public ulong InputBasisBladeIndex
        => InputBasisBladeId.BasisBladeIdToIndex();

    public ulong OutputBasisBladeIndex
        => OutputBasisBladeId.BasisBladeIdToIndex();

    public bool IsPositiveIdentity
        => OutputBasisBladeId == InputBasisBladeId &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId == InputBasisBladeId &&
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


    private RGaFloat64UnilinearCombinationTerm(Float64Scalar inputScalar, RGaMetric metric, ulong inputBasisBladeId, ulong outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        InputBasisBladeId = inputBasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }


    public Pair<ulong> GetUniqueKey()
    {
        return new Pair<ulong>(
            InputBasisBladeId, 
            OutputBasisBladeId
        );
    }


    public override string ToString()
    {
        var inputId =
            InputBasisBladeId
                .BasisBladeIdToBasisVectorIndices()
                .Select(i => i.ToString())
                .ConcatenateText(",", "<", ">");

        var outputId =
            OutputBasisBladeId
                .BasisBladeIdToBasisVectorIndices()
                .Select(i => i.ToString())
                .ConcatenateText(",", "<", ">");

        return $"{outputId} += ({InputScalar:G}){inputId}";
    }
}