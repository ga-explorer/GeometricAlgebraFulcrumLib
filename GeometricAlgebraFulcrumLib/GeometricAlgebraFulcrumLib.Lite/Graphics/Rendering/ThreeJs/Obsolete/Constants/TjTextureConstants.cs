namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Constants;

public static class TjTextureConstants
{
    public enum MappingModes
    {
        UvMapping,
        CubeReflectionMapping,
        CubeRefractionMapping,
        EquiRectangularReflectionMapping,
        EquiRectangularRefractionMapping,
        CubeUvReflectionMapping,
        CubeUvRefractionMapping
    }

    public enum WrappingModes
    {
        RepeatWrapping,
        ClampToEdgeWrapping,
        MirroredRepeatWrapping
    }
        
    public enum MagnificationFilters
    {
        NearestFilter,
        LinearFilter
    }
        
    public enum MinificationFilters
    {
        NearestFilter,
        NearestMipmapNearestFilter,
        NearestMipmapLinearFilter,
        LinearFilter,
        LinearMipmapNearestFilter,
        LinearMipmapLinearFilter
    }

    public enum DataTypes
    {
        UnsignedByteType,
        ByteType,
        ShortType,
        UnsignedShortType,
        IntType,
        UnsignedIntType,
        FloatType,
        HalfFloatType,
        UnsignedShort4444Type,
        UnsignedShort5551Type,
        UnsignedShort565Type,
        UnsignedInt248Type
    }

    public enum ColorFormats
    {
        AlphaFormat,
        RedFormat,
        RedIntegerFormat,
        RgFormat,
        RgIntegerFormat,
        RgbFormat,
        RgbIntegerFormat,
        RgbaFormat,
        RgbaIntegerFormat,
        LuminanceFormat,
        LuminanceAlphaFormat,
        RgbeFormat,
        DepthFormat,
        DepthStencilFormat
    }

    public enum DdsSt3CCompressedTextureFormats
    {
        RgbS3TcDxt1Format,
        RgbaS3TcDxt1Format,
        RgbaS3TcDxt3Format,
        RgbaS3TcDxt5Format,
    }

    public enum PvrtcCompressedTextureFormats
    {
        RgbPvrtc4Bppv1Format,
        RgbPvrtc2Bppv1Format,
        RgbaPvrtc4Bppv1Format,
        RgbaPvrtc2Bppv1Format,
    }

    public enum EtcCompressedTextureFormat
    {
        RgbEtc1Format,
        RgbEtc2Format,
        RgbaEtc2EacFormat
    }

    public enum AstcCompressedTextureFormat
    {
        RgbaAstc4X4Format,
        RgbaAstc5X4Format,
        RgbaAstc5X5Format,
        RgbaAstc6X5Format,
        RgbaAstc6X6Format,
        RgbaAstc8X5Format,
        RgbaAstc8X6Format,
        RgbaAstc8X8Format,
        RgbaAstc10X5Format,
        RgbaAstc10X6Format,
        RgbaAstc10X8Format,
        RgbaAstc10X10Format,
        RgbaAstc12X10Format,
        RgbaAstc12X12Format,
        Srgb8Alpha8Astc4X4Format,
        Srgb8Alpha8Astc5X4Format,
        Srgb8Alpha8Astc5X5Format,
        Srgb8Alpha8Astc6X5Format,
        Srgb8Alpha8Astc6X6Format,
        Srgb8Alpha8Astc8X5Format,
        Srgb8Alpha8Astc8X6Format,
        Srgb8Alpha8Astc8X8Format,
        Srgb8Alpha8Astc10X5Format,
        Srgb8Alpha8Astc10X6Format,
        Srgb8Alpha8Astc10X8Format,
        Srgb8Alpha8Astc10X10Format,
        Srgb8Alpha8Astc12X10Format,
        Srgb8Alpha8Astc12X12Format
    }

    public enum ColorInternalFormats
    {
        Alpha,
        Rgb,
        Rgba,
        Luminance,
        LuminanceAlpha,
        RedInteger,
        R8,
        R8SNorm,
        R8I,
        R8Ui,
        R16I,
        R16Ui,
        R16F,
        R32I,
        R32Ui,
        R32F,
        Rg8,
        Rg8SNorm,
        Rg8I,
        Rg8Ui,
        Rg16I,
        Rg16Ui,
        Rg16F,
        Rg32I,
        Rg32Ui,
        Rg32F,
        Rgb565,
        Rgb8,
        Rgb8SNorm,
        Rgb8I,
        Rgb8Ui,
        Rgb16I,
        Rgb16Ui,
        Rgb16F,
        Rgb32I,
        Rgb32Ui,
        Rgb32F,
        Rgb9E5,
        Srgb8,
        R11Fg11Fb10F,
        Rgba4,
        Rgba8,
        Rgba8SNorm,
        Rgba8I,
        Rgba8Ui,
        Rgba16I,
        Rgba16Ui,
        Rgba16F,
        Rgba32I,
        Rgba32Ui,
        Rgba32F,
        Rgb5A1,
        Rgb10A2,
        Rgb10A2Ui,
        Srgb8Alpha8,
        DepthComponent16,
        DepthComponent24,
        DepthComponent32F,
        Depth24Stencil8,
        Depth32FStencil8
    }

    public enum ColorEncodings
    {
        LinearEncoding,
        SRgbEncoding,
        GammaEncoding,
        RgbeEncoding,
        LogLuvEncoding,
        RgbM7Encoding,
        RgbM16Encoding,
        RgbDEncoding,
        BasicDepthPacking,
        RgbaDepthPacking,
    }

        
}