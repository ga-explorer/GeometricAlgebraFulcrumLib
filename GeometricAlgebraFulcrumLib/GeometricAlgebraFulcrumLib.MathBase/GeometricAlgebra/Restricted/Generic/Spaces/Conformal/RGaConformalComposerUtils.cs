using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces.Conformal
{
    public static class RGaConformalComposerUtils
    {
        /// <summary>
        /// Convert a Euclidean position vector into a conformal null vector
        /// </summary>
        /// <param name="space"></param>
        /// <param name="positionVector"></param>
        /// <returns></returns>
        public static RGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this RGaConformalSpace<T> space, params T[] positionVector)
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
            
            return new RGaConformalIpnsPoint<T>(
                space,
                composer.GetVector()
            );
        }

        public static RGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this RGaConformalSpace<T> space, RGaVector<T> positionVector)
        {
            if (positionVector.Keys.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
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
                    new KeyValuePair<ulong, T>(
                        p.Key.ShiftOnes(2),
                        p.Value
                    )
                )
            );
            
            return new RGaConformalIpnsPoint<T>(
                space,
                composer.GetVector()
            );
        }
        
        public static RGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this RGaConformalSpace<T> space, RGaVector<T> centerPoint, double radiusSquared)
        {
            return CreateIpnsHyperSphere(
                space,
                centerPoint,
                space.ScalarProcessor.GetScalarFromNumber(radiusSquared)
            );
        }

        public static RGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this RGaConformalSpace<T> space, RGaVector<T> centerPoint, string radiusSquared)
        {
            return CreateIpnsHyperSphere(
                space,
                centerPoint,
                space.ScalarProcessor.GetScalarFromText(radiusSquared)
            );
        }

        public static RGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this RGaConformalSpace<T> space, RGaVector<T> centerPoint, T radiusSquared)
        {
            if (centerPoint.Keys.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
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
                    new KeyValuePair<ulong, T>(
                        p.Key.ShiftOnes(2), 
                        p.Value
                    )
                )
            );

            return new RGaConformalIpnsHyperSphere<T>(
                space,
                composer.GetVector()
            );
        }
        
        public static RGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this RGaConformalSpace<T> space, RGaVector<T> normal, double delta)
        {
            return CreateIpnsHyperPlane(
                space,
                normal,
                space.ScalarProcessor.GetScalarFromNumber(delta)
            );
        }

        public static RGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this RGaConformalSpace<T> space, RGaVector<T> normal, string delta)
        {
            return CreateIpnsHyperPlane(
                space,
                normal,
                space.ScalarProcessor.GetScalarFromText(delta)
            );
        }

        public static RGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this RGaConformalSpace<T> space, RGaVector<T> normal, T delta)
        {
            if (normal.Keys.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
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
                    new KeyValuePair<ulong, T>(
                        p.Key.ShiftOnes(2), 
                        p.Value
                    )
                )
            );

            return new RGaConformalIpnsHyperPlane<T>(
                space,
                composer.GetVector()
            );
        }


        public static RGaConformalOpnsRound<T> CreateOpnsRound<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
        {
            if (points.Length < 2 || points.Length > space.VSpaceDimensions - 1)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op();

            return new RGaConformalOpnsRound<T>(space, kVector);
        }

        public static RGaConformalOpnsHyperSphere<T> CreateOpnsHyperSphere<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
        {
            if (points.Length != space.VSpaceDimensions - 1)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op()
                    .Op(space.InfinityBasisVector);

            return new RGaConformalOpnsHyperSphere<T>(space, kVector);
        }

        public static RGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
        {
            if (points.Length < 2 || points.Length >= space.VSpaceDimensions - 1)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op()
                    .Op(space.InfinityBasisVector);

            return new RGaConformalOpnsFlat<T>(space, kVector);
        }

        public static RGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this RGaConformalSpace<T> space, RGaVector<T> positionVector, RGaKVector<T> directionBlade)
        {
            if (positionVector.VSpaceDimensions > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var kVector = 
                positionVector
                    .Op(directionBlade)
                    .Op(space.InfinityBasisVector);

            return new RGaConformalOpnsFlat<T>(space, kVector);
        }

        public static RGaConformalOpnsDirection<T> CreateOpnsDirection<T>(this RGaConformalSpace<T> space, RGaKVector<T> directionBlade)
        {
            if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var kVector = directionBlade.Op(space.InfinityBasisVector);

            return new RGaConformalOpnsDirection<T>(space, kVector);
        }

        public static RGaConformalOpnsHyperPlane<T> CreateOpnsHyperPlane<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
        {
            if (points.Length != space.VSpaceDimensions - 2)
                throw new InvalidOperationException();

            var kVector = 
                points
                    .Select(p => space.CreateIpnsPoint(p).Vector)
                    .Op()
                    .Op(space.InfinityBasisVector);

            return new RGaConformalOpnsHyperPlane<T>(space, kVector);
        }
    }
}