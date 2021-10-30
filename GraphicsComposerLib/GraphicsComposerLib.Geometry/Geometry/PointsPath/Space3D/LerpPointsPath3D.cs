using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space3D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D
{
    public sealed class LerpPointsPath3D 
        : IPointsPath3D
    {
        public LerpPathsMesh3D BaseMesh { get; }

        public IPointsPath3D Path1 
            => BaseMesh.Path1;

        public IPointsPath3D Path2 
            => BaseMesh.Path2;

        public double ParamValue { get; set; }

        public int Count 
            => Path1.Count;

        public ITuple3D this[int index] 
            => ParamValue.Lerp(
                Path1[index], 
                Path2[index]
            );

        public Pair<ITuple3D> this[int index1, int index2] 
            => new Pair<ITuple3D>(
                this[index1],
                this[index2]
            );

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public LerpPointsPath3D(LerpPathsMesh3D baseMesh)
        {
            BaseMesh = baseMesh;
            ParamValue = 0.5d;
        }

        public LerpPointsPath3D(LerpPathsMesh3D baseMesh, double paramValue)
        {
            BaseMesh = baseMesh;
            ParamValue = paramValue;
        }


        public IEnumerator<ITuple3D> GetEnumerator()
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
}
