﻿using System;
using System.Collections.Generic;
using System.Linq;

using SharpGLTF.Schema2;

using SCHEMA2NODE = SharpGLTF.Scenes.Schema2SceneBuilder.IOperator<SharpGLTF.Schema2.Node>;
using SCHEMA2SCENE = SharpGLTF.Scenes.Schema2SceneBuilder.IOperator<SharpGLTF.Schema2.Scene>;

namespace SharpGLTF.Scenes
{
    /// <summary>
    /// Groups instances of the same mesh being attached to the same node.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Rigid Node[{_DebugName,nq}] = GpuMeshInstances[{_Children.Count}]")]
    internal readonly struct _MeshInstancing : SCHEMA2SCENE
    {
        #region debug

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _DebugName => string.IsNullOrWhiteSpace(_ParentNode.Name) ? "*" : _ParentNode.Name;

        #endregion

        #region lifecycle

        /// <summary>
        /// Groups a collection of <see cref="FixedTransformer"/> items into a sequence of <see cref="_MeshInstancing"/>.
        /// </summary>
        /// <param name="instances">The input instances.</param>
        /// <param name="gpuMinCount">The minimum number of instances required to enable gpu mesh instancing extension.</param>
        /// <returns>A collection of grouped instances.</returns>
        public static IEnumerable<SCHEMA2SCENE> CreateFrom(IEnumerable<FixedTransformer> instances, int gpuMinCount)
        {
            // gather all FixedTransformers with renderables

            var renderables = instances.Where(item => item.HasRenderableContent);

            // gather all renderables attached to the scene root.

            var nullParentGroup = renderables
                .Where(item => item.ParentNode == null)
                .GroupBy(item => (IRenderableContent)item.Content);

            foreach (var nullParentAndSameContent in nullParentGroup)
            {
                yield return new _MeshInstancing(null, nullParentAndSameContent, gpuMinCount);
            }

            // gather all renderables attached to the same child NodeBuilder.

            var sameParentGroup = renderables
                .Where(item => item.ParentNode != null)
                .GroupBy(item => item.ParentNode);

            foreach (var sameParent in sameParentGroup)
            {
                var sameParentAndSameContent = sameParent.GroupBy(item => (IRenderableContent)item.Content);

                foreach (var group in sameParentAndSameContent)
                {
                    yield return new _MeshInstancing(sameParent.Key, group, gpuMinCount);
                }
            }
        }

        private _MeshInstancing(NodeBuilder parentNode, IEnumerable<FixedTransformer> children, int gpuMinCount)
        {
            System.Diagnostics.Debug.Assert(children.All(item => item.ParentNode == parentNode), "all items must have the same parentNode");

            #if DEBUG
            var hasMoreThanOne = children
                .Select(item => item.Content)
                .Cast<IRenderableContent>()
                .Distinct()
                .Skip(1)
                .Any();

            System.Diagnostics.Debug.Assert(!hasMoreThanOne, "Content must be the same for all");
            #endif

            _ParentNode = parentNode;
            _Children = children.ToList();
            _GpuMinCount = gpuMinCount;
        }

        #endregion

        #region data

        private readonly NodeBuilder _ParentNode;

        private readonly IReadOnlyList<FixedTransformer> _Children;

        private readonly int _GpuMinCount;

        #endregion

        #region API

        public void ApplyTo(Scene dstScene, Schema2SceneBuilder context)
        {
            System.Diagnostics.Debug.Assert(_Children.Count > 0);

            if (_ParentNode == null)
            {
                _AddInstances(dstScene, context);
                return;
            }
            else
            {
                var dstNode = context.GetNode(_ParentNode);

                if (Schema2SceneBuilder.HasContent(dstNode))
                {
                    dstNode = dstNode.CreateNode();
                }

                _AddInstances(dstNode, context);
                return;
            }
        }

        private void _AddInstances(IVisualNodeContainer dst, Schema2SceneBuilder context)
        {
            if (_Children.Count < _GpuMinCount)
            {
                foreach (var srcChild in _Children)
                {
                    if (srcChild.Content is SCHEMA2NODE srcOperator)
                    {
                        var dstNode = dst.CreateNode();
                        dstNode.LocalTransform = srcChild.ChildTransform;
                        srcOperator.ApplyTo(dstNode, context);
                        try
                        {
                            System.Diagnostics.Debug.Assert(dstNode.WorldMatrix == srcChild.GetPoseWorldMatrix(), "Transform mismatch NomalMode!");

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            else
            {
                if (_Children.First().Content is SCHEMA2NODE srcOperator)
                {
                    var xforms = _Children
                        .Select(item => item.ChildTransform)
                        .ToList();

                    if (!(dst is Node dstNode)) dstNode = dst.CreateNode();

                    System.Diagnostics.Debug.Assert(dstNode.Mesh == null);
                    System.Diagnostics.Debug.Assert(dstNode.Skin == null);
                    System.Diagnostics.Debug.Assert(dstNode.GetGpuInstancing() == null);

                    srcOperator.ApplyTo(dstNode, context);

                    dstNode
                        .UseGpuInstancing()
                        .WithInstanceAccessors(xforms);

                    #if DEBUG
                    var dstInstances = dstNode.GetGpuInstancing();
                    for (int i = 0; i < _Children.Count; ++i)
                    {
                        var srcXform = _Children[i].GetPoseWorldMatrix();
                        var dstXform = dstInstances.GetWorldMatrix(i);

                        try
                        {
                            System.Diagnostics.Debug.Assert(srcXform == dstXform, "Transform mismatch Instance Mode!");

                        }
                        catch (Exception e )
                        {

                            throw;
                        }
                    }
                    #endif
                }
            }
        }

        #endregion
    }

    partial class RigidTransformer : SCHEMA2SCENE
    {
        void SCHEMA2SCENE.ApplyTo(Scene dstScene, Schema2SceneBuilder context)
        {
            if (Content is SCHEMA2NODE schema2Target)
            {
                var dstNode = context.GetNode(_Node);

                schema2Target.ApplyTo(dstNode, context);

                Schema2SceneBuilder.SetMorphAnimation(dstNode, this.Morphings);

                System.Diagnostics.Debug.Assert(dstNode.WorldMatrix == this.GetPoseWorldMatrix(), "Transform mismatch!");
            }
        }
    }

    partial class SkinnedTransformer : SCHEMA2SCENE
    {
        void SCHEMA2SCENE.ApplyTo(Scene dstScene, Schema2SceneBuilder context)
        {
            if (!(Content is SCHEMA2NODE schema2Target)) return;

            // a skinned mesh is controlled indirectly by its bones,
            // so we need to create a dummy container for it:
            var skinnedMeshNode = dstScene.CreateNode();
            skinnedMeshNode.Name = _NodeName;
            skinnedMeshNode.Extras = _NodeExtras;

            if (_MeshPoseWorldTransform.HasValue)
            {
                var dstNodes = new Node[_Joints.Count];

                for (int i = 0; i < dstNodes.Length; ++i)
                {
                    var (joints, inverseBindMatrix) = _Joints[i];

                    System.Diagnostics.Debug.Assert(!inverseBindMatrix.HasValue);

                    dstNodes[i] = context.GetNode(joints);
                }

                #if DEBUG
                for (int i = 0; i < dstNodes.Length; ++i)
                {
                    var (joints, inverseBindMatrix) = _Joints[i];
                    System.Diagnostics.Debug.Assert(dstNodes[i].WorldMatrix == joints.WorldMatrix);
                }
                #endif

                skinnedMeshNode.WithSkinBinding(_MeshPoseWorldTransform.Value.Matrix, dstNodes);
            }
            else
            {
                var skinnedJoints = _Joints
                .Select(j => (context.GetNode(j.Joint), j.InverseBindMatrix.Value))
                .ToArray();

                skinnedMeshNode.WithSkinBinding(skinnedJoints);
            }

            // set skeleton
            // var root = _Joints[0].Joint.Root;
            // skinnedMeshNode.Skin.Skeleton = context.GetNode(root);

            schema2Target.ApplyTo(skinnedMeshNode, context);
            Schema2SceneBuilder.SetMorphAnimation(skinnedMeshNode, this.Morphings);
        }
    }
}