﻿using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Faces;

/// <summary>
/// This interface represents an object with vertex information like
/// position, normal, color, or texture coordinates
/// </summary>
public interface IGraphicsFaceData3D
{
    Color Color { get; set; }
        
    Normal3D Normal { get; }
        
    double NormalX { get; }

    double NormalY { get; }

    double NormalZ { get; }
        
    bool HasNormal { get; }

    bool HasColor { get; }

    GraphicsFaceDataKind3D DataKind { get; }
}