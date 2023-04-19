//
// System.Collections.Generic.RBTree
//
// Authors:
//   Raja R Harinath <rharinath@novell.com>
//

//
// Copyright (C) 2007, Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#define ONE_MEMBER_CACHE

using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.ODS
{
	[Serializable]
	internal class RbTree : IEnumerable<RbTree.Node> {
		public interface INodeHelper<in T> {
			int Compare (T key, Node node);
			Node CreateNode (T key);
		}

		public abstract class Node {
			public Node Left, Right;
            private uint _sizeBlack;

            private const uint BlackMask = 1;
            private const int BlackShift = 1;
			public bool IsBlack {
				get => (_sizeBlack & BlackMask) == BlackMask;
                set => _sizeBlack = value ? (_sizeBlack | BlackMask) : (_sizeBlack & ~BlackMask);
            }

			public uint Size {
				get => _sizeBlack >> BlackShift;
                set => _sizeBlack = (value << BlackShift) | (_sizeBlack & BlackMask);
            }

			public uint FixSize ()
			{
				Size = 1;
				if (Left != null)
					Size += Left.Size;
				if (Right != null)
					Size += Right.Size;
				return Size;
			}

			public Node ()
			{
				_sizeBlack = 2; // Size == 1, IsBlack = false
			}

			public abstract void SwapValue (Node other);

#if DEBUG
			public int VerifyInvariants ()
			{
				var blackDepthL = 0;
				var blackDepthR = 0;
				uint size = 1;
				var childIsRed = false;
				if (Left != null) {
					blackDepthL = Left.VerifyInvariants ();
					size += Left.Size;
					childIsRed |= !Left.IsBlack;
				}

				if (Right != null) {
					blackDepthR = Right.VerifyInvariants ();
					size += Right.Size;
					childIsRed |= !Right.IsBlack;
				}

				if (blackDepthL != blackDepthR)
					throw new SystemException ("Internal error: black depth mismatch");

				if (!IsBlack && childIsRed)
					throw new SystemException ("Internal error: red-red conflict");
				if (Size != size)
					throw new SystemException ("Internal error: metadata error");

				return blackDepthL + (IsBlack ? 1 : 0);
			}
#endif
		}

		internal Node Root;
        private object _hlp;
        private uint _version;

#if ONE_MEMBER_CACHE
#if TARGET_JVM
		static readonly LocalDataStoreSlot _cachedPathStore = System.Threading.Thread.AllocateDataSlot ();

		static List<Node> cached_path {
			get { return (List<Node>) System.Threading.Thread.GetData (_cachedPathStore); }
			set { System.Threading.Thread.SetData (_cachedPathStore, value); }
		}
#else
		[ThreadStatic] private static List<Node> _cachedPath;
#endif
        private static List<Node> alloc_path ()
		{
			if (_cachedPath == null)
				return new List<Node> ();

			var path = _cachedPath;
			_cachedPath = null;
			return path;
		}

        private static void release_path (List<Node> path)
		{
			if (_cachedPath == null || _cachedPath.Capacity < path.Capacity) {
				path.Clear ();
				_cachedPath = path;
			}
		}
#else
		static List<Node> alloc_path ()
		{
			return new List<Node> ();
		}

		static void release_path (List<Node> path)
		{
		}
#endif

		public RbTree (object hlp)
		{
			// hlp is INodeHelper<T> for some T
			_hlp = hlp;
		}

		public void Clear ()
		{
			Root = null;
			++_version;
		}

		// if key is already in the tree, return the node associated with it
		// if not, insert new_node into the tree, and return it
		public Node Intern<T> (T key, Node newNode)
		{
			if (Root == null) {
				if (newNode == null)
					newNode = ((INodeHelper<T>) _hlp).CreateNode (key);
				Root = newNode;
				Root.IsBlack = true;
				++_version;
				return Root;
			}

			var path = alloc_path ();
			var inTreeCmp = find_key (key, path);
			var retval = path [^1];
			if (retval == null) {
				if (newNode == null)
					newNode = ((INodeHelper<T>) _hlp).CreateNode (key);
				retval = do_insert (inTreeCmp, newNode, path);
			}
			// no need for a try .. finally, this is only used to mitigate allocations
			release_path (path);
			return retval;
		}

		// returns the just-removed node (or null if the value wasn't in the tree)
		public Node Remove<T> (T key)
		{
			if (Root == null)
				return null;

			var path = alloc_path ();
			var inTreeCmp = find_key (key, path);
			Node retval = null;
			if (inTreeCmp == 0)
				retval = do_remove (path);
			// no need for a try .. finally, this is only used to mitigate allocations
			release_path (path);
			return retval;
		}

		public Node Lookup<T> (T key)
		{
			var hlp = (INodeHelper<T>) _hlp;
			var current = Root;
			while (current != null) {
				var c = hlp.Compare (key, current);
				if (c == 0)
					break;
				current = c < 0 ? current.Left : current.Right;
			}
			return current;
		}

		public void Bound<T> (T key, ref Node lower, ref Node upper)
		{
			var hlp = (INodeHelper<T>) _hlp;
			var current = Root;
			while (current != null) {
				var c = hlp.Compare (key, current);
				if (c <= 0)
					upper = current;
				if (c >= 0)
					lower = current;
				if (c == 0)
					break;
				current = c < 0 ? current.Left : current.Right;
			}
		}

		public int Count => Root == null ? 0 : (int) Root.Size;

        public Node this [int index] {
			get {
				if (index < 0 || index >= Count)
					throw new IndexOutOfRangeException ("index");

				var current = Root;
				while (current != null) {
					var leftSize = current.Left == null ? 0 : (int) current.Left.Size;
					if (index == leftSize)
						return current;
					if (index < leftSize) {
						current = current.Left;
					} else {
						index -= leftSize + 1;
						current = current.Right;
					}
				}
				throw new SystemException ("Internal Error: index calculation");
			}
		}

		public NodeEnumerator GetEnumerator ()
		{
			return new NodeEnumerator (this);
		}

		// Get an enumerator that starts at 'key' or the next higher element in the tree
		public NodeEnumerator GetSuffixEnumerator<T> (T key)
		{
			var pennants = new Stack<Node> ();
			var hlp = (INodeHelper<T>) _hlp;
			var current = Root;
			while (current != null) {
				var c = hlp.Compare (key, current);
				if (c <= 0)
					pennants.Push (current);
				if (c == 0)
					break;
				current = c < 0 ? current.Left : current.Right;
			}
			return new NodeEnumerator (this, pennants);
		}

		IEnumerator<Node> IEnumerable<Node>.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

#if DEBUG
		public void VerifyInvariants ()
		{
			if (Root != null) {
				if (!Root.IsBlack)
					throw new SystemException ("Internal Error: root is not black");
				Root.VerifyInvariants ();
			}
		}
#endif

		// Pre-condition: root != null
        private int find_key<T> (T key, List<Node> path)
		{
			var hlp = (INodeHelper<T>) _hlp;
			var c = 0;
			Node sibling = null;
			var current = Root;

			if (path != null)
				path.Add (Root);

			while (current != null) {
				c = hlp.Compare (key, current);
				if (c == 0)
					return c;

				if (c < 0) {
					sibling = current.Right;
					current = current.Left;
				} else {
					sibling = current.Left;
					current = current.Right;
				}

				if (path != null) {
					path.Add (sibling);
					path.Add (current);
				}
			}

			return c;
		}

        private Node do_insert (int inTreeCmp, Node current, List<Node> path)
		{
			path [^1] = current;
			var parent = path [^3];

			if (inTreeCmp < 0)
				parent.Left = current;
			else
				parent.Right = current;
			for (var i = 0; i < path.Count - 2; i += 2)
				++ path [i].Size;

			if (!parent.IsBlack)
				rebalance_insert (path);

			if (!Root.IsBlack)
				throw new SystemException ("Internal error: root is not black");

			++_version;
			return current;
		}

        private Node do_remove (List<Node> path)
		{
			var curpos = path.Count - 1;

			var current = path [curpos];
			if (current.Left != null) {
				var pred = right_most (current.Left, current.Right, path);
				current.SwapValue (pred);
				if (pred.Left != null) {
					var ppred = pred.Left;
					path.Add (null); path.Add (ppred);
					pred.SwapValue (ppred);
				}
			} else if (current.Right != null) {
				var succ = current.Right;
				path.Add (null); path.Add (succ);
				current.SwapValue (succ);
			}

			curpos = path.Count - 1;
			current = path [curpos];

			if (current.Size != 1)
				throw new SystemException ("Internal Error: red-black violation somewhere");

			// remove it from our data structures
			path [curpos] = null;
			node_reparent (curpos == 0 ? null : path [curpos-2], current, 0, null);

			for (var i = 0; i < path.Count - 2; i += 2)
				-- path [i].Size;

			if (current.IsBlack) {
				current.IsBlack = false;
				if (curpos != 0)
					rebalance_delete (path);
			}

			if (Root != null && !Root.IsBlack)
				throw new SystemException ("Internal Error: root is not black");

			++_version;
			return current;
		}

		// Pre-condition: current is red
        private void rebalance_insert (List<Node> path)
		{
			var curpos = path.Count - 1;
			do {
				// parent == curpos-2, uncle == curpos-3, grandpa == curpos-4
				if (path [curpos-3] == null || path [curpos-3].IsBlack) {
					rebalance_insert__rotate_final (curpos, path);
					return;
				}

				path [curpos-2].IsBlack = path [curpos-3].IsBlack = true;

				curpos -= 4; // move to the grandpa

				if (curpos == 0) // => current == root
					return;
				path [curpos].IsBlack = false;
			} while (!path [curpos-2].IsBlack);
		}

		// Pre-condition: current is black
        private void rebalance_delete (List<Node> path)
		{
			var curpos = path.Count - 1;
			do {
				var sibling = path [curpos-1];
				// current is black => sibling != null
				if (!sibling.IsBlack) {
					// current is black && sibling is red 
					// => both sibling.left and sibling.right are black, and are not null
					curpos = ensure_sibling_black (curpos, path);
					// one of the nephews became the new sibling -- in either case, sibling != null
					sibling = path [curpos-1];
				}

				if ((sibling.Left != null && !sibling.Left.IsBlack) ||
				    (sibling.Right != null && !sibling.Right.IsBlack)) {
					rebalance_delete__rotate_final (curpos, path);
					return;
				}

				sibling.IsBlack = false;

				curpos -= 2; // move to the parent

				if (curpos == 0)
					return;
			} while (path [curpos].IsBlack);
			path [curpos].IsBlack = true;
		}

        private void rebalance_insert__rotate_final (int curpos, List<Node> path)
		{
			var current = path [curpos];
			var parent = path [curpos-2];
			var grandpa = path [curpos-4];

			var grandpaSize = grandpa.Size;

			Node newRoot;

			var l1 = parent == grandpa.Left;
			var l2 = current == parent.Left;
			if (l1 && l2) {
				grandpa.Left = parent.Right; parent.Right = grandpa;
				newRoot = parent;
			} else if (l1 && !l2) {
				grandpa.Left = current.Right; current.Right = grandpa;
				parent.Right = current.Left; current.Left = parent;
				newRoot = current;
			} else if (!l1 && l2) {
				grandpa.Right = current.Left; current.Left = grandpa;
				parent.Left = current.Right; current.Right = parent;
				newRoot = current;
			} else { // (!l1 && !l2)
				grandpa.Right = parent.Left; parent.Left = grandpa;
				newRoot = parent;
			}

			grandpa.FixSize (); grandpa.IsBlack = false;
			if (newRoot != parent)
				parent.FixSize (); /* parent is red already, so no need to set it */

			newRoot.IsBlack = true;
			node_reparent (curpos == 4 ? null : path [curpos-6], grandpa, grandpaSize, newRoot);
		}

		// Pre-condition: sibling is black, and one of sibling.left and sibling.right is red
        private void rebalance_delete__rotate_final (int curpos, List<Node> path)
		{
			//Node current = path [curpos];
			var sibling = path [curpos-1];
			var parent = path [curpos-2];

			var parentSize = parent.Size;
			var parentWasBlack = parent.IsBlack;

			Node newRoot;
			if (parent.Right == sibling) {
				// if far nephew is black
				if (sibling.Right == null || sibling.Right.IsBlack) {
					// => near nephew is red, move it up
					var nephew = sibling.Left;
					parent.Right = nephew.Left; nephew.Left = parent;
					sibling.Left = nephew.Right; nephew.Right = sibling;
					newRoot = nephew;
				} else {
					parent.Right = sibling.Left; sibling.Left = parent;
					sibling.Right.IsBlack = true;
					newRoot = sibling;
				}
			} else {
				// if far nephew is black
				if (sibling.Left == null || sibling.Left.IsBlack) {
					// => near nephew is red, move it up
					var nephew = sibling.Right;
					parent.Left = nephew.Right; nephew.Right = parent;
					sibling.Right = nephew.Left; nephew.Left = sibling;
					newRoot = nephew;
				} else {
					parent.Left = sibling.Right; sibling.Right = parent;
					sibling.Left.IsBlack = true;
					newRoot = sibling;
				}
			}

			parent.FixSize (); parent.IsBlack = true;
			if (newRoot != sibling)
				sibling.FixSize (); /* sibling is already black, so no need to set it */

			newRoot.IsBlack = parentWasBlack;
			node_reparent (curpos == 2 ? null : path [curpos-4], parent, parentSize, newRoot);
		}

		// Pre-condition: sibling is red (=> parent, sibling.left and sibling.right are black)
        private int ensure_sibling_black (int curpos, List<Node> path)
		{
			var current = path [curpos];
			var sibling = path [curpos-1];
			var parent = path [curpos-2];

			bool currentOnLeft;
			var parentSize = parent.Size;

			if (parent.Right == sibling) {
				parent.Right = sibling.Left; sibling.Left = parent;
				currentOnLeft = true;
			} else {
				parent.Left = sibling.Right; sibling.Right = parent;
				currentOnLeft = false;
			}

			parent.FixSize (); parent.IsBlack = false;

			sibling.IsBlack = true;
			node_reparent (curpos == 2 ? null : path [curpos-4], parent, parentSize, sibling);

			// accomodate the rotation
			if (curpos+1 == path.Count) {
				path.Add (null);
				path.Add (null);
			}

			path [curpos-2] = sibling;
			path [curpos-1] = currentOnLeft ? sibling.Right : sibling.Left;
			path [curpos] = parent;
			path [curpos+1] = currentOnLeft ? parent.Right : parent.Left;
			path [curpos+2] = current;

			return curpos + 2;
		}

        private void node_reparent (Node origParent, Node orig, uint origSize, Node updated)
		{
			if (updated != null && updated.FixSize () != origSize)
				throw new SystemException ("Internal error: rotation");

			if (orig == Root)
				Root = updated;
			else if (orig == origParent.Left)
				origParent.Left = updated;
			else if (orig == origParent.Right)
				origParent.Right = updated;
			else
				throw new SystemException ("Internal error: path error");
		}

		// Pre-condition: current != null
        private static Node right_most (Node current, Node sibling, List<Node> path)
		{
			for (;;) {
				path.Add (sibling);
				path.Add (current);
				if (current.Right == null)
					return current;
				sibling = current.Left;
				current = current.Right;
			}
		}

		[Serializable]
		public struct NodeEnumerator : IEnumerator<Node> {
            private RbTree _tree;
            private uint _version;

            private Stack<Node> _pennants, _initPennants;

			internal NodeEnumerator (RbTree tree)
				: this ()
			{
				_tree = tree;
				_version = tree._version;
			}

			internal NodeEnumerator (RbTree tree, Stack<Node> initPennants)
				: this (tree)
			{
				_initPennants = initPennants;
			}

			public void Reset ()
			{
				check_version ();
				_pennants = null;
			}

			public Node Current => _pennants.Peek ();

            object IEnumerator.Current {
				get {
					check_current ();
					return Current;
				}
			}

			public bool MoveNext ()
			{
				check_version ();

				Node next;
				if (_pennants == null) {
					if (_tree.Root == null)
						return false;
					if (_initPennants != null) {
						_pennants = _initPennants;
						_initPennants = null;
						return _pennants.Count != 0;
					}
					_pennants = new Stack<Node> ();
					next = _tree.Root;
				} else {
					if (_pennants.Count == 0)
						return false;
					var current = _pennants.Pop ();
					next = current.Right;
				}
				for (; next != null; next = next.Left)
					_pennants.Push (next);

				return _pennants.Count != 0;
			}

			public void Dispose ()
			{
				_tree = null;
				_pennants = null;
			}

            private void check_version ()
			{
				if (_tree == null)
					throw new ObjectDisposedException ("enumerator");
				if (_version != _tree._version)
					throw new InvalidOperationException ("tree modified");
			}

			internal void check_current ()
			{
				check_version ();
				if (_pennants == null)
					throw new InvalidOperationException ("state invalid before the first MoveNext()");
			}
		}
	}
}

