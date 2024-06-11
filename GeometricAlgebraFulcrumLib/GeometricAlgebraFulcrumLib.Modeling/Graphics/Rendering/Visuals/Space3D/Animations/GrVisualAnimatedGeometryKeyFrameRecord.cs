namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public abstract record GrVisualAnimatedGeometryKeyFrameRecord(
    int FrameIndex,
    double Time,
    double Visibility
);