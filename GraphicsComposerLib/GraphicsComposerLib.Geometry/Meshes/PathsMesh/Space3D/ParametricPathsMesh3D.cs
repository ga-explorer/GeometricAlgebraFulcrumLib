using System;
using DataStructuresLib.Sequences.Periodic1D;
using GraphicsComposerLib.Geometry.Meshes.PointsPath;

namespace GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space3D
{
    /// <summary>
    /// This class represents a mesh composed by a parametric mapping of
    /// a real parameter to 3D point paths
    /// </summary>
    public sealed class ParametricPathsMesh3D : 
        PSeqMapped1D<double, IPointsPath3D>, 
        IPathsMesh3D
    {
        /// <summary>
        /// The mapping first input holds the parameter value, while the
        /// second input defines the number of points per computed path
        /// </summary>
        public Func<double, int, IPointsPath3D> Mapping { get; set; }

        public int PathPointsCount { get; }

        public int MeshPointsCount 
            => Count * PathPointsCount;


        public ParametricPathsMesh3D(int pathVerticesCount, IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
            PathPointsCount = pathVerticesCount;
        }

        public ParametricPathsMesh3D(int pathVerticesCount, IPeriodicSequence1D<double> parameterSequence, Func<double, int, IPointsPath3D> mapping)
            : base(parameterSequence)
        {
            PathPointsCount = pathVerticesCount;
            Mapping = mapping;
        }


        protected override IPointsPath3D MappingFunction(double input)
        {
            return Mapping(input, PathPointsCount);
        }
    }
}