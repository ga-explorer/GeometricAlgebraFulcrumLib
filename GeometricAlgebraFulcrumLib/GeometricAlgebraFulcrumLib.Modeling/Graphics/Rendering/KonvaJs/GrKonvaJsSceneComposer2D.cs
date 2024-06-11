//using GraphicsComposerLib.Rendering.KonvaJs.Containers;
//using GraphicsComposerLib.Rendering.Visuals.Space2D;
//using GraphicsComposerLib.Rendering.Visuals.Space2D.Basic;
//using GraphicsComposerLib.Rendering.Visuals.Space2D.Curves;
//using GraphicsComposerLib.Rendering.Visuals.Space2D.Grids;
//using GraphicsComposerLib.Rendering.Visuals.Space2D.Images;
//using Humanizer;
//using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

//namespace GraphicsComposerLib.Rendering.KonvaJs
//{
//    public class GrKonvaJsSceneComposer2D :
//        GrVisualElementsSceneComposer2D<GrKonvaJsStage>
//    {
//        public override GrKonvaJsStage SceneObject { get; }


//        public GrKonvaJsSceneComposer2D()
//        {
//            SceneObject = 
//                new GrKonvaJsStage(
//                    "stage", 
//                    "'renderDiv'"
//                ).SetOptions(new GrKonvaJsStage.StageOptions())
//                .SetProperties(new GrKonvaJsStage.StageProperties());

//            //SnapshotSpecs = new GrKonvaJsSnapshotSpecs();

//            AddInitialObjects();
//        }
        
//        public GrKonvaJsSceneComposer2D(string constName)
//        {
//            SceneObject = 
//                new GrKonvaJsStage(
//                    constName, 
//                    "renderDiv"
//                ).SetOptions(new GrKonvaJsStage.StageOptions())
//                .SetProperties(new GrKonvaJsStage.StageProperties());

//            //SnapshotSpecs = new GrKonvaJsSnapshotSpecs();

//            AddInitialObjects();
//        }


//        private void AddInitialObjects()
//        {

//        }

//        public override GrVisualLaTeXText2D AddLaTeXText(GrVisualLaTeXText2D visualElement)
//        {
//            throw new NotImplementedException();
//        }

//        public override GrVisualSquareGrid2D AddSquareGrid(GrVisualSquareGrid2D visualElement)
//        {
//            throw new NotImplementedException();
//        }

//        public override IGrVisualImage2D AddImage(IGrVisualImage2D visualElement)
//        {
//            throw new NotImplementedException();
//        }

//        public override GrVisualPoint2D AddPoint(GrVisualPoint2D visualElement)
//        {
//            throw new NotImplementedException();
//        }
        
//        public override GrVisualCurve2D AddCurve(GrVisualCurve2D visualElement)
//        {
//            throw new NotImplementedException();
//        }
        
//        public string GetCreateSceneCode()
//        {
//            var sceneName = SceneObject.ConstName;
//            var sceneCode = SceneObject.GetCode();

//            var codeComposer = new LinearTextComposer();

//            codeComposer
//                .AppendLine($"function create{sceneName.Pascalize()}() {{")
//                .IncreaseIndentation();

//            codeComposer.AppendLineAtNewLine(@$"
//{sceneCode}

//const light = new BABYLON.HemisphericLight(""light"", new BABYLON.Vector3(0, 1, 0), {sceneName});
////light.intensity = 0.7;

//");

//            //codeComposer.AppendLineAtNewLine(
//            //    SceneObject.KeyFramesCache.GetCode()
//            //);

//            foreach (var babylonObject in SceneObject.Layers)
//                codeComposer.AppendLineAtNewLine(babylonObject.GetCode());

//            //if (SceneObject.BeforeSceneRenderCode.Count > 0)
//            //{
//            //    var beforeSceneRenderCode = 
//            //        SceneObject.BeforeSceneRenderCode.Concatenate(Environment.NewLine);

//            //    codeComposer
//            //        .AppendLineAtNewLine($"{sceneName}.registerBeforeRender(function() {{")
//            //        .IncreaseIndentation()
//            //        .AppendLineAtNewLine(beforeSceneRenderCode)
//            //        .DecreaseIndentation()
//            //        .AppendLineAtNewLine("});");
//            //}

//            //if (SnapshotSpecs.Enabled)
//            //    codeComposer.AppendLineAtNewLine(
//            //        SnapshotSpecs.GetSnapshotCode(sceneName)
//            //    );

//            //else if (ShowDebugLayer)
//            //    codeComposer.AppendLineAtNewLine(
//            //        $"{sceneName}.debugLayer.show();"
//            //    );

//            codeComposer
//                .AppendLine()
//                .AppendLine($"return {sceneName};")
//                .DecreaseIndentation()
//                .AppendLine("}");

//            return codeComposer.ToString();
//        }

//        public string GetAddSceneCode()
//        {
//            var sceneName = SceneObject.ConstName;

//            return $"window.scenes.push( create{sceneName.Pascalize()}() );";
//        }
//    }
//}
