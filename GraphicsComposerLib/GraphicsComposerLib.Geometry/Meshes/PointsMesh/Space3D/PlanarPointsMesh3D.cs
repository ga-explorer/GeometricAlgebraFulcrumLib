using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh.Space3D
{
    public class PlanarPointsMesh3D
        : IPointsMesh3D
    {
        public ITuple3D Origin { get; set; }

        public ITuple3D Direction1 { get; set; }

        public ITuple3D Direction2 { get; set; }

        public IPeriodicSequence1D<double> Parameters1 { get; }

        public IPeriodicSequence1D<double> Parameters2 { get; }

        public int Count 
            => Parameters1.Count * Parameters2.Count;

        public int Count1 
            => Parameters1.Count;

        public int Count2 
            => Parameters2.Count;

        public ITuple3D this[int index]
        {
            get
            {
                index = index.Mod(Count);
                var index1 = index % Count1;
                var index2 = (index - index1) / Count1;

                return this[index1, index2];
            }
        }

        public ITuple3D this[int index1, int index2]
        {
            get
            {
                var t1 = Parameters1[index1];
                var t2 = Parameters2[index2];

                return new Tuple3D(
                    Origin.X + t1 * Direction1.X + t2 * Direction2.X,
                    Origin.Y + t1 * Direction1.Y + t2 * Direction2.Y,
                    Origin.Z + t1 * Direction1.Z + t2 * Direction2.Z
                );
            }
        }

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PlanarPointsMesh3D(IPeriodicSequence1D<double> parameters1, IPeriodicSequence1D<double> parameters2)
        {
            Parameters1 = parameters1;
            Parameters2 = parameters2;

            Origin = new Tuple3D(0, 0, 0);
            Direction1 = new Tuple3D(1, 0, 0);
            Direction2 = new Tuple3D(0, 1, 0);
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
            for (var i2 = 0; i2 < Count2; i2++)
            for (var i1 = 0; i1 < Count1; i1++)
                yield return this[i1, i2];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}