using UnityEditor;
using UnityEngine;

/// <summary>
/// Create context menu for TextMeshPro.
/// Menu item "Set Readonly" makes component readonly
/// Menu item "Set Editable" makes component editable
/// </summary>
public static class ReadOnlyText
{
    [MenuItem("CONTEXT/TextMeshPro/Set Readonly")]
    static void SetReadonly(MenuCommand command)
    {
        command.context.hideFlags = HideFlags.NotEditable;
    }

    [MenuItem("CONTEXT/TextMeshPro/Set Editable")]
    static void SetWriteble(MenuCommand command)
    {
        command.context.hideFlags = HideFlags.None;
    }
}