#if TEST
namespace Mono.ValidationTest {
	using System.Collections.Generic;
    using UAM.Kora;

	internal class TreeSet<T> : IEnumerable<T>, IEnumerable
	{
		public class Node : RBTree.Node {
			public T value;

			public Node (T v)
			{
				value = v;
			}

			public override void SwapValue (RBTree.Node other)
			{
				Node o = (Node) other;
				T v = value;
				value = o.value;
				o.value = v;
			}

			public override void Dump (string indent)
			{
				Console.WriteLine ("{0}{1} {2}({3})", indent, value, IsBlack ? "*" : "", Size);
				if (left != null)
					left.Dump (indent + "  /");
				if (right != null)
					right.Dump (indent + "  \\");
			}
		}

		public class NodeHelper : RBTree.INodeHelper<T> {
			IComparer<T> cmp;

			public int Compare (T value, RBTree.Node node)
			{
				return cmp.Compare (value, ((Node) node).value);
			}

			public RBTree.Node CreateNode (T value)
			{
				return new Node (value);
			}

			private NodeHelper (IComparer<T> cmp)
			{
				this.cmp = cmp;
			}
			static NodeHelper Default = new NodeHelper (Comparer<T>.Default);
			public static NodeHelper GetHelper (IComparer<T> cmp)
			{
				if (cmp == null || cmp == Comparer<T>.Default)
					return Default;
				return new NodeHelper (cmp);
			}
		}

