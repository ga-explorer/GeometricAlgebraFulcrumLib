﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;

/// <summary>
/// This interface represents the information of a single vertex
/// like position, normal, color, or texture coordinates
/// </summary>
public interface IGraphicsVertex3D : 
    IGraphicsSurfaceLocalFrame3D
{
    bool HasParameterValue { get; }

    bool HasNormal { get; }

    bool HasColor { get; }

    GraphicsVertexDataKind3D DataKind { get; }
}