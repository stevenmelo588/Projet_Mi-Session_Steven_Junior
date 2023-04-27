// using SurviveTheRust.Assets.Scripts.InputActions;

using UnityEditor;
using UnityEngine;

namespace SurviveTheRust.Assets.Managers
{
    // [CustomEditor(typeof(UserInputManager))]
    //public class UserInputManagerCustomEditor : Editor
    //{
    //    // private SerializedProperty playerInputSerializedProperty;
    //    // private SerializedProperty m_ActionMapsProperty;

    //    //private SerializedProperty playerInputSerializedProperty;

    //    //private void OnEnable()
    //    //{
    //    //    playerInputSerializedProperty = serializedObject.FindProperty("m_ActionMaps");
    //    //}

    //    //public override void OnInspectorGUI()
    //    //{
    //    //    serializedObject.Update();

    //    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

    //    //    EditorGUILayout.Space();

    //    //    EditorGUILayout.LabelField("Actions:");

    //    //    EditorGUI.indentLevel++;

    //    //    for (int i = 0; i < playerInputSerializedProperty.arraySize; i++)
    //    //    {
    //    //        SerializedProperty actionMapSerializedProperty = playerInputSerializedProperty.GetArrayElementAtIndex(i);

    //    //        SerializedProperty actionMapNameSerializedProperty = actionMapSerializedProperty.FindPropertyRelative("m_Name");
    //    //        SerializedProperty actionsSerializedProperty = actionMapSerializedProperty.FindPropertyRelative("m_Actions");

    //    //        EditorGUILayout.LabelField(actionMapNameSerializedProperty.stringValue);

    //    //        EditorGUI.indentLevel++;

    //    //        for (int j = 0; j < actionsSerializedProperty.arraySize; j++)
    //    //        {
    //    //            SerializedProperty actionSerializedProperty = actionsSerializedProperty.GetArrayElementAtIndex(j);

    //    //            SerializedProperty actionNameSerializedProperty = actionSerializedProperty.FindPropertyRelative("m_Name");
    //    //            SerializedProperty bindingsSerializedProperty = actionSerializedProperty.FindPropertyRelative("m_SingletonActionBindings");

    //    //            EditorGUILayout.BeginHorizontal();
    //    //            EditorGUILayout.LabelField(actionNameSerializedProperty.stringValue, GUILayout.MaxWidth(120f));

    //    //            EditorGUI.BeginChangeCheck();

    //    //            SerializedProperty bindingsProperty = actionSerializedProperty.FindPropertyRelative("m_SingletonActionBindings");
    //    //            int bindingCount = bindingsProperty.arraySize;
    //    //            for (int k = 0; k < bindingCount; k++)
    //    //            {
    //    //                SerializedProperty bindingProperty = bindingsProperty.GetArrayElementAtIndex(k);

    //    //                SerializedProperty pathProperty = bindingProperty.FindPropertyRelative("path");
    //    //                SerializedProperty actionProperty = bindingProperty.FindPropertyRelative("action");
    //    //                SerializedProperty interactionsProperty = bindingProperty.FindPropertyRelative("interactions");
    //    //                SerializedProperty processorsProperty = bindingProperty.FindPropertyRelative("processors");

    //    //                EditorGUILayout.BeginVertical(GUILayout.MaxWidth(120f));
    //    //                EditorGUILayout.PropertyField(pathProperty, GUIContent.none);
    //    //                EditorGUILayout.PropertyField(actionProperty, GUIContent.none);
    //    //                EditorGUILayout.PropertyField(interactionsProperty, GUIContent.none);
    //    //                EditorGUILayout.PropertyField(processorsProperty, GUIContent.none);
    //    //                EditorGUILayout.EndVertical();
    //    //            }

    //    //            if (EditorGUI.EndChangeCheck())
    //    //            {
    //    //                serializedObject.ApplyModifiedProperties();
    //    //            }

    //    //            EditorGUILayout.EndHorizontal();
    //    //        }

    //    //        EditorGUI.indentLevel--;
    //    //    }

    //    //    EditorGUI.indentLevel--;

    //    //    serializedObject.ApplyModifiedProperties();
    //    //}


    //    // public InputActionAsset PlayerInputActionsAsset => new @PlayerInputActions().asset;

    //    // private SerializedObject playerInputSerializedObject;
    //    // private List<InputActionMap> playerInputActionMaps = new List<InputActionMap>();
    //    // private List<InputAction> playerInputActions = new List<InputAction>();

    //    // private void OnEnable()
    //    // {
    //    //     playerInputSerializedProperty = serializedObject.FindProperty("playerInputActionsAsset");
    //    // }

    //    // actionMapsProperty = playerInputSerializedProperty.FindPropertyRelative("playerInputActionMaps");
    //    // actionsProperty = playerInputSerializedProperty.FindPropertyRelative("playerInputActions");

    //    // actionMapsList = new ReorderableList(serializedObject, actionMapsProperty, true, true, true, true);
    //    // actionsList = new ReorderableList(serializedObject, actionsProperty, true, true, true, true);

