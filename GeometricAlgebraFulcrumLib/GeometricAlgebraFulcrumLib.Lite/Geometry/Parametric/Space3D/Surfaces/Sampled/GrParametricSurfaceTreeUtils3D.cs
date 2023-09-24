namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces.Sampled
{
    public static class GrParametricSurfaceTreeUtils3D
    {
        public static GrParametricSurfaceTreeNodeSide3D GetOppositeSide(this GrParametricSurfaceTreeNodeSide3D side)
        {
            return side switch
            {
                GrParametricSurfaceTreeNodeSide3D.SideX0 => GrParametricSurfaceTreeNodeSide3D.SideX1,
                GrParametricSurfaceTreeNodeSide3D.SideX1 => GrParametricSurfaceTreeNodeSide3D.SideX0,
                GrParametricSurfaceTreeNodeSide3D.Side0X => GrParametricSurfaceTreeNodeSide3D.Side1X,
                _ => GrParametricSurfaceTreeNodeSide3D.Side0X
            };
        }

    }
}