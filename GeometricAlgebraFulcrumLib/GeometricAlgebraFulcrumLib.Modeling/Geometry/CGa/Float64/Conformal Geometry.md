# Conforermal Geometry

Under this namespace you can find useful classes for defining and manipulating CGA geometric elements.

The first thing to do is to create a conformal space object of desired dimensions (in the range 4 to 63):

```c#
// Create a 5-dimensional CGA space
var cga = XGaConformalSpace.Create(5);
```

Internally, this code defines 5 orthonormal basis blades $e_{-},e_{+},e_{1},e_{2},e_{3}$ with $e_{-}^2=-1$, $e_{+}^2=e_{i}^2=1$ for $i \in \{ 1,2,3 \}$, and $e_{r} \cdot e_{s}=0$ for $r,s \in \{-,+,1,2,3\}$ and $r \neq s$. All computations on multivectors are made using this basis set, while the final results are displayed using the more common basis $e_{o},e_{1},e_{2},e_{3},e_{\infty}$ with $e_{o} = \frac{1}{2}e_{-}+\frac{1}{2}e_{+}$, $e_{\infty}=e_{-}-e_{+}$.

## Elements

Using the CGA space object, you can now define CGA geometry elements. A CGA element is not initially encoded as a blade, but rather is a simple structure holding Euclidean information about the components of the element. Any CGA element contains a weight $w$, a direction $D$, and a position component $P$. The element weight is a positive number, its direction is a unit blade encoding a Euclidean subspace, and its position is a point in Euclidean space. Additionally, the round CGA elements have a squared radius component, which can be any finite real number.

The Euclidean components of a CGA element can be accessed using the properties `Weight`, `Direction`, `Position`, and `RadiusSquared`. Additional properties exists for an element to get more information on the element such as `NormalDirection`, and `RealRadius`.

### Directions

A direction $\{w,D\}$ is the simplest CGA element with position at the Euclidean origin. You can define directions in 5-dimensional CGA space as shown in this code snippet:

```c#
// Create a 5-dimensional CGA space
var cga = XGaConformalSpace.Create(5);

// Define a weighted direction from a scalar, 
// only the sign of the scalar is used
// This is a kind of "signed point at the origin" 
// or "signed 0D subspace" geometry
var d0 = cga.DefineDirectionScalar(
    2.1,
    -3.4
);

// Define a weighted 1D direction based on a 
// 3D vector. Internally, the vector is 
// normalized to a unit vector
var d1 = cga.DefineDirectionLine(
    2.1,
    Float64Vector3D.Create(-1, -1.2, -3)
);

// Define a weighted 2D direction from a 3D bivector
var d2 = cga.DefineDirectionPlane(
    2.1,
    Float64Bivector3D.Create(-1.4, -1.3, -2.1)
);

// Define a weighted 3D direction from a 3D trivector.
// only the sign of the scalar is used here.
var d3 = cga.DefineDirectionVolume(
    2.1,
    Float64Trivector3D.Create(-3.4)
);
```

### Tangents and Flats

A tangent element $\{w,D, P\}$ is like a direction, but can have any Euclidean position $P$. We can use similiar code to define tangent elements:

```c#
// Define a weighted 2D tangent plane based on a 3D 
// direction bivector and a 3D position.
// Internally, the bivector is normalized to a 
// unit bivector
var t2 = cga.DefineTangentPlane(
    2.1,
    Float64Vector3D.Create(-1.3, -2.1, -1.1),
    Float64Bivector3D.Create(-1.4, -1.3, -2.1)
);
```

The same can be done with flat elements. Although flats have different geometry from tangents, they share the same basic Euclidean components.

```c#
// Define a weighted 2D flat plane based on a 3D 
// direction bivector and a 3D position.
// Internally, the bivector is normalized to a 
// unit bivector
var f2 = cga.DefineFlatPlane(
    2.1,
    Float64Vector3D.Create(-1.3, -2.1, -1.1),
    Float64Bivector3D.Create(-1.4, -1.3, -2.1)
);
```

Some additional methods are added to flat elements to perform some useful queries and operations. For example, we can use the following code to test if a point belongs to the surface of a given flat:

```c#
if (f2.SurfaceNearContainsPoint(1.2, 2,4, -1.2)) ...
```

Also, we can use this code to get defining points on the surface of the flat:

```c#
var surfacePoints = f2.GetSurfacePointVectors3D();
```

### Rounds

CGA rounds include point pairs, circles, sphere, and hyper-spheres of higher dimensions. A round element $\{w,D, P, \rho \}$ is essentially a real ($\rho>0$) or imaginary ($\rho<0$) hyper-sphere with given radius $r=\sqrt{\left|\rho\right|}$ intersected with some Euclidean direction subspace $D$, and translated to an arbitrary center $P$. We can, for example, define a weighted imaginary circle with radius 3 using this code:

