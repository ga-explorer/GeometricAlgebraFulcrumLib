using System.Collections;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh.Space3D
{
    /// <summary>
    /// This class represents a paths mesh composed from a base 3D points mesh
    /// </summary>
    public sealed class PointsMeshAsPathsMesh3D
        : IPathsMesh3D
    {
        public IPointsMesh3D BasePointsMesh { get; }

        public int Count 
            => BasePointsMesh.Count2;

        public int PathPointsCount 
            => BasePointsMesh.Count1;

        public int MeshPointsCount 
            => Count * PathPointsCount;

        public IPointsPath3D this[int index]
            => new PointsMeshSlicePointsPath3D(BasePointsMesh, 0, index);

        public bool IsBasic
            => true;

        public bool IsOperator
            => false;


        public PointsMeshAsPathsMesh3D(IPointsMesh3D basePointsMesh)
        {
            BasePointsMesh = basePointsMesh;
        }


        public IEnumerator<IPointsPath3D> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(i => this[i])
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}