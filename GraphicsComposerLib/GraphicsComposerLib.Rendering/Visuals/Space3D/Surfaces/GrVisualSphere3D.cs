﻿using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualSphere3D :
    GrVisualSurface3D
{
    public ITuple3D Center { get; set; }

    public double Radius { get; set; }


    public GrVisualSphere3D(string name) 
        : base(name)
    {
    }
}