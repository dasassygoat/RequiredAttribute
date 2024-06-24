using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredAttributePropertyDrawer : PropertyDrawer
{
    readonly Color k_errorColor = new Color(1f,.2f,.2f,.1f);

    /// <summary>
    /// Returns the height of the GUI for the property.
    /// </summary>
    /// <param name="property">The serialized property to calculate the height for.</param>
    /// <param name="label">The label of the property.</param>
    /// <returns>The height of the GUI for the property.</returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //set a could of line heights
        if (IsFieldEmpty(property))
        {
            float height = EditorGUIUtility.singleLineHeight * 2f;
            height += base.GetPropertyHeight(property, label);

            return height;
        }
        else
        {
            return base.GetPropertyHeight(property, label);
        }
    }

    /// <summary>
    /// Draws the GUI for the property.
    /// </summary>
    /// <param name="position">The position of the property in the GUI.</param>
    /// <param name="property">The serialized property to draw.</param>
    /// <param name="label">The label of the property.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!IsFieldSupported(property))
        {
            Debug.LogError("RequiredAttribute placed on incompatible field type");
            return;
        }

        if (IsFieldEmpty(property))
        {
            position.height = EditorGUIUtility.singleLineHeight * 2f;
            position.height += base.GetPropertyHeight(property, label);

            EditorGUI.HelpBox(position, "This field is required", MessageType.Error);
            EditorGUI.DrawRect(position, k_errorColor);

            position.height = base.GetPropertyHeight(property, label);
            position.y += EditorGUIUtility.singleLineHeight * 2f;
        }

        EditorGUI.PropertyField(position, property, label);
    }



    // Is our field empty? Two basic ways to check
    // First is objects, second is for strings

    bool IsFieldEmpty(SerializedProperty property)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null) return true;
        if (property.propertyType == SerializedPropertyType.String && string.IsNullOrEmpty(property.stringValue)) return true;

        return false;
    }

    bool IsFieldSupported(SerializedProperty property)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference) return true;
        if (property.propertyType == SerializedPropertyType.String) return true;

        return false;
    }


}
