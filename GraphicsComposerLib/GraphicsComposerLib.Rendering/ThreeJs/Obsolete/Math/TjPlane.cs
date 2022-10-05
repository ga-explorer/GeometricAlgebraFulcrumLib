namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Math
{
    /// <summary>
    /// A two dimensional surface that extends infinitely in 3d space,
    /// represented in Hessian normal form by a unit length normal vector
    /// and a constant.
    /// https://threejs.org/docs/#api/en/math/Plane
    /// </summary>
    public sealed class TjPlane :
        TjComponentSimple
    {
        public override string JavaScriptClassName 
            => "Plane";

        public TjVector3 NormalVector { get; set; }
            = new TjVector3(0, 1, 0);

        public double Constant { get; set; } = 0d;


        protected override string GetConstructorArgumentsText()
        {
            return $"{NormalVector.GetJavaScriptVariableNameOrCode()}, {Constant:G}";
        }

        protected override string GetSetMethodArgumentsText()
        {
            throw new System.NotImplementedException();
        }
    }
}