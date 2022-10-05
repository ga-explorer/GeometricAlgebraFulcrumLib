namespace GraphicsComposerLib.Rendering.GraphViz.Dot.Value
{
    /// <summary>
    /// This class represents a node shape value and contains enumeration of GraphViz node shapes
    /// See http://www.graphviz.org/content/node-shapes for more details
    /// </summary>
    public sealed class DotNodeShape : DotStoredValue
    {
        public static readonly DotNodeShape Box = new DotNodeShape("box");
        public static readonly DotNodeShape Polygon = new DotNodeShape("polygon");
        public static readonly DotNodeShape Ellipse = new DotNodeShape("ellipse");
        public static readonly DotNodeShape Oval = new DotNodeShape("oval");
        public static readonly DotNodeShape Circle = new DotNodeShape("circle");
        public static readonly DotNodeShape Point = new DotNodeShape("point");
        public static readonly DotNodeShape Egg = new DotNodeShape("egg");
        public static readonly DotNodeShape Triangle = new DotNodeShape("triangle");
        public static readonly DotNodeShape PlainText = new DotNodeShape("plaintext");
        public static readonly DotNodeShape Plain = new DotNodeShape("plain");
        public static readonly DotNodeShape Diamond = new DotNodeShape("diamond");
        public static readonly DotNodeShape Trapezium = new DotNodeShape("trapezium");
        public static readonly DotNodeShape Parallelogram = new DotNodeShape("parallelogram");
        public static readonly DotNodeShape House = new DotNodeShape("house");
        public static readonly DotNodeShape Pentagon = new DotNodeShape("pentagon");
        public static readonly DotNodeShape Hexagon = new DotNodeShape("hexagon");
        public static readonly DotNodeShape Septagon = new DotNodeShape("septagon");
        public static readonly DotNodeShape Octagon = new DotNodeShape("octagon");
        public static readonly DotNodeShape DoubleCircle = new DotNodeShape("doublecircle");
        public static readonly DotNodeShape DoubleOctagon = new DotNodeShape("doubleoctagon");
        public static readonly DotNodeShape TripleOctagon = new DotNodeShape("tripleoctagon");
        public static readonly DotNodeShape InvTriangle = new DotNodeShape("invtriangle");
        public static readonly DotNodeShape InvTrapezium = new DotNodeShape("invtrapezium");
        public static readonly DotNodeShape InvHouse = new DotNodeShape("invhouse");
        public static readonly DotNodeShape MDiamond = new DotNodeShape("Mdiamond");
        public static readonly DotNodeShape MSquare = new DotNodeShape("Msquare");
        public static readonly DotNodeShape MCircle = new DotNodeShape("Mcircle");
        public static readonly DotNodeShape Rect = new DotNodeShape("rect");
        public static readonly DotNodeShape Rectangle = new DotNodeShape("rectangle");
        public static readonly DotNodeShape Square = new DotNodeShape("square");
        public static readonly DotNodeShape Star = new DotNodeShape("star");
        public static readonly DotNodeShape None = new DotNodeShape("none");
        public static readonly DotNodeShape Underline = new DotNodeShape("underline");
        public static readonly DotNodeShape Note = new DotNodeShape("note");
        public static readonly DotNodeShape Tab = new DotNodeShape("tab");
        public static readonly DotNodeShape Folder = new DotNodeShape("folder");
        public static readonly DotNodeShape Box3D = new DotNodeShape("box3d");
        public static readonly DotNodeShape Component = new DotNodeShape("component");
        public static readonly DotNodeShape Promoter = new DotNodeShape("promoter");
        public static readonly DotNodeShape Cds = new DotNodeShape("cds");
        public static readonly DotNodeShape Terminator = new DotNodeShape("terminator");
        public static readonly DotNodeShape Utr = new DotNodeShape("utr");
        public static readonly DotNodeShape PrimerSite = new DotNodeShape("primersite");
        public static readonly DotNodeShape RestrictionSite = new DotNodeShape("restrictionsite");
        public static readonly DotNodeShape FivePoverhang = new DotNodeShape("fivepoverhang");
        public static readonly DotNodeShape ThreePoverhang = new DotNodeShape("threepoverhang");
        public static readonly DotNodeShape Noverhang = new DotNodeShape("noverhang");
        public static readonly DotNodeShape Assembly = new DotNodeShape("assembly");
        public static readonly DotNodeShape Signature = new DotNodeShape("signature");
        public static readonly DotNodeShape Insulator = new DotNodeShape("insulator");
        public static readonly DotNodeShape RiboSite = new DotNodeShape("ribosite");
        public static readonly DotNodeShape RnasTab = new DotNodeShape("rnastab");
        public static readonly DotNodeShape ProteaseSite = new DotNodeShape("proteasesite");
        public static readonly DotNodeShape ProteinsTab = new DotNodeShape("proteinstab");
        public static readonly DotNodeShape RPromoter = new DotNodeShape("rpromoter");
        public static readonly DotNodeShape RArrow = new DotNodeShape("rarrow");
        public static readonly DotNodeShape LArrow = new DotNodeShape("larrow");
        public static readonly DotNodeShape LPromoter = new DotNodeShape("lpromoter");


        private DotNodeShape(string value)
            : base(value)
        {
        }
    }
}
