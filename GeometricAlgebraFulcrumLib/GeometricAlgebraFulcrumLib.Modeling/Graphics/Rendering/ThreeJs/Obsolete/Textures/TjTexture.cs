using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Math;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Textures;

public class TjTexture :
    TjTextureBase
{
    public override string JavaScriptClassName 
        => "Texture";

    public TjTextureConstants.MappingModes MappingMode { get; set; }
        = TjTextureConstants.MappingModes.UvMapping;

    public TjTextureConstants.WrappingModes WrappingModeS { get; set; }
        = TjTextureConstants.WrappingModes.ClampToEdgeWrapping;

    public TjTextureConstants.WrappingModes WrappingModeT { get; set; }
        = TjTextureConstants.WrappingModes.ClampToEdgeWrapping;

    public TjTextureConstants.MagnificationFilters MagnificationFilter { get; set; }
        = TjTextureConstants.MagnificationFilters.LinearFilter;

    public TjTextureConstants.MinificationFilters MinificationFilter { get; set; }
        = TjTextureConstants.MinificationFilters.LinearMipmapLinearFilter;

    public int Anisotropy { get; set; } = 1;

    public TjTextureConstants.ColorFormats ColorFormat { get; set; }
        = TjTextureConstants.ColorFormats.RgbaFormat;

    //public TjTextureConstants.InternalFormats InternalFormat { get; set; }

    public TjTextureConstants.DataTypes DataType { get; set; }
        = TjTextureConstants.DataTypes.UnsignedByteType;

    public TjVector2 Offset { get; set; } = null;

    public TjVector2 Repeat { get; set; } = null;

    public double RotationAngle { get; set; } = 0d;

    public TjVector2 RotationCenter { get; set; } = new TjVector2(0, 0);

    public bool MatrixAutoUpdate { get; set; } = true;

    public TjMatrix3 Matrix { get; set; } = null;

    public bool GenerateMipMaps { get; set; } = true;

    public bool PreMultiplyAlpha { get; set; } = false;

    public bool FlipY { get; set; } = true;

    public int UnpackAlignment { get; set; } = 4;

    public TjTextureConstants.ColorEncodings ColorEncoding { get; set; }
        = TjTextureConstants.ColorEncodings.LinearEncoding;


}