﻿using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;

public sealed class GrVisualXzSquareGrid3D :
    GrVisualLineGridImage3D
{
    public ITuple3D Origin { get; set; } 
        = new Tuple3D(-12, 0, -12);

    public double UnitSize { get; set; } 
        = 1;

    public int UnitCountX { get; set; } 
        = 24;

    public int UnitCountZ { get; set; } 
        = 24;

    public double SizeX 
        => UnitCountX * UnitSize;

    public double SizeZ 
        => UnitCountZ * UnitSize;

    public double Opacity { get; set; } 
        = 0.2;


    public GrVisualXzSquareGrid3D(string name) 
        : base(name)
    {
    }
}