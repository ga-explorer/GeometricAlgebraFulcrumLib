﻿using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;

public sealed class LerpPointsPath2D
    : IPointsPath2D
{
    public LerpPathsMesh2D BaseMesh { get; }

    public IPointsPath2D Path1
        => BaseMesh.Path1;

    public IPointsPath2D Path2
        => BaseMesh.Path2;

    public double ParamValue { get; set; }

    public int Count
        => Path1.Count;

    public ILinFloat64Vector2D this[int index]
        => ParamValue.Lerp(
            Path1[index],
            Path2[index]
        );

    public Pair<ILinFloat64Vector2D> this[int index1, int index2]
        => new Pair<ILinFloat64Vector2D>(
            this[index1],
            this[index2]
        );

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public LerpPointsPath2D(LerpPathsMesh2D baseMesh, double paramValue)
    {
        BaseMesh = baseMesh;
        ParamValue = paramValue;
    }

        
    public bool IsValid()
    {
        return ParamValue.IsValid() && 
               Path1.IsValid() &&
               Path2.IsValid();
    }
        
    public IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping)
    {
        return new ArrayPointsPath2D(this.Select(pointMapping));
    }

    public IEnumerator<ILinFloat64Vector2D> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }
}