using System.Diagnostics;

namespace Day05
{
    [DebuggerDisplay("{from}-{to}")]
    internal record Range (long from, long to)
    {
    }
}
