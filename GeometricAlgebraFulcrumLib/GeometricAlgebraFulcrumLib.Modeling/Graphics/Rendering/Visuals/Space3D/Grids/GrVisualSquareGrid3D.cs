using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;

public sealed class GrVisualSquareGrid3D :
    GrVisualElement3D
{
    public static GrVisualSquareGrid3D DefaultXy(ITriplet<Float64Scalar> center, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultXy",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.Identity, 
            GrVisualGridImageComposer.Default(opacity).GetImage().ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultXy(ITriplet<Float64Scalar> center, IGrVisualImageSource texture, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultXy",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.Identity, 
            texture
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultXy(ITriplet<Float64Scalar> center, Image image, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultXy",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.Identity, 
            image.ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    //public static GrVisualSquareGrid3D DefaultXy(ITriplet<Float64Scalar> center, IGrVisualImageSource imageComposer, int unitCount, double unitSize = 1, double opacity = 1)
    //{
    //    return new GrVisualSquareGrid3D(
    //        "defaultXy",
    //        center.ToLinVector3D(), 
    //        LinFloat64Quaternion.Identity, 
    //        imageComposer.GetImageAsTexture()
    //    )
    //    {
    //        UnitSize = unitSize,
    //        UnitCount1 = unitCount,
    //        UnitCount2 = unitCount,
    //        Opacity = opacity
    //    };
    //}

    
    public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultYz",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.XyToYz, 
            GrVisualGridImageComposer.Default(opacity).GetImage().ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, IGrVisualImageSource texture, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultYz",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.XyToYz, 
            texture
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }
    
    public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, Image image, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultYz",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.XyToYz, 
            image.ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    //public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, IGrVisualImageComposer imageComposer, int unitCount, double unitSize = 1, double opacity = 1)
    //{
    //    return new GrVisualSquareGrid3D(
    //        "defaultYz",
    //        center.ToLinVector3D(), 
    //        LinFloat64Quaternion.XyToYz, 
    //        imageComposer.GetImageAsTexture()
    //    )
    //    {
    //        UnitSize = unitSize,
    //        UnitCount1 = unitCount,
    //        UnitCount2 = unitCount,
    //        Opacity = opacity
    //    };
    //}

    
    public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultZx",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.XyToZx, 
            GrVisualGridImageComposer.Default(opacity).GetImage().ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, IGrVisualImageSource texture, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultZx",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.XyToZx, 
            texture
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }
    
    public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, Image image, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultZx",
            center.ToLinVector3D(), 
            LinFloat64Quaternion.XyToZx, 
            image.ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    //public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, IGrVisualImageComposer imageComposer, int unitCount, double unitSize = 1, double opacity = 1)
    //{
    //    return new GrVisualSquareGrid3D(
    //        "defaultZx",
    //        center.ToLinVector3D(), 
    //        LinFloat64Quaternion.XyToZx, 
    //        imageComposer.GetImageAsTexture()
    //    )
    //    {
    //        UnitSize = unitSize,
    //        UnitCount1 = unitCount,
    //        UnitCount2 = unitCount,
    //        Opacity = opacity
    //    };
    //}


    public static GrVisualSquareGrid3D Default(string name, ITriplet<Float64Scalar> center, LinFloat64Quaternion orientation, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            name,
            center.ToLinVector3D(),
            orientation,
            GrVisualGridImageComposer.Default(opacity).GetImage().ToStoredImageTexture()
        )
        {
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            UnitSize = unitSize,
            Opacity = opacity
        };
    }


    public IGrVisualImageSource Texture { get; }

    public LinFloat64Quaternion Orientation { get; }

    public LinFloat64Vector3D Center { get; }

    public double UnitSize { get; init; } 
        = 1;

    public int UnitCount1 { get; init; } 
        = 24;

    public int UnitCount2 { get; init; } 
        = 24;

    public double Size1 
        => UnitCount1 * UnitSize;

    public double Size2 
        => UnitCount2 * UnitSize;

    public double Opacity { get; init; } 
        = 0.2;


    private GrVisualSquareGrid3D(string name, LinFloat64Vector3D center, LinFloat64Quaternion orientation, IGrVisualImageSource texture) 
        : base(name)
    {
        Texture = texture;
        Orientation = orientation;
        Center = center;
    }
    

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}