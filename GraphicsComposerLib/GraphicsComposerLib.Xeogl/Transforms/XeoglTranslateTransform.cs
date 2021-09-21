﻿using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Xeogl.Transforms
{
    public sealed class XeoglTranslateTransform : IXeoglNumericalTransform
    {
        public static XeoglTranslateTransform Create(double tx, double ty, double tz)
        {
            return new XeoglTranslateTransform()
            {
                TranslateX = tx,
                TranslateY = ty,
                TranslateZ = tz
            };
        }

        public static XeoglTranslateTransform Create(ITuple3D t)
        {
            return new XeoglTranslateTransform()
            {
                TranslateX = t.X,
                TranslateY = t.Y,
                TranslateZ = t.Z
            };
        }


        public double TranslateX { get; set; }

        public double TranslateY { get; set; }

        public double TranslateZ { get; set; }


        public bool ContainsMatrix => false;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate => false;

        public bool ContainsScale => false;

        public bool ContainsTranslate
            => TranslateX > 0 || TranslateY > 0 || TranslateZ > 0 ||
               TranslateX < 0 || TranslateY < 0 || TranslateZ < 0;


        public XeoglTranslateTransform()
        {
        }

        public XeoglTranslateTransform(double tx, double ty, double tz)
        {
            TranslateX = tx;
            TranslateY = ty;
            TranslateZ = tz;
        }

        public XeoglTranslateTransform(ITuple3D t)
        {
            TranslateX = t.X;
            TranslateY = t.Y;
            TranslateZ = t.Z;
        }


        public Matrix4X4 GetMatrix()
            => Matrix4X4.CreateIdentityMatrix();

        public Tuple4D GetQuaternionTuple()
            => new Tuple4D(0, 0, 0, 1);

        public Tuple3D GetRotateTuple()
            => Tuple3D.Zero;

        public Tuple3D GetScaleTuple()
            => new Tuple3D(1, 1, 1);

        public Tuple3D GetTranslateTuple()
            => new Tuple3D(TranslateX, TranslateY, TranslateZ);


        public string GetMatrixText()
            => GetMatrix().ToXeoglNumbersArrayText();

        public string GetQuaternionText()
            => GetQuaternionTuple().ToXeoglNumbersArrayText();

        public string GetRotateText()
            => GetRotateTuple().ToXeoglNumbersArrayText();

        public string GetScaleText()
            => GetScaleTuple().ToXeoglNumbersArrayText();

        public string GetTranslateText()
            => GetTranslateTuple().ToXeoglNumbersArrayText();


        public override string ToString()
        {
            var composer = new LinearTextComposer();

            if (ContainsTranslate)
                composer
                    .AppendAtNewLine("position: [")
                    .Append(TranslateX.ToString("G"))
                    .Append(",")
                    .Append(TranslateY.ToString("G"))
                    .Append(",")
                    .Append(TranslateZ.ToString("G"))
                    .Append("]");

            return composer.ToString();
        }
    }
}