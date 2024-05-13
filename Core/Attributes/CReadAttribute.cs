using System;
using Sirenix.OdinInspector;

namespace iCare.Core.Attributes {
    [IncludeMyAttributes]
    [ShowInInspector]
    [ReadOnly]
    [GUIColor(0.56f, 1f, 0.47f)]
    public sealed class CReadAttribute : Attribute {
    }

    [IncludeMyAttributes]
    [ShowInInspector]
    [ReadOnly]
    [GUIColor(0.56f, 1f, 0.47f)]
    internal sealed class CReadRuntimeAttribute : Attribute {
    }
}