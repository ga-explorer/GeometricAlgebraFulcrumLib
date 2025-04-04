﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Transforms;

public sealed class XeoglERotateTransform : IXeoglNumericalTransform
{
    public static XeoglERotateTransform CreateRotateXtoY()
    {
        return new XeoglERotateTransform()
        {
            RotateZ = 90
        };
    }

    public static XeoglERotateTransform CreateRotateYtoX()
    {
        return new XeoglERotateTransform()
        {
            RotateZ = -90
        };
    }

    public static XeoglERotateTransform CreateRotateYtoZ()
    {
        return new XeoglERotateTransform()
        {
            RotateX = 90
        };
    }

    public static XeoglERotateTransform CreateRotateZtoY()
    {
        return new XeoglERotateTransform()
        {
            RotateX = -90
        };
    }

    public static XeoglERotateTransform CreateRotateZtoX()
    {
        return new XeoglERotateTransform()
        {
            RotateY = 90
        };
    }

    public static XeoglERotateTransform CreateRotateXtoZ()
    {
        return new XeoglERotateTransform()
        {
            RotateY = -90
        };
    }


    public double RotateX { get; set; }

    public double RotateY { get; set; }

    public double RotateZ { get; set; }


    public bool ContainsMatrix => false;

    public bool ContainsQuaternion => false;

    public bool ContainsRotate
        => RotateX > 0 || RotateY > 0 || RotateZ > 0 ||
           RotateX < 0 || RotateY < 0 || RotateZ < 0;

    public bool ContainsScale => false;

    public bool ContainsTranslate => false;


    public SquareMatrix4 GetMatrix()
        => SquareMatrix4.CreateIdentityMatrix();

    public LinFloat64Quaternion GetQuaternionTuple()
        => LinFloat64Quaternion.Create(1, 0, 0, 0);

    public LinFloat64Vector3D GetRotateTuple()
        => LinFloat64Vector3D.Create(RotateX, RotateY, RotateZ);

    public LinFloat64Vector3D GetScaleTuple()
        => LinFloat64Vector3D.Create(1, 1, 1);

    public LinFloat64Vector3D GetTranslateTuple()
        => LinFloat64Vector3D.Zero;


    public XeoglERotateTransform SetRotate(ILinFloat64Vector3D rotateTuple)
    {
        RotateX = rotateTuple.X;
        RotateY = rotateTuple.Y;
        RotateZ = rotateTuple.Z;

        return this;
    }

    public XeoglERotateTransform SetRotate(double rotateX, double rotateY, double rotateZ)
    {
        RotateX = rotateX;
        RotateY = rotateY;
        RotateZ = rotateZ;

        return this;
    }


    public string GetMatrixText()
        => GetMatrix().ToJavaScriptNumbersArrayText();

    public string GetQuaternionText()
        => GetQuaternionTuple().ToJavaScriptNumbersArrayText();

    public string GetRotateText()
        => GetRotateTuple().ToJavaScriptNumbersArrayText();

    public string GetScaleText()
        => GetScaleTuple().ToJavaScriptNumbersArrayText();

    public string GetTranslateText()
        => GetTranslateTuple().ToJavaScriptNumbersArrayText();


    public override string ToString()
    {
        var composer = new LinearTextComposer();

        if (ContainsRotate)
        {
            composer
                .Append("rotation: [")
                .Append(RotateX.ToString("G"))
                .Append(",")
                .Append(RotateY.ToString("G"))
                .Append(",")
                .Append(RotateZ.ToString("G"))
                .Append("]");
        }

        return composer.ToString();
    }
}