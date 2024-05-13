using Sirenix.OdinInspector;

namespace iCare.Core.Dependency.Runtime.Attributes.Auto {
    [IncludeMyAttributes]
    [ReadOnly]
    [FoldoutGroup("Dependencies")]
    public sealed class AutoAttribute : BaseAutoAttribute {
        public AutoAttribute(string id = null) : base(id) {
        }
    }
}