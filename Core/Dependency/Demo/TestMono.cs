using iCare.Core.Dependency.Runtime;
using iCare.Core.Dependency.Runtime.Attributes.Auto;
using Sirenix.OdinInspector;
using UnityEngine;

namespace iCare.Core.Dependency.Demo {
    public class TestMono : SerializedMonoBehaviour {
        [SerializeField] [Auto(nameof(CRef.NewTestService))]
        TestService testService;


        [SerializeField] [Auto(nameof(CRefSerialized.NewTestSerializedService))]
        TestSerializedService testSerializedService;

        [SerializeField] [Auto(nameof(CRef.NewTestService))]
        ITestService ItestService;
    }
}