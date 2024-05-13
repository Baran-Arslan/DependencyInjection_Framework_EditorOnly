using System;
using System.Collections.Generic;
using iCare.Core.Dependency.Runtime.Interfaces;
using UnityEngine;

namespace iCare.Core.Dependency.Runtime {
    public abstract class CScriptableService : ScriptableObject, IService {
        public virtual object Service => this;
        public virtual string ID => name;

        public virtual IEnumerable<Type> ServiceTypes {
            get {
                var allTypes = new List<Type> { GetType() };
                allTypes.AddRange(GetType().GetInterfaces());
                return allTypes;
            }
        }
    }
}