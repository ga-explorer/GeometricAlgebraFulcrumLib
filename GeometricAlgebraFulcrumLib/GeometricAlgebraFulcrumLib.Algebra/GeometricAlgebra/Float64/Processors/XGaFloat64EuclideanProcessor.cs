﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Spaces;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

public class XGaFloat64EuclideanProcessor :
    XGaFloat64Processor
{
    public static XGaFloat64EuclideanProcessor Instance { get; }
        = new XGaFloat64EuclideanProcessor();


    private XGaFloat64EuclideanProcessor()
        : base(0, 0)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64EuclideanSpace CreateSpace(int vSpaceDimensions)
    {
        return new XGaFloat64EuclideanSpace(vSpaceDimensions);
    }


}