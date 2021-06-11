using System;
using System.Collections;
using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes;
using EuclideanGeometryLib.Borders;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Mutable;

namespace EuclideanGeometryLib.Accelerators.Grids.Space3D
{
    public class AccGrid3D<T> : IAccGrid3D<T>
        where T : IFiniteGeometricShape3D
    {
        private readonly IReadOnlyList<T> _geometricObjectsList;
        private readonly List<T>[,,] _gridCells;
        private readonly double _invCellLengthX;
        private readonly double _invCellLengthY;
        private readonly double _invCellLengthZ;


        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsValid => true;

        public bool IsInvalid => false;

        public int Count
            => _geometricObjectsList.Count;

        public T this[int index]
            => _geometricObjectsList[index];

        public IReadOnlyList<T> this[int ix, int iy, int iz]
            => _gridCells[ix, iy, iz];

        public BoundingBox3D BoundingBox { get; }

        public int CellsCountX { get; }

        public int CellsCountY { get; }

        public int CellsCountZ { get; }

        public double CellVolume
            => BoundingBox.GetVolume() / CellsCount;

        public int CellsCount
            => CellsCountX * CellsCountY * CellsCountZ;

        public int NonEmptyCellsCount
        {
            get
            {
                var n = 0;

                for (var ix = 0; ix < CellsCountX; ix++)
                for (var iy = 0; iy < CellsCountY; iy++)
                for (var iz = 0; iz < CellsCountZ; iz++)
                {
                    if (_gridCells[ix, iy, iz] != null)
                        n++;
                }

                return n;
            }
        }

        public int EmptyCellsCount
            => CellsCount - NonEmptyCellsCount;

        public double AverageSurfacesPerCell
        {
            get
            {
                var n = 0;
                double k = 0;

                for (var ix = 0; ix < CellsCountX; ix++)
                for (var iy = 0; iy < CellsCountY; iy++)
                for (var iz = 0; iz < CellsCountZ; iz++)
                {
                    if (_gridCells[ix, iy, iz] == null) continue;

                    n++;
                    k += _gridCells[ix, iy, iz].Count;
                }

                return k / n;
            }
        }


        public AccGrid3D(IReadOnlyList<T> geometricObjectsList)
        {
            _geometricObjectsList = geometricObjectsList;

            if (_geometricObjectsList == null || _geometricObjectsList.Count == 0)
            {
                return;
            }

            //Compute bounding box for entire grid
            BoundingBox = BoundingBox3D.Create(
                (IEnumerable<IFiniteGeometricShape3D>)_geometricObjectsList
            );

            var minX = BoundingBox.MinX;
            var minY = BoundingBox.MinY;
            var minZ = BoundingBox.MinZ;
            var maxX = BoundingBox.MaxX;
            var maxY = BoundingBox.MaxY;
            var maxZ = BoundingBox.MaxZ;
            var lengthX = maxX - minX;
            var lengthY = maxY - minY;
            var lengthZ = maxZ - minZ;

            //Setup grid cells
            var s = Math.Pow(BoundingBox.GetVolume() / _geometricObjectsList.Count, -1 / 3.0d);

            CellsCountX = (int)(2.0 * lengthX * s) + 1;
            CellsCountY = (int)(2.0 * lengthY * s) + 1;
            CellsCountZ = (int)(2.0 * lengthZ * s) + 1;

            _invCellLengthX = CellsCountX / lengthX;
            _invCellLengthY = CellsCountY / lengthY;
            _invCellLengthZ = CellsCountZ / lengthZ;

            _gridCells = new List<T>[CellsCountX, CellsCountY, CellsCountZ];

            //For each geometric object find all cells it occupies and put it
            //in all of them
            foreach (var geometricObject in _geometricObjectsList)
            {
                var boundingBox = geometricObject.GetBoundingBox();

                var minCorner =
                    PointToCellIndex(boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ);

                var maxCorner =
                    PointToCellIndex(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MaxZ);

                for (var ix = minCorner.ItemX; ix <= maxCorner.ItemX; ix++)
                for (var iy = minCorner.ItemY; iy <= maxCorner.ItemY; iy++)
                for (var iz = minCorner.ItemZ; iz <= maxCorner.ItemZ; iz++)
                {
                    if (_gridCells[ix, iy, iz] == null)
                        _gridCells[ix, iy, iz] = new List<T>();

                    _gridCells[ix, iy, iz].Add(geometricObject);
                }
            }
        }


        public IntTuple3D PointToCellIndex(double x, double y, double z)
        {
            var ix = ((x - BoundingBox.MinX) * _invCellLengthX).ClampToInt(CellsCountX - 1);
            var iy = ((y - BoundingBox.MinY) * _invCellLengthY).ClampToInt(CellsCountY - 1);
            var iz = ((z - BoundingBox.MinZ) * _invCellLengthZ).ClampToInt(CellsCountZ - 1);

            return new IntTuple3D(ix, iy, iz);
        }

        public int PointXToCellIndex(double x)
        {
            return ((x - BoundingBox.MinX) * _invCellLengthX).ClampToInt(CellsCountX - 1);
        }

        public int PointYToCellIndex(double y)
        {
            return ((y - BoundingBox.MinY) * _invCellLengthY).ClampToInt(CellsCountY - 1);
        }

        public int PointZToCellIndex(double z)
        {
            return ((z - BoundingBox.MinZ) * _invCellLengthZ).ClampToInt(CellsCountZ - 1);
        }

        public BoundingBox3D GetBoundingBox()
        {
            return BoundingBox;
        }

        public MutableBoundingBox3D GetMutableBoundingBox()
        {
            return MutableBoundingBox3D.Create(BoundingBox);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _geometricObjectsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _geometricObjectsList.GetEnumerator();
        }
    }


