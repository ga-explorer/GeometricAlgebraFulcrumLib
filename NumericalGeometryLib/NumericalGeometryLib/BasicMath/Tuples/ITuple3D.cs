﻿using DataStructuresLib.Basic;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public interface ITuple3D : 
        IGeometricElement, 
        ITriplet<double>
    {
        double X { get; }

        double Y { get; }

        double Z { get; }
    }
}