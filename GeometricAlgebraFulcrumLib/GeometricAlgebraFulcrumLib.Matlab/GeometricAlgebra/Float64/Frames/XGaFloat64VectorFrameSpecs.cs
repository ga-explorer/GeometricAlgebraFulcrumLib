using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;

public sealed class XGaFloat64VectorFrameSpecs
{
    
    public static XGaFloat64VectorFrameSpecs CreateUndefinedSpecs()
    {
        return new XGaFloat64VectorFrameSpecs()
        {
            LinearlyIndependent = null,
            EqualNormSquared = null,
            EqualScalarProduct = null,
            UnitNormSquared = null,
            Orthogonal = null
        };
    }

    
    public static XGaFloat64VectorFrameSpecs CreateLinearlyIndependentSpecs()
    {
        return new XGaFloat64VectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = null,
            EqualScalarProduct = null,
            UnitNormSquared = null,
            Orthogonal = null
        };
    }

    
    public static XGaFloat64VectorFrameSpecs CreateUnitBasisSpecs()
    {
        return new XGaFloat64VectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = true,
            EqualScalarProduct = true,
            UnitNormSquared = true,
            Orthogonal = true
        };
    }

    
    public static XGaFloat64VectorFrameSpecs CreateScaledBasisSpecs()
    {
        return new XGaFloat64VectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = true,
            EqualScalarProduct = true,
            UnitNormSquared = null,
            Orthogonal = true
        };
    }

    
    public static XGaFloat64VectorFrameSpecs CreateSimplexSpecs()
    {
        return new XGaFloat64VectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = true,
            EqualScalarProduct = true,
            UnitNormSquared = null,
            Orthogonal = false
        };
    }


    /// <summary>
    /// The frame vectors are linearly independent
    /// </summary>
    public bool? LinearlyIndependent { get; set; }

    /// <summary>
    /// The frame vectors are orthogonal
    /// </summary>
    public bool? Orthogonal { get; set; }

    /// <summary>
    /// The frame vectors are orthogonal and have squared norm of +1 or -1
    /// </summary>
    public bool? Orthonormal
        => Orthogonal.HasValue && UnitNormSquared.HasValue
            ? Orthogonal.Value && UnitNormSquared.Value
            : null;

    /// <summary>
    /// The frame vectors have equal squared norm
    /// </summary>
    public bool? EqualNormSquared { get; set; }

    /// <summary>
    /// All frame vectors have squared norm of +1 or -1
    /// </summary>
    public bool? UnitNormSquared { get; set; }

    /// <summary>
    /// All pairs of frame vectors have the same scalar product
    /// </summary>
    public bool? EqualScalarProduct { get; set; }

    
    
    public XGaFloat64VectorFrame CreateVectorFrame(XGaFloat64Vector vector1, XGaFloat64Vector vector2)
    {
        return XGaFloat64VectorFrame.Create(
            this,
            [vector1, vector2]
        );
    }

    
    public XGaFloat64VectorFrame CreateVectorFrame(params XGaFloat64Vector[] vectorsList)
    {
        return XGaFloat64VectorFrame.Create(this, vectorsList);
    }

    
    public XGaFloat64VectorFrame CreateVectorFrame(IEnumerable<XGaFloat64Vector> vectorsList)
    {
        return XGaFloat64VectorFrame.Create(this, vectorsList);
    }

}