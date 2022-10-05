using System.Collections;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;

public sealed class GrVisualFrame3D :
    GrVisualElement3D,
    IGrVisualElementList3D
{
    public ITuple3D Origin { get; set; } 
        = Tuple3D.Zero;

    public ITuple3D Direction1 { get; set; } 
        = Tuple3D.E1;

    public ITuple3D Direction2 { get; set; } 
        = Tuple3D.E2;

    public ITuple3D Direction3 { get; set; } 
        = Tuple3D.E3;

    public GrVisualFrameStyle3D Style { get; set; } 

    public GrVisualImage3D? OriginTextImage { get; set; }

    public GrVisualImage3D? Direction1TextImage { get; set; }

    public GrVisualImage3D? Direction2TextImage { get; set; }

    public GrVisualImage3D? Direction3TextImage { get; set; }

    public int Count 
        => 4;

    public IGrVisualElement3D this[int index]
    {
        get
        {
            return index switch
            {
                0 => GetVisualOrigin(),
                1 => GetVisualVector1(),
                2 => GetVisualVector2(),
                3 => GetVisualVector3(),
                _ => throw new IndexOutOfRangeException()
            };
        }
    }


    public GrVisualFrame3D(string name) 
        : base(name)
    {
    }


    public GrVisualPoint3D GetVisualOrigin()
    {
        return new GrVisualPoint3D($"{Name}Origin")
        {
            Position = Origin,
            Style = new GrVisualThickSurfaceStyle3D()
            {
                Material = Style.OriginMaterial,
                Thickness = Style.OriginThickness
            }
        };
    }

    public GrVisualVector3D GetVisualVector1()
    {
        return new GrVisualVector3D($"{Name}Vector1")
        {
            Origin = Origin,
            Direction = Direction1,
            Style = new GrVisualVectorStyle3D
            {
                Material = Style.DirectionMaterial1,
                Thickness = Style.DirectionThickness
            }
        };
    }

    public GrVisualVector3D GetVisualVector2()
    {
        return new GrVisualVector3D($"{Name}Vector2")
        {
            Origin = Origin,
            Direction = Direction2,
            Style = new GrVisualVectorStyle3D
            {
                Material = Style.DirectionMaterial2,
                Thickness = Style.DirectionThickness
            }
        };
    }

    public GrVisualVector3D GetVisualVector3()
    {
        return new GrVisualVector3D($"{Name}Vector3")
        {
            Origin = Origin,
            Direction = Direction3,
            Style = new GrVisualVectorStyle3D
            {
                Material = Style.DirectionMaterial3,
                Thickness = Style.DirectionThickness
            }
        };
    }

    public IEnumerator<IGrVisualElement3D> GetEnumerator()
    {
        yield return GetVisualOrigin();
        yield return GetVisualVector1();
        yield return GetVisualVector2();
        yield return GetVisualVector3();

        if (OriginTextImage is not null) yield return OriginTextImage;
        if (Direction1TextImage is not null) yield return Direction1TextImage;
        if (Direction2TextImage is not null) yield return Direction2TextImage;
        if (Direction3TextImage is not null) yield return Direction3TextImage;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}