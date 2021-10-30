namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.DrawingSpace
{
    //public static class TjDrawingSpaceUtils
    //{
    //    public static TjDrawingSpace AddViewSpaceAmbientLight(this TjDrawingSpace drawingSpace, Color color, double intensity)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjAmbientLight()
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity
    //            }
    //        );
    //    }

    //    public static TjDrawingSpace AddViewSpaceDirectionalLight(this TjDrawingSpace drawingSpace, Color color, double intensity, ITuple3D direction)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjDirectionalLight(direction)
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity,
    //                Space = TjSpace.View
    //            }
    //        );
    //    }

    //    public static TjDrawingSpace AddViewSpacePointLight(this TjDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjPointLight(position)
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity,
    //                Space = TjSpace.View
    //            }
    //        );
    //    }

    //    public static TjDrawingSpace AddViewSpaceSpotLight(this TjDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position, ITuple3D direction)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjSpotLight(position, direction)
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity,
    //                Space = TjSpace.View
    //            }
    //        );
    //    }

    //    public static TjDrawingSpace AddWorldSpaceDirectionalLight(this TjDrawingSpace drawingSpace, Color color, double intensity, ITuple3D direction)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjDirectionalLight(direction)
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity,
    //                Space = TjSpace.World
    //            }
    //        );
    //    }

    //    public static TjDrawingSpace AddWorldSpacePointLight(this TjDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjPointLight(position)
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity,
    //                Space = TjSpace.World
    //            }
    //        );
    //    }

    //    public static TjDrawingSpace AddWorldSpaceSpotLight(this TjDrawingSpace drawingSpace, Color color, double intensity, ITuple3D position, ITuple3D direction)
    //    {
    //        return drawingSpace.AddLight(
    //            new TjSpotLight(position, direction)
    //            {
    //                LightColor = color,
    //                LightIntensity = intensity,
    //                Space = TjSpace.World
    //            }
    //        );
    //    }


    //    public static TjDrawingSpaceLayer DrawSphere(this TjDrawingSpaceLayer layer, double radius)
    //    {
    //        return layer.DrawGeometry(
    //            TjSphereGeometry.Create(radius)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawSphere(this TjDrawingSpaceLayer layer, double radius, int widthSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(
    //            TjSphereGeometry.Create(radius, widthSegments, heightSegments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawSphere(this TjDrawingSpaceLayer layer, ITuple3D center, double radius)
    //    {
    //        return layer.DrawGeometry(
    //            TjSphereGeometry.Create(center, radius)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawSphere(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, int widthSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(
    //            TjSphereGeometry.Create(center, radius, widthSegments, heightSegments)
    //        );
    //    }


    //    public static TjDrawingSpaceLayer DrawBox(this TjDrawingSpaceLayer layer, double halfSize)
    //    {
    //        return layer.DrawGeometry(
    //            TjBoxGeometry.Create(halfSize)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawBox(this TjDrawingSpaceLayer layer, ITuple3D halfSize)
    //    {
    //        return layer.DrawGeometry(
    //            TjBoxGeometry.Create(halfSize)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawBox(this TjDrawingSpaceLayer layer, ITuple3D center, double halfSize)
    //    {
    //        return layer.DrawGeometry(
    //            TjBoxGeometry.Create(center, halfSize)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawBox(this TjDrawingSpaceLayer layer, ITuple3D center, ITuple3D halfSize)
    //    {
    //        return layer.DrawGeometry(
    //            TjBoxGeometry.Create(center, halfSize)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawBox(this TjDrawingSpaceLayer layer, IBoundingBox3D box)
    //    {
    //        return layer.DrawGeometry(
    //            TjBoxGeometry.Create(box)
    //        );
    //    }


    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, double size, int segments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(size, segments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, double size, int xSegments, int zSegments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(size, xSegments, zSegments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, ITuple2D size, int segments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(size, segments)
    //        );
    //    }

        
    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, ITuple2D size, int xSegments, int zSegments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(size, xSegments, zSegments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, ITuple3D center, double size, int segments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(center, size, segments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, ITuple3D center, double size, int xSegments, int zSegments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(center, size, xSegments, zSegments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, ITuple3D center, ITuple2D size, int segments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(center, size, segments)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawPlaneSegment(this TjDrawingSpaceLayer layer, ITuple3D center, ITuple2D size, int xSegments, int zSegments)
    //    {
    //        return layer.DrawGeometry(
    //            TjPlaneGeometry.Create(center, size, xSegments, zSegments)
    //        );
    //    }


    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, double radius, double height)
    //    {
    //        return layer.DrawGeometry(
    //            TjCylinderGeometry.CreateClosed(radius, height)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, double radius, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments
    //        });
    //    }


    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, double radius, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, double radius, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radius,
    //            RadiusBottom = radius,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCylinder(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusTop, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = radiusTop,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments,
    //            OpenEnded = true
    //        });
    //    }


    //    public static TjDrawingSpaceLayer DrawClosedCone(this TjDrawingSpaceLayer layer, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCone(this TjDrawingSpaceLayer layer, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCone(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawClosedCone(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments
    //        });
    //    }


    //    public static TjDrawingSpaceLayer DrawOpenedCone(this TjDrawingSpaceLayer layer, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCone(this TjDrawingSpaceLayer layer, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry()
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCone(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            OpenEnded = true
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawOpenedCone(this TjDrawingSpaceLayer layer, ITuple3D center, double radiusBottom, double height, int radialSegments, int heightSegments)
    //    {
    //        return layer.DrawGeometry(new TjCylinderGeometry(center)
    //        {
    //            RadiusTop = 0,
    //            RadiusBottom = radiusBottom,
    //            Height = height,
    //            RadialSegments = radialSegments,
    //            HeightSegments = heightSegments,
    //            OpenEnded = true
    //        });
    //    }


    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry(center)
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius, double arcAngle)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry(center)
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius,
    //            ArcAngle = arcAngle
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius, int radialSegments, int tubeSegments)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry(center)
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius,
    //            RadialSegments = radialSegments,
    //            TubeSegments = tubeSegments
    //        });
    //    }
        
    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, ITuple3D center, double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry(center)
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius,
    //            ArcAngle = arcAngle,
    //            RadialSegments = radialSegments,
    //            TubeSegments = tubeSegments
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, double radius, double tubeRadius)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry()
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, double radius, double tubeRadius, double arcAngle)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry()
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius,
    //            ArcAngle = arcAngle
    //        });
    //    }

    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, double radius, double tubeRadius, int radialSegments, int tubeSegments)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry()
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius,
    //            RadialSegments = radialSegments,
    //            TubeSegments = tubeSegments
    //        });
    //    }
        
    //    public static TjDrawingSpaceLayer DrawTorus(this TjDrawingSpaceLayer layer, double radius, double tubeRadius, double arcAngle, int radialSegments, int tubeSegments)
    //    {
    //        return layer.DrawGeometry(new TjTorusGeometry()
    //        {
    //            Radius = radius,
    //            TubeRadius = tubeRadius,
    //            ArcAngle = arcAngle,
    //            RadialSegments = radialSegments,
    //            TubeSegments = tubeSegments
    //        });
    //    }


    //    public static TjDrawingSpaceLayer DrawLineSegment(this TjDrawingSpaceLayer layer, ILineSegment3D lineSegment)
    //    {
    //        return layer.DrawGeometry(
    //            new TjLinesGeometry(
    //                GraphicsLinesGeometry3D.Create(lineSegment)
    //            )
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawLineSegments(this TjDrawingSpaceLayer layer, IEnumerable<ILineSegment3D> lineSegmentsList)
    //    {
    //        return layer.DrawGeometry(
    //            lineSegmentsList.ToGraphicsLinesListGeometry().ToTjGeometry()
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawLineStrip(this TjDrawingSpaceLayer layer, IEnumerable<ITuple3D> pointsList)
    //    {
    //        return layer.DrawGeometry(
    //            pointsList.ToGraphicsLineStripGeometry().ToTjGeometry()
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawLineLoop(this TjDrawingSpaceLayer layer, IEnumerable<ITuple3D> pointsList)
    //    {
    //        return layer.DrawGeometry(
    //            pointsList.ToGraphicsLineLoopGeometry().ToTjGeometry()
    //        );
    //    }


    //    public static TjDrawingSpaceLayer DrawMarker(this TjDrawingSpaceLayer layer, TjGeometry markerGeometry, ITuple3D point)
    //    {
    //        return layer.DrawGeometry(
    //            markerGeometry,
    //            TjTranslateTransform.Create(point)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawMarker(this TjDrawingSpaceLayer layer, TjGeometry markerGeometry, params ITuple3D[] pointsList)
    //    {
    //        foreach (var point in pointsList)
    //            layer.DrawGeometry(
    //                markerGeometry,
    //                TjTranslateTransform.Create(point)
    //            );

    //        return layer;
    //    }

    //    public static TjDrawingSpaceLayer DrawMarker(this TjDrawingSpaceLayer layer, TjGeometry markerGeometry, IEnumerable<ITuple3D> pointsList)
    //    {
    //        foreach (var point in pointsList)
    //            layer.DrawGeometry(
    //                markerGeometry,
    //                TjTranslateTransform.Create(point)
    //            );

    //        return layer;
    //    }

    //    public static TjDrawingSpaceLayer DrawStoredMarker(this TjDrawingSpaceLayer layer, string geometryVariableName, ITuple3D point)
    //    {
    //        return layer.DrawStoredGeometry(
    //            geometryVariableName,
    //            TjTranslateTransform.Create(point)
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawStoredMarker(this TjDrawingSpaceLayer layer, string geometryVariableName, params ITuple3D[] pointsList)
    //    {
    //        foreach (var point in pointsList)
    //            layer.DrawStoredGeometry(
    //                geometryVariableName,
    //                TjTranslateTransform.Create(point)
    //            );

    //        return layer;
    //    }

    //    public static TjDrawingSpaceLayer DrawStoredMarker(this TjDrawingSpaceLayer layer, string geometryVariableName, IEnumerable<ITuple3D> pointsList)
    //    {
    //        foreach (var point in pointsList)
    //            layer.DrawStoredGeometry(
    //                geometryVariableName,
    //                TjTranslateTransform.Create(point)
    //            );

    //        return layer;
    //    }


    //    public static TjDrawingSpaceLayer DrawWireWorldAxes(this TjDrawingSpaceLayer layer, double length, int thickness)
    //    {
    //        return layer.DrawWireWorldAxes(
    //            Tuple3D.Zero,
    //            length,
    //            thickness
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawWireWorldAxes(this TjDrawingSpaceLayer layer, ITuple3D origin, double length, int thickness)
    //    {
    //        //x-axis
    //        layer.DrawGeometry(
    //            TjLinesGeometry.CreateLineSegment(
    //                origin,
    //                new Tuple3D(origin.X + length, origin.Y, origin.Z)
    //            ),
    //            new TjPhongMaterial()
    //            {
    //                EmissiveColor = Color.Red,
    //                LinePixelsWidth = thickness
    //            }
    //        );

    //        //y-axis
    //        layer.DrawGeometry(
    //            TjLinesGeometry.CreateLineSegment(
    //                origin,
    //                new Tuple3D(origin.X, origin.Y + length, origin.Z)
    //            ),
    //            new TjPhongMaterial()
    //            {
    //                EmissiveColor = Color.Green,
    //                LinePixelsWidth = thickness
    //            }
    //        );

    //        //z-axis
    //        layer.DrawGeometry(
    //            TjLinesGeometry.CreateLineSegment(
    //                origin,
    //                new Tuple3D(origin.X, origin.Y, origin.Z + length)
    //            ),
    //            new TjPhongMaterial()
    //            {
    //                EmissiveColor = Color.Blue,
    //                LinePixelsWidth = thickness
    //            }
    //        );

    //        return layer;
    //    }

    //    public static TjDrawingSpaceLayer DrawSolidWorldAxes(this TjDrawingSpaceLayer layer, double length = 1, double thickness = 0.075, double arrowHeadLength = 0.3)
    //    {
    //        return layer.DrawSolidWorldAxes(
    //            Tuple3D.Zero,
    //            length,
    //            thickness,
    //            arrowHeadLength
    //        );
    //    }

    //    public static TjDrawingSpaceLayer DrawSolidWorldAxes(this TjDrawingSpaceLayer layer, ITuple3D origin, double length = 1, double thickness = 0.075, double arrowHeadLength = 0.3)
    //    {
    //        var arrowHeadBaseRadius = arrowHeadLength / 3;
    //        var arrowEdgeLength = length - arrowHeadLength;

    //        var baseMaterial = new TjPhongMaterial()
    //        {
    //            AmbientColor = Color.Black,
    //            SpecularColor = ColorsUtils.ToSystemColor(0.6, 0.6, 0.3),
    //            Shininess = 80,
    //            LinePixelsWidth = 2
    //        };

    //        //x-axis
    //        layer.DrawGeometry(
    //            TjCylinderGeometry.CreateOpened(thickness / 2, arrowEdgeLength),
    //            TjERotateTranslateTransform
    //                .CreateRotateYtoX()
    //                .SetTranslate(
    //                    origin.X + 0.5 * arrowEdgeLength, 
    //                    origin.Y, 
    //                    origin.Z
    //                ),
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.DarkRed
    //            }
    //        );

    //        layer.DrawGeometry(
    //            TjCylinderGeometry.CreateClosedCone(arrowHeadBaseRadius, arrowHeadLength),
    //            TjERotateTranslateTransform
    //                .CreateRotateYtoX()
    //                .SetTranslate(
    //                    origin.X + arrowEdgeLength + 0.5 * arrowHeadLength, 
    //                    origin.Y, 
    //                    origin.Z
    //                ),
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.Red
    //            }
    //        );

    //        //y-axis
    //        layer.DrawGeometry(
    //            TjCylinderGeometry.CreateOpened(thickness / 2, length - arrowHeadLength),
    //            TjTranslateTransform
    //                .Create(
    //                    origin.X, 
    //                    origin.Y + 0.5 * arrowEdgeLength, 
    //                    origin.Z
    //                ),
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.DarkGreen
    //            }
    //        );

    //        layer.DrawGeometry(
    //            TjCylinderGeometry.CreateClosedCone(arrowHeadBaseRadius, arrowHeadLength),
    //            TjTranslateTransform
    //                .Create(
    //                    origin.X, 
    //                    origin.Y + arrowEdgeLength + 0.5 * arrowHeadLength, 
    //                    origin.Z
    //                ),
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.Green
    //            }
    //        );

    //        //z-axis
    //        layer.DrawGeometry(
    //            TjCylinderGeometry.CreateOpened(thickness / 2, length - arrowHeadLength),
    //            TjERotateTranslateTransform
    //                .CreateRotateYtoZ()
    //                .SetTranslate(
    //                    origin.X, 
    //                    origin.Y, 
    //                    origin.Z + 0.5 * arrowEdgeLength
    //                ),
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.DarkBlue
    //            }
    //        );

    //        layer.DrawGeometry(
    //            TjCylinderGeometry.CreateClosedCone(arrowHeadBaseRadius, arrowHeadLength),
    //            TjERotateTranslateTransform
    //                .CreateRotateYtoZ()
    //                .SetTranslate(
    //                    origin.X, 
    //                    origin.Y, 
    //                    origin.Z + arrowEdgeLength + 0.5 * arrowHeadLength
    //                ),
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.Blue
    //            }
    //        );

    //        //Origin
    //        layer.DrawGeometry(
    //            TjSphereGeometry.Create(origin, 1.5 * thickness), 
    //            new TjPhongMaterial(baseMaterial)
    //            {
    //                DiffuseColor = Color.DarkSlateGray
    //            }
    //        );

    //        return layer;
    //    }


    //    public static TjDrawingSpaceLayer DrawWireArrow(this TjDrawingSpaceLayer layer, ITuple3D origin, ITuple3D direction, int thickness, Color color)
    //    {
    //        var endPoint = origin.GetPointInDirection(direction);

    //        layer.DrawGeometry(
    //            TjLinesGeometry.CreateLineSegment(
    //                origin,
    //                endPoint
    //            ),
    //            new TjPhongMaterial()
    //            {
    //                EmissiveColor = color,
    //                LinePixelsWidth = thickness
    //            }
    //        );

    //        return layer;
    //    }

    //    public static TjDrawingSpaceLayer DrawWireArrows(this TjDrawingSpaceLayer layer, IEnumerable<Tuple<ITuple3D, ITuple3D>> arrowsList, double maxLength, int thickness, Color color)
    //    {
    //        var arrowsArray = arrowsList.ToArray();

    //        var scaleFactor = 
    //            maxLength / arrowsArray.Select(t => t.Item2.GetLength()).Max();

    //        var lineSegmentsGeometry =
    //            arrowsArray.Select(t =>
    //                LineSegment3D.CreateFromPointAndScaledVector(
    //                    t.Item1, 
    //                    t.Item2, 
    //                    scaleFactor
    //                )
    //            )
    //            .ToGraphicsLinesListGeometry()
    //            .ToTjGeometry();

    //        layer.DrawGeometry(
    //            lineSegmentsGeometry,
    //            color.ToTjEmissivePhongMaterial(thickness)
    //        );

    //        return layer;
    //    }
    //}
}
