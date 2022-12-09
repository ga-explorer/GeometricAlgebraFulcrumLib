using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TextComposerLib.Text;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable;

public sealed record Float64Tuple :
    IGeometricElement,
    IReadOnlyList<double>
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Vector<double>(Float64Tuple tuple)
    //{
    //    return Vector<double>.Build.DenseOfArray(tuple.ScalarArray);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Float64Tuple(Vector<double> vector)
    //{
    //    return new Float64Tuple(vector.ToArray());
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static explicit operator double[](Float64Tuple tuple)
    //{
    //    return tuple.ScalarArray;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static explicit operator Float64Tuple(double[] vector)
    //{
    //    return new Float64Tuple(vector);
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple operator -(Float64Tuple tuple1)
    {
        return new Float64Tuple(
            tuple1.ScalarArray.VectorNegative()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple operator +(Float64Tuple tuple1, Float64Tuple tuple2)
    {
        var v1 = tuple1.ScalarArray;
        var v2 = tuple2.ScalarArray;

        return new Float64Tuple(
            v1.VectorAdd(v2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple operator -(Float64Tuple tuple1, Float64Tuple tuple2)
    {
        var v1 = tuple1.ScalarArray;
        var v2 = tuple2.ScalarArray;

        return new Float64Tuple(
            v1.VectorSubtract(v2)
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple operator *(Float64Tuple tuple1, double scalar)
    {
        var v1 = tuple1.ScalarArray;

        return new Float64Tuple(
            v1.VectorTimes(scalar)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple operator *(double scalar, Float64Tuple tuple1)
    {
        var v1 = tuple1.ScalarArray;

        return new Float64Tuple(
            v1.VectorTimes(scalar)
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple operator /(Float64Tuple tuple1, double scalar)
    {
        var v1 = tuple1.ScalarArray;

        return new Float64Tuple(
            v1.VectorTimes(1d / scalar)
        );
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64Tuple(double item0)
    //{
    //    Debug.Assert(item0.IsNotNaN());

    //    MathNetVector = Vector.Build.DenseOfArray(new []
    //    {
    //        item0
    //    });
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(double item0)
    {
        var vector = new []
        {
            item0
        };

        return new Float64Tuple(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(double item0, double item1)
    {
        var vector = new []
        {
            item0,
            item1
        };

        return new Float64Tuple(vector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(params double[] scalarArray)
    {
        return new Float64Tuple(scalarArray);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateCopy(double[] scalarArray)
    {
        var scalarArray1 = new double[scalarArray.Length];
        scalarArray.CopyTo(scalarArray1, 0);

        return new Float64Tuple(scalarArray1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateCopy(Float64Tuple tuple)
    {
        return CreateCopy(tuple.ScalarArray);
    }
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateCopy(IReadOnlyList<double> tuple)
    {
        var scalarArray = new double[tuple.Count];

        for (var i = 0; i < scalarArray.Length; i++)
            scalarArray[i] = tuple[i];

        return new Float64Tuple(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(IEnumerable<double> scalarList)
    {
        var scalarArray = scalarList.ToArray();

        return new Float64Tuple(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(IPair<double> tuple)
    {
        var scalarArray = tuple.GetItemArray();

        return new Float64Tuple(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(ITriplet<double> tuple)
    {
        var scalarArray = tuple.GetItemArray();

        return new Float64Tuple(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(IQuad<double> tuple)
    {
        var scalarArray = tuple.GetItemArray();

        return new Float64Tuple(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(IQuint<double> tuple)
    {
        var scalarArray = tuple.GetItemArray();

        return new Float64Tuple(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple Create(IHexad<double> tuple)
    {
        var scalarArray = tuple.GetItemArray();

        return new Float64Tuple(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateFromRgba(Color color)
    {
        var tuple = color.ToPixel<Rgba32>();
        
        var scalarArray = new []
        {
            tuple.R / 255d,
            tuple.G / 255d,
            tuple.B / 255d,
            tuple.A / 255d
        };

        return new Float64Tuple(scalarArray);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateZero(int dimensions)
    {
        return new Float64Tuple(new double[dimensions]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateBasis(int dimensions, int index)
    {
        var scalarArray = new double[dimensions];
        scalarArray[index] = 1d;

        return new Float64Tuple(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Tuple CreateScaledBasis(int dimensions, int index, double scalar)
    {
        var scalarArray = new double[dimensions];
        scalarArray[index] = scalar;

        return new Float64Tuple(scalarArray);
    }


    internal double[] ScalarArray { get; private set; }


    public int Dimensions 
        => ScalarArray.Length;

    public int Count 
        => ScalarArray.Length;

    public double this[int index]
    {
        get => ScalarArray[index];
        //set
        //{
        //    Debug.Assert(value.IsNotNaN());

        //    ScalarArray[index] = value;
        //}
    }

    public double X
    {
        get => this[0];
        //set => this[0] = value;
    }
        
    public double Y
    {
        get => this[1];
        //set => this[1] = value;
    }
        
    public double Z
    {
        get => this[2];
        //set => this[2] = value;
    }
        
    public double W
    {
        get => this[3];
        //set => this[3] = value;
    }

    public IEnumerable<int> Keys 
        => ScalarArray.Length.GetRange();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Tuple(double[] scalarArray)
    {
        ScalarArray = scalarArray;

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ScalarArray.All(s => s.IsNotNaN());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return ScalarArray.All(s => s == 0d);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1e-12)
    {
        return GetVectorNorm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnit(double epsilon = 1e-12)
    {
        return GetVectorNormSquared().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearOrthonormalWith(Float64Tuple vector, double epsilon = 1e-12)
    {
        return ScalarArray.IsVectorNearOrthonormalWith(vector.ScalarArray, epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearOrthonormalWithUnit(Float64Tuple vector, double epsilon = 1e-12)
    {
        return ScalarArray.IsVectorNearOrthonormalWithUnit(vector.ScalarArray, epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearParallelTo(Float64Tuple vector, double epsilon = 1e-12)
    {
        return ScalarArray.IsVectorNearParallelTo(vector.ScalarArray, epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearParallelToUnit(Float64Tuple vector, double epsilon = 1e-12)
    {
        return ScalarArray.IsVectorNearParallelToUnit(vector.ScalarArray, epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearOrthogonalTo(Float64Tuple vector, double epsilon = 1e-12)
    {
        return ScalarArray.IsVectorNearOrthogonalTo(vector.ScalarArray, epsilon);
    }

    public bool IsVectorBasis(int basisIndex)
    {
        for (var i = 0; i < basisIndex; i++)
            if (!ScalarArray[i].IsExactZero())
                return false;

        if (!ScalarArray[basisIndex].IsExactOne())
            return false;

        for (var i = basisIndex + 1; i < Dimensions; i++)
            if (!ScalarArray[i].IsExactZero())
                return false;

        return true;
    }
    
    public bool IsNearVectorBasis(int basisIndex, double epsilon = 1e-12d)
    {
        var d = 0d;

        for (var i = 0; i < basisIndex; i++)
            d += ScalarArray[i].Square();

        d += (ScalarArray[basisIndex] - 1d).Square();

        for (var i = basisIndex + 1; i < Dimensions; i++)
            d += ScalarArray[i].Square();

        return d.IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple Clear()
    {
        ScalarArray = new double[ScalarArray.Length];

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple2D ToTuple2D()
    {
        return new Float64Tuple2D(
            ScalarArray[0], 
            ScalarArray[1]
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple3D ToTuple3D()
    {
        return new Float64Tuple3D(
            ScalarArray[0], 
            ScalarArray[1], 
            ScalarArray[2]
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple4D ToTuple4D()
    {
        return new Float64Tuple4D(
            ScalarArray[0], 
            ScalarArray[1], 
            ScalarArray[2], 
            ScalarArray[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color ToColorRgba()
    {
        Debug.Assert(
            ScalarArray[0] >= 0d && ScalarArray[0] <= 1d &&
            ScalarArray[1] >= 0d && ScalarArray[1] <= 1d &&
            ScalarArray[2] >= 0d && ScalarArray[2] <= 1d &&
            ScalarArray[3] >= 0d && ScalarArray[3] <= 1d
        );

        return Color.FromRgba(
            (byte) (ScalarArray[0] * 255),
            (byte) (ScalarArray[1] * 255),
            (byte) (ScalarArray[2] * 255),
            (byte) (ScalarArray[3] * 255)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color ToColorRgb()
    {
        Debug.Assert(
            ScalarArray[0] >= 0d && ScalarArray[0] <= 1d &&
            ScalarArray[1] >= 0d && ScalarArray[1] <= 1d &&
            ScalarArray[2] >= 0d && ScalarArray[2] <= 1d
        );

        return Color.FromRgb(
            (byte) (ScalarArray[0] * 255),
            (byte) (ScalarArray[1] * 255),
            (byte) (ScalarArray[2] * 255)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetScalarArrayCopy()
    {
        return ScalarArray.GetShallowCopy();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple InPlaceMap(Func<double, double> scalarMapping)
    {
        for (var i = 0; i < ScalarArray.Length; i++)
            ScalarArray[i] = scalarMapping(ScalarArray[i]);
        
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple InPlaceMap(Func<int, double, double> scalarMapping)
    {
        for (var i = 0; i < ScalarArray.Length; i++)
            ScalarArray[i] = scalarMapping(i, ScalarArray[i]);

        return this;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64DenseTuple InPlaceAdd(Float64Tuple tuple)
    //{
    //    for (var index = 0; index < tuple._itemArray.Length; index++)
    //        this[index] += tuple._itemArray[index];
    //    MathNetVector.MapIndexedInplace();
    //    return this;
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Float64DenseTuple InPlaceSubtract(Float64DenseTuple tuple)
    //{
    //    for (var index = 0; index < tuple._itemArray.Length; index++)
    //        this[index] -= tuple._itemArray[index];

    //    return this;
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple InPlaceNegative()
    {
        for (var i = 0; i < ScalarArray.Length; i++)
            ScalarArray[i] = -ScalarArray[i];

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple InPlaceScale(double scalar)
    {
        Debug.Assert(scalar.IsNotNaN());

        for (var i = 0; i < ScalarArray.Length; i++)
            ScalarArray[i] *= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple InPlaceNormalize()
    {
        var length = GetVectorNorm();

        return length == 0 ? this : InPlaceScale(1d / length);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetComponent(Axis axis)
    {
        var component = this[axis.Index];

        return axis.IsNegative ? -component : component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetComponent(int axisIndex, bool axisNegative = false)
    {
        var component = this[axisIndex];

        return axisNegative ? -component : component;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetVectorNormSquared()
    {
        return ScalarArray.Aggregate(
            0d, 
            (length, item) => length + item * item
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetVectorNorm()
    {
        return GetVectorNormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDistanceSquaredToPoint(Float64Tuple point)
    {
        var v = ScalarArray.VectorSubtract(point.ScalarArray);

        return v.VectorDot(v);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDistanceToPoint(Float64Tuple point)
    {
        return GetDistanceSquaredToPoint(point).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple ToUnitVector()
    {
        var length = 
            ScalarArray.GetVectorNorm();

        return length.IsExactZero()
            ? CreateZero(Dimensions)
            : this / length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double VectorDot(Float64Tuple tuple)
    {
        return ScalarArray.VectorDot(tuple.ScalarArray);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double VectorDot(Axis axis)
    {
        var d = ScalarArray[axis.Index];

        return axis.IsNegative ? -d : d;
    }

    public double GetAngleCos(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );

        var u = ScalarArray;
        var v = vector.ScalarArray;

        var uuDot = 0d;
        var vvDot = 0d;
        var uvDot = 0d;

        for (var i = 0; i < Dimensions; i++)
        {
            uuDot += u[i].Square();
            vvDot += v[i].Square();
            uvDot += u[i] * v[i];
        }

        var norm = (uuDot * vvDot).Sqrt();

        return norm.IsExactZero() 
            ? 0d 
            : (uvDot / norm).Clamp(-1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PlanarAngle GetAngle(Float64Tuple vector)
    {
        return GetAngleCos(vector).ArcCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple ProjectOnUnitVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions &&
            vector.IsNearUnit()
        );

        return VectorDot(vector) * vector;
    }
    
    public Float64Tuple ProjectOnVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );

        var u = vector.ScalarArray;
        var x = ScalarArray;

        var uuDot = 0d;
        var xuDot = 0d;

        for (var i = 0; i < Dimensions; i++)
        {
            uuDot += u[i].Square();
            xuDot += x[i] * u[i];
        }

        return (xuDot / uuDot) * vector;
    }
    
    public Float64Tuple RejectOnUnitVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );

        var x = ScalarArray;
        var u = vector.ScalarArray;

        var xuDot = 0d;

        for (var i = 0; i < Dimensions; i++) 
            xuDot += x[i] * u[i];

        var y = new double[Dimensions];

        for (var i = 0; i < Dimensions; i++)
            y[i] = x[i] - xuDot * u[i];

        return new Float64Tuple(y);
    }

    public Float64Tuple RejectOnVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );

        var x = ScalarArray;
        var u = vector.ScalarArray;

        var uuDot = 0d;
        var xuDot = 0d;

        for (var i = 0; i < Dimensions; i++)
        {
            uuDot += u[i].Square();
            xuDot += x[i] * u[i];
        }

        var s = xuDot / uuDot;
        var y = new double[Dimensions];

        for (var i = 0; i < Dimensions; i++)
            y[i] = x[i] - s * u[i];

        return new Float64Tuple(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple ReflectOnUnitVector(Float64Tuple vector)
    {
        var x = ScalarArray;
        var u = vector.ScalarArray;
        
        Debug.Assert(
            x.Length == u.Length &&
            u.GetVectorNormSquared().IsNearOne()
        );

        return new Float64Tuple(
            u.VectorTimes(2d * x.VectorDot(u)).VectorSubtract(x)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple ReflectOnUnitNormalHyperPlane(Float64Tuple unitNormal)
    {
        var x = ScalarArray;
        var u = unitNormal.ScalarArray;
        
        Debug.Assert(
            x.Length == u.Length &&
            u.VectorDot(u).IsNearOne()
        );

        return new Float64Tuple(
            x.VectorSubtract(u.VectorTimes(2d * x.VectorDot(u)))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple RotateToUnitVector(Float64Tuple unitVector, PlanarAngle angle)
    {
        var u = ScalarArray;
        var v = unitVector.ScalarArray;

        Debug.Assert(
            u.Length == v.Length &&
            u.GetVectorNormSquared().IsNearOne() &&
            v.GetVectorNormSquared().IsNearOne()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = v.VectorSubtract(u.VectorTimes(v.VectorDot(u)));
        var v1Length = v1.GetVectorNorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        v1.VectorTimesInPlace(angle.Sin() / v1Length);

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return new Float64Tuple(
            u.VectorTimes(angle.Cos()).VectorAddInPlace(v1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple RotateFromUnitVector(Float64Tuple unitVector, PlanarAngle angle)
    {
        var u = unitVector.ScalarArray;
        var v = ScalarArray;

        Debug.Assert(
            u.Length == v.Length &&
            u.GetVectorNormSquared().IsNearOne() &&
            v.GetVectorNormSquared().IsNearOne()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = v.VectorSubtract(u.VectorTimes(v.VectorDot(u)));
        var v1Length = v1.GetVectorNorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        v1.VectorTimesInPlace(angle.Sin() / v1Length);

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return new Float64Tuple(
            u.VectorTimes(angle.Cos()).VectorAddInPlace(v1)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        return ((IEnumerable<double>)ScalarArray).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return this
            .Select(s => s.ToString("G"))
            .Concatenate(", ", "(", ")");
    }
}