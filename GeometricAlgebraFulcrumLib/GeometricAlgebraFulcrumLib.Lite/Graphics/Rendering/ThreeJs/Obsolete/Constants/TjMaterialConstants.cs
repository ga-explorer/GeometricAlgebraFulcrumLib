namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Constants;

public static class TjMaterialConstants
{
    public enum DepthMode
    {
        NeverDepth,
        AlwaysDepth,
        EqualDepth,
        LessDepth,
        LessEqualDepth,
        GreaterEqualDepth,
        GreaterDepth,
        NotEqualDepth
    }

    public enum BlendingMode
    {
        NoBlending,
        NormalBlending,
        AdditiveBlending,
        SubtractiveBlending,
        MultiplyBlending,
        CustomBlending
    }

    public enum RenderedSide
    {
        FrontSide,
        BackSide,
        DoubleSide
    }

    public enum StencilFunctions
    {
        NeverStencilFunc,
        LessStencilFunc,
        EqualStencilFunc,
        LessEqualStencilFunc,
        GreaterStencilFunc,
        NotEqualStencilFunc,
        GreaterEqualStencilFunc,
        AlwaysStencilFunc
    }

    public enum StencilOperations
    {
        ZeroStencilOp,
        KeepStencilOp,
        ReplaceStencilOp,
        IncrementStencilOp,
        DecrementStencilOp,
        IncrementWrapStencilOp,
        DecrementWrapStencilOp,
        InvertStencilOp
    }

    public enum TextureCombineOperations
    {
        MultiplyOperation,
        MixOperation,
        AddOperation
    }


    public static string GetName(this RenderedSide value)
    {
        return value switch
        {
            RenderedSide.BackSide => "THREE.BackSide",
            RenderedSide.FrontSide => "THREE.FrontSide",
            RenderedSide.DoubleSide => "THREE.DoubleSide",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public static string GetName(this BlendingMode value)
    {
        return value switch
        {
            BlendingMode.NoBlending => "THREE.NoBlending",
            BlendingMode.NormalBlending => "THREE.NormalBlending",
            BlendingMode.AdditiveBlending => "THREE.AdditiveBlending",
            BlendingMode.SubtractiveBlending => "THREE.SubtractiveBlending",
            BlendingMode.MultiplyBlending => "THREE.MultiplyBlending",
            BlendingMode.CustomBlending => "THREE.CustomBlending",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public static string GetName(this DepthMode value)
    {
        return value switch
        {
            DepthMode.NeverDepth => "NeverDepth",
            DepthMode.AlwaysDepth => "AlwaysDepth",
            DepthMode.EqualDepth => "EqualDepth",
            DepthMode.LessDepth => "LessDepth",
            DepthMode.LessEqualDepth => "LessEqualDepth",
            DepthMode.GreaterEqualDepth => "GreaterEqualDepth",
            DepthMode.GreaterDepth => "GreaterDepth",
            DepthMode.NotEqualDepth => "NotEqualDepth",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public static string GetName(this TextureCombineOperations value)
    {
        return value switch
        {
            TextureCombineOperations.MultiplyOperation => "THREE.MultiplyOperation",
            TextureCombineOperations.MixOperation => "THREE.MixOperation",
            TextureCombineOperations.AddOperation => "THREE.AddOperation",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
        
    public static string GetName(this StencilFunctions value)
    {
        return value switch
        {
            StencilFunctions.NeverStencilFunc => "THREE.NeverStencilFunc",
            StencilFunctions.LessStencilFunc => "THREE.LessStencilFunc",
            StencilFunctions.EqualStencilFunc => "THREE.EqualStencilFunc",
            StencilFunctions.LessEqualStencilFunc => "THREE.LessEqualStencilFunc",
            StencilFunctions.GreaterStencilFunc => "THREE.GreaterStencilFunc",
            StencilFunctions.NotEqualStencilFunc => "THREE.NotEqualStencilFunc",
            StencilFunctions.GreaterEqualStencilFunc => "THREE.GreaterEqualStencilFunc",
            StencilFunctions.AlwaysStencilFunc => "THREE.AlwaysStencilFunc",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
        
    public static string GetName(this StencilOperations value)
    {
        return value switch
        {
            StencilOperations.ZeroStencilOp => "ZeroStencilOp",
            StencilOperations.KeepStencilOp => "KeepStencilOp",
            StencilOperations.ReplaceStencilOp => "ReplaceStencilOp",
            StencilOperations.IncrementStencilOp => "IncrementStencilOp",
            StencilOperations.DecrementStencilOp => "DecrementStencilOp",
            StencilOperations.IncrementWrapStencilOp => "IncrementWrapStencilOp",
            StencilOperations.DecrementWrapStencilOp => "DecrementWrapStencilOp",
            StencilOperations.InvertStencilOp => "InvertStencilOp",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}