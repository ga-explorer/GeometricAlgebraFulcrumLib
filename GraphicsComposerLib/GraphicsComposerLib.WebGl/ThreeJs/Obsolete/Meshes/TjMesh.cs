using GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Geometry;
using GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Materials;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Meshes
{
    /// <summary>
    /// Class representing triangular polygon mesh based objects. Also serves
    /// as a base for other classes such as SkinnedMesh.
    /// https://threejs.org/docs/#api/en/objects/Mesh
    /// </summary>
    public class TjMesh :
        TjComponentWithAttributes
    {
        public override string JavaScriptClassName 
            => "Mesh";

        public TjGeometry Geometry { get; set; }

        public TjMaterialBase Material { get; set; }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            //var geometry = Geometry

            composer
                .SetTextValue("geometry", Geometry.ToString(), string.Empty)
                .SetTextValue("material", Material.ToString(), string.Empty);
        }
    }
}
