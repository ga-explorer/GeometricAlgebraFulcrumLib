using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

/// <summary>
/// Conformal Geometric Algebra for 2D Euclidean Space
/// </summary>
public sealed class CGaFloat64GeometricSpace4D :
    CGaFloat64GeometricSpace
{
    public static CGaFloat64GeometricSpace4D Instance { get; }
        = new CGaFloat64GeometricSpace4D();


    public CGaFloat64Visualizer Visualizer { get; }
    
    public GrBabylonJsGeometryAnimationComposer VisualizerAnimationComposer 
        => Visualizer.AnimationComposer;
    
    public WclKaTeXComposer VisualizerKaTeXComposer 
        => Visualizer.AnimationComposer.KaTeXComposer;

    public GrBabylonJsSceneComposer VisualizerSceneComposer 
        => Visualizer.AnimationComposer.SceneComposer;


    private CGaFloat64GeometricSpace4D()
        : base(4)
    {
        Visualizer = new CGaFloat64Visualizer(this);
    }
}