using System.Collections.Immutable;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Statistics.Continuous
{
    public sealed class SimplexFunction1D
    {

    }

    public class PiecewiseAffineFunction
    {
        public sealed record Breakpoint
        {
            public static Breakpoint Create(double x, double y)
            {
                return new Breakpoint(x, y);
            }

            public static Breakpoint Create(double x, double yMinus, double y, double yPlus)
            {
                return new Breakpoint(x, yMinus, y, yPlus);
            }


            private double _x;
            public double X
            {
                get => _x;
                private set
                {
                    if (double.IsNaN(value))
                        throw new ArgumentException(nameof(value));

                    _x = value;
                }
            }

            private double _y;
            public double Y
            {
                get => _y;
                set
                {
                    if (double.IsNaN(value) || double.IsInfinity(value))
                        throw new ArgumentException(nameof(value));

                    _y = value;
                }
            }

            private double _yMinus;
            public double YMinus
            {
                get => _yMinus;
                set
                {
                    if (double.IsNaN(value) || double.IsInfinity(value))
                        throw new ArgumentException(nameof(value));

                    _yMinus = value;
                }
            }

            private double _yPlus;
            

            public double YPlus
            {
                get => _yPlus;
                set
                {
                    if (double.IsNaN(value) || double.IsInfinity(value))
                        throw new ArgumentException(nameof(value));

                    _yPlus = value;
                }
            }

            public double DeltaY
                => YPlus - YMinus;

            public bool IsZero
                => YMinus.Equals(0d) && 
                   Y.Equals(0d) &&
                   YPlus.Equals(0d);
            
            public bool IsSymmetric
                => YMinus.Equals(YPlus);

            public bool IsDiscrete
                => !Y.Equals(YMinus) && !Y.Equals(YPlus);

            public bool IsContinuous
                => Y.Equals(YMinus) && Y.Equals(YPlus);

            public bool IsDiscontinuous
                => !IsContinuous;

            public bool IsLeftContinuous
                => Y.Equals(YMinus) && !Y.Equals(YPlus);

            public bool IsRightContinuous
                => !Y.Equals(YMinus) && Y.Equals(YPlus);


            private Breakpoint(double x, double y)
            {
                if (double.IsNaN(x) || double.IsInfinity(x))
                    throw new ArgumentException(nameof(x));

                if (double.IsNaN(y) || double.IsInfinity(y))
                    throw new ArgumentException(nameof(y));

                _x = x;
                _y = y;
                _yMinus = y;
                _yPlus = y;
            }

            private Breakpoint(double x, double yMinus, double y, double yPlus)
            {
                if (double.IsNaN(x) || double.IsInfinity(x))
                    throw new ArgumentException(nameof(x));

                if (double.IsNaN(y) || double.IsInfinity(y))
                    throw new ArgumentException(nameof(y));

                if (double.IsNaN(yMinus) || double.IsInfinity(yMinus))
                    throw new ArgumentException(nameof(yMinus));

                if (double.IsNaN(yPlus) || double.IsInfinity(yPlus))
                    throw new ArgumentException(nameof(yPlus));

                _x = x;
                _y = y;
                _yMinus = yMinus;
                _yPlus = yPlus;
            }


            public Breakpoint NegativeX()
            {
                return new Breakpoint(-X, YPlus, Y, YMinus);
            }
            
            public Breakpoint NegativeY()
            {
                return new Breakpoint(X, -YMinus, -Y, -YPlus);
            }

            public Breakpoint NegativeXy()
            {
                return new Breakpoint(-X, -YPlus, -Y, -YMinus);
            }


            public Breakpoint InPlaceShiftX(double deltaX)
            {
                var x = _x + deltaX;

                if (double.IsNaN(x) || double.IsInfinity(x))
                    throw new InvalidOperationException();

                _x = x;

                return this;
            }
            
            public Breakpoint InPlaceSetYToZero()
            {
                YMinus = 0d;
                Y = 0d;
                YPlus = 0d;

                return this;
            }

            public Breakpoint InPlaceScaleY(double scalingFactor)
            {
                YMinus *= scalingFactor;
                Y *= scalingFactor;
                YPlus *= scalingFactor;

                return this;
            }

        }

        public sealed record Sample(double X, double Y)
        {

        }

        public sealed record Segment(double X1, double X2, double Y1, double Y2)
        {
            public double XMid 
                => (X1 + X2) / 2;

            public double YMid 
                => (Y1 + Y2) / 2;

            public double XDelta
                => X2 - X1;

            public double YDelta
                => Y2 - Y1;

            public LinFloat64PolarAngle Angle
                => double.IsInfinity(X1) || double.IsInfinity(X2) || Y1.Equals(Y2)
                    ? X1 <= X2 ? LinFloat64PolarAngle.Angle0 : LinFloat64PolarAngle.Angle180
                    : LinFloat64PolarAngle.CreateFromVector(XDelta, YDelta);

            public bool IsZeroY
                => Y1.Equals(0d) && Y2.Equals(0d);

            public bool IsFiniteX 
                => double.IsFinite(X1) && double.IsFinite(X2);

            public bool IsConstantY 
                => Y1.Equals(Y2);


            public bool Contains(double x)
            {
                return x > X1 && x < X2;
            }

            public double GetValue(double x)
            {
                if (x <= X1 || x >= X2)
                    return 0;

                var t = (x - X1) / (X2 - X1);

                return (1d - t) * Y1 + t * Y2;
            }
            
            public Pair<double> Lerp(double t)
            {
                return new Pair<double>(
                    (1d - t) * X1 + t * X2,
                    (1d - t) * Y1 + t * Y2
                );
            }

            public double LerpX(double t)
            {
                return (1d - t) * X1 + t * X2;
            }

            public double LerpY(double t)
            {
                return (1d - t) * Y1 + t * Y2;
            }

            public Pair<Segment> SplitAt(double x)
            {
                if (!Contains(x))
                    throw new InvalidOperationException();

                return new Pair<Segment>(
                    this with { X2 = x, Y2 = GetValue(x) },
                    this with { X1 = x, Y1 = GetValue(x) }
                );
            }

            public Segment GetFirstPart(double x)
            {
                if (!Contains(x))
                    throw new InvalidOperationException();

                return this with { X2 = x, Y2 = GetValue(x) };
            }
            
            public Segment GetLastPart(double x)
            {
                if (!Contains(x))
                    throw new InvalidOperationException();

                return this with { X1 = x, Y1 = GetValue(x) };
            }

            public double GetArea()
            {
                return IsZeroY 
                    ? 0d 
                    : IsFiniteX 
                        ? 0.5d * Math.Abs((X2 - X1) * (Y1 + Y2))
                        : double.PositiveInfinity;
            }

            public Segment SetX1(double x1)
            {
                return this with { X1 = x1 };
            }

            public Segment SetX2(double x2)
            {
                return this with { X2 = x2 };
            }
        }


        public static PiecewiseAffineFunction CreateContinuous(IReadOnlyList<double> xValues, IReadOnlyList<double> yValues)
        {
            var n = xValues.Count;

            if (yValues.Count != n)
                throw new InvalidOperationException();

            var function = new PiecewiseAffineFunction();

            for (var i = 0; i < n; i++)
                function.InsertBreakpoint(xValues[i], yValues[i]);

            return function;
        }

        public static PiecewiseAffineFunction CreateContinuous(IReadOnlyList<double> xValues, IReadOnlyList<double> yValues, double angleTolerance)
        {
            var sampleCount = xValues.Count;

            if (yValues.Count != sampleCount)
                throw new InvalidOperationException();

            var indexList = new SortedSet<int>
            {
                0,
                sampleCount - 1
            };


            // Forward direction
            var refAngle = LinFloat64PolarAngle.CreateFromVector(
                xValues[1] - xValues[0],
                yValues[1] - yValues[0]
            );

            for (var i = 1; i <= sampleCount - 2; i++)
            {
                var angle = LinFloat64PolarAngle.CreateFromVector(
                    xValues[i + 1] - xValues[i],
                    yValues[i + 1] - yValues[i]
                );

                var angleDelta = (angle.RadiansValue - refAngle.RadiansValue).Abs();

                if (angleDelta < angleTolerance) continue;

                indexList.Add(i);

                refAngle = angle;
            }


            //// Backward direction
            //refAngle = Math.Atan2(
            //    yValues[^2] - yValues[^1],
            //    xValues[^2] - xValues[^1]
            //);

            //for (var i = sampleCount - 2; i >= 1; i--)
            //{
            //    var angle = Math.Atan2(
            //        yValues[i] - yValues[i + 1],
            //        xValues[i] - xValues[i + 1]
            //    );

            //    var angleDelta = Math.Abs(angle - refAngle);

            //    if (angleDelta < angleTolerance) continue;

            //    indexList.Add(i);

            //    refAngle = angle;
            //}

            return CreateContinuous(
                indexList.Select(i => xValues[i]).ToImmutableArray(),
                indexList.Select(i => yValues[i]).ToImmutableArray()
            );
        }

        public static PiecewiseAffineFunction CreateContinuous(Func<double, double> smoothFunc, double minX, double maxX, int sampleCount)
        {
            var xValues = Enumerable.Range(0, sampleCount).Select(
                i =>
                {
                    var t = i / (sampleCount - 1d);
                    return (1d - t) * minX + t * maxX;
                }
            ).ToImmutableArray();

            var yValues =
                xValues.Select(smoothFunc).ToImmutableArray();

            return CreateContinuous(xValues, yValues);
        }

        public static PiecewiseAffineFunction CreateContinuous(Func<double, double> smoothFunc, double minX, double maxX, int sampleCount, double angleTolerance)
        {
            var xValues = Enumerable.Range(0, sampleCount).Select(
                i =>
                {
                    var t = i / (sampleCount - 1d);
                    return (1d - t) * minX + t * maxX;
                }
            ).ToImmutableArray();

            var yValues =
                xValues.Select(smoothFunc).ToImmutableArray();

            return CreateContinuous(xValues, yValues, angleTolerance);
        }


        protected SortedDictionary<double, Breakpoint> BreakpointsDictionary { get; }
            = new SortedDictionary<double, Breakpoint>();


        public int BreakpointCount
            => BreakpointsDictionary.Count;

        public int SampleCount
            => BreakpointsDictionary.Values.Count(p => p.IsDiscontinuous);

        public int SegmentCount
            => BreakpointsDictionary.Count + 1;

        public Breakpoint MinBreakpoint
            => BreakpointsDictionary.Values.First();

        public Breakpoint MaxBreakpoint
            => BreakpointsDictionary.Values.Last();

        public double MinBreakpointX
            => BreakpointsDictionary.Values.First().X;

        public double MaxBreakpointX
            => BreakpointsDictionary.Values.Last().X;

        public double LengthX
            => MaxBreakpointX - MinBreakpointX;

        public bool IsFinite
            => MinBreakpoint.YMinus.Equals(0d) &&
               MaxBreakpoint.YPlus.Equals(0d);

        public IEnumerable<double> XValues
            => BreakpointsDictionary.Values.Select(b => b.X);

        public IEnumerable<double> YValues
            => BreakpointsDictionary.Values.Select(b => b.Y);

        public IEnumerable<double> DeltaXValues
        {
            get
            {
                var p0 = 
                    BreakpointsDictionary.Values.First();

                foreach (var p1 in BreakpointsDictionary.Values.Skip(1))
                {
                    yield return p1.X - p0.X;

                    p0 = p1;
                }
            }
        }
        
        public IEnumerable<double> DeltaYValues
        {
            get
            {
                var p0 = 
                    BreakpointsDictionary.Values.First();

                foreach (var p1 in BreakpointsDictionary.Values.Skip(1))
                {
                    yield return p1.YMinus - p0.YPlus;

                    p0 = p1;
                }
            }
        }

        public double MinYValue
            => YValues.Min();

        public double MaxYValue
            => YValues.Max();
        
        public double MinAbsYValue
            => YValues.Min(Math.Abs);
        
        public double MaxAbsYValue
            => YValues.Max(Math.Abs);

        public IEnumerable<Breakpoint> Breakpoints
            => BreakpointsDictionary.Values;

        public IEnumerable<Sample> Samples
            => Breakpoints
                .Where(p => p.IsDiscontinuous)
                .Select(p => new Sample(p.X, p.Y));

        public IEnumerable<Segment> Segments
        {
            get
            {
                var breakpoint1 = MinBreakpoint;

                yield return new Segment(
                    double.NegativeInfinity,
                    breakpoint1.X,
                    breakpoint1.YMinus,
                    breakpoint1.YMinus
                );

                foreach (var breakpoint2 in BreakpointsDictionary.Values.Skip(1))
                {
                    yield return new Segment(
                        breakpoint1.X,
                        breakpoint2.X,
                        breakpoint1.YPlus,
                        breakpoint2.YMinus
                    );

                    breakpoint1 = breakpoint2;
                }

                yield return new Segment(
                    breakpoint1.X,
                    double.PositiveInfinity,
                    breakpoint1.YPlus,
                    breakpoint1.YPlus
                );
            }
        }
        
        public IEnumerable<Segment> FiniteSegments
        {
            get
            {
                var breakpoint1 = MinBreakpoint;

                foreach (var breakpoint2 in BreakpointsDictionary.Values)
                {
                    yield return new Segment(
                        breakpoint1.X,
                        breakpoint2.X,
                        breakpoint1.YPlus,
                        breakpoint2.YMinus
                    );

                    breakpoint1 = breakpoint2;
                }
            }
        }

        
        public PiecewiseAffineFunction InsertBreakpoint(double x)
        {
            if (BreakpointsDictionary.ContainsKey(x))
                return this;

            var y = GetValue(x);

            return InsertBreakpoint(x, y);
        }

        public PiecewiseAffineFunction InsertBreakpoint(double x, double y)
        {
            var breakpoint = Breakpoint.Create(x, y);

            if (BreakpointsDictionary.ContainsKey(x))
                BreakpointsDictionary[x] = breakpoint;
            else
                BreakpointsDictionary.Add(x, breakpoint);

            return this;
        }

        public PiecewiseAffineFunction InsertBreakpoint(double x, double yMinus, double y, double yPlus)
        {
            var breakpoint = Breakpoint.Create(x, yMinus, y, yPlus);

            if (BreakpointsDictionary.ContainsKey(x))
                BreakpointsDictionary[x] = breakpoint;
            else
                BreakpointsDictionary.Add(x, breakpoint);

            return this;
        }
        
        public PiecewiseAffineFunction InsertBreakpoint(Breakpoint breakpoint)
        {
            if (BreakpointsDictionary.ContainsKey(breakpoint.X))
                BreakpointsDictionary[breakpoint.X] = breakpoint;
            else
                BreakpointsDictionary.Add(breakpoint.X, breakpoint);

            return this;
        }

        public PiecewiseAffineFunction InsertBreakpoints(IEnumerable<Breakpoint> breakpointList)
        {
            foreach (var breakpoint in breakpointList)
                InsertBreakpoint(breakpoint);

            return this;
        }

        
        public PiecewiseAffineFunction MakeFinite()
        {
            MinBreakpoint.InPlaceSetYToZero();
            MaxBreakpoint.InPlaceSetYToZero();

            return this;
        }

        public PiecewiseAffineFunction MakeOdd()
        {
            var pList = new List<Breakpoint>(BreakpointsDictionary.Count);

            foreach (var breakpoint in Breakpoints)
            {
                if (breakpoint.X < 0) 
                    throw new InvalidOperationException();
                
                if (breakpoint.X.Equals(0d) && !breakpoint.IsZero)
                    throw new InvalidOperationException();

                pList.Add(breakpoint.NegativeXy());
            }

            InsertBreakpoints(pList);

            return this;
        }

        public PiecewiseAffineFunction ShiftX(double deltaX)
        {
            foreach (var breakpoint in Breakpoints)
                breakpoint.InPlaceShiftX(deltaX);

            return this;
        }

        public PiecewiseAffineFunction ScaleY(double scalingFactor)
        {
            foreach (var breakpoint in Breakpoints)
                breakpoint.InPlaceScaleY(scalingFactor);

            return this;
        }

        public PiecewiseAffineFunction MakeEven()
        {
            var pList = new List<Breakpoint>(BreakpointsDictionary.Count);

            foreach (var breakpoint in Breakpoints)
            {
                if (breakpoint.X < 0) 
                    throw new InvalidOperationException();

                if (breakpoint.X.Equals(0d) && !breakpoint.IsSymmetric)
                    throw new InvalidOperationException();

                pList.Add(breakpoint.NegativeX());
            }

            InsertBreakpoints(pList);

            return this;
        }

        public double GetValue(double x)
        {
            if (double.IsNaN(x))
                throw new ArgumentException(nameof(x));

            if (BreakpointsDictionary.Count == 0)
                return 0d;

            var minBreakpoint = BreakpointsDictionary.Values.First();

            if (x < minBreakpoint.X)
                return minBreakpoint.YMinus;

            var maxBreakpoint = BreakpointsDictionary.Values.Last();

            if (x > maxBreakpoint.X)
                return maxBreakpoint.YPlus;

            if (BreakpointsDictionary.TryGetValue(x, out var breakpoint))
                return breakpoint.Y;

            // This is highly inefficient, but works for now
            var breakpoint1 = minBreakpoint;
            foreach (var breakpoint2 in BreakpointsDictionary.Values.Skip(1))
            {
                if (x > breakpoint1.X && x < breakpoint2.X)
                {
                    var t = (x - breakpoint1.X) / (breakpoint2.X - breakpoint1.X);

                    return (1d - t) * breakpoint1.YPlus + t * breakpoint2.YMinus;
                }

                breakpoint1 = breakpoint2;
            }

            return 0.0;
        }

        //public IEnumerable<Segment> GetSegments(double minX, double maxX)
        //{
        //    if (minX.IsNaNOrInfinite() || maxX.IsNaNOrInfinite() || minX.Equals(maxX))
        //        throw new InvalidOperationException();

        //    var (x1, x2) = 
        //        minX < maxX ? (minX, maxX) : (maxX, minX);

        //    var segmentList = new List<Segment>();

        //    var segments = 
        //        Segments.Where(s => 
        //            s.X1 < x2 && s.X2 > x1
        //        );

        //    foreach (var segment in segments)
        //    {
        //        if (x1 > segment.X1 && x1 < segment.X2)

        //    }

        //    return segmentList;
        //}

        public double GetArea()
        {
            return IsFinite 
                ? Segments.Select(s => s.GetArea()).Sum()
                : double.PositiveInfinity;
        }

        public string GetMatlabCode(bool showMarkers = true)
        {
            return GetMatlabCode(
                MinBreakpointX - 0.1d * LengthX,
                MaxBreakpointX + 0.1d * LengthX,
                showMarkers
            );
        }

        public string GetMatlabCode(double minValue, double maxValue, bool showMarkers = true)
        {
            var composer = new StringBuilder();

            if (minValue.IsNaNOrInfinite() || maxValue.IsNaNOrInfinite())
                throw new InvalidOperationException();

            minValue = Math.Min(MinBreakpointX - 0.1d * LengthX, minValue);
            maxValue = Math.Max(MaxBreakpointX + 0.1d * LengthX, maxValue);

            var segmentList = Segments.ToArray();

            segmentList[0] = segmentList[0].SetX1(minValue);
            segmentList[^1] = segmentList[^1].SetX2(maxValue);

            {
                var x1Text = segmentList.Select(s => s.X1.ToString("G")).Concatenate(", ");
                var x2Text = segmentList.Select(s => s.X2.ToString("G")).Concatenate(", ");
                var y1Text = segmentList.Select(s => s.Y1.ToString("G")).Concatenate(", ");
                var y2Text = segmentList.Select(s => s.Y2.ToString("G")).Concatenate(", ");

                composer.AppendLine(
                    $"plot([{x1Text}; {x2Text}], [{y1Text}; {y2Text}], 'Color', [0,0,0], 'LineWidth', 2);"
                ).AppendLine("hold on;");
            }

            //var breakPoints = 
            //    Breakpoints.ToImmutableArray();

            //if (breakPoints.Length > 0)
            //{
            //    var x1Text = breakPoints.Select(p => p.X.ToString("G")).Concatenate(", ");
            //    var y1Text = breakPoints.Select(p => p.Y.ToString("G")).Concatenate(", ");

            //    composer.AppendLine(
            //        $"plot([{x1Text}], [{y1Text}], 'Color', [0,0,0], 'LineStyle', 'none', 'LineWidth', 1, 'Marker', 'o', 'MarkerSize', 4, 'MarkerEdgeColor', [0,0,0], 'MarkerFaceColor', [0,0,0]);"
            //    );
            //}

            var breakPoints =
                Breakpoints.Where(b => b.IsDiscontinuous).ToImmutableArray();

            if (showMarkers && breakPoints.Length > 0)
            {
                var x1Text = breakPoints.Select(p => p.X.ToString("G")).Concatenate(", ");
                var y1Text = breakPoints.Select(p => p.YMinus.ToString("G")).Concatenate(", ");
                var y2Text = breakPoints.Select(p => p.YPlus.ToString("G")).Concatenate(", ");
                var y3Text = breakPoints.Select(p => p.Y.ToString("G")).Concatenate(", ");

                composer.AppendLine(
                    $"plot([{x1Text}; {x1Text}], [{y1Text}; {y2Text}], 'Color', [0,0,0], 'LineWidth', 1, 'LineStyle', ':');"
                );

                composer.AppendLine(
                    $"plot([{x1Text}], [{y1Text}], 'Color', [0,0,0], 'LineStyle', 'none', 'LineWidth', 1, 'Marker', 'o', 'MarkerSize', 5, 'MarkerEdgeColor', [0,0,0], 'MarkerFaceColor', [1,1,1]);"
                );

                composer.AppendLine(
                    $"plot([{x1Text}], [{y2Text}], 'Color', [0,0,0], 'LineStyle', 'none', 'LineWidth', 1, 'Marker', 'o', 'MarkerSize', 5, 'MarkerEdgeColor', [0,0,0], 'MarkerFaceColor', [1,1,1]);"
                );

                composer.AppendLine(
                    $"plot([{x1Text}], [{y3Text}], 'Color', [0,0,0], 'LineStyle', 'none', 'LineWidth', 1, 'Marker', 'o', 'MarkerSize', 5, 'MarkerEdgeColor', [0,0,0], 'MarkerFaceColor', [0,0,0]);"
                );
            }

            breakPoints =
                Breakpoints.Where(b => b.IsDiscrete).ToImmutableArray();

            if (breakPoints.Length > 0)
            {
                var x1Text = breakPoints.Select(p => p.X.ToString("G")).Concatenate(", ");
                var y1Text = breakPoints.Select(p => p.Y.ToString("G")).Concatenate(", ");

                composer.AppendLine(
                    $"plot([{x1Text}; {x1Text}], [{y1Text}; zeros(1, {breakPoints.Length})], 'Color', [0,0,0], 'LineWidth', 1);"
                );
            }

            composer.AppendLine("hold off;");

            return composer.ToString();
        }
    }
}
