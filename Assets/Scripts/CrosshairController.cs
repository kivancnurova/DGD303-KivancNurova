using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshair;

    void Start()
    {
        Cursor.visible = false;    
    }

    void Update()
    {
        Vector3 cursorPosition = Input.mousePosition;
        crosshair.position = cursorPosition;
    }
}
