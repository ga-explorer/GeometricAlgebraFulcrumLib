﻿using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;

public sealed class MappedPointsPath2D
    : PSeqMapped1D<IFloat64Vector2D>, IPointsPath2D
{
    public IPointsPath2D BasePath { get; }

    public Func<IFloat64Vector2D, IFloat64Vector2D> Mapping { get; }


    public MappedPointsPath2D(IPointsPath2D basePath, Func<IFloat64Vector2D, IFloat64Vector2D> mapping)
        : base(basePath)
    {
        BasePath = basePath;
        Mapping = mapping;
    }


    protected override IFloat64Vector2D MappingFunction(IFloat64Vector2D input)
    {
        return Mapping(input);
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }
        
    public IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping)
    {
        return new MappedPointsPath2D(
            BasePath,
            p => pointMapping(Mapping(p))
        );
    }
}