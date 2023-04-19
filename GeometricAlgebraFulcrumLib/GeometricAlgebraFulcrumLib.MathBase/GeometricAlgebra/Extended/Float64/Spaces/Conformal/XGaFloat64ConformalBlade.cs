using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public abstract class XGaFloat64ConformalBlade :
    IGeometricElement
{
    public XGaFloat64ConformalSpace Space { get; }

    public XGaFloat64ConformalProcessor Processor 
        => XGaFloat64Processor.Conformal;

    public abstract XGaFloat64KVector Blade { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64ConformalBlade(XGaFloat64ConformalSpace space)
    {
        Space = space;
    }


    public abstract bool IsValid();
}