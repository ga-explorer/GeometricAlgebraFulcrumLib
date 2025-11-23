//using System;
//using Aspose.ThreeD;
//using Aspose.ThreeD.Shading;
//using Aspose.ThreeD.Utilities;
//using GraphicsComposerLib.Geometry.LatticeShapes;
//using GraphicsComposerLib.Geometry.LatticeShapes.Surfaces;
//using GraphicsComposerLib.IO;

//namespace GraphicsComposerLib.Samples.IO
//{
//    public static class Sample2
//    {
//        private const int SamplesCount1 = 36;
//        private const int SamplesCount2 = 19;

//        private static Node CreateOriginNode(Scene scene)
//        {
//            var material = new PbrMaterial
//            {
//                MetallicFactor = 0.9, // an almost metal material
//                RoughnessFactor = 0.9, // material surface is very rough
//                EmissiveColor = new Vector3(0.2, 0.2, 0.2)
//            };
            
//            var dataSet = new GrLatticeSurfaceList3D();

//            dataSet
//                .AddZSphereSurface(SamplesCount1, SamplesCount2)
//                .ScalePointsBy(0.15d);

//            dataSet.FinalizeSet();
            
//            Console.WriteLine(dataSet.ToString());

//            var mesh = dataSet.GenerateAspose3DTrianglesMesh();

//            var node = scene.RootNode.CreateChildNode("OriginSphere", mesh, material);

//            //var line = dataSet.GenerateAspose3DLinesMesh();

//            //var node2 = scene.RootNode.CreateChildNode("OriginSphereNormals", line);

//            return node;
//        }

//        private static GrLatticeSurfaceList3D CreateZAxisVertexDataSet()
//        {
//            var dataSet = new GrLatticeSurfaceList3D();

//            dataSet
//                .AddZSphereSurface(SamplesCount1, SamplesCount2)
//                .ScalePointsBy(0.1d);

//            //dataSet
//            //    .AddZSphereBatch(SamplesCount1, SamplesCount2)
//            //    .ScalePointsBy(0.1d)
//            //    .TranslatePointsBy(0, 0, 1);

//            dataSet
//                .AddZCylinderSurface(SamplesCount1, 2)
//                .TranslatePointsBy(0, 0, 0.5)
//                .ScalePointsBy(0.1, 0.1, 0.7);

//            dataSet
//                .AddClosedZConeSurface(SamplesCount1, 2)
//                .TranslatePointsBy(0, 0, 0.5)
//                .ScalePointsBy(0.2, 0.2, 0.3)
//                .TranslatePointsBy(0, 0, 0.7);
            
//            return dataSet;
//        }

//        private static Node CreateXAxisNode(Scene scene)
//        {
//            var material = new PbrMaterial
//            {
//                MetallicFactor = 0.9, // an almost metal material
//                RoughnessFactor = 0.9, // material surface is very rough
//                EmissiveColor = new Vector3(1, 0, 0)
//            };

//            var dataSet = CreateZAxisVertexDataSet();

//            dataSet.YRotatePointsByDegrees(90);

//            dataSet.FinalizeSet();

//            var mesh = dataSet.GenerateAspose3DTrianglesMesh();

//            var node = scene.RootNode.CreateChildNode("XAxis", mesh, material);

//            return node;
//        }
        
//        private static Node CreateYAxisNode(Scene scene)
//        {
//            var material = new PbrMaterial
//            {
//                MetallicFactor = 0.9, // an almost metal material
//                RoughnessFactor = 0.9, // material surface is very rough
//                EmissiveColor = new Vector3(0, 1, 0)
//            };

//            var dataSet = CreateZAxisVertexDataSet();

//            dataSet.XRotatePointsByDegrees(-90);

//            dataSet.FinalizeSet();

//            var mesh = dataSet.GenerateAspose3DTrianglesMesh();

//            var node = scene.RootNode.CreateChildNode("YAxis", mesh, material);

//            return node;
//        }
        
//        private static Node CreateZAxisNode(Scene scene)
//        {
//            var material = new PbrMaterial
//            {
//                MetallicFactor = 0.9, // an almost metal material
//                RoughnessFactor = 0.9, // material surface is very rough
//                EmissiveColor = new Vector3(0, 0, 1)
//            };

//            var dataSet = CreateZAxisVertexDataSet();

//            dataSet.FinalizeSet();

//            var mesh = dataSet.GenerateAspose3DTrianglesMesh();

//            var node = scene.RootNode.CreateChildNode("ZAxis", mesh, material);

//            return node;
//        }
        
//        public static void Execute()
//        {
//            var scene = new Scene();

//            CreateOriginNode(scene);
//            CreateXAxisNode(scene);
//            CreateYAxisNode(scene);
//            CreateZAxisNode(scene);
            
//            // Save drawing
//            var output = @"D:\Downloads\New folder\Frame.gltf";
//            scene.Save(output, FileFormat.GLTF2);
//        }


//    }
//}