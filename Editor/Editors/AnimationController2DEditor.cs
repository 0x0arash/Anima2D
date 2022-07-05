using ArashGh.Anima2D.Components;
using ArashGh.Anima2D.Definitions;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace ArashGh.Anima2D.Editor
{
    [CustomEditor(typeof(AnimationController2D))]
    public class AnimationController2DEditor : UnityEditor.Editor
    {
        private AnimationController2D controller;

        private SerializedProperty AnimationsList;

        private ReorderableList animationsList;

        private string[] availableOptions;

        private void OnEnable()
        {
            GUIStyle highlightStyle = new GUIStyle();
            GUIStyle normalStyle = new GUIStyle();
            normalStyle.normal.textColor = Color.white;
            highlightStyle.normal.textColor = Color.cyan;

            controller = (AnimationController2D)target;

            AnimationsList = serializedObject.FindProperty(nameof(AnimationController2D.animations));

            ReorderableList.HeaderCallbackDelegate DrawHeader = rect =>
            {
                Event evt = Event.current;
                var dragging = false;

                switch (evt.type)
                {
                    case EventType.DragUpdated:
                    case EventType.DragPerform:
                        if (!rect.Contains(evt.mousePosition))
                            return;

                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        dragging = true;

                        if (evt.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();

                            foreach (Object dragged_object in DragAndDrop.objectReferences)
                            {
                                if (dragged_object != null)
                                {
                                    if (dragged_object is Animation2DDefinition)
                                    {
                                        controller.animations.Add(dragged_object as Animation2DDefinition);

                                        availableOptions = controller.animations.Select(item => item != null ? item.name : null).ToArray();
                                    }
                                }
                            }
                        }
                        break;
                }
                
                EditorGUI.LabelField(rect, "Animations List", dragging ? highlightStyle : normalStyle);
            };
            ReorderableList.ElementCallbackDelegate DrawElement = (rect, index, focused, active) =>
            {
                var element = AnimationsList.GetArrayElementAtIndex(index);
                var availableIDs = controller.animations.Select(a => a.name);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUI.GetPropertyHeight(element)), element, new GUIContent(controller.animations[index].name));
            };
            ReorderableList.ElementHeightCallbackDelegate ElementHeight = index =>
            {
                var element = AnimationsList.GetArrayElementAtIndex(index);
                var availableIDs = controller.animations;
                var height = EditorGUI.GetPropertyHeight(element);

                return height;
            };

            animationsList = new ReorderableList(serializedObject, AnimationsList)
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                drawHeaderCallback = DrawHeader,
                drawElementCallback = DrawElement,
                elementHeightCallback = ElementHeight
            };

            availableOptions = controller.animations.Select(item => item.name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "animations", "StartAnimation");

            EditorGUILayout.Space();

            if (controller.animations.Count <= 0)
                GUI.enabled = false;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Start Animation");
            controller.startAnimationIndex = EditorGUILayout.Popup(controller.startAnimationIndex, availableOptions);
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            EditorGUILayout.Space();

            if (controller.animations.Count > 0)
                controller.StartAnimation = controller.animations[controller.startAnimationIndex];
            else
                controller.StartAnimation = null;

            EditorGUI.BeginChangeCheck();
            animationsList.DoLayoutList();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                availableOptions = controller.animations.Select(item => item != null ? item.name : null).ToArray();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}