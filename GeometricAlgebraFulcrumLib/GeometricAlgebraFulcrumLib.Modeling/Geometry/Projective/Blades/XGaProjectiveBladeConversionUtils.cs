using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;

public static class XGaProjectiveBladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> ToProjectiveBlade<T>(this XGaKVector<T> cgaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> ScalarPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> VectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaMultivector.GetVectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> VectorPartToProjectiveEGaBlade<T>(this XGaMultivector<T> cgaMultivector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaMultivector.GetVectorPart().GetVectorPart(i => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> BivectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaMultivector.GetBivectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> KVectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, int grade, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaMultivector.GetKVectorPart(grade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> FirstKVectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            cgaMultivector.GetFirstKVectorPart()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> GetEGaVectorPart<T>(this XGaProjectiveBlade<T> vector)
    {
        Debug.Assert(vector.IsVector);

        return new XGaProjectiveBlade<T>(
            vector.ProjectiveSpace,
            vector.InternalVector.GetVectorPart(i => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EGaVectorToPGaPoint<T>(this XGaProjectiveBlade<T> blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );
        
        var projectiveSpace = blade.ProjectiveSpace;

        return (projectiveSpace.Eo + blade).PGaDual();
    }

}