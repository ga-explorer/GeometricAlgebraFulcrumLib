﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Transforms;

public sealed class XeoglQRotateScaleTranslateTransform : IXeoglNumericalTransform
{
    public static XeoglQRotateScaleTranslateTransform CreateTranslate(double tx, double ty, double tz)
    {
        return new XeoglQRotateScaleTranslateTransform()
        {
            TranslateX = tx,
            TranslateY = ty,
            TranslateZ = tz
        };
    }
        
    public static XeoglQRotateScaleTranslateTransform CreateTranslate(ILinFloat64Vector3D t)
    {
        return new XeoglQRotateScaleTranslateTransform()
        {
            TranslateX = t.X,
            TranslateY = t.Y,
            TranslateZ = t.Z
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotate(double angle, ILinFloat64Vector3D rotateVector)
    {
        var d = 1 / rotateVector.VectorENorm();
        var cosAngle = d * Math.Cos(angle / 2);
        var sinAngle = d * Math.Sin(angle / 2);

        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = sinAngle * rotateVector.X,
            QuaternionY = sinAngle * rotateVector.Y,
            QuaternionZ = sinAngle * rotateVector.Z,
            QuaternionW = cosAngle
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotate(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        var lengthSquared1 = vector1.VectorENormSquared();
        var lengthSquared2 = vector2.VectorENormSquared();

        var n1 = Math.Sqrt(lengthSquared1 * lengthSquared2);
        
        //TODO: Validate this
        var w = n1 + vector1.VectorESp(vector2);

        var angle = w.CosToDoublePolarAngle();

        double vx, vy, vz; 
        if (w < 1e-12 * n1)
        {
            var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                ? LinFloat64Vector3D.Create(-vector1.Y, vector1.X, 0)
                : LinFloat64Vector3D.Create(0, -vector1.Z, vector1.Y);

            var d = 1 / v1.VectorENorm();

            vx = d * v1.X;
            vy = d * v1.Y;
            vz = d * v1.Z;
        }
        else
        {
            var v2 = vector1.VectorCross(vector2);

            var d = 1 / v2.VectorENorm();

            vx = d * v2.X;
            vy = d * v2.Y;
            vz = d * v2.Z;
        }

        var sinAngle = angle.HalfPolarAngle().SinValue;

        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = sinAngle * vx,
            QuaternionY = sinAngle * vy,
            QuaternionZ = sinAngle * vz,
            QuaternionW = w
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateXtoY()
    {
        //Rotate about z-axis by 90 degrees
        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = 0,
            QuaternionY = 0,
            QuaternionZ = MathNet.Numerics.Constants.Sqrt1Over2,
            QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateYtoX()
    {
        //Rotate about z-axis by -90 degrees
        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = 0,
            QuaternionY = 0,
            QuaternionZ = -MathNet.Numerics.Constants.Sqrt1Over2,
            QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateYtoZ()
    {
        //Rotate about x-axis by 90 degrees
        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = MathNet.Numerics.Constants.Sqrt1Over2,
            QuaternionY = 0,
            QuaternionZ = 0,
            QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateZtoY()
    {
        //Rotate about x-axis by -90 degrees
        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = -MathNet.Numerics.Constants.Sqrt1Over2,
            QuaternionY = 0,
            QuaternionZ = 0,
            QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateZtoX()
    {
        //Rotate about y-axis by 90 degrees
        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = 0,
            QuaternionY = MathNet.Numerics.Constants.Sqrt1Over2,
            QuaternionZ = 0,
            QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateXtoZ()
    {
        //Rotate about y-axis by -90 degrees
        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = 0,
            QuaternionY = -MathNet.Numerics.Constants.Sqrt1Over2,
            QuaternionZ = 0,
            QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateScale(double angle, ILinFloat64Vector3D rotateVector)
    {
        var scaleFactor = rotateVector.VectorENorm();
        var d = 1 / scaleFactor;
        var cosAngle = d * Math.Cos(angle / 2);
        var sinAngle = d * Math.Sin(angle / 2);

        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = sinAngle * rotateVector.X,
            QuaternionY = sinAngle * rotateVector.Y,
            QuaternionZ = sinAngle * rotateVector.Z,
            QuaternionW = cosAngle,
            ScaleX = scaleFactor,
            ScaleY = scaleFactor,
            ScaleZ = scaleFactor
        };
    }

    public static XeoglQRotateScaleTranslateTransform CreateRotateScale(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        var lengthSquared1 = vector1.VectorENormSquared();
        var lengthSquared2 = vector2.VectorENormSquared();
        var scaleFactor = Math.Sqrt(lengthSquared2 / lengthSquared1);

        var n1 = Math.Sqrt(lengthSquared1 * lengthSquared2);

        var w = n1 + vector1.VectorESp(vector2);

        var angle = w.CosToDoublePolarAngle();

        double vx, vy, vz; 
        if (w < 1e-12 * n1)
        {
            var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                ? LinFloat64Vector3D.Create(-vector1.Y, vector1.X, 0)
                : LinFloat64Vector3D.Create(0, -vector1.Z, vector1.Y);

            var d = 1 / v1.VectorENorm();

            vx = d * v1.X;
            vy = d * v1.Y;
            vz = d * v1.Z;
        }
        else
        {
            var v2 = vector1.VectorCross(vector2);

            var d = 1 / v2.VectorENorm();

            vx = d * v2.X;
            vy = d * v2.Y;
            vz = d * v2.Z;
        }

        var sinAngle = angle.HalfPolarAngle().SinValue;

        return new XeoglQRotateScaleTranslateTransform
        {
            QuaternionX = sinAngle * vx,
            QuaternionY = sinAngle * vy,
            QuaternionZ = sinAngle * vz,
            QuaternionW = w,
            ScaleX = scaleFactor,
            ScaleY = scaleFactor,
            ScaleZ = scaleFactor
        };
    }


    public double QuaternionX { get; set; }

    public double QuaternionY { get; set; }

    public double QuaternionZ { get; set; }

    public double QuaternionW { get; set; } = 1;


    public double ScaleX { get; set; } = 1;

    public double ScaleY { get; set; } = 1;

    public double ScaleZ { get; set; } = 1;


    public double TranslateX { get; set; }

    public double TranslateY { get; set; }

    public double TranslateZ { get; set; }


    public bool ContainsMatrix => false;

    public bool ContainsQuaternion
        => QuaternionX > 0 || QuaternionY > 0 || QuaternionZ > 0 || QuaternionW > 1 ||
           QuaternionX < 0 || QuaternionY < 0 || QuaternionZ < 0 || QuaternionW < 1;

    public bool ContainsRotate => false;

    public bool ContainsScale
        => ScaleX > 1 || ScaleY > 1 || ScaleZ > 1 ||
           ScaleX < 1 || ScaleY < 1 || ScaleZ < 1;

    public bool ContainsTranslate
        => TranslateX > 0 || TranslateY > 0 || TranslateZ > 0 ||
           TranslateX < 0 || TranslateY < 0 || TranslateZ < 0;


    public SquareMatrix4 GetMatrix()
        => SquareMatrix4.CreateIdentityMatrix();

    public LinFloat64Quaternion GetQuaternionTuple()
        => LinFloat64Quaternion.Create(QuaternionW, QuaternionX, QuaternionY, QuaternionZ);

    public LinFloat64Vector3D GetRotateTuple()
        => LinFloat64Vector3D.Zero;

    public LinFloat64Vector3D GetScaleTuple()
        => LinFloat64Vector3D.Create(ScaleX, ScaleY, ScaleZ);

    public LinFloat64Vector3D GetTranslateTuple()
        => LinFloat64Vector3D.Create(TranslateX, TranslateY, TranslateZ);


    public XeoglQRotateScaleTranslateTransform SetQuaternion(ILinFloat64Vector4D quaternionTuple)
    {
        QuaternionX = quaternionTuple.X;
        QuaternionY = quaternionTuple.Y;
        QuaternionZ = quaternionTuple.Z;
        QuaternionW = quaternionTuple.W;

        return this;
    }

    public XeoglQRotateScaleTranslateTransform SetQuaternion(double quaternionX, double quaternionY, double quaternionZ, double quaternionW)
    {
        QuaternionX = quaternionX;
        QuaternionY = quaternionY;
        QuaternionZ = quaternionZ;
        QuaternionW = quaternionW;

        return this;
    }


    public XeoglQRotateScaleTranslateTransform SetScale(double scale)
    {
        ScaleX = scale;
        ScaleY = scale;
        ScaleZ = scale;

        return this;
    }

    public XeoglQRotateScaleTranslateTransform SetScale(ILinFloat64Vector3D scaleTuple)
    {
        ScaleX = scaleTuple.X;
        ScaleY = scaleTuple.Y;
        ScaleZ = scaleTuple.Z;

        return this;
    }

    public XeoglQRotateScaleTranslateTransform SetScale(double scaleX, double scaleY, double scaleZ)
    {
        ScaleX = scaleX;
        ScaleY = scaleY;
        ScaleZ = scaleZ;

        return this;
    }


    public XeoglQRotateScaleTranslateTransform SetTranslate(ILinFloat64Vector3D translateTuple)
    {
        TranslateX = translateTuple.X;
        TranslateY = translateTuple.Y;
        TranslateZ = translateTuple.Z;

        return this;
    }

    public XeoglQRotateScaleTranslateTransform SetTranslate(double translateX, double translateY, double translateZ)
    {
        TranslateX = translateX;
        TranslateY = translateY;
        TranslateZ = translateZ;

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

        var commaFlag = false;

        if (ContainsQuaternion)
        {
            composer
                .Append("quaternion: [")
                .Append(QuaternionX.ToString("G"))
                .Append(",")
                .Append(QuaternionY.ToString("G"))
                .Append(",")
                .Append(QuaternionZ.ToString("G"))
                .Append(",")
                .Append(QuaternionW.ToString("G"))
                .Append("]");

            commaFlag = true;
        }

        if (ContainsScale)
        {
            if (commaFlag)
                composer.AppendLine(",");

            composer
                .AppendAtNewLine("scale: [")
                .Append(ScaleX.ToString("G"))
                .Append(",")
                .Append(ScaleY.ToString("G"))
                .Append(",")
                .Append(ScaleZ.ToString("G"))
                .Append("]");

            commaFlag = true;
        }

        if (ContainsTranslate)
        {
            if (commaFlag)
                composer.AppendLine(",");

            composer
                .AppendAtNewLine("position: [")
                .Append(TranslateX.ToString("G"))
                .Append(",")
                .Append(TranslateY.ToString("G"))
                .Append(",")
                .Append(TranslateZ.ToString("G"))
                .Append("]");
        }

        return composer.ToString();
    }
}