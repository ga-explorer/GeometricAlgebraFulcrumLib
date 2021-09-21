using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ConformalGeometryFactory
    {
        /// <summary>
        /// Convert a Euclidean position vector into a conformal null vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="positionVector"></param>
        /// <returns></returns>
        public static ConformalIpnsPoint<T> CreateIpnsPoint<T>(this GeometricAlgebraConformalProcessor<T> processor, params T[] positionVector)
        {
            if (positionVector.Length > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var composer = processor.CreateVectorStorageComposer();

            // Add Euclidean part
            composer.AddTerms(positionVector);

            var x2 = processor.Add(
                positionVector.Select(v => processor.Times(v, v))
            );

            // Add e_o part
            composer.AddTerm(
                processor.VSpaceDimension - 2,
                processor.Divide(processor.SubtractOne(x2), processor.ScalarTwo)
            );

            // Add e_i part
            composer.AddTerm(
                processor.VSpaceDimension - 1,
                processor.Divide(processor.AddOne(x2), processor.ScalarTwo)
            );

            return new ConformalIpnsPoint<T>(
                processor,
                composer.CreateVectorStorage()
            );
        }

        public static ConformalIpnsPoint<T> CreateIpnsPoint<T>(this GeometricAlgebraConformalProcessor<T> processor, Vector<T> positionVector)
        {
            if (positionVector.VectorStorage.MinVSpaceDimension > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var composer = processor.CreateVectorStorageComposer();

            // Add Euclidean part
            composer.AddTerms(positionVector.VectorStorage.GetIndexScalarRecords());

            var x2 = processor.Add(
                positionVector.VectorStorage.GetScalars().Select(v => processor.Times(v, v))
            );

            // Add e_o part
            composer.AddTerm(
                processor.VSpaceDimension - 2,
                processor.Divide(processor.SubtractOne(x2), processor.ScalarTwo)
            );

            // Add e_i part
            composer.AddTerm(
                processor.VSpaceDimension - 1,
                processor.Divide(processor.AddOne(x2), processor.ScalarTwo)
            );

            return new ConformalIpnsPoint<T>(
                processor,
                composer.CreateVectorStorage()
            );
        }
        
        public static ConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this GeometricAlgebraConformalProcessor<T> processor, Vector<T> centerPoint, T radiusSquared)
        {
            if (centerPoint.VectorStorage.MinVSpaceDimension > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var composer = processor.CreateVectorStorageComposer();

            // Add Euclidean part
            composer.AddTerms(centerPoint.VectorStorage.GetIndexScalarRecords());

            var x2 = processor.Subtract(
                processor.Add(
                    centerPoint.VectorStorage.GetScalars().Select(v => processor.Times(v, v))
                ),
                radiusSquared
            );

            // Add e_+ part
            composer.AddTerm(
                processor.VSpaceDimension - 2,
                processor.Divide(processor.SubtractOne(x2), processor.ScalarTwo)
            );

            // Add e_- part
            composer.AddTerm(
                processor.VSpaceDimension - 1,
                processor.Divide(processor.AddOne(x2), processor.ScalarTwo)
            );

            return new ConformalIpnsHyperSphere<T>(
                processor, 
                composer.CreateVectorStorage()
            );
        }
        
        public static ConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this GeometricAlgebraConformalProcessor<T> processor, Vector<T> normal, T delta)
        {
            if (normal.VectorStorage.MinVSpaceDimension > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var composer = processor.CreateVectorStorageComposer();

            // Add Euclidean part
            composer.AddTerms(normal.VectorStorage.GetIndexScalarRecords());
            
            // Add e_+ part
            composer.AddTerm(
                processor.VSpaceDimension - 2,
                delta
            );

            // Add e_- part
            composer.AddTerm(
                processor.VSpaceDimension - 1,
                delta
            );

            return new ConformalIpnsHyperPlane<T>(
                processor, 
                composer.CreateVectorStorage()
            );
        }


        public static ConformalOpnsRound<T> CreateOpnsRound<T>(this GeometricAlgebraConformalProcessor<T> processor, params Vector<T>[] points)
        {
            if (points.Length < 2 || points.Length > processor.VSpaceDimension - 1)
                throw new InvalidOperationException();

            var kVector = processor.Op(
                points.Select(p => processor.CreateIpnsPoint(p).VectorStorage)
            );

            return new ConformalOpnsRound<T>(processor, kVector);
        }

        public static ConformalOpnsHyperSphere<T> CreateOpnsHyperSphere<T>(this GeometricAlgebraConformalProcessor<T> processor, params Vector<T>[] points)
        {
            if (points.Length != processor.VSpaceDimension - 1)
                throw new InvalidOperationException();

            var kVector = processor.Op(
                processor.Op(
                    points.Select(p => processor.CreateIpnsPoint(p).VectorStorage)
                ), 
                processor.InfinityBasisVector.VectorStorage
            );

            return new ConformalOpnsHyperSphere<T>(processor, kVector);
        }

        public static ConformalOpnsFlat<T> CreateOpnsFlat<T>(this GeometricAlgebraConformalProcessor<T> processor, params Vector<T>[] points)
        {
            if (points.Length < 2 || points.Length >= processor.VSpaceDimension - 1)
                throw new InvalidOperationException();

            var kVector = processor.Op(
                processor.Op(
                    points.Select(p => processor.CreateIpnsPoint(p).VectorStorage)
                ), 
                processor.InfinityBasisVector.VectorStorage
            );

            return new ConformalOpnsFlat<T>(processor, kVector);
        }

        public static ConformalOpnsFlat<T> CreateOpnsFlat<T>(this GeometricAlgebraConformalProcessor<T> processor, Vector<T> positionVector, KVector<T> directionBlade)
        {
            if (positionVector.VectorStorage.MinVSpaceDimension > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            if (directionBlade.KVectorStorage.MinVSpaceDimension > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var kVector = processor.Op(
                    positionVector.VectorStorage,
                    directionBlade.KVectorStorage,
                    processor.InfinityBasisVector.VectorStorage
                );

            return new ConformalOpnsFlat<T>(processor, kVector);
        }
        
        public static ConformalOpnsDirection<T> CreateOpnsDirection<T>(this GeometricAlgebraConformalProcessor<T> processor, KVector<T> directionBlade)
        {
            if (directionBlade.KVectorStorage.MinVSpaceDimension > processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var kVector = processor.Op(
                directionBlade.KVectorStorage,
                processor.InfinityBasisVector.VectorStorage
            );

            return new ConformalOpnsDirection<T>(processor, kVector);
        }

        public static ConformalOpnsHyperPlane<T> CreateOpnsHyperPlane<T>(this GeometricAlgebraConformalProcessor<T> processor, params Vector<T>[] points)
        {
            if (points.Length != processor.VSpaceDimension - 2)
                throw new InvalidOperationException();

            var kVector = processor.Op(
                processor.Op(
                    points.Select(p => processor.CreateIpnsPoint(p).VectorStorage)
                ), 
                processor.InfinityBasisVector.VectorStorage
            );

            return new ConformalOpnsHyperPlane<T>(processor, kVector);
        }
    }
}