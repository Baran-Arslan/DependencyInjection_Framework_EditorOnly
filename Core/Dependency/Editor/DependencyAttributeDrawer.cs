using System;
using iCare.Core.Dependency.Runtime;
using iCare.Core.Dependency.Runtime.Attributes.Auto;
using iCare.Core.Dependency.Runtime.Attributes.Get;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace iCare.Core.Dependency.Editor {
    internal abstract class DependencyAttributeDrawer<T> : OdinAttributeDrawer<T> where T : Attribute {
        protected override void DrawPropertyLayout(GUIContent label) {
            EditorGUILayout.BeginHorizontal();

            var icon = EditorIcons.Download.Active;

            GUILayout.Label(new GUIContent(icon), GUILayout.Width(20), GUILayout.Height(20));

            CallNextDrawer(label);

            EditorGUILayout.EndHorizontal();
        }
    }

    internal sealed class AutoAttributeDrawer : DependencyAttributeDrawer<AutoAttribute> {
    }

    internal sealed class AutoListAttributeDrawer : DependencyAttributeDrawer<AutoListAttribute> {
    }

    internal sealed class GetParentAttributeDrawer : DependencyAttributeDrawer<InParentAttribute> {
    }

    internal sealed class GetChildrenAttributeDrawer : DependencyAttributeDrawer<InChildrenAttribute> {
    }

    internal sealed class GetSelfAttributeDrawer : DependencyAttributeDrawer<InSelfAttribute> {
    }
}