```c#
// Define an imaginary weighted circle based on a 3D 
// direction bivector and a 3D center position.
// Internally, the bivector is normalized to a 
// unit bivector
var r2 = cga.DefineRoundCircle(
    2.1, // weight
    -9,  // squared radius
    Float64Vector3D.Create(-1.3, -2.1, -1.1), // center
    Float64Bivector3D.Create(-1.4, -1.3, -2.1) // direction
);
```

Rounds have surfaces, so they have additional helping methods, like `SurfaceNearContainsPoint()` and `GetSurfacePointVectors3D()`, similar to flats.

## Blades

The power of GA comes from the unified encoding of geometric objects, as blades, and orthogonal maps, as versors. In the current implementation, there are several classes of blades that can encode geometric CGA elements:

* EGA blades are defined using only the $n$ Euclidean basis vectors $\{ e_{1}, e_{2}, \ldots, e_{n} \}$. These can only encode Euclidean subspaces and, by some geometric misuse, Euclidean points. The direction and position components of elements are actually stored as EGA blades.
* PGA blades are defined using $n+1$ projective basis vectors $\{ e_{o}, e_{1}, e_{2}, \ldots, e_{n} \}$. These blades can be used to represent flats using PGA.
* CGA OPNS\IPNS blades are the standard direct\dual representations of CGA elements, which may use the full set of $n+2$ CGA basis vectors $\{ e_{o}, e_{1}, e_{2}, \ldots, e_{n}, e_{\infty} \}$.

While directions can be encoded as EGA, PGA, OPNS, or IPNS blades, flats can be encoded only using PGA, OPNS, or IPNS blades. Furthermore, tangents and rounds can only be encoded as OPNS\IPNS blades.

The `CGaFloat64Blade` class represents CGA blades. This class contains most standard GA operations such as reverse, grade involution, addition\subtraction, geometric, outer, and other products, dual\undual, norm, etc. Additionally, there are some methods in the `CGaFloat64Blade` class to convert a blade from one encoding to another such as `OpnsToIpns()`, `IpnsToOpns()`, `OpnsToPGa()`, `PGaToOpns()`, etc.

After initially defining CGA elements for some geometric computation, we typically need to encode them into suitable blades and perform standard GA operations to get new blades. We can then decode the output blades back to CGA elements to read their properties and components, and to visualize them if needed.

For this purpose, all CGA elements contain the two methods `EncodeOpnsBlade()` and `EncodeIpnsBlade()` for encoding the element as an OPNS\IPNS blade (i.e. a blade directly\dually representing the element in CGA basis). Additionally, direction and flat elements can be encoded as PGA blades using the `EncodePGaBlade()` method. The direction and position associated with any CGA element are already encoded as a EGA blades.

For example, we can use this simple code to intersect a sphere with a plane and get the final element:

```c#
var cga = XGaConformalSpace.Create(5);

// Define a plane in 3-dimensions using distance from origin and normal vector
var plane = cga.DefineFlatPlane(
    3,
    Float64Vector3D.Create(1, 2, -1)
);

// Define a real sphere with radius 5
var sphere = cga.DefineRealRoundSphere(
    5,
    Float64Vector3D.Create(1, 1, 1)
);

// Get the IPNS blades for the plane and sphere
var planeBlade = plane.EncodeIpnsBlade();
var sphereBlade = sphere.EncodeIpnsBlade();

// Compute the IPNS blade of intersection using the outer product
var intersectionBlade = sphereBlade.Op(planeBlade);

// Decode the blade of intersection into a CGA element
var intersectionElement = intersectionBlade.DecodeIpnsElement();
```

Note that this is only an illustrative example, there is a simpler way for intersecting CGA elements using a single equivalent function call:

```c#
var intersectionElement = plane.Intersect(sphere);
```

It is also possible to encode CGA blades directly from Eucludean components without defining elements using code similar to:

```c#
var cga = XGaConformalSpace.Create(5);

// Encode a plane in 3-dimensions as a IPNS blade
var planeBlade = cga.EncodeIpnsFlatPlane(
    3,
    Float64Vector3D.Create(1, 2, -1)
);

// Encode a real sphere with radius 5 as a IPNS blade
var sphereBlade = cga.EncodeIpnsRealRoundSphere(
    5,
    Float64Vector3D.Create(1, 1, 1)
);

// Compute the IPNS blade of intersection using the outer product
var intersectionBlade = sphereBlade.Op(planeBlade);
```

## Versors

### EGA Versors

### PGA Versors

### CGA Versors

## Operations

### Intersection

### Reflection

### Projection

### Interpolation

### Orthogonal Maps

## Visualizations

You can find example 2D\3D visualizations in [here](https://drive.google.com/drive/folders/1qdWBOg6FvsyDGaeJTTxeL5EPt0Tswaje).

### Visualizing Static 2D\3D Elements

### Visualizing Parametric 2D\3D Elements
