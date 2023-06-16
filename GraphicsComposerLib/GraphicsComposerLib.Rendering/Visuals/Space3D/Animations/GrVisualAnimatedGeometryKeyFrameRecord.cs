namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public abstract record GrVisualAnimatedGeometryKeyFrameRecord(
    int FrameIndex,
    double Time,
    double Visibility
);