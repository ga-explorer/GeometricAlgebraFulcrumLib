﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicOperations;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.Borders.Space3D;
using EuclideanGeometryLib.Colors;
using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using GraphicsComposerLib.Xeogl.Constants;
using GraphicsComposerLib.Xeogl.Geometry;
using GraphicsComposerLib.Xeogl.Geometry.Builtin;
using GraphicsComposerLib.Xeogl.Geometry.Primitives;
using GraphicsComposerLib.Xeogl.Lights;
using GraphicsComposerLib.Xeogl.Materials;
using GraphicsComposerLib.Xeogl.Transforms;

namespace GraphicsComposerLib.Xeogl.DrawingSpace
{
    public static class XeoglDrawingSpaceUtils
    {
        public static XeoglDrawingSpace AddViewSpaceAmbientLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity)
        {
            return drawingSpace.AddLight(
                new XeoglAmbientLight()
                {
                    LightColor = color,
                    LightIntensity = intensity
                }
            );
        }

        public static XeoglDrawingSpace AddViewSpaceDirectionalLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity, ITuple3D direction)
        {
            return drawingSpace.AddLight(
                new XeoglDirectionalLight(direction)
                {
                    LightColor = color,
                    LightIntensity = intensity,
                    Space = XeoglSpace.View
                }
            );
        }

        public static XeoglDrawingSpace AddViewSpacePointLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position)
        {
            return drawingSpace.AddLight(
                new XeoglPointLight(position)
                {
                    LightColor = color,
                    LightIntensity = intensity,
                    Space = XeoglSpace.View
                }
            );
        }

        public static XeoglDrawingSpace AddViewSpaceSpotLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position, ITuple3D direction)
        {
            return drawingSpace.AddLight(
                new XeoglSpotLight(position, direction)
                {
                    LightColor = color,
                    LightIntensity = intensity,
                    Space = XeoglSpace.View
                }
            );
        }

        public static XeoglDrawingSpace AddWorldSpaceDirectionalLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity, ITuple3D direction)
        {
            return drawingSpace.AddLight(
                new XeoglDirectionalLight(direction)
                {
                    LightColor = color,
                    LightIntensity = intensity,
                    Space = XeoglSpace.World
                }
            );
        }

        public static XeoglDrawingSpace AddWorldSpacePointLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position)
        {
            return drawingSpace.AddLight(
                new XeoglPointLight(position)
                {
                    LightColor = color,
                    LightIntensity = intensity,
                    Space = XeoglSpace.World
                }
            );
        }

        public static XeoglDrawingSpace AddWorldSpaceSpotLight(this XeoglDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position, ITuple3D direction)
        {
            return drawingSpace.AddLight(
                new XeoglSpotLight(position, direction)
                {
                    LightColor = color,
                    LightIntensity = intensity,
                    Space = XeoglSpace.World
                }
            );
        }


        public static XeoglDrawingSpaceLayer DrawSphere(this XeoglDrawingSpaceLayer layer, double radius)
        {
            return layer.DrawGeometry(
                XeoglSphereGeometry.Create(radius)
            );
        }

        public static XeoglDrawingSpaceLayer DrawSphere(this XeoglDrawingSpaceLayer layer, double radius, int widthSegments, int heightSegments)
        {
            return layer.DrawGeometry(
                XeoglSphereGeometry.Create(radius, widthSegments, heightSegments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawSphere(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius)
        {
            return layer.DrawGeometry(
                XeoglSphereGeometry.Create(center, radius)
            );
        }

        public static XeoglDrawingSpaceLayer DrawSphere(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, int widthSegments, int heightSegments)
        {
            return layer.DrawGeometry(
                XeoglSphereGeometry.Create(center, radius, widthSegments, heightSegments)
            );
        }


        public static XeoglDrawingSpaceLayer DrawBox(this XeoglDrawingSpaceLayer layer, double halfSize)
        {
            return layer.DrawGeometry(
                XeoglBoxGeometry.Create(halfSize)
            );
        }

        public static XeoglDrawingSpaceLayer DrawBox(this XeoglDrawingSpaceLayer layer, ITuple3D halfSize)
        {
            return layer.DrawGeometry(
                XeoglBoxGeometry.Create(halfSize)
            );
        }

        public static XeoglDrawingSpaceLayer DrawBox(this XeoglDrawingSpaceLayer layer, ITuple3D center, double halfSize)
        {
            return layer.DrawGeometry(
                XeoglBoxGeometry.Create(center, halfSize)
            );
        }

        public static XeoglDrawingSpaceLayer DrawBox(this XeoglDrawingSpaceLayer layer, ITuple3D center, ITuple3D halfSize)
        {
            return layer.DrawGeometry(
                XeoglBoxGeometry.Create(center, halfSize)
            );
        }

        public static XeoglDrawingSpaceLayer DrawBox(this XeoglDrawingSpaceLayer layer, IBoundingBox3D box)
        {
            return layer.DrawGeometry(
                XeoglBoxGeometry.Create(box)
            );
        }


        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, double size, int segments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(size, segments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, double size, int xSegments, int zSegments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(size, xSegments, zSegments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, ITuple2D size, int segments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(size, segments)
            );
        }

        
        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, ITuple2D size, int xSegments, int zSegments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(size, xSegments, zSegments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, ITuple3D center, double size, int segments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(center, size, segments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, ITuple3D center, double size, int xSegments, int zSegments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(center, size, xSegments, zSegments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, ITuple3D center, ITuple2D size, int segments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(center, size, segments)
            );
        }

        public static XeoglDrawingSpaceLayer DrawPlaneSegment(this XeoglDrawingSpaceLayer layer, ITuple3D center, ITuple2D size, int xSegments, int zSegments)
        {
            return layer.DrawGeometry(
                XeoglPlaneGeometry.Create(center, size, xSegments, zSegments)
            );
        }


        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, double radius, double height)
        {
            return layer.DrawGeometry(
                XeoglCylinderGeometry.CreateClosed(radius, height)
            );
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, double radius, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments
            });
        }


        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, double radius, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, double radius, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radius,
                RadiusBottom = radius,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCylinder(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = radiusTop,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments,
                OpenEnded = true
            });
        }


        public static XeoglDrawingSpaceLayer DrawClosedCone(this XeoglDrawingSpaceLayer layer, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCone(this XeoglDrawingSpaceLayer layer, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCone(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height
            });
        }

        public static XeoglDrawingSpaceLayer DrawClosedCone(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments
            });
        }


        public static XeoglDrawingSpaceLayer DrawOpenedCone(this XeoglDrawingSpaceLayer layer, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCone(this XeoglDrawingSpaceLayer layer, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry()
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCone(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height,
                OpenEnded = true
            });
        }

        public static XeoglDrawingSpaceLayer DrawOpenedCone(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height, int radialSegments, int heightSegments)
        {
            return layer.DrawGeometry(new XeoglCylinderGeometry(center)
            {
                RadiusTop = 0,
                RadiusBottom = radiusBottom,
                Height = height,
                RadialSegments = radialSegments,
                HeightSegments = heightSegments,
                OpenEnded = true
            });
        }


        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius
            });
        }

        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius, double arcAngle)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle
            });
        }

        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius, int radialSegments, int tubeSegments)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            });
        }
        
        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry(center)
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            });
        }

        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, double radius, double tubeRadius)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius
            });
        }

        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, double radius, double tubeRadius, double arcAngle)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle
            });
        }

        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, double radius, double tubeRadius, int radialSegments, int tubeSegments)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            });
        }
        
        public static XeoglDrawingSpaceLayer DrawTorus(this XeoglDrawingSpaceLayer layer, double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
        {
            return layer.DrawGeometry(new XeoglTorusGeometry()
            {
                Radius = radius,
                TubeRadius = tubeRadius,
                ArcAngle = arcAngle,
                RadialSegments = radialSegments,
                TubeSegments = tubeSegments
            });
        }


        public static XeoglDrawingSpaceLayer DrawLineSegment(this XeoglDrawingSpaceLayer layer, ILineSegment3D lineSegment)
        {
            return layer.DrawGeometry(
                new XeoglLinesGeometry(
                    GraphicsLinesGeometry3D.Create(lineSegment)
                )
            );
        }

        public static XeoglDrawingSpaceLayer DrawLineSegments(this XeoglDrawingSpaceLayer layer, IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            return layer.DrawGeometry(
                lineSegmentsList.ToGraphicsLinesListGeometry().ToXeoglGeometry()
            );
        }

        public static XeoglDrawingSpaceLayer DrawLineStrip(this XeoglDrawingSpaceLayer layer, IEnumerable<ITuple3D> pointsList)
        {
            return layer.DrawGeometry(
                pointsList.ToGraphicsLineStripGeometry().ToXeoglGeometry()
            );
        }

        public static XeoglDrawingSpaceLayer DrawLineLoop(this XeoglDrawingSpaceLayer layer, IEnumerable<ITuple3D> pointsList)
        {
            return layer.DrawGeometry(
                pointsList.ToGraphicsLineLoopGeometry().ToXeoglGeometry()
            );
        }


        public static XeoglDrawingSpaceLayer DrawMarker(this XeoglDrawingSpaceLayer layer, XeoglGeometry markerGeometry, ITuple3D point)
        {
            return layer.DrawGeometry(
                markerGeometry,
                XeoglTranslateTransform.Create(point)
            );
        }

        public static XeoglDrawingSpaceLayer DrawMarker(this XeoglDrawingSpaceLayer layer, XeoglGeometry markerGeometry, params ITuple3D[] pointsList)
        {
            foreach (var point in pointsList)
                layer.DrawGeometry(
                    markerGeometry,
                    XeoglTranslateTransform.Create(point)
                );

            return layer;
        }

        public static XeoglDrawingSpaceLayer DrawMarker(this XeoglDrawingSpaceLayer layer, XeoglGeometry markerGeometry, IEnumerable<ITuple3D> pointsList)
        {
            foreach (var point in pointsList)
                layer.DrawGeometry(
                    markerGeometry,
                    XeoglTranslateTransform.Create(point)
                );

            return layer;
        }

        public static XeoglDrawingSpaceLayer DrawStoredMarker(this XeoglDrawingSpaceLayer layer, string geometryVariableName, ITuple3D point)
        {
            return layer.DrawStoredGeometry(
                geometryVariableName,
                XeoglTranslateTransform.Create(point)
            );
        }

        public static XeoglDrawingSpaceLayer DrawStoredMarker(this XeoglDrawingSpaceLayer layer, string geometryVariableName, params ITuple3D[] pointsList)
        {
            foreach (var point in pointsList)
                layer.DrawStoredGeometry(
                    geometryVariableName,
                    XeoglTranslateTransform.Create(point)
                );

            return layer;
        }

        public static XeoglDrawingSpaceLayer DrawStoredMarker(this XeoglDrawingSpaceLayer layer, string geometryVariableName, IEnumerable<ITuple3D> pointsList)
        {
            foreach (var point in pointsList)
                layer.DrawStoredGeometry(
                    geometryVariableName,
                    XeoglTranslateTransform.Create(point)
                );

            return layer;
        }


        public static XeoglDrawingSpaceLayer DrawWireWorldAxes(this XeoglDrawingSpaceLayer layer, double length, int thickness)
        {
            return layer.DrawWireWorldAxes(
                Tuple3D.Zero,
                length,
                thickness
            );
        }

        public static XeoglDrawingSpaceLayer DrawWireWorldAxes(this XeoglDrawingSpaceLayer layer, ITuple3D origin, double length, int thickness)
        {
            //x-axis
            layer.DrawGeometry(
                XeoglLinesGeometry.CreateLineSegment(
                    origin,
                    new Tuple3D(origin.X + length, origin.Y, origin.Z)
                ),
                new XeoglPhongMaterial()
                {
                    EmissiveColor = Color.Red,
                    LinePixelsWidth = thickness
                }
            );

            //y-axis
            layer.DrawGeometry(
                XeoglLinesGeometry.CreateLineSegment(
                    origin,
                    new Tuple3D(origin.X, origin.Y + length, origin.Z)
                ),
                new XeoglPhongMaterial()
                {
                    EmissiveColor = Color.Green,
                    LinePixelsWidth = thickness
                }
            );

            //z-axis
            layer.DrawGeometry(
                XeoglLinesGeometry.CreateLineSegment(
                    origin,
                    new Tuple3D(origin.X, origin.Y, origin.Z + length)
                ),
                new XeoglPhongMaterial()
                {
                    EmissiveColor = Color.Blue,
                    LinePixelsWidth = thickness
                }
            );

            return layer;
        }

        public static XeoglDrawingSpaceLayer DrawSolidWorldAxes(this XeoglDrawingSpaceLayer layer, double length = 1, double thickness = 0.075, double arrowHeadLength = 0.3)
        {
            return layer.DrawSolidWorldAxes(
                Tuple3D.Zero,
                length,
                thickness,
                arrowHeadLength
            );
        }

        public static XeoglDrawingSpaceLayer DrawSolidWorldAxes(this XeoglDrawingSpaceLayer layer, ITuple3D origin, double length = 1, double thickness = 0.075, double arrowHeadLength = 0.3)
        {
            var arrowHeadBaseRadius = arrowHeadLength / 3;
            var arrowEdgeLength = length - arrowHeadLength;

            var baseMaterial = new XeoglPhongMaterial()
            {
                AmbientColor = Color.Black,
                SpecularColor = ColorsUtils.ToSystemColor(0.6, 0.6, 0.3),
                Shininess = 80,
                LinePixelsWidth = 2
            };

            //x-axis
            layer.DrawGeometry(
                XeoglCylinderGeometry.CreateOpened(thickness / 2, arrowEdgeLength),
                XeoglERotateTranslateTransform
                    .CreateRotateYtoX()
                    .SetTranslate(
                        origin.X + 0.5 * arrowEdgeLength, 
                        origin.Y, 
                        origin.Z
                    ),
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.DarkRed
                }
            );

            layer.DrawGeometry(
                XeoglCylinderGeometry.CreateClosedCone(arrowHeadBaseRadius, arrowHeadLength),
                XeoglERotateTranslateTransform
                    .CreateRotateYtoX()
                    .SetTranslate(
                        origin.X + arrowEdgeLength + 0.5 * arrowHeadLength, 
                        origin.Y, 
                        origin.Z
                    ),
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.Red
                }
            );

            //y-axis
            layer.DrawGeometry(
                XeoglCylinderGeometry.CreateOpened(thickness / 2, length - arrowHeadLength),
                XeoglTranslateTransform
                    .Create(
                        origin.X, 
                        origin.Y + 0.5 * arrowEdgeLength, 
                        origin.Z
                    ),
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.DarkGreen
                }
            );

            layer.DrawGeometry(
                XeoglCylinderGeometry.CreateClosedCone(arrowHeadBaseRadius, arrowHeadLength),
                XeoglTranslateTransform
                    .Create(
                        origin.X, 
                        origin.Y + arrowEdgeLength + 0.5 * arrowHeadLength, 
                        origin.Z
                    ),
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.Green
                }
            );

            //z-axis
            layer.DrawGeometry(
                XeoglCylinderGeometry.CreateOpened(thickness / 2, length - arrowHeadLength),
                XeoglERotateTranslateTransform
                    .CreateRotateYtoZ()
                    .SetTranslate(
                        origin.X, 
                        origin.Y, 
                        origin.Z + 0.5 * arrowEdgeLength
                    ),
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.DarkBlue
                }
            );

            layer.DrawGeometry(
                XeoglCylinderGeometry.CreateClosedCone(arrowHeadBaseRadius, arrowHeadLength),
                XeoglERotateTranslateTransform
                    .CreateRotateYtoZ()
                    .SetTranslate(
                        origin.X, 
                        origin.Y, 
                        origin.Z + arrowEdgeLength + 0.5 * arrowHeadLength
                    ),
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.Blue
                }
            );

            //Origin
            layer.DrawGeometry(
                XeoglSphereGeometry.Create(origin, 1.5 * thickness), 
                new XeoglPhongMaterial(baseMaterial)
                {
                    DiffuseColor = Color.DarkSlateGray
                }
            );

            return layer;
        }


        public static XeoglDrawingSpaceLayer DrawWireArrow(this XeoglDrawingSpaceLayer layer, ITuple3D origin, ITuple3D direction, int thickness, Color color)
        {
            var endPoint = origin.GetPointInDirection(direction);

            layer.DrawGeometry(
                XeoglLinesGeometry.CreateLineSegment(
                    origin,
                    endPoint
                ),
                new XeoglPhongMaterial()
                {
                    EmissiveColor = color,
                    LinePixelsWidth = thickness
                }
            );

            return layer;
        }

        public static XeoglDrawingSpaceLayer DrawWireArrows(this XeoglDrawingSpaceLayer layer, IEnumerable<Tuple<ITuple3D, ITuple3D>> arrowsList, double maxLength, int thickness, Color color)
        {
            var arrowsArray = arrowsList.ToArray();

            var scaleFactor = 
                maxLength / arrowsArray.Select(t => t.Item2.GetLength()).Max();

            var lineSegmentsGeometry =
                arrowsArray.Select(t =>
                    LineSegment3D.CreateFromPointAndScaledVector(
                        t.Item1, 
                        t.Item2, 
                        scaleFactor
                    )
                )
                .ToGraphicsLinesListGeometry()
                .ToXeoglGeometry();

            layer.DrawGeometry(
                lineSegmentsGeometry,
                color.ToXeoglEmissivePhongMaterial(thickness)
            );

            return layer;
        }
    }
}
