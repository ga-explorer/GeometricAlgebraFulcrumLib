using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal
{
    public static class XGaConformalComposerUtils
    {
        /// <summary>
        /// Convert a Euclidean position vector into a conformal null vector
        /// </summary>
        /// <param name="space"></param>
        /// <param name="positionVector"></param>
        /// <returns></returns>
        public static XGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this XGaConformalSpace<T> space, params T[] positionVector)
        {
            if (positionVector.Length > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var processor = space.Processor;
            var scalarProcessor = space.ScalarProcessor;
            var composer = processor.CreateComposer();
            
            var x2 = 
                positionVector
                    .Select(v => scalarProcessor.Times(v, v))
                    .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);

            // e+ part
            composer.SetVectorTerm(
                0, 
                scalarProcessor.Half(scalarProcessor.SubtractOne(x2))
            );

            // e- part
            composer.SetVectorTerm(
                1, 
                scalarProcessor.Half(scalarProcessor.AddOne(x2))
            );

            // Euclidean part
            composer.SetVectorTerms(2, positionVector);
            
            return new XGaConformalIpnsPoint<T>(
                space,
                composer.GetVector()
            );
        }

        public static XGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this XGaConformalSpace<T> space, XGaVector<T> positionVector)
        {
            if (positionVector.Keys.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();
            
            var processor = space.Processor;
            var scalarProcessor = space.ScalarProcessor;
            var composer = processor.CreateComposer();

            var x2 = scalarProcessor.AddSquares(positionVector.Scalars);
            
            // e+ part
            composer.SetVectorTerm(
                0, 
                scalarProcessor.Half(scalarProcessor.SubtractOne(x2))
            );

            // e- part
            composer.SetVectorTerm(
                1, 
                scalarProcessor.Half(scalarProcessor.AddOne(x2))
            );

            // Euclidean part
            composer.SetTerms(
                positionVector.Select(p => 
                    new KeyValuePair<IIndexSet, T>(
                        p.Key.ShiftIndices(2), 
                        p.Value
                    )
                )
            );
            
            return new XGaConformalIpnsPoint<T>(
                space,
                composer.GetVector()
            );
        }
        
        public static XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this XGaConformalSpace<T> space, XGaVector<T> centerPoint, double radiusSquared)
        {
            return CreateIpnsHyperSphere(
                space,
                centerPoint,
                space.ScalarProcessor.GetScalarFromNumber(radiusSquared)
            );
        }

        public static XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this XGaConformalSpace<T> space, XGaVector<T> centerPoint, string radiusSquared)
        {
            return CreateIpnsHyperSphere(
                space,
                centerPoint,
                space.ScalarProcessor.GetScalarFromText(radiusSquared)
            );
        }

        public static XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this XGaConformalSpace<T> space, XGaVector<T> centerPoint, T radiusSquared)
        {
            if (centerPoint.Keys.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();
            
            var processor = space.Processor;
            var scalarProcessor = space.ScalarProcessor;
            var composer = processor.CreateComposer();

            var x2 = scalarProcessor.Subtract(
                scalarProcessor.AddSquares(centerPoint.Values),
                radiusSquared
            );
            
            // e+ part
            composer.SetVectorTerm(
                0, 
                scalarProcessor.Half(scalarProcessor.SubtractOne(x2))
            );

            // e- part
            composer.SetVectorTerm(
                1, 
                scalarProcessor.Half(scalarProcessor.AddOne(x2))
            );

            // Euclidean part
            composer.SetTerms(
                centerPoint.Select(p => 
                    new KeyValuePair<IIndexSet, T>(
                        p.Key.ShiftIndices(2), 
                        p.Value
                    )
                )
            );

            return new XGaConformalIpnsHyperSphere<T>(
                space,
                composer.GetVector()
            );
        }
        
        public static XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this XGaConformalSpace<T> space, XGaVector<T> normal, double delta)
        {
            return CreateIpnsHyperPlane(
                space,
                normal,
                space.ScalarProcessor.GetScalarFromNumber(delta)
            );
        }

        public static XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this XGaConformalSpace<T> space, XGaVector<T> normal, string delta)
        {
            return CreateIpnsHyperPlane(
                space,
                normal,
                space.ScalarProcessor.GetScalarFromText(delta)
            );
        }

        public static XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this XGaConformalSpace<T> space, XGaVector<T> normal, T delta)
        {
            if (normal.Keys.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();
            
            var processor = space.Processor;
            var composer = processor.CreateComposer();
            
            // e+ part
            composer.SetVectorTerm(0, delta);

            // e- part
            composer.SetVectorTerm(1, delta);
            
            // Euclidean part
            composer.SetTerms(
                normal.Select(p => 
                    new KeyValuePair<IIndexSet, T>(
                        p.Key.ShiftIndices(2), 
                        p.Value
                    )
                )
            );

            return new XGaConformalIpnsHyperPlane<T>(
                space,
                composer.GetVector()
            );
        }


        public static XGaConformalOpnsRound<T> CreateOpnsRound<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
        {
            if (points.Length < 2 || points.Length > space.VSpaceDimensions - 1)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op();

            return new XGaConformalOpnsRound<T>(space, kVector);
        }

        public static XGaConformalOpnsHyperSphere<T> CreateOpnsHyperSphere<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
        {
            if (points.Length != space.VSpaceDimensions - 1)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op()
                    .Op(space.InfinityBasisVector);

            return new XGaConformalOpnsHyperSphere<T>(space, kVector);
        }

        public static XGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
        {
            if (points.Length < 2 || points.Length >= space.VSpaceDimensions - 1)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op()
                    .Op(space.InfinityBasisVector);

            return new XGaConformalOpnsFlat<T>(space, kVector);
        }

        public static XGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this XGaConformalSpace<T> space, XGaVector<T> positionVector, XGaKVector<T> directionBlade)
        {
            if (positionVector.VSpaceDimensions > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var kVector = 
                positionVector
                    .Op(directionBlade)
                    .Op(space.InfinityBasisVector);

            return new XGaConformalOpnsFlat<T>(space, kVector);
        }

        public static XGaConformalOpnsDirection<T> CreateOpnsDirection<T>(this XGaConformalSpace<T> space, XGaKVector<T> directionBlade)
        {
            if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var kVector = directionBlade.Op(space.InfinityBasisVector);

            return new XGaConformalOpnsDirection<T>(space, kVector);
        }

        public static XGaConformalOpnsHyperPlane<T> CreateOpnsHyperPlane<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
        {
            if (points.Length != space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op()
                    .Op(space.InfinityBasisVector);

            return new XGaConformalOpnsHyperPlane<T>(space, kVector);
        }
    }
}