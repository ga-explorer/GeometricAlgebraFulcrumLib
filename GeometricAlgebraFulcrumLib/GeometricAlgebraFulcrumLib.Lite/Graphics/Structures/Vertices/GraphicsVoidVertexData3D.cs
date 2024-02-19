﻿using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;

public sealed class GraphicsVoidVertexData3D 
    : IGraphicsVertexData3D
{
    public static GraphicsVoidVertexData3D DefaultData { get; }
        = new GraphicsVoidVertexData3D();


    public IFloat64Vector2D TextureUv
    {
        get => Float64Vector2D.Zero;
        set => throw new InvalidOperationException();
    }

    public Normal3D Normal
        => null;

    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public bool HasTextureUv 
        => false;

    public bool HasNormal 
        => false;

    public bool HasColor 
        => false;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.VoidData;
        
    public double TextureU 
        => 0;

    public double TextureV 
        => 0;

    public double NormalX 
        => 0;

    public double NormalY 
        => 0;

    public double NormalZ 
        => 0;
        

    private GraphicsVoidVertexData3D()
    {
    }
}