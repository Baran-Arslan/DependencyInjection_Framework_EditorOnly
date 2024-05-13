using iCare.Core.Dependency.Runtime;
using iCare.Core.Dependency.Runtime.Attributes.Auto;
using iCare.Core.Logger;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace iCare.Core.CPlayerLoop {
    internal static class LoopInitializer {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize() {
            var loopManagerPrefab = Resources.Load<LoopManager>("iCareCore");
            if (loopManagerPrefab == null) {
                CLog.Error("PlayerLoopManager prefab not found in Resources folder.");
                return;
            }

            Object.Instantiate(loopManagerPrefab);
        }
    }

    [DefaultExecutionOrder(-9999)]
    internal sealed class LoopManager : SerializedMonoBehaviour {
        [SerializeField] [AutoList] IListenAwake[] awakeListeners;
        [SerializeField] [AutoList] IListenOnDestroy[] destroyListeners;
        [SerializeField] [AutoList] IListenOnDisable[] disableListeners;
        [SerializeField] [AutoList] IListenOnEnable[] enableListeners;
        [SerializeField] [AutoList] IListenFixedUpdate[] fixedUpdateListeners;
        [SerializeField] [AutoList] IListenLateUpdate[] lateUpdateListeners;
        [SerializeField] [AutoList] IListenStart[] startListeners;
        [SerializeField] [AutoList] IListenUpdate[] updateListeners;

        void Awake() {
            DontDestroyOnLoad(gameObject);
            if (awakeListeners.IsNullOrEmpty()) return;
            foreach (var listener in awakeListeners) listener.CAwake();
        }

        void Start() {
            if (startListeners.IsNullOrEmpty()) return;
            foreach (var listener in startListeners) listener.CStart();
        }

        void Update() {
            if (updateListeners.IsNullOrEmpty()) return;
            foreach (var listener in updateListeners) listener.CUpdate();
        }

        void FixedUpdate() {
            if (fixedUpdateListeners.IsNullOrEmpty()) return;
            foreach (var listener in fixedUpdateListeners) listener.CFixedUpdate();
        }

        void LateUpdate() {
            if (lateUpdateListeners.IsNullOrEmpty()) return;
            foreach (var listener in lateUpdateListeners) listener.CLateUpdate();
        }

        void OnEnable() {
            if (enableListeners.IsNullOrEmpty()) return;
            foreach (var listener in enableListeners) listener.CEnable();
        }

        void OnDisable() {
            if (disableListeners.IsNullOrEmpty()) return;
            foreach (var listener in disableListeners) listener.CDisable();
        }

        void OnDestroy() {
            if (destroyListeners.IsNullOrEmpty()) return;
            foreach (var listener in destroyListeners) listener.CDestroy();
        }
    }
}