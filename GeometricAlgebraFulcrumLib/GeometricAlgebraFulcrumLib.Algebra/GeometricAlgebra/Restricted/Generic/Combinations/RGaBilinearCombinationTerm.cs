using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Combinations;

public sealed class RGaBilinearCombinationTerm<T>
{
    public static RGaBilinearCombinationTerm<T> Create(RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        return new RGaBilinearCombinationTerm<T>(
            outputBasisBlade.Sign.ScalarFromNumber(metric.ScalarProcessor),
            metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaBilinearCombinationTerm<T> Create(Scalar<T> inputScalar, RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        return new RGaBilinearCombinationTerm<T>(
            inputScalar,
            metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaBilinearCombinationTerm<T> Create(Scalar<T> inputScalar, RGaProcessor<T> metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong outputBasisBladeId)
    {
        return new RGaBilinearCombinationTerm<T>(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );
    }


    public RGaProcessor<T> Metric { get; }

    public ulong Input1BasisBladeId { get; }

    public ulong Input2BasisBladeId { get; }

    public ulong OutputBasisBladeId { get; }

    public Scalar<T> InputScalar { get; internal set; }
    
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
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           InputScalar.IsMinusOne();

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


    private RGaBilinearCombinationTerm(Scalar<T> inputScalar, RGaProcessor<T> metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong outputBasisBladeId)
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