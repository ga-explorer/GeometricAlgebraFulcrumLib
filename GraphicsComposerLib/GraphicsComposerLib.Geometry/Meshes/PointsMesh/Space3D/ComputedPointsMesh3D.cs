using System;
using DataStructuresLib.Sequences.Periodic2D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh.Space3D
{
    public sealed class ComputedPointsMesh3D : 
        PSeqComputed2D<ITuple3D>, 
        IPointsMesh3D
    {
        public ComputedPointsMesh3D(int count1, int count2, Func<int, int, ITuple3D> mappingFunc) 
            : base(count1, count2, mappingFunc)
        {
        }

        
        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }
    }
}