namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ODS;

/// <summary>
/// https://github.com/vosen/kora
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class YFastTrie<T> : SortedDictionaryBase<T>
{
    private readonly int _upperLimit;
    private readonly int _lowerLimit;
    private int _count;
    private uint _version;
    internal XFastTrie<RbTree> Cluster;

    // removes new RBTree from the old tree
    internal static RbTree SplitNew(ref RbTree tree)
    {
        var nodes = tree.ToSortedArray();
        tree = RbUtils.FromSortedList(nodes, (nodes.Length >> 1), nodes.Length - 1);
        return RbUtils.FromSortedList(nodes, 0, (nodes.Length >> 1) - 1);
    }

    private YFastTrie(XFastTrie<RbTree> newTree)
    {
        Cluster = newTree;
        _lowerLimit = (Cluster.Width / 2);
        _upperLimit = (Cluster.Width * 2);
    }

    public YFastTrie()
        : this(new XFastTrie<RbTree>())
    { }

    public YFastTrie(int width)
        : this(new XFastTrie<RbTree>(width))
    {}

    public static YFastTrie<T> FromDictionary<TDict>(int width) where TDict : IDictionary<uint, XFastNode>, new()
    {
        return new YFastTrie<T>(XFastTrie<RbTree>.FromDictionary<TDict>(width));
    }

    private XFastTrie<RbTree>.LeafNode Separator(uint key)
    {
        if (Cluster.LeafList == null)
            return null;
        // special check for maxval
        if (((XFastTrie<RbTree>.LeafNode)Cluster.LeafList.Left).Key == key)
            return (XFastTrie<RbTree>.LeafNode)Cluster.LeafList.Left;
        var succ = Cluster.HigherNode(key);
        if (succ == null)
            return null;
        var left = (XFastTrie<RbTree>.LeafNode)succ.Left;
        if (left.Key == key)
            return left;
        return succ;
    }

    private void AddChecked(uint key, T value, bool overwrite)
    {
            
        var separator = Separator(key);
        if (separator == null)
        {
            // add first element
            var newTree = new RbTree(Node.Helper);
            newTree.Root = new RbuIntNode(key, value) { IsBlack = true };
            Cluster.Add(BitHacks.MaxValue(Cluster.Width), newTree);
            return;
        }
        var newNode = new RbuIntNode(key, value);
        var interned = (RbuIntNode)separator.Value.Intern(key, newNode);
        if (interned != newNode)
        {
            if (overwrite)
                interned.Value = value;
            else
                throw new ArgumentException();
        }
        else
        {
            _count++;
            _version++;
        }
        SplitIfTooLarge(separator);
    }

    public override void Add(uint key, T value)
    {
        AddChecked(key, value, false);
    }

    private void SplitIfTooLarge(XFastTrie<RbTree>.LeafNode separator)
    {
        if (separator.Value.Count <= _upperLimit)
            return;
        var newTree = SplitNew(ref separator.Value);
        Cluster.Add(((RbuIntNode)newTree.LastNode()).Key, newTree);
    }

    public override bool TryGetValue(uint key, out T value)
    {
        var sep = Separator(key);
        if (sep == null)
        {
            value = default;
            return false;
        }
        var candidate = (RbuIntNode)sep.Value.Lookup(key);
        if (candidate == null)
        {
            value = default;
            return false;
        }
        else 
        {
            value = candidate.Value;
            return true;
        }
    }

    public override T this[uint key]
    {
        get
        {
            T temp;
            if (!TryGetValue(key, out temp))
                throw new KeyNotFoundException();
            return temp;
        }
        set => AddChecked(key, value, true);
    }

    public override bool Remove(uint key)
    {
        var separator = Separator(key);
        // TODO: remove this check
        if (separator != null && !Cluster.ContainsKey(separator.Key))
            throw new Exception();
        if (separator == null || separator.Value.Remove(key) == null)
            return false;
        _count--;
        _version++;
        // at this point key is removed from bst
        if (separator.Left == separator || separator.Value.Count >= _lowerLimit)
            return true;
        // we need to rebuild
        XFastTrie<RbTree>.LeafNode lower;
        XFastTrie<RbTree>.LeafNode higher;
        var left = (XFastTrie<RbTree>.LeafNode)separator.Left;
        var right = (XFastTrie<RbTree>.LeafNode)separator.Right;
        // pick best merge candidate
        if (left.Key > separator.Key)
        {
            // we are the first leaf
            lower = separator;
            higher = right;
        }
        else if (right.Key < separator.Key)
        {
            // we are the last leaf
            lower = left;
            higher = separator;
        }
        // separate branch, we want to avoid situation where we compare count of
        // leaf on the other side of linked list's circle
        else if (left.Value.Count < right.Value.Count)
        {
            lower = left;
            higher = separator;
        }
        else
        {
            lower = separator;
            higher = right;
        }
        if (MergeSplit(ref higher.Value, ref lower.Value))
        {
            // Split happened: reinsert lower key
            System.Diagnostics.Debug.Assert(Cluster.Remove(lower.Key));
            lower.Key = ((RbuIntNode)lower.Value.LastNode()).Key;
            Cluster.Add(lower.Key, lower.Value);
        }
        else
        {
            // Only merge happened: just remove lower node
            Cluster.Remove(lower.Key);
        }
        return true;
    }

    private bool MergeSplit(ref RbTree higher, ref RbTree lower)
    {
        var array = new RbTree.Node[higher.Count + lower.Count];
        lower.CopySorted(array, 0);
        higher.CopySorted(array, lower.Count);
        if (array.Length > _upperLimit)
        {
            lower = RbUtils.FromSortedList(array, 0, array.Length >> 1);
            higher = RbUtils.FromSortedList(array, (array.Length >> 1) + 1, array.Length - 1);
            return true;
        }
        else
        {
            higher = RbUtils.FromSortedList(array, 0, array.Length - 1);
            return false;
        }
    }

    public override KeyValuePair<uint, T>? First()
    {
        var firstXNode = Cluster.First();
        if (firstXNode == null)
            return null;
        var firstRbNode = (RbuIntNode)firstXNode.Value.Value.FirstNode();
        if (firstRbNode == null)
            return null;
        return new KeyValuePair<uint, T>(firstRbNode.Key, firstRbNode.Value);
    }

    public override KeyValuePair<uint, T>? Last()
    {
        var lastXNode = Cluster.Last();
        if (lastXNode == null)
            return null;
        var lastRbNode = (RbuIntNode)lastXNode.Value.Value.LastNode();
        if (lastRbNode == null)
            return null;
        return new KeyValuePair<uint, T>(lastRbNode.Key, lastRbNode.Value);
    }

    public override KeyValuePair<uint, T>? Lower(uint key)
    {
        var separator = Separator(key);
        if (separator == null)
            return null;
        var predNode = RbUtils.LowerNode(separator.Value, key);
        if (predNode == null)
        {
            if (separator == Cluster.LeafList)
                return null;
            var highestLeft = (RbuIntNode)((XFastTrie<RbTree>.LeafNode)separator.Left).Value.LastNode();
            return new KeyValuePair<uint, T>(highestLeft.Key, highestLeft.Value);
        }
        return new KeyValuePair<uint, T>(predNode.Key, predNode.Value);
    }

    public override KeyValuePair<uint, T>? Higher(uint key)
    {
        var higherNode = Cluster.HigherNode(key);
        if (higherNode == null)
            return null;
        var left = (XFastTrie<RbTree>.LeafNode)higherNode.Left;
        if (((XFastTrie<RbTree>.LeafNode)higherNode.Left).Key == key)
        {
            var succNode = (RbuIntNode)higherNode.Value.FirstNode();
            return new KeyValuePair<uint,T>(succNode.Key, succNode.Value);
        }
        var rbHigher = RbUtils.HigherNode(higherNode.Value, key);
        if (rbHigher == null)
            return null;
        return new KeyValuePair<uint, T>(rbHigher.Key, rbHigher.Value);
    }

    public override void Clear()
    {
        _count = 0;
        _version = 0;
        Cluster = new XFastTrie<RbTree>();
    }

    public override int Count => _count;

    public override bool ContainsKey(uint key)
    {
        throw new NotImplementedException();
    }

    public override IEnumerator<KeyValuePair<uint, T>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

#if DEBUG
    public void Verify()
    {
        Cluster.Verify();
        foreach (var node in Cluster)
        {
            node.Value.VerifyInvariants();
        }
    }
#endif
}