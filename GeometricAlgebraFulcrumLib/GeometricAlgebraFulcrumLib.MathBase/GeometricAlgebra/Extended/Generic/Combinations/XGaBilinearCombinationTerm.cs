﻿using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Combinations;

public sealed class XGaBilinearCombinationTerm<T>
{
    public static XGaBilinearCombinationTerm<T> Create(XGaProcessor<T> metric, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        return new XGaBilinearCombinationTerm<T>(
            outputBasisBlade.Sign.CreateScalar(metric.ScalarProcessor),
            metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaBilinearCombinationTerm<T> Create(Scalar<T> inputScalar, XGaProcessor<T> metric, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaBilinearCombinationTerm<T>(
            inputScalar,
            metric,
            input1BasisBlade.Id,
            input2BasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaBilinearCombinationTerm<T> Create(Scalar<T> inputScalar, XGaProcessor<T> metric, IIndexSet input1BasisBladeId, IIndexSet input2BasisBladeId, IIndexSet outputBasisBladeId)
    {
        return new XGaBilinearCombinationTerm<T>(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            outputBasisBladeId
        );
    }


    public XGaProcessor<T> Metric { get; }

    public IIndexSet Input1BasisBladeId { get; }

    public IIndexSet Input2BasisBladeId { get; }

    public IIndexSet OutputBasisBladeId { get; }

    public Scalar<T> InputScalar { get; internal set; }
    
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


    private XGaBilinearCombinationTerm(Scalar<T> inputScalar, XGaProcessor<T> metric, IIndexSet input1BasisBladeId, IIndexSet input2BasisBladeId, IIndexSet outputBasisBladeId)
    {
        Metric = metric;
        InputScalar = inputScalar;
        Input1BasisBladeId = input1BasisBladeId;
        Input2BasisBladeId = input2BasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }

    
    public Triplet<IIndexSet> GetUniqueKey(bool assumeEqualInputs)
    {
        if (assumeEqualInputs)
        {
            var (id1, id2) = IndexSetUtils.Sort(
                Input1BasisBladeId, 
                Input2BasisBladeId
            );

            return new Triplet<IIndexSet>(
                id1, 
                id2, 
                OutputBasisBladeId
            );
        }

        return new Triplet<IIndexSet>(
            Input1BasisBladeId, 
            Input2BasisBladeId, 
            OutputBasisBladeId
        );
    }
}