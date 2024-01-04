using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D uiCursor;

    void Start()
    {
        // Set the default cursor at the start.
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseEnter()
    {
        // Call this method when the mouse enters a UI element.
        Cursor.SetCursor(uiCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        // Call this method when the mouse exits the UI element.
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}

