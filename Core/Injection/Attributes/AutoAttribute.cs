using System;
using Sirenix.OdinInspector;


namespace iCare.Core.Injection.Attributes
{
    [IncludeMyAttributes]
    [ReadOnly]
    [Required]
    [GUIColor(0.56f, 1f, 0.47f)]
    public sealed class AutoAttribute : Attribute
    {
        public string ID = "Default";
    }
    
    
}