using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PathsMesh.Space2D;

/// <summary>
/// This class represents a mesh composed by a parametric mapping of
/// a real parameter to 2D point paths
/// </summary>
public sealed class ParametricPathsMesh2D : 
    PSeqMapped1D<double, IPointsPath2D>, 
    IPathsMesh2D
{
    /// <summary>
    /// The mapping first input holds the parameter value, while the
    /// second input defines the number of points per computed path
    /// </summary>
    public Func<double, int, IPointsPath2D> Mapping { get; set; }

    public int PathPointsCount { get; }

    public int MeshPointsCount
        => Count * PathPointsCount;


    public ParametricPathsMesh2D(int pathVerticesCount, IPeriodicSequence1D<double> parameterSequence)
        : base(parameterSequence)
    {
        PathPointsCount = pathVerticesCount;
    }

    public ParametricPathsMesh2D(int pathVerticesCount, IPeriodicSequence1D<double> parameterSequence, Func<double, int, IPointsPath2D> mapping)
        : base(parameterSequence)
    {
        PathPointsCount = pathVerticesCount;
        Mapping = mapping;
    }


    protected override IPointsPath2D MappingFunction(double input)
    {
        return Mapping(input, PathPointsCount);
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}