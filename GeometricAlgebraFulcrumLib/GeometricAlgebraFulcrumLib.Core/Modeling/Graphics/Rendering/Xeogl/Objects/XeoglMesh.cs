using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry.Builtin;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Materials;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using JsCodeComponentUtils = GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.Obsolete.JsCodeComponentUtils;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Objects;

/// <summary>
/// A Mesh is an Object that represents a drawable 3D primitive.
/// </summary>
/// <remarks>
/// A Mesh represents a WebGL draw call.
/// Each Mesh has six components: Geometry for shape, Material for normal
/// rendered appearance, three EmphasisMaterials for ghosted, highlighted
/// and selected effects, and EdgeMaterial for rendering emphasized edges.
/// By default, Meshes in the same Scene share the same global scene flyweight
/// instances of those components among themselves. The default component
/// instances are provided by the Scene's geometry, material, ghostMaterial,
/// highlightMaterial, selectedMaterial and edgeMaterial properties.
/// A Mesh with all defaults is a white unit-sized box centered at the World-
/// space origin.
/// Customize your Meshes by attaching your own instances of those component
/// types, to override the defaults as needed.
/// For best performance, reuse as many of the same component instances among
/// your Meshes as possible.
/// Use Group components to organize Meshes into hierarchies, if required.
///
/// See Also: http://xeogl.org/docs/classes/Mesh.html
/// </remarks>
public class XeoglMesh : 
    XeoglObject
{
    public override string JavaScriptClassName 
        => "Mesh";

    public XeoglGeometry Geometry { get; set; }
        = new XeoglBoxGeometry();

    public XeoglMaterial Material { get; set; }

    public XeoglMaterial OutlineMaterial { get; set; }

    public XeoglMaterial GhostMaterial { get; set; }

    public XeoglMaterial HighlightMaterial { get; set; }

    public XeoglMaterial SelectedMaterial { get; set; }

    public int Layer { get; set; }

    public bool IsStationary { get; set; }

    public XeoglBillboardBehaviour BillboardBehaviour { get; set; }
        = XeoglBillboardBehaviour.None;

    public bool OBbVisible { get; set; }

    public bool IsLoading { get; set; }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(JsCodeComponentUtils.SetValue(composer, "geometry", Geometry, null), "material", Material, null), "outlineMaterial", OutlineMaterial, null), "ghostMaterial", GhostMaterial, null), "highlightMaterial", HighlightMaterial, null), "selectedMaterial", SelectedMaterial, null)
            .SetValue("layer", Layer, 0)
            .SetValue("billboard", BillboardBehaviour, XeoglBillboardBehaviour.None)
            .SetValue("stationary", IsStationary, false)
            .SetValue("obbVisible", OBbVisible, false)
            .SetValue("loading", IsLoading, false);
    }
}