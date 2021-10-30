using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh.Space3D
{
    public sealed class PathsMeshAsPointsMesh3D
        : IPointsMesh3D
    {
        public IPathsMesh3D BaseMesh { get; }

        public int Count 
            => BaseMesh.MeshPointsCount;

        public ITuple3D this[int index]
        {
            get
            {
                index = index.Mod(Count);
                var index1 = index % Count1;
                var index2 = (index - index1) / Count1;

                return BaseMesh[index2][index1];
            }
        }

        public int Count1 
            => BaseMesh.PathPointsCount;

        public int Count2 
            => BaseMesh.Count;

        public ITuple3D this[int index1, int index2] 
            => BaseMesh[index2][index1];

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PathsMeshAsPointsMesh3D(IPathsMesh3D baseMesh)
        {
            BaseMesh = baseMesh;
        }


        public PSeqSlice1D<ITuple3D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public IEnumerator<ITuple3D> GetEnumerator()
        {
            return BaseMesh.SelectMany(p => p).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}