    //    // actionMapsList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Action Maps");
    //    // actionMapsList.drawElementCallback = (rect, index, isActive, isFocused) =>
    //    // {
    //    //     var actionMapProperty = actionMapsProperty.GetArrayElementAtIndex(index);
    //    //     EditorGUI.LabelField(rect, actionMapProperty.FindPropertyRelative("m_Name").stringValue);
    //    // };

    //    // actionsList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Actions");
    //    // actionsList.drawElementCallback = (rect, index, isActive, isFocused) =>
    //    // {
    //    //     var actionMapProperty = actionMapsProperty.GetArrayElementAtIndex(index);
    //    //     EditorGUI.LabelField(rect, actionMapProperty.FindPropertyRelative("m_Name").stringValue);
    //    // };
    //    // playerInputSerializedObject = new SerializedObject(serializedObject.targetObject);

    //    // public override void OnInspectorGUI()
    //    // {
    //    //     serializedObject.Update();
    //    //     EditorGUILayout.PropertyField(playerInputSerializedProperty, true);

    //    //     if (playerInputSerializedProperty.objectReferenceValue != null)
    //    //     {
    //    //         InputActionAsset playerInputActionAsset = (InputActionAsset)playerInputSerializedProperty.objectReferenceValue;
    //    //         // InputActionAsset inputActionAsset = (InputActionAsset)playerInputSerializedProperty.objectReferenceValue;
    //    //         // InputActionMap[] actionMaps = inputActionAsset.actionMaps.ToArray();

    //    //         // foreach (InputActionMap actionMap in actionMaps)
    //    //         // {
    //    //         //     EditorGUILayout.LabelField(actionMap.name);

    //    //         //     InputAction[] actions = actionMap.actions.ToArray();

    //    //         //     foreach (InputAction action in playerInputActions)
    //    //         //     {
    //    //         //         EditorGUILayout.ObjectField((UnityEngine.Object)action, typeof(InputAction), true);
    //    //         //     }
    //    //         // }


    //    //         if (playerInputActionMaps == null)
    //    //         {
    //    //             // playerInputActionMaps = new List<InputActionMap>();
    //    //             // playerInputActions = new List<InputAction>();

    //    //             foreach (InputActionMap actionsMap in playerInputActionAsset.actionMaps)
    //    //             {
    //    //                 playerInputActionMaps.Add(actionsMap);

    //    //                 foreach (InputAction action in actionsMap.actions)
    //    //                 {
    //    //                     playerInputActions.Add(action);
    //    //                 }
    //    //             }
    //    //         }

    //    //         GUILayout.Label("Action Maps:");
    //    //         foreach (InputActionMap actionMap in playerInputActionMaps)
    //    //         {
    //    //             EditorGUI.BeginChangeCheck();
    //    //             EditorGUILayout.PropertyField(serializedObject.FindProperty(actionMap.name), true);

    //    //             if (EditorGUI.EndChangeCheck())
    //    //             {
    //    //                 serializedObject.ApplyModifiedProperties();
    //    //                 ReloadBindings(playerInputActionAsset);
    //    //             }
    //    //             // EditorGUILayout.ObjectField(actionMap, typeof(InputActionMap), true);
    //    //         }

    //    //         GUILayout.Label("Actions:");
    //    //         foreach (InputAction action in playerInputActions)
    //    //         {
    //    //             EditorGUI.BeginChangeCheck();
    //    //             EditorGUILayout.PropertyField(serializedObject.FindProperty(action.name), true);

    //    //             if (EditorGUI.EndChangeCheck())
    //    //             {
    //    //                 serializedObject.ApplyModifiedProperties();
    //    //                 ReloadBindings(playerInputActionAsset);
    //    //             }

    //    //             // EditorGUILayout.ObjectField(action, typeof(InputAction), true);
    //    //         }
    //    //     } 

    //    //     serializedObject.ApplyModifiedProperties();

    //    //     // actionMapsList.DoLayoutList();
    //    //     // actionsList.DoLayoutList();

    //    //     // if (playerInputSerializedProperty.objectReferenceValue != null)
    //    //     // {
    //    //     //     // InputActionAsset playerInputActions = playerInputSerializedProperty.objectReferenceValue as InputActionAsset;

    //    //     //     EditorGUILayout.Space();
    //    //     //     EditorGUILayout.LabelField("Action Maps", EditorStyles.boldLabel);

    //    //     //     foreach (InputActionMap actionMap in (playerInputSerializedProperty.objectReferenceValue as InputActionAsset).actionMaps)
    //    //     //     {
    //    //     //         // EditorGUILayout.PropertyField(actionMap, true);
    //    //     //         EditorGUILayout.LabelField(actionMap.name, EditorStyles.boldLabel);
    //    //     //         foreach (InputAction action in actionMap.actions)
    //    //     //         {
    //    //     //             EditorGUILayout.LabelField(action.name);
    //    //     //         }

    //    //     //         EditorGUILayout.Space();
    //    //     //     }
    //    //     // }

    //    //     // playerInputSerializedObject.ApplyModifiedProperties();
    //    // }

    //    // private void ReloadBindings(InputActionAsset inputActionAsset)
    //    // {
    //    //     var json = inputActionAsset.ToJson();
    //    //     inputActionAsset.LoadFromJson(json);
    //    // }
    //}
}