		public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T> {
			RBTree.NodeEnumerator host;

			internal Enumerator (TreeSet<T> tree)
			{
				host = new RBTree.NodeEnumerator (tree.tree);
			}

			void IEnumerator.Reset ()
			{
				host.Reset ();
			}

			public T Current {
				get { return ((Node) host.Current).value; }
			}

			object IEnumerator.Current {
				get { return Current; }
			}

			public bool MoveNext ()
			{
				return host.MoveNext ();
			}

			public void Dispose ()
			{
				host.Dispose ();
			}
		}

		RBTree tree;

		public TreeSet () : this (null)
		{
		}

		public TreeSet (IComparer<T> cmp)
		{
			tree = new RBTree (NodeHelper.GetHelper (cmp));
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		public Enumerator GetEnumerator ()
		{
			return new Enumerator (this);
		}

		// returns true if the value was inserted, false if the value already existed in the tree
		public bool Insert (T value)
		{
			RBTree.Node n = new Node (value);
			return tree.Intern (value, n) == n;
		}

		// returns true if the value was removed, false if the value didn't exist in the tree
		public bool Remove (T value)
		{
			return tree.Remove (value) != null;
		}

		public bool Contains (T value)
		{
			return tree.Lookup (value) != null;
		}

		public T this [int index] {
			get { return ((Node) tree [index]).value; }
		}

		public int Count {
			get { return (int) tree.Count; }
		}

		public void VerifyInvariants ()
		{
			tree.VerifyInvariants ();
		}

		public void Dump ()
		{
			tree.Dump ();
		}
	}
	
