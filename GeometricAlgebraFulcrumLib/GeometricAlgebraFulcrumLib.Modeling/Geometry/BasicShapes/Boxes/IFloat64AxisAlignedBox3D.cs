﻿namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Boxes;

public interface IFloat64AxisAlignedBox3D : 
    IFloat64FiniteGeometricShape3D
{
    double Corner1X { get; }

    double Corner1Y { get; }

    double Corner1Z { get; }


    double Corner2X { get; }

    double Corner2Y { get; }

    double Corner2Z { get; }
}