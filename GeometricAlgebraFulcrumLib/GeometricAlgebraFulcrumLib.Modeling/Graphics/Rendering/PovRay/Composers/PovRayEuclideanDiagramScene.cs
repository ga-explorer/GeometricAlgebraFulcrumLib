//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
//using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.CSG;
//using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;
//using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

//namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Custom;

//public class PovRayEuclideanDiagramScene : 
//    GrPovRayScene
//{
//    public GrPovRayOrthographicCamera DefaultCamera()
//    {
//        var camera = new GrPovRayOrthographicCamera().SetProperties(
//            new GrPovRayFullCamera.FullCameraProperties
//            {
//                Location = GrPovRayVector3Value.Create(300, 100, 200),
//                LookAt = GrPovRayVector3Value.Zero
//            }
//        );

//        return camera;
//    }

//    public GrPovRayCsgObject Vector(double ox, double oy, double oz, double dx, double dy, double dz, double thickness)
//    {
//        var vectorLength = dx*dx + dy*dy + dz*dz;
//        var arrowLength = thickness * 24;
//        var axisLength = vectorLength - arrowLength;
//        var axisToVectorRatio = axisLength/vectorLength;
//        var headToVectorRatio = arrowLength / vectorLength;

//        var axisStartPoint = LinFloat64Vector3D.Create(ox, oy, oz);
//        var axisEndPoint = axisStartPoint + axisToVectorRatio * LinFloat64Vector3D.Create(dx, dy, dz);
//        var arrowEndPoint = axisStartPoint + LinFloat64Vector3D.Create(dx, dy, dz);

//        var axis = new GrPovRayCylinder(
//            axisStartPoint, 
//            axisEndPoint, 
//            thickness, 
//            false
//        );

//        var axisStartSphere = new GrPovRaySphere(
//                axisStartPoint,
//                thickness
//            );

//        var axisEndSphere = new GrPovRaySphere(
//                axisEndPoint,
//                thickness
//            );

//        var arrow =
//            new GrPovRayCone(
//                axisEndPoint,
//                arrowEndPoint,
//                arrowLength,
//                GrPovRayFloat32Value.Zero,
//                false
//            );

//        var vector = new GrPovRayCsgObject()
//        {
//            CsgOperation = GrPovRayCsgOperation.Union
//        };

//        vector.ChildObjects.AddThese(axis, axisStartSphere, axisEndSphere, arrow);

//        return vector;
//    }
//}