	class Test {
		static void Main (string [] args)
		{
			Random r = new Random ();
			Dictionary<int, int> d = new Dictionary<int, int> ();
			TreeSet<int> t = new TreeSet<int> ();
			int iters = args.Length == 0 ? 100000 : Int32.Parse (args [0]);
			int watermark = 1;

			for (int i = 0; i < iters; ++i) {
				if (i >= watermark) {
					watermark += 1 + watermark/4;
					t.VerifyInvariants ();
				}

				int n = r.Next ();
				if (d.ContainsKey (n))
					continue;
				d [n] = n;

				try {
					if (t.Contains (n))
						throw new Exception ("tree says it has a number it shouldn't");
					if (!t.Insert (n))
						throw new Exception ("tree says it has a number it shouldn't");
				} catch {
					Console.Error.WriteLine ("Exception while inserting {0} in iteration {1}", n, i);
					throw;
				}
			}
			t.VerifyInvariants ();
			if (d.Count != t.Count)
				throw new Exception ("tree count is wrong?");

			Console.WriteLine ("Tree has {0} elements", t.Count);

			foreach (int n in d.Keys)
				if (!t.Contains (n))
					throw new Exception ("tree says it doesn't have a number it should");

			Dictionary<int, int> d1 = new Dictionary<int, int> (d);

			int prev = -1;
			foreach (int n in t) {
				if (n < prev)
					throw new Exception ("iteration out of order");
				if (!d1.Remove (n))
					throw new Exception ("tree has a number it shouldn't");
				prev = n;
			}

			if (d1.Count != 0)
				throw new Exception ("tree has numbers it shouldn't");

			for (int i = 0; i < iters; ++i) {
				int n = r.Next ();
				if (!d.ContainsKey (n)) {
					if (t.Contains (n))
						throw new Exception ("tree says it doesn't have a number it should");
				} else if (!t.Contains (n)) {
					throw new Exception ("tree says it has a number it shouldn't");
				}
			}

			int count = t.Count;
			foreach (int n in d.Keys) {
				if (count <= watermark) {
					watermark -= watermark/4;
					t.VerifyInvariants ();
				}
				try {
					if (!t.Remove (n))
						throw new Exception ("tree says it doesn't have a number it should");
					--count;
					if (t.Count != count)
						throw new Exception ("Remove didn't remove exactly one element");
				} catch {
					Console.Error.WriteLine ("While trying to remove {0} from tree of size {1}", n, t.Count);
					t.Dump ();
					t.VerifyInvariants ();
					throw;
				}
				if (t.Contains (n))
					throw new Exception ("tree says it has a number it shouldn't");
			}
			t.VerifyInvariants ();

			if (t.Count != 0)
				throw new Exception ("tree claims to have elements");
		}
	}
}
#endif

