using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.ParametricShapes.Volumes.Sampled;

public static class GrParametricVolumeTreeUtils3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrParametricVolumeTreeNodeSide3D GetOppositeSide(this GrParametricVolumeTreeNodeSide3D side)
    {
        return side switch
        {
            GrParametricVolumeTreeNodeSide3D.SideXX0 => GrParametricVolumeTreeNodeSide3D.SideXX1,
            GrParametricVolumeTreeNodeSide3D.SideXX1 => GrParametricVolumeTreeNodeSide3D.SideXX0,
            GrParametricVolumeTreeNodeSide3D.SideX0X => GrParametricVolumeTreeNodeSide3D.SideX1X,
            GrParametricVolumeTreeNodeSide3D.SideX1X => GrParametricVolumeTreeNodeSide3D.SideX0X,
            GrParametricVolumeTreeNodeSide3D.Side0XX => GrParametricVolumeTreeNodeSide3D.Side1XX,
            _ => GrParametricVolumeTreeNodeSide3D.Side0XX
        };
    }

}