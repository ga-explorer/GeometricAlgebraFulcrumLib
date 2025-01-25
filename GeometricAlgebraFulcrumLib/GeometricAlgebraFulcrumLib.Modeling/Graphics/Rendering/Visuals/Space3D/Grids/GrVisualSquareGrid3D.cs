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
            GrVisualSquareGridPlane3D.XyPlane, 
            GrVisualGridImageComposer.Default(opacity).GetImageAsTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultXy(ITriplet<Float64Scalar> center, IGrVisualTexture texture, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultXy",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.XyPlane, 
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
            GrVisualSquareGridPlane3D.XyPlane, 
            image.ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultXy(ITriplet<Float64Scalar> center, IGrVisualImageComposer imageComposer, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultXy",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.XyPlane, 
            imageComposer.GetImageAsTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    
    public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultYz",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.YzPlane, 
            GrVisualGridImageComposer.Default(opacity).GetImageAsTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, IGrVisualTexture texture, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultYz",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.YzPlane, 
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
            GrVisualSquareGridPlane3D.YzPlane, 
            image.ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultYz(ITriplet<Float64Scalar> center, IGrVisualImageComposer imageComposer, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultYz",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.YzPlane, 
            imageComposer.GetImageAsTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    
    public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultZx",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.ZxPlane, 
            GrVisualGridImageComposer.Default(opacity).GetImageAsTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, IGrVisualTexture texture, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultZx",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.ZxPlane, 
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
            GrVisualSquareGridPlane3D.ZxPlane, 
            image.ToStoredImageTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }

    public static GrVisualSquareGrid3D DefaultZx(ITriplet<Float64Scalar> center, IGrVisualImageComposer imageComposer, int unitCount, double unitSize = 1, double opacity = 1)
    {
        return new GrVisualSquareGrid3D(
            "defaultZx",
            center.ToLinVector3D(), 
            GrVisualSquareGridPlane3D.ZxPlane, 
            imageComposer.GetImageAsTexture()
        )
        {
            UnitSize = unitSize,
            UnitCount1 = unitCount,
            UnitCount2 = unitCount,
            Opacity = opacity
        };
    }


    public IGrVisualTexture Texture { get; }

    public GrVisualSquareGridPlane3D GridPlane { get; }

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

    //public LinFloat64Vector3D Origin
    //    => GridPlane switch
    //    {
    //        GrVisualSquareGridPlane3D.XyPlane => 
    //            LinFloat64Vector3D.Create(
    //                Offset1 - 0.5 * Size1, 
    //                Offset2 - 0.5 * Size2,
    //                DistanceToOrigin
    //            ),
            
    //        GrVisualSquareGridPlane3D.YzPlane => 
    //            LinFloat64Vector3D.Create(
    //                DistanceToOrigin, 
    //                Offset1 - 0.5 * Size1, 
    //                Offset2 - 0.5 * Size2
    //            ),
            
    //        GrVisualSquareGridPlane3D.ZxPlane => 
    //            LinFloat64Vector3D.Create(
    //                Offset2 - 0.5 * Size2,
    //                DistanceToOrigin, 
    //                Offset1 - 0.5 * Size1
    //            ),

    //        _ => throw new NotSupportedException()
    //    };
    

    private GrVisualSquareGrid3D(string name, LinFloat64Vector3D center, GrVisualSquareGridPlane3D gridPlane, IGrVisualTexture texture) 
        : base(name)
    {
        Texture = texture;
        GridPlane = gridPlane;
        Center = center;
    }
    

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}