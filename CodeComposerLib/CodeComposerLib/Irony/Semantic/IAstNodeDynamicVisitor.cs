using DataStructuresLib;

namespace CodeComposerLib.Irony.Semantic
{
    public interface IAstNodeDynamicVisitor : IDynamicTreeVisitor<IIronyAstObject>
    {
    }

    public interface IAstNodeDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<IIronyAstObject, TReturnValue>
    {
    }
}
