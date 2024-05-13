using Sirenix.OdinInspector;
using UnityEngine;

namespace iCare.Core.Attributes {
    [IncludeMyAttributes]
    [Required]
    [AssetsOnly]
    [GUIColor(1f, 0.31f, 0.25f)]
    public sealed class CAssetAttribute : PropertyAttribute {
    }
}