    //public sealed class RegularGrid3D
    //{
    //    private int _objectsCount = 0;
    //    private List<GoSurface3D> _GeometricObjects = new List<GoSurface3D>(10);
    //    private RegularGridCell3D[,,] _gridCells;
    //    private int _nx, _ny, _nz;
    //    private BoundingBox3Dp _boundingBox = null;



    //    public override BoundingBox3Dp BoundingBox => _boundingBox;

    //    public double CellVolume
    //        => _boundingBox.Volume / ((double)_nx * _ny * _nz);

    //    public int NonEmptyCellCount
    //    {
    //        get
    //        {
    //            var n = 0;

    //            for (var ix = 0; ix < _nx; ix++)
    //            for (var iy = 0; iy < _ny; iy++)
    //            for (var iz = 0; iz < _nz; iz++)
    //            {
    //                if (_gridCells[ix, iy, iz] != null)
    //                    n++;
    //            }

    //            return n;
    //        }
    //    }

    //    public int CellCount => _nx * _ny * _nz;

    //    public double AverageObjectsPerCell
    //    {
    //        get
    //        {
    //            var n = 0;
    //            double k = 0;

    //            for (var ix = 0; ix < _nx; ix++)
    //            for (var iy = 0; iy < _ny; iy++)
    //            for (var iz = 0; iz < _nz; iz++)
    //            {
    //                if (_gridCells[ix, iy, iz] == null) continue;

    //                n++;
    //                k += _gridCells[ix, iy, iz].ObjectsCount;
    //            }

    //            return k / (double)n;
    //        }
    //    }


    //    public RegularGrid3D()
    //    {
    //    }


    //    public override bool AddObject(GoSurface3D Obj)
    //    {
    //        if (_GeometricObjects == null)
    //            throw new Exception("Addition is not allowed after cells are created");

    //        BoundingBox BB = Obj.BoundingBox;

    //        //Grid can not store unbounded objects
    //        if ((object)BB == null) return false;

    //        _GeometricObjects.Add(Obj);

    //        _objectsCount++;

    //        return true;
    //    }

    //    public override int ObjectsCount => _objectsCount;

    //    public override void ComputeBoundingBox()
    //    {
    //        if (_GeometricObjects == null || _GeometricObjects.Count == 0) return;

    //        _boundingBox = _GeometricObjects[0].BoundingBox;

    //        for (var i = 0; i < _GeometricObjects.Count; i++)
    //            _boundingBox.ExpandToInclude(_GeometricObjects[i].BoundingBox);

    //        //_BoundingBox.EnlargeByPercent(10);
    //    }

    //    public override void SetupStructure()
    //    {
    //        if (_GeometricObjects.Count == 0) return;

    //        this.ComputeBoundingBox();

    //        Point3D p0 = _boundingBox.Corner0;
    //        Point3D p1 = _boundingBox.Corner1;
    //        double s = Math.Pow(_boundingBox.Volume / _GeometricObjects.Count, -1.0 / 3.0);

    //        _nx = (int)(2.0 * _boundingBox.LengthX * s) + 1;
    //        _ny = (int)(2.0 * _boundingBox.LengthY * s) + 1;
    //        _nz = (int)(2.0 * _boundingBox.LengthZ * s) + 1;

