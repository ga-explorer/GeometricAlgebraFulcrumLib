using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(double centerX, double centerY)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(centerX, centerY),
            Encode.Scalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(double centerX, double centerY, double centerZ)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(centerX, centerY, centerZ),
            Encode.Scalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(params double[] center)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(LinFloat64Vector center)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(LinFloat64Vector3D center, double direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(double weight, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            this,
            weight,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(double weight, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            this,
            weight,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(double weight, LinFloat64Vector center)
    {
        return new CGaFloat64Round(
            this,
            weight,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPoint(double weight, LinFloat64Vector3D center, double direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            0,
            Encode.VGa.Vector(center),
            Encode.Scalar(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double radiusSquared, double[] center, double[] direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double radiusSquared, LinFloat64Vector2D center, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double radiusSquared, LinFloat64Vector center, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double radiusSquared, XGaFloat64Vector center, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double weight, double radiusSquared, double[] center, double[] direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double weight, double radiusSquared, LinFloat64Vector2D center, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double weight, double radiusSquared, LinFloat64Vector center, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundPointPair(double weight, double radiusSquared, XGaFloat64Vector center, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle2D(double radiusSquared, double centerX, double centerY)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(centerX, centerY),
            Encode.VGa.Bivector(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle2D(double radiusSquared, double[] center)
    {
        if (center.Length != 2)
            throw new ArgumentException();

        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double radiusSquared, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double radiusSquared, LinFloat64Vector2D center, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle2D(double weight, double radiusSquared, double centerX, double centerY)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(centerX, centerY),
            Encode.VGa.Bivector(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle2D(double weight, double radiusSquared, double[] center)
    {
        if (center.Length != 2)
            throw new ArgumentException();

        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D normalDirection)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double weight, double radiusSquared, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double weight, double radiusSquared, LinFloat64Vector2D center, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle3D(double radiusSquared, double[] center, double[] normalDirection)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(normalDirection.ToLinVector3D().NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D normalDirection)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double radiusSquared, LinFloat64Vector3D center, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double radiusSquared, XGaFloat64Vector center, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle3D(double weight, double radiusSquared, double[] center, double[] normalDirection)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(normalDirection.ToLinVector3D().NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundCircle(double weight, double radiusSquared, XGaFloat64Vector center, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere3D(double radiusSquared, double[] center)
    {
        if (center.Length != 3)
            throw new ArgumentException();

        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Trivector(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere3D(double radiusSquared, double centerX, double centerY, double centerZ)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(centerX, centerY, centerZ),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere(double radiusSquared, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere(double radiusSquared, LinFloat64Vector3D center, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Trivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere3D(double weight, double radiusSquared, double[] center)
    {
        if (center.Length != 3)
            throw new ArgumentException();

        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Trivector(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere3D(double weight, double radiusSquared, double centerX, double centerY, double centerZ)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(centerX, centerY, centerZ),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere(double weight, double radiusSquared, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundSphere(double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Round(double radiusSquared, XGaFloat64Vector center, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Round(double radiusSquared, CGaFloat64Blade center, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Round(double weight, double radiusSquared, XGaFloat64Vector center, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Round(double weight, double radiusSquared, CGaFloat64Blade center, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            center,
            direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromRowVectors(double radiusSquared, LinFloat64Vector2D center, double[,] directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .RowsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromColumnVectors(double radiusSquared, LinFloat64Vector2D center, double[,] directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .ColumnsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double radiusSquared, LinFloat64Vector2D center, params LinFloat64Vector2D[] directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double radiusSquared, LinFloat64Vector3D center, params LinFloat64Vector3D[] directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double radiusSquared, LinFloat64Vector center, params LinFloat64Vector[] directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double radiusSquared, XGaFloat64Vector center, params XGaFloat64Vector[] directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            center,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double radiusSquared, XGaFloat64Vector center, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Round(
            1,
            radiusSquared,
            center,
            directionVectors.Op(Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromRowVectors(double weight, double radiusSquared, LinFloat64Vector2D center, double[,] directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .RowsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromColumnVectors(double weight, double radiusSquared, LinFloat64Vector2D center, double[,] directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .ColumnsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double weight, double radiusSquared, LinFloat64Vector2D center, params LinFloat64Vector2D[] directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double weight, double radiusSquared, LinFloat64Vector3D center, params LinFloat64Vector3D[] directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double weight, double radiusSquared, LinFloat64Vector center, params LinFloat64Vector[] directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double weight, double radiusSquared, XGaFloat64Vector center, params XGaFloat64Vector[] directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromVectors(double weight, double radiusSquared, XGaFloat64Vector center, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Round(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromRowPoints(double[,] egaPoints)
    {
        var kVector =
            egaPoints.RowsToXGaVectors().Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromColumnPoints(double[,] egaPoints)
    {
        var kVector =
            egaPoints.ColumnsToXGaVectors().Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(params LinFloat64Vector2D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(params LinFloat64Vector3D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(params LinFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(params XGaFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(IEnumerable<XGaFloat64Vector> egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromRowPoints(double weight, double[,] egaPoints)
    {
        var kVector =
            weight * egaPoints.RowsToXGaVectors().Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromColumnPoints(double weight, double[,] egaPoints)
    {
        var kVector =
            weight * egaPoints.ColumnsToXGaVectors().Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(double weight, params LinFloat64Vector2D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(double weight, params LinFloat64Vector3D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(double weight, params LinFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(double weight, params XGaFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RoundFromPoints(double weight, IEnumerable<XGaFloat64Vector> egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                Encode.IpnsRound.Point(p).InternalVector
            ).Op(Processor);

        return kVector.EncodeVGaBlade(this).DecodeOpnsRound.Element();
    }

}