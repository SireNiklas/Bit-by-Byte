using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Magio
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MagioObjectMaster))]
    public class MagioEffectMasterEditor : Editor
    {
        public struct EffectInfo
        {
            public bool differentMaster;
            public bool enableOnStart;
            public MagioObjectEffect magioObj;
            public MagioObjectMaster master;
        }


        private SerializedProperty effectParent;
        private void OnEnable()
        {
            effectParent = serializedObject.FindProperty("effectParent");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
        }
    }
}

