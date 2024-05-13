using Sirenix.OdinInspector;

namespace iCare.Core.Dependency.Runtime.Attributes.Auto {
    [IncludeMyAttributes]
    [ReadOnly]
    [FoldoutGroup("Dependencies")]
    public sealed class AutoListAttribute : BaseAutoAttribute {
        public AutoListAttribute(string id = null) : base(id) {
        }
    }
}