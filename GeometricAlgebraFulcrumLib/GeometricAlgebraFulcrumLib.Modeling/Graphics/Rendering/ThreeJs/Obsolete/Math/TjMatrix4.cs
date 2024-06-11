using System.Numerics;
using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Math;

public sealed class TjMatrix3 :
    TjComponentSimple
{
    public override string JavaScriptClassName 
        => "Matrix3";

    public double[,] MatrixValue { get; }
        = new double[3, 3];


    public TjMatrix3()
    {

    }

    public TjMatrix3(IAffineMap2D affineMap)
    {
        MatrixValue = affineMap.GetArray2D();
    }


    protected override string GetConstructorArgumentsText()
    {
        return string.Empty;
    }

    protected override string GetSetMethodArgumentsText()
    {
        var composer = new StringBuilder();

        composer
            .AppendLine($"{MatrixValue[0, 0]:G}, {MatrixValue[0, 1]:G}, {MatrixValue[0, 2]:G},")
            .AppendLine($"{MatrixValue[1, 0]:G}, {MatrixValue[1, 1]:G}, {MatrixValue[1, 2]:G},")
            .Append($"{MatrixValue[2, 0]:G}, {MatrixValue[2, 1]:G}, {MatrixValue[2, 2]:G}");

        return composer.ToString();
    }
}
/// <summary>
/// A class representing a 4x4 matrix.
/// The most common use of a 4x4 matrix in 3D computer graphics is as a Transformation Matrix. For an introduction to transformation matrices as used in WebGL, check out this tutorial.
/// This allows a Vector3 representing a point in 3D space to undergo transformations such as translation, rotation, shear, scale, reflection, orthogonal or perspective projection and so on, by being multiplied by the matrix. This is known as applying the matrix to the vector.
/// 
/// Every Object3D has three associated Matrix4s:
/// - Object3D.matrix: This stores the local transform of the object. This is the object's transformation relative to its parent.
/// - Object3D.matrixWorld: The global or world transform of the object. If the object has no parent, then this is identical to the local transform stored in matrix.
/// - Object3D.modelViewMatrix: This represents the object's transformation relative to the camera's coordinate system. An object's modelViewMatrix is the object's matrixWorld pre-multiplied by the camera's matrixWorldInverse.
/// 
/// Cameras have three additional Matrix4s:
/// - Camera.matrixWorldInverse: The view matrix - the inverse of the Camera's matrixWorld.
/// - Camera.projectionMatrix: Represents the information how to project the scene to clip space.
/// - Camera.projectionMatrixInverse: The inverse of projectionMatrix.
/// 
/// Note: Object3D.normalMatrix is not a Matrix4, but a Matrix3.
/// https://threejs.org/docs/#api/en/math/Matrix4
/// </summary>
public sealed class TjMatrix4 :
    TjComponentSimple
{
    public override string JavaScriptClassName 
        => "Matrix4";

    public Matrix4x4 MatrixValue { get; set; }


    public TjMatrix4(Matrix4x4 matrixValue)
    {
        MatrixValue = matrixValue;
    }

    public TjMatrix4(IAffineMap3D affineMap)
    {
        MatrixValue = affineMap.GetMatrix4x4();
    }


    protected override string GetConstructorArgumentsText()
    {
        return string.Empty;
    }

    protected override string GetSetMethodArgumentsText()
    {
        var composer = new StringBuilder();

        composer
            .AppendLine($"{MatrixValue.M11:G}, {MatrixValue.M12:G}, {MatrixValue.M13:G}, {MatrixValue.M14:G},")
            .AppendLine($"{MatrixValue.M21:G}, {MatrixValue.M22:G}, {MatrixValue.M23:G}, {MatrixValue.M24:G},")
            .AppendLine($"{MatrixValue.M31:G}, {MatrixValue.M32:G}, {MatrixValue.M33:G}, {MatrixValue.M34:G},")
            .Append($"{MatrixValue.M41:G}, {MatrixValue.M42:G}, {MatrixValue.M43:G}, {MatrixValue.M44:G}");

        return composer.ToString();
    }
}