using System.Collections;
using DataStructuresLib.Basic;
using TextComposerLib.Text;

namespace WebComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandSequence : 
        SvgPathCommand,
        IReadOnlyList<SvgPathCommandSimple>
    {
        public static SvgPathCommandSequence Create()
        {
            return new SvgPathCommandSequence();
        }

        public static SvgPathCommandSequence Create(SvgPathCommandSimple command)
        {
            return new SvgPathCommandSequence().AppendCommand(command);
        }

        public static SvgPathCommandSequence Create(params SvgPathCommandSimple[] commandsList)
        {
            return new SvgPathCommandSequence().AppendCommands(commandsList);
        }

        public static SvgPathCommandSequence Create(IEnumerable<SvgPathCommandSimple> commandsList)
        {
            return new SvgPathCommandSequence().AppendCommands(commandsList);
        }


        private readonly List<SvgPathCommandSimple> _commandsList
            = new List<SvgPathCommandSimple>();


        public SvgPathCommandSimple this[int index]
        {
            get => _commandsList[index];
            set => _commandsList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public override string ValueText
            => _commandsList
                .Select(c => c.ValueText)
                .Concatenate(" ");
        
        public int Count 
            => _commandsList.Count;


        private SvgPathCommandSequence()
        {
        }


        public SvgPathCommandSequence Clear()
        {
            _commandsList.Clear();

            return this;
        }

        public SvgPathCommandSequence AppendCommand(SvgPathCommandSimple command)
        {
            _commandsList.Add(command);

            return this;
        }

        public SvgPathCommandSequence AppendCommands(params SvgPathCommandSimple[] commandsList)
        {
            _commandsList.AddRange(commandsList);

            return this;
        }

        public SvgPathCommandSequence AppendCommands(IEnumerable<SvgPathCommandSimple> commandsList)
        {
            _commandsList.AddRange(commandsList);

            return this;
        }
        
        public SvgPathCommandSequence PrependCommand(SvgPathCommandSimple command)
        {
            _commandsList.Insert(0, command);

            return this;
        }

        public SvgPathCommandSequence PrependCommands(params SvgPathCommandSimple[] commandsList)
        {
            _commandsList.InsertRange(
                0,
                commandsList
            );

            return this;
        }

        public SvgPathCommandSequence PrependCommands(IEnumerable<SvgPathCommandSimple> commandsList)
        {
            _commandsList.InsertRange(
                0,
                commandsList
            );

            return this;
        }

        public SvgPathCommandSequence InsertCommand(int index, SvgPathCommandSimple command)
        {
            _commandsList.Insert(index, command);

            return this;
        }

        public SvgPathCommandSequence InsertCommands(int index, params SvgPathCommandSimple[] commandsList)
        {
            _commandsList.InsertRange(
                index,
                commandsList
            );

            return this;
        }

        public SvgPathCommandSequence InsertCommands(int index, IEnumerable<SvgPathCommandSimple> commandsList)
        {
            _commandsList.InsertRange(
                index,
                commandsList
            );

            return this;
        }

        
        public SvgPathCommandSequence MoveTo(double x, double y)
        {
            return AppendCommand(
                SvgPathCommandMoveTo.CreateAbsolute(x, y)
            );
        }

        public SvgPathCommandSequence MoveTo(IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandMoveTo.CreateAbsolute(endPoint)
            );
        }
        
        public SvgPathCommandSequence MoveToRelative(double x, double y)
        {
            return AppendCommand(
                SvgPathCommandMoveTo.CreateRelative(x, y)
            );
        }
        
        public SvgPathCommandSequence MoveToRelative(IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandMoveTo.CreateRelative(endPoint)
            );
        }


        public SvgPathCommandSequence LineTo(double x, double y)
        {
            return AppendCommand(
                SvgPathCommandLineTo.CreateAbsolute(x, y)
            );
        }
        
        public SvgPathCommandSequence LineTo(IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandLineTo.CreateAbsolute(endPoint)
            );
        }

        public SvgPathCommandSequence LineTo(params IPair<double>[] endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandLineTo.CreateAbsolute)
            );
        }
        
        public SvgPathCommandSequence LineTo(IEnumerable<IPair<double>> endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandLineTo.CreateAbsolute)
            );
        }
        
        public SvgPathCommandSequence LineToRelative(double x, double y)
        {
            return AppendCommand(
                SvgPathCommandLineTo.CreateRelative(x, y)
            );
        }

        public SvgPathCommandSequence LineToRelative(IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandLineTo.CreateRelative(endPoint)
            );
        }
        
        public SvgPathCommandSequence LineToRelative(params IPair<double>[] endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandLineTo.CreateRelative)
            );
        }

        public SvgPathCommandSequence LineToRelative(IEnumerable<IPair<double>> endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandLineTo.CreateRelative)
            );
        }
        

        public SvgPathCommandSequence HorizontalLineTo(double x)
        {
            return AppendCommand(
                SvgPathCommandHorizontalLineTo.CreateAbsolute(x)
            );
        }
        
        public SvgPathCommandSequence HorizontalLineToRelative(double x)
        {
            return AppendCommand(
                SvgPathCommandHorizontalLineTo.CreateRelative(x)
            );
        }


        public SvgPathCommandSequence VerticalLineTo(double y)
        {
            return AppendCommand(
                SvgPathCommandVerticalLineTo.CreateAbsolute(y)
            );
        }
        
        public SvgPathCommandSequence VerticalLineToRelative(double y)
        {
            return AppendCommand(
                SvgPathCommandVerticalLineTo.CreateRelative(y)
            );
        }
        
        
        public SvgPathCommandSequence CubicBezierTo(IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandCubicBezierTo.CreateAbsolute(startControlPoint, endControlPoint, endPoint)
            );
        }
        
        public SvgPathCommandSequence CubicBezierToRelative(IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandCubicBezierTo.CreateRelative(startControlPoint, endControlPoint, endPoint)
            );
        }

        
        public SvgPathCommandSequence SmoothCubicBezierTo(IPair<double> endControlPoint, IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandSmoothCubicBezierTo.CreateAbsolute(endControlPoint, endPoint)
            );
        }
        
        public SvgPathCommandSequence SmoothCubicBezierToRelative(IPair<double> endControlPoint, IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandSmoothCubicBezierTo.CreateRelative(endControlPoint, endPoint)
            );
        }


        public SvgPathCommandSequence QuadraticBezierTo(IPair<double> controlPoint, IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandQuadraticBezierTo.CreateAbsolute(controlPoint, endPoint)
            );
        }
        
        public SvgPathCommandSequence QuadraticBezierToRelative(IPair<double> controlPoint, IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandQuadraticBezierTo.CreateRelative(controlPoint, endPoint)
            );
        }


        public SvgPathCommandSequence SmoothQuadraticBezierTo(IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandSmoothQuadraticBezierTo.CreateAbsolute(endPoint)
            );
        }
        
        public SvgPathCommandSequence SmoothQuadraticBezierTo(params IPair<double>[] endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandSmoothQuadraticBezierTo.CreateAbsolute)
            );
        }

        public SvgPathCommandSequence SmoothQuadraticBezierTo(IEnumerable<IPair<double>> endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandSmoothQuadraticBezierTo.CreateAbsolute)
            );
        }

        public SvgPathCommandSequence SmoothQuadraticBezierToRelative(IPair<double> endPoint)
        {
            return AppendCommand(
                SvgPathCommandSmoothQuadraticBezierTo.CreateRelative(endPoint)
            );
        }
        
        public SvgPathCommandSequence SmoothQuadraticBezierToRelative(params IPair<double>[] endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandSmoothQuadraticBezierTo.CreateRelative)
            );
        }

        public SvgPathCommandSequence SmoothQuadraticBezierToRelative(IEnumerable<IPair<double>> endPointList)
        {
            return AppendCommands(
                endPointList.Select(SvgPathCommandSmoothQuadraticBezierTo.CreateRelative)
            );
        }


        public SvgPathCommandSequence ArcTo(double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
        {
            return AppendCommand(
                SvgPathCommandArc.CreateAbsolute(
                    radiusX, 
                    radiusY, 
                    xRotationAngle, 
                    largeArcFlag, 
                    sweepFlag, 
                    point
                )
            );
        }
        
        public SvgPathCommandSequence ArcToRelative(double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
        {
            return AppendCommand(
                SvgPathCommandArc.CreateRelative(
                    radiusX, 
                    radiusY, 
                    xRotationAngle, 
                    largeArcFlag, 
                    sweepFlag, 
                    point
                )
            );
        }



        public SvgPathCommandSequence ClosePath()
        {
            return AppendCommand(
                SvgPathCommandClosePath.DefaultCommand
            );
        }


        public IEnumerator<SvgPathCommandSimple> GetEnumerator()
        {
            return _commandsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
