using System;
using System.Collections.Generic;

namespace iCare.Core.Dependency.Runtime.Interfaces {
    public interface IService {
        object Service { get; }
        string ID { get; }
        IEnumerable<Type> ServiceTypes { get; }
    }
}