using Sirenix.OdinInspector;

namespace iCare.Core.Dependency.Runtime.Attributes.Get {
    [IncludeMyAttributes]
    [ReadOnly]
    [FoldoutGroup("Dependencies")]
    public sealed class InChildrenAttribute : BaseDependencyAttribute {
        public bool IncludeInactive = true;
        public bool IncludeSelf = true;
    }
}