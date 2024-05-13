using System;
using System.Collections.Generic;
using iCare.Core.Dependency.Runtime.Interfaces;
using iCare.Core.ScriptableSingeleton;

namespace iCare.Core.Dependency.Runtime {
    public abstract class CScriptableSingeletonService : CScriptableSingeleton<CScriptableSingeletonService>, IService {
        public virtual object Service => this;
        public virtual string ID => null;

        public virtual IEnumerable<Type> ServiceTypes {
            get {
                var allTypes = new List<Type> { GetType() };
                allTypes.AddRange(GetType().GetInterfaces());
                return allTypes;
            }
        }
    }
}