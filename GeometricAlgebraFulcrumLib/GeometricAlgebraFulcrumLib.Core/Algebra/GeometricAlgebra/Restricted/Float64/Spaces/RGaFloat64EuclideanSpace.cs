using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces;

public class RGaFloat64EuclideanSpace :
    RGaFloat64Space
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64EuclideanSpace Create(int vSpaceDimensions)
    {
        return new RGaFloat64EuclideanSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public RGaFloat64EuclideanProcessor EuclideanProcessor 
        => RGaFloat64Processor.Euclidean;

    public override RGaFloat64Processor Processor 
        => RGaFloat64Processor.Euclidean;


    internal RGaFloat64EuclideanSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }
    
}