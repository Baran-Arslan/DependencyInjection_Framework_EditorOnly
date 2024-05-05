using System;
using System.Collections.Generic;
using UnityEngine;

namespace iCare.Core.Injection.ServicePresets
{
    [CreateAssetMenu(menuName = "iCare/Services/GameObject Service")]
    internal sealed class GameObjectService : ScriptableObject, IService
    {
        [SerializeField] GameObject gameObject;
        [SerializeField] string id = "Default";

        public object ServiceData => gameObject;
        public string GetServiceID() => id;
        public IEnumerable<Type> RegisterTypes => new[] { typeof(GameObject) };
    }
    
    
}