﻿using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Geometry.Primitives;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Transforms;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;
using TextComposerLib;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl;

public static class XeoglUtils
{
    //public static JsCodeComponentAttributesDictionary SetValue(this JsCodeComponentAttributesDictionary composer, string key, XeoglGeometry value, XeoglGeometry valueDefault)
    //{
    //    composer.SetTextValue(
    //        key, 
    //        value?.ToString() ?? string.Empty, 
    //        valueDefault?.ToString() ?? string.Empty
    //    );

    //    return composer;
    //}

    //public static JsCodeComponentAttributesDictionary SetValue(this JsCodeComponentAttributesDictionary composer, string key, XeoglMaterial value, XeoglMaterial valueDefault)
    //{
    //    composer.SetTextValue(
    //        key,
    //        value?.ToString() ?? string.Empty,
    //        valueDefault?.ToString() ?? string.Empty
    //    );

    //    return composer;
    //}

    //public static JsCodeComponentAttributesDictionary SetValue(this JsCodeComponentAttributesDictionary composer, string key, XeoglFresnelEffect value, XeoglFresnelEffect valueDefault)
    //{
    //    composer.SetTextValue(
    //        key,
    //        value?.ToString() ?? string.Empty,
    //        valueDefault?.ToString() ?? string.Empty
    //    );

    //    return composer;
    //}

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, GraphicsPrimitiveType3D value, GraphicsPrimitiveType3D valueDefault)
    {
        composer.SetTextValue(
            key, 
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, XeoglSpace value, XeoglSpace valueDefault)
    {
        composer.SetTextValue(
            key,
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, XeoglWindingDirection value, XeoglWindingDirection valueDefault)
    {
        composer.SetTextValue(
            key,
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, XeoglBillboardBehaviour value, XeoglBillboardBehaviour valueDefault)
    {
        composer.SetTextValue(
            key, 
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, XeoglCameraProjectionType value, XeoglCameraProjectionType valueDefault)
    {
        composer.SetTextValue(
            key,
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }

    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, XeoglPerspectiveFieldOfViewAxis value, XeoglPerspectiveFieldOfViewAxis valueDefault)
    {
        composer.SetTextValue(
            key,
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }
        
    public static JavaScriptAttributesDictionary SetValue(this JavaScriptAttributesDictionary composer, string key, XeoglAlphaMode value, XeoglAlphaMode valueDefault)
    {
        composer.SetTextValue(
            key,
            value.GetName().DoubleQuote(),
            valueDefault.GetName().DoubleQuote()
        );

        return composer;
    }

        
    public static XeoglMatrixTransform ToXeoglTransform(this SquareMatrix4 matrix)
    {
        return new XeoglMatrixTransform(matrix);
    }

    public static XeoglQRotateScaleTranslateTransform ToXeoglTransform(this RotateScaleTranslateMap3D affineMap)
    {
        var quaternion = affineMap.RotateQuaternion;

        return new XeoglQRotateScaleTranslateTransform()
        {
            TranslateX = affineMap.TranslateX,
            TranslateY = affineMap.TranslateY,
            TranslateZ = affineMap.TranslateZ,
            ScaleX = affineMap.ScaleX,
            ScaleY = affineMap.ScaleY,
            ScaleZ = affineMap.ScaleZ,
            QuaternionX = quaternion.ScalarI,
            QuaternionY = quaternion.ScalarJ,
            QuaternionZ = quaternion.ScalarK,
            QuaternionW = quaternion.Scalar
        };
    }
        

    public static XeoglTrianglesGeometry ToXeoglTrianglesGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints, bool reverseNormals)
    {
        var geometry = trianglesList.ToGraphicsTrianglesGeometry(reversePoints);
        geometry.ComputeVertexNormals(reverseNormals);

        return new XeoglTrianglesGeometry(geometry);
    }

    public static XeoglTrianglesGeometry ToXeoglTrianglesListGeometry(this IEnumerable<ITriangle3D> trianglesList, bool reversePoints, bool reverseNormals)
    {
        var geometry = trianglesList.ToGraphicsTrianglesListGeometry(reversePoints);
        geometry.ComputeVertexNormals(reverseNormals);

        return new XeoglTrianglesGeometry(geometry);
    }
}