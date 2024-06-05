﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public sealed class PointsMeshSlicePointsPath3D : 
    PSeqSlice1D<ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public IPointsMesh3D BaseMesh { get; }


    internal PointsMeshSlicePointsPath3D(IPointsMesh3D baseMesh, int sliceDimension, int sliceIndex) 
        : base(baseMesh, sliceDimension, sliceIndex)
    {
        BaseMesh = baseMesh;
    }

        
    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            this.Select(pointMapping).ToArray()
        );
    }
}