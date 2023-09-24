using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    public abstract class E3DLine<T>
    {
        public abstract IScalarProcessor<T> ScalarProcessor { get; }

        public abstract bool IsSegment { get; }

        public abstract bool IsTangent { get; }

        public abstract E3DPoint<T> Point1 { get; }

        public abstract E3DPoint<T> Point2 { get; }

        public abstract E3DVector<T> Direction12 { get; }

        public abstract E3DVector<T> Direction21 { get; }


        public abstract E3DLineSegment<T> ToSegment();

        public abstract E3DLineTangent<T> ToTangent();

        public abstract E3DPoint<T> GetPoint(T t);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(float t)
        {
            return GetPoint(
                ScalarProcessor.GetScalarFromNumber(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(double t)
        {
            return GetPoint(
                ScalarProcessor.GetScalarFromNumber(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> GetSegment(float t1, float t2)
        {
            return GetSegment(
                ScalarProcessor.GetScalarFromNumber(t1),
                ScalarProcessor.GetScalarFromNumber(t2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> GetSegment(double t1, double t2)
        {
            return GetSegment(
                ScalarProcessor.GetScalarFromNumber(t1),
                ScalarProcessor.GetScalarFromNumber(t2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> GetSegment(T t1, T t2)
        {
            return new E3DLineSegment<T>(
                GetPoint(t1),
                GetPoint(t2)
            );
        }


        public E3DLinePlaneIntersectionRecord<T> GetIntersection(E3DPlane<T> plane)
        {
            var line = this;
            var sp = ScalarProcessor;

            //Begin GA-FuL MetaContext Code Generation, 2022-07-15T14:19:39.1612800+02:00
            //MetaContext: Line segment-triangle intersection
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 224 sub-expressions, 0 generated temps, 224 total.
            //Target Temp Variables: 11 total.
            //Output Variables: 5 total.
            //Computations: 0.7641921397379913 average, 175 total.
            //Memory Reads: 1.7641921397379912 average, 404 total.
            //Memory Writes: 229 total.
            //
            //MetaContext Binding Data:
            //   linePoint1X = parameter: line.Point1.X
            //   linePoint1Y = parameter: line.Point1.Y
            //   linePoint1Z = parameter: line.Point1.Z
            //   linePoint2X = parameter: line.Point2.X
            //   linePoint2Y = parameter: line.Point2.Y
            //   linePoint2Z = parameter: line.Point2.Z
            //   planePoint1X = parameter: plane.Point1.X
            //   planePoint1Y = parameter: plane.Point1.Y
            //   planePoint1Z = parameter: plane.Point1.Z
            //   planePoint2X = parameter: plane.Point2.X
            //   planePoint2Y = parameter: plane.Point2.Y
            //   planePoint2Z = parameter: plane.Point2.Z
            //   planePoint3X = parameter: plane.Point3.X
            //   planePoint3Y = parameter: plane.Point3.Y
            //   planePoint3Z = parameter: plane.Point3.Z

            //tmpVar264 = Times[linePoint1X, planePoint1Y]
            var temp0 = sp.Times(line.Point1.X, plane.Point1.Y);

            //tmpVar265 = Times[linePoint1Y, planePoint1X]
            var temp1 = sp.Times(line.Point1.Y, plane.Point1.X);

            //tmpVar266 = Minus[tmpVar265]
            temp1 = sp.Negative(temp1);

            //tmpVar267 = Plus[tmpVar264, tmpVar266]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar268 = Times[planePoint2Z, tmpVar267]
            temp1 = sp.Times(plane.Point2.Z, temp0);

            //tmpVar269 = Times[linePoint1Z, planePoint1X]
            var temp2 = sp.Times(line.Point1.Z, plane.Point1.X);

            //tmpVar270 = Minus[tmpVar269]
            temp2 = sp.Negative(temp2);

            //tmpVar271 = Times[linePoint1X, planePoint1Z]
            var temp3 = sp.Times(line.Point1.X, plane.Point1.Z);

            //tmpVar272 = Plus[tmpVar270, tmpVar271]
            temp2 = sp.Add(temp2, temp3);

            //tmpVar273 = Times[planePoint2Y, tmpVar272]
            temp3 = sp.Times(plane.Point2.Y, temp2);

            //tmpVar274 = Minus[tmpVar273]
            temp3 = sp.Negative(temp3);

            //tmpVar275 = Plus[tmpVar268, tmpVar274]
            temp1 = sp.Add(temp1, temp3);

            //tmpVar276 = Times[linePoint1Y, planePoint1Z]
            temp3 = sp.Times(line.Point1.Y, plane.Point1.Z);

            //tmpVar277 = Times[linePoint1Z, planePoint1Y]
            var temp4 = sp.Times(line.Point1.Z, plane.Point1.Y);

            //tmpVar278 = Minus[tmpVar277]
            temp4 = sp.Negative(temp4);

            //tmpVar279 = Plus[tmpVar276, tmpVar278]
            temp3 = sp.Add(temp3, temp4);

            //tmpVar280 = Times[planePoint2X, tmpVar279]
            temp4 = sp.Times(plane.Point2.X, temp3);

            //tmpVar281 = Plus[tmpVar275, tmpVar280]
            temp1 = sp.Add(temp1, temp4);

            //tmpVar282 = Minus[planePoint1X]
            temp4 = sp.Negative(plane.Point1.X);

            //tmpVar283 = Plus[linePoint1X, tmpVar282]
            var temp5 = sp.Add(line.Point1.X, temp4);

            //tmpVar284 = Times[planePoint2Y, tmpVar283]
            var temp6 = sp.Times(plane.Point2.Y, temp5);

            //tmpVar285 = Minus[tmpVar284]
            temp6 = sp.Negative(temp6);

            //tmpVar286 = Plus[tmpVar267, tmpVar285]
            temp0 = sp.Add(temp0, temp6);

            //tmpVar287 = Minus[planePoint1Y]
            temp6 = sp.Negative(plane.Point1.Y);

            //tmpVar288 = Plus[linePoint1Y, tmpVar287]
            var temp7 = sp.Add(line.Point1.Y, temp6);

            //tmpVar289 = Times[planePoint2X, tmpVar288]
            var temp8 = sp.Times(plane.Point2.X, temp7);

            //tmpVar290 = Plus[tmpVar286, tmpVar289]
            temp0 = sp.Add(temp0, temp8);

            //tmpVar291 = Times[planePoint3Z, tmpVar290]
            temp0 = sp.Times(plane.Point3.Z, temp0);

            //tmpVar292 = Minus[tmpVar291]
            temp0 = sp.Negative(temp0);

            //tmpVar293 = Plus[tmpVar281, tmpVar292]
            temp0 = sp.Add(temp1, temp0);

            //tmpVar294 = Times[planePoint2Z, tmpVar283]
            temp1 = sp.Times(plane.Point2.Z, temp5);

            //tmpVar295 = Minus[tmpVar294]
            temp1 = sp.Negative(temp1);

            //tmpVar296 = Plus[tmpVar272, tmpVar295]
            temp1 = sp.Add(temp2, temp1);

            //tmpVar297 = Minus[planePoint1Z]
            temp2 = sp.Negative(plane.Point1.Z);

            //tmpVar298 = Plus[linePoint1Z, tmpVar297]
            temp5 = sp.Add(line.Point1.Z, temp2);

            //tmpVar299 = Times[planePoint2X, tmpVar298]
            temp8 = sp.Times(plane.Point2.X, temp5);

            //tmpVar300 = Plus[tmpVar296, tmpVar299]
            temp1 = sp.Add(temp1, temp8);

            //tmpVar301 = Times[planePoint3Y, tmpVar300]
            temp1 = sp.Times(plane.Point3.Y, temp1);

            //tmpVar302 = Plus[tmpVar293, tmpVar301]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar303 = Times[planePoint2Z, tmpVar288]
            temp1 = sp.Times(plane.Point2.Z, temp7);

            //tmpVar304 = Minus[tmpVar303]
            temp1 = sp.Negative(temp1);

            //tmpVar305 = Plus[tmpVar279, tmpVar304]
            temp1 = sp.Add(temp3, temp1);

            //tmpVar306 = Times[planePoint2Y, tmpVar298]
            temp3 = sp.Times(plane.Point2.Y, temp5);

            //tmpVar307 = Plus[tmpVar305, tmpVar306]
            temp1 = sp.Add(temp1, temp3);

            //tmpVar308 = Times[planePoint3X, tmpVar307]
            temp1 = sp.Times(plane.Point3.X, temp1);

            //tmpVar309 = Minus[tmpVar308]
            temp1 = sp.Negative(temp1);

            //tmpVar56 = Plus[tmpVar302, tmpVar309]
            var linePoint1Distance = sp.Add(temp0, temp1);

            //tmpVar310 = Times[linePoint2Y, planePoint1Z]
            temp0 = sp.Times(line.Point2.Y, plane.Point1.Z);

            //tmpVar311 = Times[linePoint2Z, planePoint1Y]
            temp1 = sp.Times(line.Point2.Z, plane.Point1.Y);

            //tmpVar312 = Minus[tmpVar311]
            temp1 = sp.Negative(temp1);

            //tmpVar313 = Plus[tmpVar310, tmpVar312]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar314 = Plus[linePoint2Y, tmpVar287]
            temp1 = sp.Add(line.Point2.Y, temp6);

            //tmpVar315 = Times[planePoint2Z, tmpVar314]
            temp3 = sp.Times(plane.Point2.Z, temp1);

            //tmpVar316 = Minus[tmpVar315]
            temp3 = sp.Negative(temp3);

            //tmpVar317 = Plus[tmpVar313, tmpVar316]
            temp3 = sp.Add(temp0, temp3);

            //tmpVar318 = Plus[linePoint2Z, tmpVar297]
            temp5 = sp.Add(line.Point2.Z, temp2);

            //tmpVar319 = Times[planePoint2Y, tmpVar318]
            temp7 = sp.Times(plane.Point2.Y, temp5);

            //tmpVar320 = Plus[tmpVar317, tmpVar319]
            temp3 = sp.Add(temp3, temp7);

            //tmpVar321 = Times[planePoint3X, tmpVar320]
            temp3 = sp.Times(plane.Point3.X, temp3);

            //tmpVar322 = Minus[tmpVar321]
            temp3 = sp.Negative(temp3);

            //tmpVar323 = Times[linePoint2X, planePoint1Y]
            temp7 = sp.Times(line.Point2.X, plane.Point1.Y);

            //tmpVar324 = Times[linePoint2Y, planePoint1X]
            temp8 = sp.Times(line.Point2.Y, plane.Point1.X);

            //tmpVar325 = Minus[tmpVar324]
            temp8 = sp.Negative(temp8);

            //tmpVar326 = Plus[tmpVar323, tmpVar325]
            temp7 = sp.Add(temp7, temp8);

            //tmpVar327 = Times[planePoint2Z, tmpVar326]
            temp8 = sp.Times(plane.Point2.Z, temp7);

            //tmpVar328 = Times[linePoint2X, planePoint1Z]
            var temp9 = sp.Times(line.Point2.X, plane.Point1.Z);

            //tmpVar329 = Times[linePoint2Z, planePoint1X]
            var temp10 = sp.Times(line.Point2.Z, plane.Point1.X);

            //tmpVar330 = Minus[tmpVar329]
            temp10 = sp.Negative(temp10);

            //tmpVar331 = Plus[tmpVar328, tmpVar330]
            temp9 = sp.Add(temp9, temp10);

            //tmpVar332 = Times[planePoint2Y, tmpVar331]
            temp10 = sp.Times(plane.Point2.Y, temp9);

            //tmpVar333 = Minus[tmpVar332]
            temp10 = sp.Negative(temp10);

            //tmpVar334 = Plus[tmpVar327, tmpVar333]
            temp8 = sp.Add(temp8, temp10);

            //tmpVar335 = Times[planePoint2X, tmpVar313]
            temp0 = sp.Times(plane.Point2.X, temp0);

            //tmpVar336 = Plus[tmpVar334, tmpVar335]
            temp0 = sp.Add(temp8, temp0);

            //tmpVar337 = Plus[linePoint2X, tmpVar282]
            temp8 = sp.Add(line.Point2.X, temp4);

            //tmpVar338 = Times[planePoint2Y, tmpVar337]
            temp10 = sp.Times(plane.Point2.Y, temp8);

            //tmpVar339 = Minus[tmpVar338]
            temp10 = sp.Negative(temp10);

            //tmpVar340 = Plus[tmpVar326, tmpVar339]
            temp7 = sp.Add(temp7, temp10);

            //tmpVar341 = Times[planePoint2X, tmpVar314]
            temp1 = sp.Times(plane.Point2.X, temp1);

            //tmpVar342 = Plus[tmpVar340, tmpVar341]
            temp1 = sp.Add(temp7, temp1);

            //tmpVar343 = Times[planePoint3Z, tmpVar342]
            temp1 = sp.Times(plane.Point3.Z, temp1);

            //tmpVar344 = Minus[tmpVar343]
            temp1 = sp.Negative(temp1);

            //tmpVar345 = Plus[tmpVar336, tmpVar344]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar346 = Times[planePoint2Z, tmpVar337]
            temp1 = sp.Times(plane.Point2.Z, temp8);

            //tmpVar347 = Minus[tmpVar346]
            temp1 = sp.Negative(temp1);

            //tmpVar348 = Plus[tmpVar331, tmpVar347]
            temp1 = sp.Add(temp9, temp1);

            //tmpVar349 = Times[planePoint2X, tmpVar318]
            temp5 = sp.Times(plane.Point2.X, temp5);

            //tmpVar350 = Plus[tmpVar348, tmpVar349]
            temp1 = sp.Add(temp1, temp5);

            //tmpVar351 = Times[planePoint3Y, tmpVar350]
            temp1 = sp.Times(plane.Point3.Y, temp1);

            //tmpVar352 = Plus[tmpVar345, tmpVar351]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar103 = Plus[tmpVar322, tmpVar352]
            var linePoint2Distance = sp.Add(temp3, temp0);

            //tmpVar353 = Times[planePoint1X, planePoint2Y]
            temp0 = sp.Times(plane.Point1.X, plane.Point2.Y);

            //tmpVar354 = Times[planePoint1Y, planePoint2X]
            temp1 = sp.Times(plane.Point1.Y, plane.Point2.X);

            //tmpVar355 = Minus[tmpVar354]
            temp1 = sp.Negative(temp1);

            //tmpVar356 = Plus[tmpVar353, tmpVar355]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar357 = Times[linePoint1Z, tmpVar356]
            temp1 = sp.Times(line.Point1.Z, temp0);

            //tmpVar358 = Times[planePoint1X, planePoint2Z]
            temp3 = sp.Times(plane.Point1.X, plane.Point2.Z);

            //tmpVar359 = Times[planePoint1Z, planePoint2X]
            temp5 = sp.Times(plane.Point1.Z, plane.Point2.X);

            //tmpVar360 = Minus[tmpVar359]
            temp5 = sp.Negative(temp5);

            //tmpVar361 = Plus[tmpVar358, tmpVar360]
            temp3 = sp.Add(temp3, temp5);

            //tmpVar362 = Times[linePoint1Y, tmpVar361]
            temp5 = sp.Times(line.Point1.Y, temp3);

            //tmpVar363 = Minus[tmpVar362]
            temp5 = sp.Negative(temp5);

            //tmpVar364 = Plus[tmpVar357, tmpVar363]
            temp1 = sp.Add(temp1, temp5);

            //tmpVar365 = Times[planePoint1Y, planePoint2Z]
            temp5 = sp.Times(plane.Point1.Y, plane.Point2.Z);

            //tmpVar366 = Times[planePoint1Z, planePoint2Y]
            temp7 = sp.Times(plane.Point1.Z, plane.Point2.Y);

            //tmpVar367 = Minus[tmpVar366]
            temp7 = sp.Negative(temp7);

            //tmpVar368 = Plus[tmpVar365, tmpVar367]
            temp5 = sp.Add(temp5, temp7);

            //tmpVar369 = Times[linePoint1X, tmpVar368]
            temp7 = sp.Times(line.Point1.X, temp5);

            //tmpVar370 = Plus[tmpVar364, tmpVar369]
            temp1 = sp.Add(temp1, temp7);

            //tmpVar371 = Minus[planePoint2X]
            temp7 = sp.Negative(plane.Point2.X);

            //tmpVar372 = Plus[planePoint1X, tmpVar371]
            temp7 = sp.Add(plane.Point1.X, temp7);

            //tmpVar373 = Times[linePoint1Y, tmpVar372]
            temp8 = sp.Times(line.Point1.Y, temp7);

            //tmpVar374 = Minus[tmpVar373]
            temp8 = sp.Negative(temp8);

            //tmpVar375 = Plus[tmpVar356, tmpVar374]
            temp0 = sp.Add(temp0, temp8);

            //tmpVar376 = Minus[planePoint2Y]
            temp8 = sp.Negative(plane.Point2.Y);

            //tmpVar377 = Plus[planePoint1Y, tmpVar376]
            temp8 = sp.Add(plane.Point1.Y, temp8);

            //tmpVar378 = Times[linePoint1X, tmpVar377]
            temp9 = sp.Times(line.Point1.X, temp8);

            //tmpVar379 = Plus[tmpVar375, tmpVar378]
            temp0 = sp.Add(temp0, temp9);

            //tmpVar380 = Times[linePoint2Z, tmpVar379]
            temp0 = sp.Times(line.Point2.Z, temp0);

            //tmpVar381 = Minus[tmpVar380]
            temp0 = sp.Negative(temp0);

            //tmpVar382 = Plus[tmpVar370, tmpVar381]
            temp0 = sp.Add(temp1, temp0);

            //tmpVar383 = Times[linePoint1Z, tmpVar372]
            temp1 = sp.Times(line.Point1.Z, temp7);

            //tmpVar384 = Minus[tmpVar383]
            temp1 = sp.Negative(temp1);

            //tmpVar385 = Plus[tmpVar361, tmpVar384]
            temp1 = sp.Add(temp3, temp1);

            //tmpVar386 = Minus[planePoint2Z]
            temp3 = sp.Negative(plane.Point2.Z);

            //tmpVar387 = Plus[planePoint1Z, tmpVar386]
            temp3 = sp.Add(plane.Point1.Z, temp3);

            //tmpVar388 = Times[linePoint1X, tmpVar387]
            temp7 = sp.Times(line.Point1.X, temp3);

            //tmpVar389 = Plus[tmpVar385, tmpVar388]
            temp1 = sp.Add(temp1, temp7);

            //tmpVar390 = Times[linePoint2Y, tmpVar389]
            temp1 = sp.Times(line.Point2.Y, temp1);

            //tmpVar391 = Plus[tmpVar382, tmpVar390]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar392 = Times[linePoint1Z, tmpVar377]
            temp1 = sp.Times(line.Point1.Z, temp8);

            //tmpVar393 = Minus[tmpVar392]
            temp1 = sp.Negative(temp1);

            //tmpVar394 = Plus[tmpVar368, tmpVar393]
            temp1 = sp.Add(temp5, temp1);

            //tmpVar395 = Times[linePoint1Y, tmpVar387]
            temp3 = sp.Times(line.Point1.Y, temp3);

            //tmpVar396 = Plus[tmpVar394, tmpVar395]
            temp1 = sp.Add(temp1, temp3);

            //tmpVar397 = Times[linePoint2X, tmpVar396]
            temp1 = sp.Times(line.Point2.X, temp1);

            //tmpVar398 = Minus[tmpVar397]
            temp1 = sp.Negative(temp1);

            //tmpVar158 = Plus[tmpVar391, tmpVar398]
            var planeLine12Distance = sp.Add(temp0, temp1);

            //tmpVar399 = Times[planePoint2X, planePoint3Y]
            temp0 = sp.Times(plane.Point2.X, plane.Point3.Y);

            //tmpVar400 = Times[planePoint2Y, planePoint3X]
            temp1 = sp.Times(plane.Point2.Y, plane.Point3.X);

            //tmpVar401 = Minus[tmpVar400]
            temp1 = sp.Negative(temp1);

            //tmpVar402 = Plus[tmpVar399, tmpVar401]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar403 = Times[linePoint1Z, tmpVar402]
            temp1 = sp.Times(line.Point1.Z, temp0);

            //tmpVar404 = Times[planePoint2X, planePoint3Z]
            temp3 = sp.Times(plane.Point2.X, plane.Point3.Z);

            //tmpVar405 = Times[planePoint2Z, planePoint3X]
            temp5 = sp.Times(plane.Point2.Z, plane.Point3.X);

            //tmpVar406 = Minus[tmpVar405]
            temp5 = sp.Negative(temp5);

            //tmpVar407 = Plus[tmpVar404, tmpVar406]
            temp3 = sp.Add(temp3, temp5);

            //tmpVar408 = Times[linePoint1Y, tmpVar407]
            temp5 = sp.Times(line.Point1.Y, temp3);

            //tmpVar409 = Minus[tmpVar408]
            temp5 = sp.Negative(temp5);

            //tmpVar410 = Plus[tmpVar403, tmpVar409]
            temp1 = sp.Add(temp1, temp5);

            //tmpVar411 = Times[planePoint2Y, planePoint3Z]
            temp5 = sp.Times(plane.Point2.Y, plane.Point3.Z);

            //tmpVar412 = Times[planePoint2Z, planePoint3Y]
            temp7 = sp.Times(plane.Point2.Z, plane.Point3.Y);

            //tmpVar413 = Minus[tmpVar412]
            temp7 = sp.Negative(temp7);

            //tmpVar414 = Plus[tmpVar411, tmpVar413]
            temp5 = sp.Add(temp5, temp7);

            //tmpVar415 = Times[linePoint1X, tmpVar414]
            temp7 = sp.Times(line.Point1.X, temp5);

            //tmpVar416 = Plus[tmpVar410, tmpVar415]
            temp1 = sp.Add(temp1, temp7);

            //tmpVar417 = Minus[planePoint3X]
            temp7 = sp.Negative(plane.Point3.X);

            //tmpVar418 = Plus[planePoint2X, tmpVar417]
            temp7 = sp.Add(plane.Point2.X, temp7);

            //tmpVar419 = Times[linePoint1Y, tmpVar418]
            temp8 = sp.Times(line.Point1.Y, temp7);

            //tmpVar420 = Minus[tmpVar419]
            temp8 = sp.Negative(temp8);

            //tmpVar421 = Plus[tmpVar402, tmpVar420]
            temp0 = sp.Add(temp0, temp8);

            //tmpVar422 = Minus[planePoint3Y]
            temp8 = sp.Negative(plane.Point3.Y);

            //tmpVar423 = Plus[planePoint2Y, tmpVar422]
            temp8 = sp.Add(plane.Point2.Y, temp8);

            //tmpVar424 = Times[linePoint1X, tmpVar423]
            temp9 = sp.Times(line.Point1.X, temp8);

            //tmpVar425 = Plus[tmpVar421, tmpVar424]
            temp0 = sp.Add(temp0, temp9);

            //tmpVar426 = Times[linePoint2Z, tmpVar425]
            temp0 = sp.Times(line.Point2.Z, temp0);

            //tmpVar427 = Minus[tmpVar426]
            temp0 = sp.Negative(temp0);

            //tmpVar428 = Plus[tmpVar416, tmpVar427]
            temp0 = sp.Add(temp1, temp0);

            //tmpVar429 = Times[linePoint1Z, tmpVar418]
            temp1 = sp.Times(line.Point1.Z, temp7);

            //tmpVar430 = Minus[tmpVar429]
            temp1 = sp.Negative(temp1);

            //tmpVar431 = Plus[tmpVar407, tmpVar430]
            temp1 = sp.Add(temp3, temp1);

            //tmpVar432 = Minus[planePoint3Z]
            temp3 = sp.Negative(plane.Point3.Z);

            //tmpVar433 = Plus[planePoint2Z, tmpVar432]
            temp3 = sp.Add(plane.Point2.Z, temp3);

            //tmpVar434 = Times[linePoint1X, tmpVar433]
            temp7 = sp.Times(line.Point1.X, temp3);

            //tmpVar435 = Plus[tmpVar431, tmpVar434]
            temp1 = sp.Add(temp1, temp7);

            //tmpVar436 = Times[linePoint2Y, tmpVar435]
            temp1 = sp.Times(line.Point2.Y, temp1);

            //tmpVar437 = Plus[tmpVar428, tmpVar436]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar438 = Times[linePoint1Z, tmpVar423]
            temp1 = sp.Times(line.Point1.Z, temp8);

            //tmpVar439 = Minus[tmpVar438]
            temp1 = sp.Negative(temp1);

            //tmpVar440 = Plus[tmpVar414, tmpVar439]
            temp1 = sp.Add(temp5, temp1);

            //tmpVar441 = Times[linePoint1Y, tmpVar433]
            temp3 = sp.Times(line.Point1.Y, temp3);

            //tmpVar442 = Plus[tmpVar440, tmpVar441]
            temp1 = sp.Add(temp1, temp3);

            //tmpVar443 = Times[linePoint2X, tmpVar442]
            temp1 = sp.Times(line.Point2.X, temp1);

            //tmpVar444 = Minus[tmpVar443]
            temp1 = sp.Negative(temp1);

            //tmpVar211 = Plus[tmpVar437, tmpVar444]
            var planeLine23Distance = sp.Add(temp0, temp1);

            //tmpVar445 = Times[planePoint1Y, planePoint3X]
            temp0 = sp.Times(plane.Point1.Y, plane.Point3.X);

            //tmpVar446 = Times[planePoint1X, planePoint3Y]
            temp1 = sp.Times(plane.Point1.X, plane.Point3.Y);

            //tmpVar447 = Minus[tmpVar446]
            temp1 = sp.Negative(temp1);

            //tmpVar448 = Plus[tmpVar445, tmpVar447]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar449 = Times[linePoint1Z, tmpVar448]
            temp1 = sp.Times(line.Point1.Z, temp0);

            //tmpVar450 = Times[planePoint1Z, planePoint3X]
            temp3 = sp.Times(plane.Point1.Z, plane.Point3.X);

            //tmpVar451 = Times[planePoint1X, planePoint3Z]
            temp5 = sp.Times(plane.Point1.X, plane.Point3.Z);

            //tmpVar452 = Minus[tmpVar451]
            temp5 = sp.Negative(temp5);

            //tmpVar453 = Plus[tmpVar450, tmpVar452]
            temp3 = sp.Add(temp3, temp5);

            //tmpVar454 = Times[linePoint1Y, tmpVar453]
            temp5 = sp.Times(line.Point1.Y, temp3);

            //tmpVar455 = Minus[tmpVar454]
            temp5 = sp.Negative(temp5);

            //tmpVar456 = Plus[tmpVar449, tmpVar455]
            temp1 = sp.Add(temp1, temp5);

            //tmpVar457 = Times[planePoint1Z, planePoint3Y]
            temp5 = sp.Times(plane.Point1.Z, plane.Point3.Y);

            //tmpVar458 = Times[planePoint1Y, planePoint3Z]
            temp7 = sp.Times(plane.Point1.Y, plane.Point3.Z);

            //tmpVar459 = Minus[tmpVar458]
            temp7 = sp.Negative(temp7);

            //tmpVar460 = Plus[tmpVar457, tmpVar459]
            temp5 = sp.Add(temp5, temp7);

            //tmpVar461 = Times[linePoint1X, tmpVar460]
            temp7 = sp.Times(line.Point1.X, temp5);

            //tmpVar462 = Plus[tmpVar456, tmpVar461]
            temp1 = sp.Add(temp1, temp7);

            //tmpVar463 = Plus[planePoint3X, tmpVar282]
            temp4 = sp.Add(plane.Point3.X, temp4);

            //tmpVar464 = Times[linePoint1Y, tmpVar463]
            temp7 = sp.Times(line.Point1.Y, temp4);

            //tmpVar465 = Minus[tmpVar464]
            temp7 = sp.Negative(temp7);

            //tmpVar466 = Plus[tmpVar448, tmpVar465]
            temp0 = sp.Add(temp0, temp7);

            //tmpVar467 = Plus[planePoint3Y, tmpVar287]
            temp6 = sp.Add(plane.Point3.Y, temp6);

            //tmpVar468 = Times[linePoint1X, tmpVar467]
            temp7 = sp.Times(line.Point1.X, temp6);

            //tmpVar469 = Plus[tmpVar466, tmpVar468]
            temp0 = sp.Add(temp0, temp7);

            //tmpVar470 = Times[linePoint2Z, tmpVar469]
            temp0 = sp.Times(line.Point2.Z, temp0);

            //tmpVar471 = Minus[tmpVar470]
            temp0 = sp.Negative(temp0);

            //tmpVar472 = Plus[tmpVar462, tmpVar471]
            temp0 = sp.Add(temp1, temp0);

            //tmpVar473 = Times[linePoint1Z, tmpVar463]
            temp1 = sp.Times(line.Point1.Z, temp4);

            //tmpVar474 = Minus[tmpVar473]
            temp1 = sp.Negative(temp1);

            //tmpVar475 = Plus[tmpVar453, tmpVar474]
            temp1 = sp.Add(temp3, temp1);

            //tmpVar476 = Plus[planePoint3Z, tmpVar297]
            temp2 = sp.Add(plane.Point3.Z, temp2);

            //tmpVar477 = Times[linePoint1X, tmpVar476]
            temp3 = sp.Times(line.Point1.X, temp2);

            //tmpVar478 = Plus[tmpVar475, tmpVar477]
            temp1 = sp.Add(temp1, temp3);

            //tmpVar479 = Times[linePoint2Y, tmpVar478]
            temp1 = sp.Times(line.Point2.Y, temp1);

            //tmpVar480 = Plus[tmpVar472, tmpVar479]
            temp0 = sp.Add(temp0, temp1);

            //tmpVar481 = Times[linePoint1Z, tmpVar467]
            temp1 = sp.Times(line.Point1.Z, temp6);

            //tmpVar482 = Minus[tmpVar481]
            temp1 = sp.Negative(temp1);

            //tmpVar483 = Plus[tmpVar460, tmpVar482]
            temp1 = sp.Add(temp5, temp1);

            //tmpVar484 = Times[linePoint1Y, tmpVar476]
            temp2 = sp.Times(line.Point1.Y, temp2);

            //tmpVar485 = Plus[tmpVar483, tmpVar484]
            temp1 = sp.Add(temp1, temp2);

            //tmpVar486 = Times[linePoint2X, tmpVar485]
            temp1 = sp.Times(line.Point2.X, temp1);

            //tmpVar487 = Minus[tmpVar486]
            temp1 = sp.Negative(temp1);

            //tmpVar258 = Plus[tmpVar480, tmpVar487]
            var planeLine31Distance = sp.Add(temp0, temp1);

            //Finish GA-FuL MetaContext Code Generation, 2022-07-15T14:19:39.5524896+02:00
        
            return new E3DLinePlaneIntersectionRecord<T>(ScalarProcessor)
            {
                LinePoint1Distance = linePoint1Distance,
                LinePoint2Distance = linePoint2Distance,
                PlaneLine12Distance = planeLine12Distance,
                PlaneLine23Distance = planeLine23Distance,
                PlaneLine31Distance = planeLine31Distance
            };
        }
    }
}