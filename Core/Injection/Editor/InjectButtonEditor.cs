using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace iCare.Core.Injection.Editor
{
    [CustomEditor(typeof(Transform))]
    internal sealed class InjectButtonEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.backgroundColor = new Color(0.56f, 1f, 0.47f);
            if (GUILayout.Button("Inject All"))
            {
                Injector.InjectEverything();
            }
        }
    }
}