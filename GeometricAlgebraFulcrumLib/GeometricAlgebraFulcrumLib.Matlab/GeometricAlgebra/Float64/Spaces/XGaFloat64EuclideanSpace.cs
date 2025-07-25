﻿using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces;

public class XGaFloat64EuclideanSpace :
    XGaFloat64Space
{
    
    public static XGaFloat64EuclideanSpace Create(int vSpaceDimensions)
    {
        return new XGaFloat64EuclideanSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public XGaFloat64EuclideanProcessor EuclideanProcessor 
        => XGaFloat64Processor.Euclidean;

    public override XGaFloat64Processor Processor 
        => XGaFloat64Processor.Euclidean;


    internal XGaFloat64EuclideanSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }
    
}