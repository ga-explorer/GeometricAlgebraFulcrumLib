using System;
using System.Linq;
using DataStructuresLib;
using GraphicsComposerLib.POVRay.SDL.Cameras;
using GraphicsComposerLib.POVRay.SDL.Directives;
using GraphicsComposerLib.POVRay.SDL.Finishes;
using GraphicsComposerLib.POVRay.SDL.Lights;
using GraphicsComposerLib.POVRay.SDL.Objects;
using GraphicsComposerLib.POVRay.SDL.Objects.FSP;
using GraphicsComposerLib.POVRay.SDL.Objects.ISP;
using GraphicsComposerLib.POVRay.SDL.Pigments;
using GraphicsComposerLib.POVRay.SDL.Textures;
using GraphicsComposerLib.POVRay.SDL.Values;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.POVRay.SDL
{
    internal sealed class SdlCodeGenContext : ISdlDynamicVisitor
    {
        internal LinearTextComposer TextComposer { get; }

        public bool IgnoreNullElements => false;

        public bool UseExceptions => false;

        public void Fallback(ISdlElement objItem, Microsoft.CSharp.RuntimeBinder.RuntimeBinderException excException)
        {
            TextComposer.Append("ISdlElement object not handled of type " + objItem.GetType().FullName);
        }


        internal SdlCodeGenContext()
        {
            TextComposer = new LinearTextComposer();
        }


        internal SdlCodeGenContext Reset()
        {
            TextComposer.Clear();

            return this;
        }


        #region General

        private void GenerateBooleanProperty(string name, bool value)
        {
            if (value == false) return;

            TextComposer.AppendLine(name);
        }

        private void GenerateValueProperty(string name, ISdlElement value)
        {
            if (ReferenceEquals(value, null)) return;

            TextComposer.Append(name);
            value.AcceptVisitor(this);
            TextComposer.AppendLine();
        }

        #endregion

        #region Values

        public void Visit(SdlStoredValue value)
        {
            TextComposer.Append(value.Value);
        }

        public void Visit(SdlColorRgbft value)
        {
            TextComposer.Append(value.TaggedValue);
        }

        public void Visit(SdlVector2D value)
        {
            TextComposer.Append(value.TaggedValue);
        }

        public void Visit(SdlVector3D value)
        {
            TextComposer.Append(value.TaggedValue);
        }

        public void Visit(SdlVectorLiteral3D value)
        {
            TextComposer.Append(value.TaggedValue);
        }

        public void Visit(SdlMatrix4X3 value)
        {
            TextComposer.Append(value.TaggedValue);
        }

        public void Visit(SdlScalarLiteral value)
        {
            TextComposer.Append(value.ToString());
        }

        public void Visit(SdlBooleanLiteral value)
        {
            TextComposer.Append(value.ToString());
        }

        #endregion

        #region Scene Objects

        private void GenerateObjectSpecs(SdlObject sceneObject)
        {
            foreach (var objModifier in sceneObject.Modifiers)
                objModifier.AcceptVisitor(this);
        }

        private void GeneratePolynomialObjectSpecs(SdlPolynomialObject sceneObject)
        {
            GenerateBooleanProperty("strum", sceneObject.SturmianRootSolver);

            GenerateObjectSpecs(sceneObject);
        }

        public void Visit(SdlNamedObject sceneObject)
        {
            TextComposer.AppendLine("object {").IncreaseIndentation();

            TextComposer.AppendLine(sceneObject.Name);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlCsgObject sceneObject)
        {
            if (sceneObject.CsgOperation == SdlCsgOperation.SplitUnion)
            {
                TextComposer.AppendLine("union {").IncreaseIndentation();

                foreach (var childObj in sceneObject.ChildObjects)
                    childObj.AcceptVisitor(this);

                TextComposer.AppendAtNewLine("split_union on");

                GenerateObjectSpecs(sceneObject);

                TextComposer.DecreaseIndentation().AppendAtNewLine("}");

                return;
            }

            TextComposer.Append(sceneObject.CsgOperationName).AppendLine(" {").IncreaseIndentation();

            foreach (var childObj in sceneObject.ChildObjects)
                childObj.AcceptVisitor(this);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlPolySurface sceneObject)
        {
            TextComposer.AppendLine("poly {").IncreaseIndentation();

            TextComposer.AppendLineAtNewLine(
                sceneObject.Coefs.Select(c => c.ScalarOrDefault()).Concatenate(", ", "<", ">")
                );

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlCubicPolySurface sceneObject)
        {
            TextComposer.AppendLine("cubic {").IncreaseIndentation();

            TextComposer.AppendLineAtNewLine(
                sceneObject.Coefs.Select(c => c.ScalarOrDefault()).Concatenate(", ", "<", ">")
                );

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlQuarticPolySurface sceneObject)
        {
            TextComposer.AppendLine("quartic {").IncreaseIndentation();

            TextComposer.AppendLineAtNewLine(
                sceneObject.Coefs.Select(c => c.ScalarOrDefault()).Concatenate(", ", "<", ">")
                );

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlQuadric sceneObject)
        {
            TextComposer.AppendLine("quadric {").IncreaseIndentation();

            TextComposer.AppendLineAtNewLine(sceneObject.CoefsString);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlPlane sceneObject)
        {
            TextComposer.AppendLine("plane {").IncreaseIndentation();

            sceneObject.Normal.AcceptVisitor(this);
            TextComposer.Append(", ");
            sceneObject.Distance.AcceptVisitor(this);
            TextComposer.AppendLine();

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlBox sceneObject)
        {
            TextComposer.AppendLine("box {").IncreaseIndentation();

            sceneObject.Corner1.AcceptVisitor(this);
            TextComposer.Append(", ");
            sceneObject.Corner1.AcceptVisitor(this);
            TextComposer.AppendLine();

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlSphere sceneObject)
        {
            TextComposer.AppendLine("sphere {").IncreaseIndentation();

            sceneObject.Center.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.Radius.AcceptVisitor(this);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlOvus sceneObject)
        {
            TextComposer.AppendLine("ovus {").IncreaseIndentation();

            sceneObject.BottomRadius.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.TopRadius.AcceptVisitor(this);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlTorus sceneObject)
        {
            TextComposer.AppendLine("torus {").IncreaseIndentation();

            sceneObject.MajorRadius.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.MinorRadius.AcceptVisitor(this);

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlSuperquadricEllipsoid sceneObject)
        {
            TextComposer.AppendLine("superellipsoid {").IncreaseIndentation();

            TextComposer.Append("<");
            sceneObject.EastWestExponent.AcceptVisitor(this);
            TextComposer.Append(", ");
            sceneObject.NorthSouthExponent.AcceptVisitor(this);
            TextComposer.Append(">");

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlCone sceneObject)
        {
            TextComposer.AppendLine("cone {").IncreaseIndentation();

            sceneObject.BasePoint.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.BaseRadius.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.CapPoint.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.CapRadius.AcceptVisitor(this);

            GenerateBooleanProperty("open", sceneObject.Open);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlCylinder sceneObject)
        {
            TextComposer.AppendLine("cylinder {").IncreaseIndentation();

            sceneObject.BasePoint.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.CapPoint.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.Radius.AcceptVisitor(this);

            GenerateBooleanProperty("open", sceneObject.Open);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlBlobSphere sceneObject)
        {
            TextComposer.AppendLine("sphere {").IncreaseIndentation();

            sceneObject.Center.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.Radius.AcceptVisitor(this);
            TextComposer.Append(", ");

            GenerateValueProperty("strength", sceneObject.Strength);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlBlobCylinder sceneObject)
        {
            TextComposer.AppendLine("cylinder {").IncreaseIndentation();

            sceneObject.BasePoint.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.CapPoint.AcceptVisitor(this);
            TextComposer.Append(", ");

            sceneObject.Radius.AcceptVisitor(this);
            TextComposer.Append(", ");

            GenerateValueProperty("strength", sceneObject.Strength);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlBlob sceneObject)
        {
            TextComposer.AppendLine("blob {").IncreaseIndentation();

            foreach (var component in sceneObject.Components)
                component.AcceptVisitor(this);

            GenerateValueProperty("threshold", sceneObject.Threshold);

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlLathe sceneObject)
        {
            TextComposer.AppendLine("lathe {").IncreaseIndentation();

            TextComposer
                .Append(sceneObject.SplineTypeName)
                .Append(" ")
                .Append(sceneObject.Points.Count)
                .AppendLine(",");

            for (var i = 0; i < sceneObject.Points.Count; i++)
            {
                if (i > 0) TextComposer.AppendLine(", ");

                sceneObject.Points[i].AcceptVisitor(this);
            }

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlPrism sceneObject)
        {
            TextComposer.AppendLine("prism {").IncreaseIndentation();

            TextComposer
                .AppendLine(sceneObject.InterpolationKindName)
                .AppendLine(sceneObject.SweepKindName);

            sceneObject.Height1.AcceptVisitor(this);
            TextComposer.AppendLine();
            sceneObject.Height2.AcceptVisitor(this);
            TextComposer.AppendLine();

            GenerateBooleanProperty("open", sceneObject.Open);

            TextComposer
                .Append(sceneObject.Points.Count)
                .AppendLine(",");

            for (var i = 0; i < sceneObject.Points.Count; i++)
            {
                if (i > 0) TextComposer.AppendLine(", ");

                sceneObject.Points[i].AcceptVisitor(this);
            }

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlSphereSweep sceneObject)
        {
            TextComposer.AppendLine("sphere_sweep {").IncreaseIndentation();

            TextComposer
                .Append(sceneObject.InterpolationKindName)
                .Append(" ")
                .Append(sceneObject.Spheres.Count)
                .AppendLine(",");

            for (var i = 0; i < sceneObject.Spheres.Count; i++)
            {
                if (i > 0) TextComposer.AppendLine(", ");

                sceneObject.Spheres[i].Center.AcceptVisitor(this);
                TextComposer.Append(", ");
                sceneObject.Spheres[i].Radius.AcceptVisitor(this);
            }

            GenerateValueProperty("tolerance", sceneObject.Tolerance);

            GenerateObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlSurfaceOfRevolution sceneObject)
        {
            TextComposer.AppendLine("sor {").IncreaseIndentation();

            TextComposer
                .Append(sceneObject.Points.Count)
                .AppendLine(",");

            for (var i = 0; i < sceneObject.Points.Count; i++)
            {
                if (i > 0) TextComposer.AppendLine(", ");

                sceneObject.Points[i].AcceptVisitor(this);
            }

            GenerateBooleanProperty("open", sceneObject.Open);

            GeneratePolynomialObjectSpecs(sceneObject);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        #endregion

        #region Textures

        public void Visit(SdlPlainTexture texture)
        {
            TextComposer.AppendLine("texture {").IncreaseIndentation();

            if (String.IsNullOrEmpty(texture.TextureIdentifier) == false)
                TextComposer.AppendLine(texture.TextureIdentifier);

            if (String.IsNullOrEmpty(texture.PigmentIdentifier) == false)
                TextComposer.AppendLine(texture.PigmentIdentifier);

            if (String.IsNullOrEmpty(texture.FinishIdentifier) == false)
                TextComposer.AppendLine(texture.FinishIdentifier);

            texture.Pigment.AcceptVisitor(this);

            texture.Finish.AcceptVisitor(this);

            foreach (var transform in texture.Transforms)
                transform.AcceptVisitor(this);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        #endregion

        #region Pigments

        private void GeneratePigmentSpecs(SdlPigment pigment)
        {
            GenerateValueProperty("quick_color", pigment.QuickColor);
        }

        public void Visit(SdlSolidColorPigment pigment)
        {
            TextComposer.AppendLine("pigment {").IncreaseIndentation();

            if (String.IsNullOrEmpty(pigment.PigmentIdentifier) == false)
                TextComposer.AppendLine(pigment.PigmentIdentifier);

            GenerateValueProperty("color", pigment.Color);

            GeneratePigmentSpecs(pigment);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        public void Visit(SdlColorListPigment pigment)
        {
            TextComposer.AppendLine("pigment {").IncreaseIndentation();

            if (String.IsNullOrEmpty(pigment.PigmentIdentifier) == false)
                TextComposer.AppendLine(pigment.PigmentIdentifier);

            TextComposer.Append(pigment.PatternName).Append(" ");

            pigment.Color1.AcceptVisitor(this);
            TextComposer.Append(", ");
            pigment.Color2.AcceptVisitor(this);
            TextComposer.Append(", ");
            pigment.Color3.AcceptVisitor(this);

            GeneratePigmentSpecs(pigment);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        #endregion

        #region Finishes

        public void Visit(SdlAmbientFinishItem finishItem)
        {
            
        }

        public void Visit(SdlBrillianceFinishItem finishItem)
        {
            
        }

        public void Visit(SdlCrandFinishItem finishItem)
        {

        }

        public void Visit(SdlDiffuseFinishItem finishItem)
        {

        }

        public void Visit(SdlEmissionFinishItem finishItem)
        {

        }

        public void Visit(SdlPhongFinishItem finishItem)
        {

        }

        public void Visit(SdlReflectionFinishItem finishItem)
        {

        }

        public void Visit(SdlSpecularFinishItem finishItem)
        {

        }

        public void Visit(SdlFinish finish)
        {
            TextComposer.AppendLine("finish {").IncreaseIndentation();

            if (String.IsNullOrEmpty(finish.FinishIdentifier) == false)
                TextComposer.AppendLine(finish.FinishIdentifier);

            foreach (var finishItem in finish.FinishItems)
                finishItem.AcceptVisitor(this);

            TextComposer.DecreaseIndentation().AppendAtNewLine("}");
        }

        #endregion

        #region Light Sources

        public void Visit(SdlLight light)
        {
            TextComposer.AppendLine("light_source {").IncreaseIndentation();

            light.Location.AcceptVisitor(this);
            TextComposer.Append(", ");
            light.Color.AcceptVisitor(this);
            TextComposer.AppendLine();

            GenerateBooleanProperty("spotlight ", light.IsConicSpotLight);
            GenerateBooleanProperty("cylinder ", light.IsCylindricalSpotLight);
            GenerateBooleanProperty("shadowless ", light.Shadowless);
            GenerateBooleanProperty("parallel ", light.Parallel);
            
            GenerateValueProperty("point_at ", light.ParallelPintAt);

            if (light.IsSpotLight)
            {
                GenerateValueProperty("radius ", light.SpotLightSpecs.Radius);
                GenerateValueProperty("falloff ", light.SpotLightSpecs.FallOff);
                GenerateValueProperty("tightness ", light.SpotLightSpecs.Tightness);
                GenerateValueProperty("point_at ", light.SpotLightSpecs.PointAt);
            }

            if (light.IsAreaLight)
            {
                TextComposer.Append("area_light ");
                light.AreaLightSpecs.Axis1.AcceptVisitor(this);
                TextComposer.Append(", ");
                light.AreaLightSpecs.Axis2.AcceptVisitor(this);
                TextComposer.Append(", ");
                TextComposer.Append(light.AreaLightSpecs.Size1);
                TextComposer.Append(", "); 
                TextComposer.AppendLine(light.AreaLightSpecs.Size2);

                GenerateValueProperty("adaptive ", light.AreaLightSpecs.Adaptive);
                GenerateValueProperty("area_illumination ", light.AreaLightSpecs.AreaIllumination);
                GenerateBooleanProperty("jitter ", light.AreaLightSpecs.Jitter);
                GenerateBooleanProperty("circular ", light.AreaLightSpecs.Circular);
                GenerateBooleanProperty("orient ", light.AreaLightSpecs.Orient);
            }

            GenerateValueProperty("looks_like ", light.LooksLike);
            GenerateValueProperty("fade_distance ", light.FadeDistance);
            GenerateValueProperty("fade_power ", light.FadePower);
            GenerateValueProperty("media_attenuation ", light.MediaAttenuation);
            GenerateValueProperty("media_interaction ", light.MediaInteraction);
            GenerateValueProperty("projected_through ", light.ProjectedThrough);

            foreach (var transform in light.Transforms)
                transform.AcceptVisitor(this);

            TextComposer.DecreaseIndentation().Append("}");
        }

        #endregion

        #region Cameras

        private void GenerateCameraSpecs(SdlCamera camera)
        {
            foreach (var transform in camera.Transforms)
                transform.AcceptVisitor(this);
        }

        private void GenerateFullCameraSpecs(SdlFullCamera camera)
        {
            GenerateValueProperty("location ", camera.Location);
            GenerateValueProperty("look_at ", camera.LookAt);
            GenerateValueProperty("angle ", camera.Angle);
            GenerateValueProperty("right ", camera.Right);
            GenerateValueProperty("up ", camera.Up);
            GenerateValueProperty("sky ", camera.Sky);
            GenerateValueProperty("direction ", camera.Direction);

            GenerateCameraSpecs(camera);
        }

        public void Visit(SdlNamedCamera camera)
        {
            TextComposer.AppendLine("camera {").IncreaseIndentation();

            TextComposer.AppendLine(camera.Name);

            GenerateCameraSpecs(camera);

            TextComposer.DecreaseIndentation().Append("}");
        }

        public void Visit(SdlPerspectiveCamera camera)
        {
            TextComposer.AppendLine("camera {").IncreaseIndentation();

            TextComposer.AppendLine("perspective ");

            GenerateFullCameraSpecs(camera);

            TextComposer.DecreaseIndentation().Append("}");
        }

        public void Visit(SdlOrthographicCamera camera)
        {
            TextComposer.AppendLine("camera {").IncreaseIndentation();

            TextComposer.AppendLine("orthographic ");

            GenerateFullCameraSpecs(camera);

            TextComposer.DecreaseIndentation().Append("}");
        }

        #endregion

        #region Directives

        public void Visit(SdlDeclareDirective directive)
        {
            TextComposer
                .AppendAtNewLine(directive.Local ? "#local " : "#declare ")
                .Append(directive.Name)
                .Append(" = ");
                
            directive.Value.AcceptVisitor(this);
        }

        public void Visit(SdlIncludeDirective directive)
        {
            TextComposer.AppendAtNewLine("#include \"" + directive.FileName + "\".inc");
        }

        #endregion

        #region Scene

        public void Visit(SdlScene scene)
        {
            foreach (var stmnt in scene.Statements)
                stmnt.AcceptVisitor(this);
        }

        #endregion

        public override string ToString()
        {
            return TextComposer.ToString();
        }
    }
}
