using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Combinations;

public sealed class RGaTrilinearCombinationTerm<T>
{
    public enum InputsKind
    {
        Distinct,
        EqualFirstSecond,
        EqualFirstThird,
        EqualSecondThird,
        Equal
    }

    public static RGaTrilinearCombinationTerm<T> Create(RGaProcessor<T> metric, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        return new RGaTrilinearCombinationTerm<T>(
            outputBasisBlade.Sign.ScalarFromNumber(metric.ScalarProcessor),
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            input3BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaTrilinearCombinationTerm<T> Create(Scalar<T> inputScalar, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaBasisBlade outputBasisBlade)
    {
        return new RGaTrilinearCombinationTerm<T>(
            inputScalar,
            input1BasisBlade.Metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            input3BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static RGaTrilinearCombinationTerm<T> Create(Scalar<T> inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong input3BasisBladeId, ulong outputBasisBladeId)
    {
        return new RGaTrilinearCombinationTerm<T>(
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

    public Scalar<T> InputScalar { get; internal set; }

    public Scalar<T> InputScalarSign
        => InputScalar.Sign();

    public Scalar<T> InputScalarMagnitude
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
    
    public bool IsPositiveIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           OutputBasisBladeId.Equals(Input3BasisBladeId) &&
           InputScalar.IsOne();

    public bool IsNegativeIdentity
        => OutputBasisBladeId.Equals(Input1BasisBladeId) &&
           OutputBasisBladeId.Equals(Input2BasisBladeId) &&
           OutputBasisBladeId.Equals(Input3BasisBladeId) &&
           InputScalar.IsMinusOne();

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


    private RGaTrilinearCombinationTerm(Scalar<T> inputScalar, RGaMetric metric, ulong input1BasisBladeId, ulong input2BasisBladeId, ulong input3BasisBladeId, ulong outputBasisBladeId)
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