using System;
using System.Collections.Generic;

namespace iCare.Core.Injection
{
    public interface IService
    {
        object ServiceData { get; }
        IEnumerable<Type> RegisterTypes { get; }

        string GetServiceID()
        {
            return "Default";
        }
    }
}