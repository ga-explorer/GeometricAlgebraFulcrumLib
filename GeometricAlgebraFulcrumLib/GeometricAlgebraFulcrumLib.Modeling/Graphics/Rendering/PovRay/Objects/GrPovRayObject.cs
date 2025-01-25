using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.CSG;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public abstract class GrPovRayObject : 
    IGrPovRayObject
{
    public static GrPovRayNamedObject Named(string objName)
    {
        return new GrPovRayNamedObject(objName);
    }

    public static GrPovRaySphere Sphere(ITriplet<Float64Scalar> center, Float64Scalar radius)
    {
        return GrPovRaySphere.Create(center, radius);
    }

    public static GrPovRaySphere Sphere(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        return GrPovRaySphere.Create(center, radius);
    }
    
    public static GrPovRaySphere Sphere(ITriplet<Float64Scalar> center, Float64Scalar radiusX, Float64Scalar radiusY, Float64Scalar radiusZ)
    {
        return GrPovRaySphere.Create(center, radiusX, radiusY, radiusZ);
    }

    public static GrPovRayBox Box(GrPovRayVector3Value corner1, GrPovRayVector3Value corner2)
    {
        return new GrPovRayBox(corner1, corner2);
    }
    
    public static GrPovRayBox Box(double xLength, double yLength, double zLength)
    {
        var corner1 = 
            LinFloat64Vector3D.Create(
                -0.5 * xLength,
                -0.5 * yLength,
                -0.5 * zLength
            );

        var corner2 = 
            LinFloat64Vector3D.Create(
                0.5 * xLength,
                0.5 * yLength,
                0.5 * zLength
            );

        return new GrPovRayBox(corner1, corner2);
    }

    public static GrPovRayCone Cone(GrPovRayVector3Value basePoint, GrPovRayVector3Value capPoint, GrPovRayFloat32Value baseRadius, GrPovRayFloat32Value capRadius, bool open = false)
    {
        return new GrPovRayCone(basePoint, capPoint, baseRadius, capRadius, open);
    }

    public static GrPovRayCylinder Cylinder(GrPovRayVector3Value basePoint, GrPovRayVector3Value capPoint, GrPovRayFloat32Value radius, bool open = false)
    {
        return new GrPovRayCylinder(basePoint, capPoint, radius, open);
    }

    public static GrPovRayOvus Ovus(GrPovRayFloat32Value bottomRadius, GrPovRayFloat32Value topRadius)
    {
        return new GrPovRayOvus(bottomRadius, topRadius);
    }

    public static GrPovRayTorus Torus(GrPovRayFloat32Value majorRadius, GrPovRayFloat32Value minorRadius)
    {
        return new GrPovRayTorus(majorRadius, minorRadius);
    }
    
    public static GrPovRayIsoSurface IsoSurface(GrPovRayFreeCode function)
    {
        return new GrPovRayIsoSurface(function);
    }

    public static GrPovRaySuperQuadricEllipsoid SuperQuadricEllipsoid(GrPovRayFloat32Value eastWestExponent, GrPovRayFloat32Value northSouthExponent)
    {
        return new GrPovRaySuperQuadricEllipsoid(eastWestExponent, northSouthExponent);
    }

    public static GrPovRayPlane PlaneFromNormalDistance(GrPovRayVector3Value normal, GrPovRayFloat32Value distance)
    {
        return GrPovRayPlane.CreateFromNormalDistance(normal, distance);
    }

    public static GrPovRaySphereSweep SphereSweep(GrPovRaySphereSweepType kind)
    {
        return new GrPovRaySphereSweep(kind);
    }

    public static GrPovRaySurfaceOfRevolution SurfaceOfRevolution(bool open)
    {
        return new GrPovRaySurfaceOfRevolution(open);
    }

    public static GrPovRayLathe Lathe(GrPovRayLatheSplineType splineType)
    {
        return new GrPovRayLathe(splineType);
    }

    public static GrPovRayPrism Prism(GrPovRayFloat32Value height1, GrPovRayFloat32Value height2, GrPovRayPrismSplineType splineType, GrPovRayPrismSweepType sweepType, bool open)
    {
        return new GrPovRayPrism(height1, height2, splineType, sweepType, open);
    }


    public static GrPovRayBicubicPatch BicubicPatch(bool preprocessSubPatches, GrPovRayVector3Value[,] convexHull)
    {
        return new GrPovRayBicubicPatch(preprocessSubPatches, convexHull);
    }

    public static GrPovRayDisc Disc(GrPovRayVector3Value center, GrPovRayVector3Value normal, GrPovRayFloat32Value radius, GrPovRayFloat32Value? holeRadius = null)
    {
        return new GrPovRayDisc(center, normal, radius, holeRadius);
    }

    public static GrPovRayTriangle Triangle(GrPovRayVector3Value corner1, GrPovRayVector3Value corner2, GrPovRayVector3Value corner3)
    {
        return new GrPovRayTriangle(corner1, corner2, corner3);
    }

    public static GrPovRaySmoothTriangle SmoothTriangle(GrPovRayVector3Value corner1, GrPovRayVector3Value normal1, GrPovRayVector3Value corner2, GrPovRayVector3Value normal2, GrPovRayVector3Value corner3, GrPovRayVector3Value normal3)
    {
        return new GrPovRaySmoothTriangle(corner1, normal1, corner2, normal2, corner3, normal3);
    }
    
    public static GrPovRayPolygon SquarePolygon(double length)
    {
        return GrPovRayPolygon.CreateSquare(length);
    }

    //public static GrPovRayPolygon RectanglePolygon(double width, double height)
    //{
    //    var rectangle = GrPovRayPolygon.CreateRectangle(width, height);
        
    //    CodeElementList.Add(rectangle);

    //    return rectangle;
    //}
    
    public static GrPovRayPolygon RectanglePolygonXy(double width, double height)
    {
        return GrPovRayPolygon.CreateRectangleXy(width, height);
    }
    
    public static GrPovRayPolygon RectanglePolygonYz(double width, double height)
    {
        return GrPovRayPolygon.CreateRectangleYz(width, height);
    }
    
    public static GrPovRayPolygon RectanglePolygonZx(double width, double height)
    {
        return GrPovRayPolygon.CreateRectangleZx(width, height);
    }
    
    public static GrPovRayPolygon RectanglePolygonXz(double width, double height)
    {
        return GrPovRayPolygon.CreateRectangleXz(width, height);
    }

    public static GrPovRayPolygon RegularPolygon(int sideCount, double radius)
    {
        return GrPovRayPolygon.CreateRegularPolygon(sideCount, radius);
    }
    
    public static GrPovRayPolygon ClosedPolygon(IReadOnlyList<IPair<Float64Scalar>> pointList)
    {
        return GrPovRayPolygon.CreateClosedPolygon(pointList);
    }
    
    public static GrPovRayPolygon ClosedPolygon(params IPair<Float64Scalar>[] pointList)
    {
        return GrPovRayPolygon.CreateClosedPolygon(pointList);
    }

    public static GrPovRayPolygon Polygon()
    {
        return GrPovRayPolygon.CreatePolygon();
    }

    
    public static GrPovRayIntersection RegularPolygonCylinder(int sideCount, double radius)
    {
        var polygonSpecs = Float64RegularPolygon2D.CreateFromOuterRadius(sideCount, radius);
        var distance = polygonSpecs.InnerRadius;

        var planeList =
            polygonSpecs.SideUnitNormals.Select(normal => 
                GrPovRayPlane.CreateFromNormalDistance(
                    normal.ToXyLinVector3D(), 
                    distance
                )
            );

        var intersection = new GrPovRayIntersection(planeList);
        
        return intersection;
    }

    public static GrPovRayIntersection RegularPolygonCylinder(int sideCount, double radius, double yMin)
    {
        var polygonSpecs = Float64RegularPolygon2D.CreateFromOuterRadius(sideCount, radius);
        var distance = polygonSpecs.InnerRadius;

        var planeList =
            polygonSpecs.SideUnitNormals.Select(normal => 
                GrPovRayPlane.CreateFromNormalDistance(
                    normal.ToXyLinVector3D(), 
                    distance
                )
            );

        var intersection = new GrPovRayIntersection(planeList);
        
        intersection.Objects.Add(
            GrPovRayPlane.CreateFromNormalDistance(
                LinFloat64Vector3D.NegativeE3, 
                yMin
            )
        );

        return intersection;
    }

    public static GrPovRayIntersection RegularPolygonCylinder(int sideCount, double radius, double yMin, double yMax)
    {
        var polygonSpecs = Float64RegularPolygon2D.CreateFromOuterRadius(sideCount, radius);
        var distance = polygonSpecs.InnerRadius;

        var planeList =
            polygonSpecs.SideUnitNormals.Select(normal => 
                GrPovRayPlane.CreateFromNormalDistance(
                    normal.ToXyLinVector3D(), 
                    distance
                )
            );

        var intersection = new GrPovRayIntersection(planeList);
        
        intersection.Objects.Add(
            GrPovRayPlane.CreateFromNormalDistance(
                LinFloat64Vector3D.NegativeE3, 
                yMin
            )
        );

        intersection.Objects.Add(
            GrPovRayPlane.CreateFromNormalDistance(
                LinFloat64Vector3D.E3, 
                yMax
            )
        );
        
        return intersection;
    }


    public static GrPovRayQuadric QuadricEllipsoid(double a, double b, double c)
    {
        return GrPovRayQuadric.Ellipsoid(a, b, c);
    }

    public static GrPovRayQuadric QuadricEllipticParaboloid(double a, double b)
    {
        return GrPovRayQuadric.EllipticParaboloid(a, b);
    }

    public static GrPovRayQuadric QuadricHyperbolicParaboloid(double a, double b)
    {
        return GrPovRayQuadric.HyperbolicParaboloid(a, b);
    }

    public static GrPovRayQuadric QuadricHyperbolicHyperboloid(double a, double b, double c)
    {
        return GrPovRayQuadric.HyperbolicHyperboloid(a, b, c);
    }

    public static GrPovRayQuadric QuadricEllipticHyperboloid(double a, double b, double c)
    {
        return GrPovRayQuadric.EllipticHyperboloid(a, b, c);
    }

    public static GrPovRayQuadric QuadricEllipticCone(double a, double b, double c)
    {
        return GrPovRayQuadric.EllipticCone(a, b, c);
    }

    public static GrPovRayQuadric QuadricEllipticCylinder(double a, double b)
    {
        return GrPovRayQuadric.EllipticCylinder(a, b);
    }

    public static GrPovRayQuadric QuadricHyperbolicCylinder(double a, double b)
    {
        return GrPovRayQuadric.HyperbolicCylinder(a, b);
    }

    public static GrPovRayQuadric QuadricParabolicCylinder(double a)
    {
        return GrPovRayQuadric.ParabolicCylinder(a);
    }

    public static GrPovRayPolynomialSurface PolynomialSurface(int order)
    {
        return new GrPovRayPolynomialSurface(order);
    }

    
    public static GrPovRayIntersection Intersection(params IGrPovRaySolidObject[] objectList)
    {
        var intersection = new GrPovRayIntersection();

        foreach (var obj in objectList) 
            intersection.Objects.Add(obj);
        
        return intersection;
    } 
    
    public static GrPovRayIntersection Intersection(IEnumerable<IGrPovRaySolidObject> objectList)
    {
        var intersection = new GrPovRayIntersection();

        foreach (var obj in objectList) 
            intersection.Objects.Add(obj);
        
        return intersection;
    } 

    public static GrPovRayDifference Difference(params IGrPovRaySolidObject[] objectList)
    {
        var difference = new GrPovRayDifference();

        foreach (var obj in objectList) 
            difference.Objects.Add(obj);
        
        return difference;
    } 
    
    public static GrPovRayDifference Difference(IEnumerable<IGrPovRaySolidObject> objectList)
    {
        var difference = new GrPovRayDifference();

        foreach (var obj in objectList) 
            difference.Objects.Add(obj);
        
        return difference;
    } 

    public static GrPovRayMerge Merge(params IGrPovRaySolidObject[] objectList)
    {
        var merge = new GrPovRayMerge();

        foreach (var obj in objectList) 
            merge.Objects.Add(obj);
        
        return merge;
    } 
    
    public static GrPovRayMerge Merge(IEnumerable<IGrPovRaySolidObject> objectList)
    {
        var merge = new GrPovRayMerge();

        foreach (var obj in objectList) 
            merge.Objects.Add(obj);
        
        return merge;
    } 

    public static GrPovRayUnion Union(params IGrPovRayObject[] objectList)
    {
        var union = new GrPovRayUnion();

        foreach (var obj in objectList) 
            union.Objects.Add(obj);
        
        return union;
    } 
    
    public static GrPovRayUnion Union(IEnumerable<IGrPovRayObject> objectList)
    {
        var union = new GrPovRayUnion();

        foreach (var obj in objectList) 
            union.Objects.Add(obj);
        
        return union;
    } 

    public static GrPovRayUnion SplitUnion(params IGrPovRayObject[] objectList)
    {
        var union = new GrPovRayUnion() { SplitUnion = true };

        foreach (var obj in objectList) 
            union.Objects.Add(obj);
        
        return union;
    }
    
    public static GrPovRayUnion SplitUnion(IEnumerable<IGrPovRayObject> objectList)
    {
        var union = new GrPovRayUnion() { SplitUnion = true };

        foreach (var obj in objectList) 
            union.Objects.Add(obj);
        
        return union;
    }



    public GrPovRayObjectProperties Properties { get; protected set; }
        = new GrPovRayObjectProperties();

    public GrPovRayMaterial? Material { get; set; }

    public Float64AffineMap3D AffineMap { get; private set; } 
        = Float64AffineMap3D.Create();

    public IFloat64AffineMap3D Transform 
        => AffineMap;

    //public GrPovRayTransformList Transforms { get; } 
    //    = new GrPovRayTransformList();

    
    public GrPovRayObject SetMaterial(GrPovRayMaterial material)
    {
        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(string baseMaterialName)
    {
        Material = GrPovRayMaterial.Create(baseMaterialName);

        return this;
    }

    public GrPovRayObject SetMaterial(GrPovRayColorValue pigmentColor)
    {
        var material = GrPovRayMaterial.Create(
            pigmentColor
        );

        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(GrPovRayColorValue pigmentColor, IGrPovRayFinish finish)
    {
        var material = GrPovRayMaterial.Create(
            pigmentColor, 
            finish
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(GrPovRayColorValue pigmentColor, GrPovRayFinishProperties finishProperties)
    {
        var material = GrPovRayMaterial.Create(
            pigmentColor, 
            finishProperties
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(GrPovRayColorValue pigmentColor, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = GrPovRayMaterial.Create(
            pigmentColor, 
            baseFinishName, 
            finishProperties
        );

        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(IGrPovRayPigment pigment)
    {
        var material = GrPovRayMaterial.Create(
            pigment
        );

        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(IGrPovRayPigment pigment, IGrPovRayFinish finish)
    {
        var material = GrPovRayMaterial.Create(
            pigment, 
            finish
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(IGrPovRayPigment pigment, GrPovRayFinishProperties finishProperties)
    {
        var material = GrPovRayMaterial.Create(
            pigment, 
            finishProperties
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(IGrPovRayPigment pigment, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = GrPovRayMaterial.Create(
            pigment, 
            baseFinishName, 
            finishProperties
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(string baseMaterialName, GrPovRayColorValue pigmentColor)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigmentColor
        );

        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(string baseMaterialName, GrPovRayColorValue pigmentColor, IGrPovRayFinish finish)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigmentColor, 
            finish
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(string baseMaterialName, GrPovRayColorValue pigmentColor, GrPovRayFinishProperties finishProperties)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigmentColor, 
            finishProperties
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(string baseMaterialName, GrPovRayColorValue pigmentColor, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigmentColor, 
            baseFinishName, 
            finishProperties
        );

        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(string baseMaterialName, IGrPovRayPigment pigment)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigment
        );

        Material = material;

        return this;
    }
    
    public GrPovRayObject SetMaterial(string baseMaterialName, IGrPovRayPigment pigment, IGrPovRayFinish finish)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigment, 
            finish
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(string baseMaterialName, IGrPovRayPigment pigment, GrPovRayFinishProperties finishProperties)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigment, 
            finishProperties
        );

        Material = material;

        return this;
    }

    public GrPovRayObject SetMaterial(string baseMaterialName, IGrPovRayPigment pigment, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = GrPovRayMaterial.Create(
            baseMaterialName, 
            pigment, 
            baseFinishName, 
            finishProperties
        );

        Material = material;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ResetAffineMap()
    {
        AffineMap = Float64AffineMap3D.Create();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject SetAffineMap(Float64AffineMap3D affineMap)
    {
        AffineMap = affineMap;

        return this;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject TranslateX(double d)
    {
        AffineMap.TranslateX(d);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject TranslateY(double d)
    {
        AffineMap.TranslateY(d);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject TranslateZ(double d)
    {
        AffineMap.TranslateZ(d);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Translate(double dx, double dy, double dz)
    {
        AffineMap.Translate(dx, dy, dz);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Translate(ITriplet<Float64Scalar> dv)
    {
        AffineMap.Translate(dv);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Scale(double sx, double sy, double sz)
    {
        AffineMap.Scale(sx, sy, sz);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Scale(double s)
    {
        AffineMap.Scale(s);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectXy()
    {
        AffineMap.ReflectXy();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectYz()
    {
        AffineMap.ReflectYz();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectZx()
    {
        AffineMap.ReflectZx();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectX()
    {
        AffineMap.ReflectX();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectY()
    {
        AffineMap.ReflectY();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectZ()
    {
        AffineMap.ReflectZ();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject ReflectOrigin()
    {
        AffineMap.ReflectOrigin();

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject MapUsing(Float64AffineMap3D map)
    {
        AffineMap.Transform(map);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject MapUsing(IFloat64AffineMap3D map)
    {
        AffineMap.Transform(map);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject RotateX(LinFloat64Angle angle)
    {
        AffineMap.RotateX(angle);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject RotateY(LinFloat64Angle angle)
    {
        AffineMap.RotateY(angle);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject RotateZ(LinFloat64Angle angle)
    {
        AffineMap.RotateZ(angle);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Rotate(ILinFloat64Vector3D unitAxis, LinFloat64Angle angle)
    {
        AffineMap.Rotate(unitAxis, angle);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Rotate(LinFloat64Quaternion quaternion)
    {
        AffineMap.Rotate(quaternion);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Rotate(LinBasisVector3D srcUnitVector, LinBasisVector3D dstUnitVector)
    {
        AffineMap.Rotate(srcUnitVector, dstUnitVector);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Rotate(ILinFloat64Vector3D srcUnitVector, ILinFloat64Vector3D dstUnitVector)
    {
        AffineMap.Rotate(srcUnitVector, dstUnitVector);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Rotate(LinBasisVectorPair3D srcVectorPair, LinBasisVectorPair3D dstVectorPair)
    {
        AffineMap.Rotate(srcVectorPair, dstVectorPair);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrPovRayObject Rotate(LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
    {
        AffineMap.Rotate(srcVectorPair, dstVector1, dstVector2);

        return this;
    }
    

    public virtual bool IsEmptyCodeElement()
    {
        return false;
    }

    protected virtual string GetModifiersCode()
    {
        var composer = new LinearTextComposer();

        composer.AppendAtNewLine(Properties.GetPovRayCode());

        if (Material is not null && !Material.IsEmptyCodeElement())
            composer.AppendAtNewLine(Material.GetPovRayCode());

        if (!Transform.IsNearIdentity()) 
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());
        
        return composer.ToString();
    }

    public abstract string GetPovRayCode();

    public override string ToString()
    {
        return GetPovRayCode();
    }
}