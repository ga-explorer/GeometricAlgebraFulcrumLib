﻿using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Combinations;

public sealed class XGaUnilinearCombinationTerm<T>
{
    public static XGaUnilinearCombinationTerm<T> Create(XGaProcessor<T> metric, XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        return new XGaUnilinearCombinationTerm<T>(
            outputBasisBlade.Sign.ScalarFromNumber(metric.ScalarProcessor),
            metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaUnilinearCombinationTerm<T> Create(Scalar<T> inputScalar, XGaProcessor<T> metric, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        return new XGaUnilinearCombinationTerm<T>(
            inputScalar,
            metric,
            inputBasisBlade.Id,
            outputBasisBlade.Id
        );
    }

    public static XGaUnilinearCombinationTerm<T> Create(Scalar<T> inputScalar, XGaProcessor<T> metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
    {
        return new XGaUnilinearCombinationTerm<T>(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );
    }


    public XGaProcessor<T> Processor { get; }

    public IndexSet InputBasisBladeId { get; }

    public IndexSet OutputBasisBladeId { get; }

    public Scalar<T> InputScalar { get; internal set; }
    
    public XGaBasisBlade InputBasisBlade
        => Processor.CreateBasisBlade(InputBasisBladeId);

    public XGaBasisBlade OutputBasisBlade
        => Processor.CreateBasisBlade(OutputBasisBladeId);

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


    private XGaUnilinearCombinationTerm(Scalar<T> inputScalar, XGaProcessor<T> metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
    {
        Processor = metric;
        InputScalar = inputScalar;
        InputBasisBladeId = inputBasisBladeId;
        OutputBasisBladeId = outputBasisBladeId;
    }


}