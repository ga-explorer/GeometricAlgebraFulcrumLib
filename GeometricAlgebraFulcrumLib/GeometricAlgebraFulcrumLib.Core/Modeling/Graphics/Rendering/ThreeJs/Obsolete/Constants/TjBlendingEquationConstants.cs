namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Constants;

public static class TjBlendingEquationConstants
{
    public enum BlendingEquations
    {
        AddEquation,
        SubtractEquation,
        ReverseSubtractEquation,
        MinEquation,
        MaxEquation
    }

    public enum SourceFactors
    {
        ZeroFactor,
        OneFactor,
        SrcColorFactor,
        OneMinusSrcColorFactor,
        SrcAlphaFactor,
        OneMinusSrcAlphaFactor,
        DstAlphaFactor,
        OneMinusDstAlphaFactor,
        DstColorFactor,
        OneMinusDstColorFactor,
        SrcAlphaSaturateFactor
    }
        
    public enum DestinationFactors
    {
        ZeroFactor,
        OneFactor,
        SrcColorFactor,
        OneMinusSrcColorFactor,
        SrcAlphaFactor,
        OneMinusSrcAlphaFactor,
        DstAlphaFactor,
        OneMinusDstAlphaFactor,
        DstColorFactor,
        OneMinusDstColorFactor
    }

        
    public static string GetName(this BlendingEquations value)
    {
        return value switch
        {
            BlendingEquations.AddEquation => "THREE.AddEquation",
            BlendingEquations.SubtractEquation => "THREE.SubtractEquation",
            BlendingEquations.ReverseSubtractEquation => "THREE.ReverseSubtractEquation",
            BlendingEquations.MinEquation => "THREE.MinEquation",
            BlendingEquations.MaxEquation => "THREE.MaxEquation",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
        
    public static string GetName(this SourceFactors value)
    {
        return value switch
        {
            SourceFactors.ZeroFactor => "ZeroFactor",
            SourceFactors.OneFactor => "OneFactor",
            SourceFactors.SrcColorFactor => "SrcColorFactor",
            SourceFactors.OneMinusSrcColorFactor => "OneMinusSrcColorFactor",
            SourceFactors.SrcAlphaFactor => "SrcAlphaFactor",
            SourceFactors.OneMinusSrcAlphaFactor => "OneMinusSrcAlphaFactor",
            SourceFactors.DstAlphaFactor => "DstAlphaFactor",
            SourceFactors.OneMinusDstAlphaFactor => "OneMinusDstAlphaFactor",
            SourceFactors.DstColorFactor => "DstColorFactor",
            SourceFactors.OneMinusDstColorFactor => "OneMinusDstColorFactor",
            SourceFactors.SrcAlphaSaturateFactor => "SrcAlphaSaturateFactor",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
        
    public static string GetName(this DestinationFactors value)
    {
        return value switch
        {
            DestinationFactors.ZeroFactor => "ZeroFactor",
            DestinationFactors.OneFactor => "OneFactor",
            DestinationFactors.SrcColorFactor => "SrcColorFactor",
            DestinationFactors.OneMinusSrcColorFactor => "OneMinusSrcColorFactor",
            DestinationFactors.SrcAlphaFactor => "SrcAlphaFactor",
            DestinationFactors.OneMinusSrcAlphaFactor => "OneMinusSrcAlphaFactor",
            DestinationFactors.DstAlphaFactor => "DstAlphaFactor",
            DestinationFactors.OneMinusDstAlphaFactor => "OneMinusDstAlphaFactor",
            DestinationFactors.DstColorFactor => "DstColorFactor",
            DestinationFactors.OneMinusDstColorFactor => "OneMinusDstColorFactor",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}