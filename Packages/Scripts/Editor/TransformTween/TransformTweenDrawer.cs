using Enpiech.TimelineExtensions.Scripts.Runtime.TransformTween;
using UnityEditor;
using UnityEngine;

namespace Enpiech.TimelineExtensions.Packages.Scripts.Editor.TransformTween
{
    [CustomPropertyDrawer(typeof(TransformTweenBehaviour))]
    public class TransformTweenDrawer : PropertyDrawer
    {
        private readonly GUIContent _customCurveContent = new("Custom Curve",
            "This should be a normalised curve (between 0,0 and 1,1) that represents how the tweening object accelerates at different points along the clip.");

        private readonly GUIContent _tweenPositionContent = new("Tween Position",
            "This should be true if the transformToMove to change position.  This causes recalulations each frame which are more CPU intensive.");

        private readonly GUIContent _tweenRotationContent =
            new("Tween Rotation", "This should be true if the transformToMove to change rotation.");

        private readonly GUIContent _tweenTypeContent = new("Tween Type",
            "Linear - the transform moves the same amount each frame (assuming static start and end locations).\n"
            + "Deceleration - the transform moves slower the closer to the end location it is.\n"
            + "Harmonic - the transform moves faster in the middle of its tween.\n"
            + "Custom - uses the customStartingSpeed and customEndingSpeed to create a curve for the desired tween.");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var fieldCount = property.FindPropertyRelative("tweenType").enumValueIndex == (int)TransformTweenBehaviour.TweenType.Custom
                ? 5
                : 3;
            return fieldCount * EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var tweenPositionProp = property.FindPropertyRelative("tweenPosition");
            var tweenRotationProp = property.FindPropertyRelative("tweenRotation");
            var tweenTypeProp = property.FindPropertyRelative("tweenType");

            var singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(singleFieldRect, tweenPositionProp, _tweenPositionContent);

            singleFieldRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(singleFieldRect, tweenRotationProp, _tweenRotationContent);

            singleFieldRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(singleFieldRect, tweenTypeProp, _tweenTypeContent);

            if (tweenTypeProp.enumValueIndex == (int)TransformTweenBehaviour.TweenType.Custom)
            {
                var customCurveProp = property.FindPropertyRelative("customCurve");

                singleFieldRect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(singleFieldRect, customCurveProp, _customCurveContent);
            }
        }
    }
}