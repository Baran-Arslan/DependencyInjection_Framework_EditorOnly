using iCare.Core.Injection.Attributes;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace iCare.Core.Injection.Editor
{
    public sealed class AutoAttributeDrawer : OdinAttributeDrawer<AutoAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();

            var icon = EditorIcons.OdinInspectorLogo;

            GUILayout.Label(new GUIContent(icon), GUILayout.Width(20), GUILayout.Height(20));

            CallNextDrawer(label);

            EditorGUILayout.EndHorizontal();
        }
    }
}