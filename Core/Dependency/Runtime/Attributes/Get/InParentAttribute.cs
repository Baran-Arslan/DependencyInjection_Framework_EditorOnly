using Sirenix.OdinInspector;

namespace iCare.Core.Dependency.Runtime.Attributes.Get {
    [IncludeMyAttributes]
    [ReadOnly]
    [FoldoutGroup("Dependencies")]
    public sealed class InParentAttribute : BaseDependencyAttribute {
        public bool IncludeInactive = true;
        public bool IncludeSelf = true;
    }
}