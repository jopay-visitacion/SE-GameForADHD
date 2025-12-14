using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFood : MonoBehaviour
{
    Camera mainCam;
    bool isDragging;
    Vector3 offset;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        -mainCam.transform.position.z)
        );

        offset = transform.position - mouseWorld;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        -mainCam.transform.position.z)
        );

        transform.position = mouseWorld + offset;
    }

}