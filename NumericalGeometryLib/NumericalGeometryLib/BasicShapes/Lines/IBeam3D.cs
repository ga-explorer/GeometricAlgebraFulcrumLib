﻿using NumericalGeometryLib.BasicMath;

namespace NumericalGeometryLib.BasicShapes.Lines
{
    public interface IBeam3D : IGeometricElement
    {
        double OriginX { get; }

        double OriginY { get; }

        double OriginZ { get; }

        double Direction1X { get; }

        double Direction1Y { get; }

        double Direction1Z { get; }

        double Direction2X { get; }

        double Direction2Y { get; }

        double Direction2Z { get; }

        double Direction3X { get; }

        double Direction3Y { get; }

        double Direction3Z { get; }
    }
}