    //        _gridCells = new GridCell[_nx, _ny, _nz];

    //        for (int i = 0; i < _GeometricObjects.Count; i++)
    //        {
    //            GoSurface3D Obj = _GeometricObjects[i];
    //            BoundingBox BB = Obj.BoundingBox;

    //            int ixMin = (int)Utility.Clamp((BB.x0 - p0.x) * (double)_nx / _boundingBox.LengthX, 0, (double)_nx - 1.0);
    //            int iyMin = (int)Utility.Clamp((BB.y0 - p0.y) * (double)_ny / _boundingBox.LengthY, 0, (double)_ny - 1.0);
    //            int izMin = (int)Utility.Clamp((BB.z0 - p0.z) * (double)_nz / _boundingBox.LengthZ, 0, (double)_nz - 1.0);
    //            int ixMax = (int)Utility.Clamp((BB.x1 - p0.x) * (double)_nx / _boundingBox.LengthX, 0, (double)_nx - 1.0);
    //            int iyMax = (int)Utility.Clamp((BB.y1 - p0.y) * (double)_ny / _boundingBox.LengthY, 0, (double)_ny - 1.0);
    //            int izMax = (int)Utility.Clamp((BB.z1 - p0.z) * (double)_nz / _boundingBox.LengthZ, 0, (double)_nz - 1.0);

    //            for (int ix = ixMin; ix <= ixMax; ix++)
    //                for (int iy = iyMin; iy <= iyMax; iy++)
    //                    for (int iz = izMin; iz <= izMax; iz++)
    //                    {
    //                        if (_gridCells[ix, iy, iz] == null)
    //                            _gridCells[ix, iy, iz] = new GridCell();

    //                        _gridCells[ix, iy, iz].AddObject(Obj);
    //                    }
    //        }

    //        _GeometricObjects = null;

    //        cAppLogger.WriteLine("Number of cells: " + this.CellCount.ToString());
    //        cAppLogger.WriteLine("Non empty cells: " + this.NonEmptyCellCount.ToString());
    //        cAppLogger.WriteLine("Cell volume: " + this.CellVolume.ToString());
    //        cAppLogger.WriteLine("Average objects per cell: " + this.AvgObjectsPerCell.ToString());
    //    }

    //    protected override bool Hit(Ray ray, out double tmin)
    //    {
    //        double ox = ray.ox;
    //        double oy = ray.oy;
    //        double oz = ray.oz;
    //        double dx = ray.dx;
    //        double dy = ray.dy;
    //        double dz = ray.dz;

    //        double x0 = _boundingBox.x0;
    //        double y0 = _boundingBox.y0;
    //        double z0 = _boundingBox.z0;
    //        double x1 = _boundingBox.x1;
    //        double y1 = _boundingBox.y1;
    //        double z1 = _boundingBox.z1;

    //        double tx_min, ty_min, tz_min;
    //        double tx_max, ty_max, tz_max;

    //        tmin = 0;

    //        // the following code includes modifications from Shirley and Morley (2003)

    //        double a = 1.0 / dx;
    //        if (a >= 0)
    //        {
    //            tx_min = (x0 - ox) * a;
    //            tx_max = (x1 - ox) * a;
    //        }
    //        else
    //        {
    //            tx_min = (x1 - ox) * a;
    //            tx_max = (x0 - ox) * a;
    //        }

    //        double b = 1.0 / dy;
    //        if (b >= 0)
    //        {
    //            ty_min = (y0 - oy) * b;
    //            ty_max = (y1 - oy) * b;
    //        }
    //        else
    //        {
    //            ty_min = (y1 - oy) * b;
    //            ty_max = (y0 - oy) * b;
    //        }

    //        double c = 1.0 / dz;
    //        if (c >= 0)
    //        {
    //            tz_min = (z0 - oz) * c;
    //            tz_max = (z1 - oz) * c;
    //        }
    //        else
    //        {
    //            tz_min = (z1 - oz) * c;
    //            tz_max = (z0 - oz) * c;
    //        }

    //        double t0, t1;

    //        if (tx_min > ty_min)
    //            t0 = tx_min;
    //        else
    //            t0 = ty_min;

    //        if (tz_min > t0)
    //            t0 = tz_min;

    //        if (tx_max < ty_max)
    //            t1 = tx_max;
    //        else
    //            t1 = ty_max;

    //        if (tz_max < t1)
    //            t1 = tz_max;

