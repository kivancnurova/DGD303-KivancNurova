using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshair;
    public Image crosshairImage;

    void Awake() 
    {
        crosshairImage = GetComponent<Image>();
    }

    void Start()
    {
        Cursor.visible = false;    
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            Cursor.visible = true;
            crosshairImage.enabled = false;
            return;
        }
        else if(Time.timeScale == 1)
        {
            Cursor.visible = false;
            crosshairImage.enabled = true;
        }


        Vector3 cursorPosition = Input.mousePosition;
        crosshair.position = cursorPosition;
    }
}
