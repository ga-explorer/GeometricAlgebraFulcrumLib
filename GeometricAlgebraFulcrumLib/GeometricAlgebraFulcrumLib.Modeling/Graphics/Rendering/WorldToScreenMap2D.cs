using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering;

public sealed class WorldToScreenMap2D
{
    public static WorldToScreenMap2D FromScreenWidth(Float64BoundingBox2D worldBox, int screenWidth, int screenMargin = 16)
    {
        var screenHeight = (int)Math.Ceiling(screenWidth * worldBox.HeightToWidthRatio);

        return new WorldToScreenMap2D(
            worldBox, 
            screenWidth, 
            screenHeight, 
            screenMargin
        );
    }

    public static WorldToScreenMap2D FromScreenHeight(Float64BoundingBox2D worldBox, int screenHeight, int screenMargin = 16)
    {
        var screenWidth = (int)Math.Ceiling(screenHeight * worldBox.WidthToHeightRatio);

        return new WorldToScreenMap2D(
            worldBox, 
            screenWidth, 
            screenHeight, 
            screenMargin
        );
    }


    public Float64BoundingBox2D WorldBox { get; }

    public double WorldMinX 
        => WorldBox.MinX;

    public double WorldMinY 
        => WorldBox.MinY;

    public double WorldMaxX 
        => WorldBox.MaxX;

    public double WorldMaxY 
        => WorldBox.MaxY;

    public int ScreenWidth { get; }

    public int ScreenHeight { get; }

    public int ScreenMargin { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public WorldToScreenMap2D(Float64BoundingBox2D worldBox, int screenWidth, int screenHeight, int screenMargin = 16)
    {
        if (screenWidth < 1 || 
            screenHeight < 1 || 
            screenMargin < 0 ||
            2 * screenMargin >= screenWidth ||
            2 * screenMargin >= screenHeight
            )
            throw new InvalidOperationException();

        WorldBox = worldBox;
        ScreenWidth = screenWidth; 
        ScreenHeight = screenHeight; 
        ScreenMargin = screenMargin;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapOrigin()
    {
        var w = ScreenWidth - 2.0 * ScreenMargin;
        var h = ScreenHeight - 2.0 * ScreenMargin;

        var sx = -WorldMinX / (WorldMaxX - WorldMinX);
        var sy = -WorldMinY / (WorldMaxY - WorldMinY);

        // flip Y to match image coordinates (y grows downward)
        var px = ScreenMargin + sx * w;
        var py = ScreenMargin + (1.0 - sy) * h;
        
        return new Pair<float>((float)px, (float)py);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapPoint(double x, double y)
    {
        var w = ScreenWidth - 2.0 * ScreenMargin;
        var h = ScreenHeight - 2.0 * ScreenMargin;

        var sx = (x - WorldMinX) / (WorldMaxX - WorldMinX);
        var sy = (y - WorldMinY) / (WorldMaxY - WorldMinY);

        // flip Y to match image coordinates (y grows downward)
        var px = ScreenMargin + sx * w;
        var py = ScreenMargin + (1.0 - sy) * h;
        
        return new Pair<float>((float)px, (float)py);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapPoint(IPair<double> point)
    {
        return MapPoint(point.Item1, point.Item2);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapVector(double x, double y)
    {
        var p1 = MapOrigin();
        var p2 = MapPoint(x, y);
        
        return new Pair<float>(
            p2.Item1 - p1.Item1,
            p2.Item2 - p1.Item2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapVector(IPair<double> vector)
    {
        return MapVector(vector.Item1, vector.Item2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapSize(double size)
    {
        var v = MapVector(size, size);

        return new Pair<float>(
            MathF.Abs(v.Item1),
            MathF.Abs(v.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<float> MapSize(double width, double height)
    {
        var v = MapVector(width, height);

        return new Pair<float>(
            MathF.Abs(v.Item1),
            MathF.Abs(v.Item2)
        );
    }

}