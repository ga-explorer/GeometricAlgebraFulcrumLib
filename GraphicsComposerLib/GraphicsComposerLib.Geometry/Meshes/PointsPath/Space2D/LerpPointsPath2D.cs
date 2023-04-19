using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space2D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D
{
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

        public IFloat64Tuple2D this[int index]
            => ParamValue.Lerp(
                Path1[index],
                Path2[index]
            );

        public Pair<IFloat64Tuple2D> this[int index1, int index2]
            => new Pair<IFloat64Tuple2D>(
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


        public IEnumerator<IFloat64Tuple2D> GetEnumerator()
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