    //        if (t0 > t1)
    //            return false;


    //        // initial cell coordinates

    //        int ix, iy, iz;

    //        if (_boundingBox.IsPointInside(ray.Origin))
    //        {  			// does the ray start inside the grid?
    //            ix = (int)Utility.Clamp((ox - x0) * (double)_nx / (x1 - x0), 0, (double)_nx - 1.0);
    //            iy = (int)Utility.Clamp((oy - y0) * (double)_ny / (y1 - y0), 0, (double)_ny - 1.0);
    //            iz = (int)Utility.Clamp((oz - z0) * (double)_nz / (z1 - z0), 0, (double)_nz - 1.0);
    //        }
    //        else
    //        {
    //            Point3D p = ray[t0];  // initial hit point with grid's bounding box
    //            ix = (int)Utility.Clamp((p.x - x0) * (double)_nx / (x1 - x0), 0, (double)_nx - 1.0);
    //            iy = (int)Utility.Clamp((p.y - y0) * (double)_ny / (y1 - y0), 0, (double)_ny - 1.0);
    //            iz = (int)Utility.Clamp((p.z - z0) * (double)_nz / (z1 - z0), 0, (double)_nz - 1.0);
    //        }

    //        // ray parameter increments per cell in the x, y, and z directions

    //        double dtx = (tx_max - tx_min) / (double)_nx;
    //        double dty = (ty_max - ty_min) / (double)_ny;
    //        double dtz = (tz_max - tz_min) / (double)_nz;

    //        double tx_next, ty_next, tz_next;
    //        int ix_step, iy_step, iz_step;
    //        int ix_stop, iy_stop, iz_stop;

    //        if (dx > 0)
    //        {
    //            tx_next = tx_min + (double)(ix + 1) * dtx;
    //            ix_step = +1;
    //            ix_stop = _nx;
    //        }
    //        else if (dx < 0)
    //        {
    //            tx_next = tx_min + (double)(_nx - ix) * dtx;
    //            ix_step = -1;
    //            ix_stop = -1;
    //        }
    //        else
    //        {
    //            tx_next = double.MaxValue;
    //            ix_step = -1;
    //            ix_stop = -1;
    //        }


    //        if (dy > 0)
    //        {
    //            ty_next = ty_min + (double)(iy + 1) * dty;
    //            iy_step = +1;
    //            iy_stop = _ny;
    //        }
    //        else if (dy < 0)
    //        {
    //            ty_next = ty_min + (double)(_ny - iy) * dty;
    //            iy_step = -1;
    //            iy_stop = -1;
    //        }
    //        else
    //        {
    //            ty_next = double.MaxValue;
    //            iy_step = -1;
    //            iy_stop = -1;
    //        }

    //        if (dz > 0)
    //        {
    //            tz_next = tz_min + (double)(iz + 1) * dtz;
    //            iz_step = +1;
    //            iz_stop = _nz;
    //        }
    //        else if (dz < 0)
    //        {
    //            tz_next = tz_min + (double)(_nz - iz) * dtz;
    //            iz_step = -1;
    //            iz_stop = -1;
    //        }
    //        else
    //        {
    //            tz_next = double.MaxValue;
    //            iz_step = -1;
    //            iz_stop = -1;
    //        }


    //        // traverse the grid

    //        while (true)
    //        {
    //            var object_ptr = _gridCells[ix, iy, iz];

    //            if (tx_next < ty_next && tx_next < tz_next)
    //            {
    //                if (object_ptr != null &&
    //                    object_ptr.Hit(ray, out tmin) &&
    //                    tmin < tx_next)
    //                {
    //                    return true;
    //                }

    //                tx_next += dtx;
    //                ix += ix_step;

    //                if (ix == ix_stop)
    //                    return false;
    //            }
    //            else if (ty_next < tz_next)
    //            {
    //                if (object_ptr != null &&
    //                    object_ptr.Hit(ray, out tmin) &&
    //                    tmin < ty_next)
    //                {
    //                    return true;
    //                }

    //                ty_next += dty;
    //                iy += iy_step;

    //                if (iy == iy_stop)
    //                    return false;
    //            }
    //            else
    //            {
    //                if (object_ptr != null &&
    //                    object_ptr.Hit(ray, out tmin) &&
    //                    tmin < tz_next)
    //                {
    //                    return true;
    //                }

    //                tz_next += dtz;
    //                iz += iz_step;

    //                if (iz == iz_stop)
    //                    return false;
    //            }
    //        }
    //    }
    //}
}
