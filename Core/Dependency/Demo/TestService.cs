using iCare.Core.Dependency.Runtime;
using UnityEngine;

namespace iCare.Core.Dependency.Demo {
    public interface ITestService {
    }

    [CreateAssetMenu]
    public sealed class TestService : CScriptableService, ITestService {
    }
}