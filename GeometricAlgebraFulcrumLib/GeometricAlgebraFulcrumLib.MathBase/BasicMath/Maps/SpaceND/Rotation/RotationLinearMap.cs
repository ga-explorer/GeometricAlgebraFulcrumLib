﻿//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Reflection;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Rotation
//{
//    public abstract class RotationLinearMap :
//        ReflectionLinearMap
//    {
//        public override bool SwapsHandedness 
//            => false;
    
//        public abstract RotationLinearMap GetVectorRotationInverse();

    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ReflectionLinearMap GetReflectionLinearMapInverse()
//        {
//            return GetVectorRotationInverse();
//        }

//        public abstract VectorToVectorRotationSequence ToVectorToVectorRotationSequence();

//        //public abstract SimpleRotationSequence ToSimpleVectorRotationSequence();
//    }
//}