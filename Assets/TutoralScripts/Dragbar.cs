using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Dragbar : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    public RectTransform _fenster;
    private float _width;

    public void OnBeginDrag(PointerEventData eventData)
    {
        middle();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _fenster.transform.position = new Vector3(Input.mousePosition.x - _width, Input.mousePosition.y);
    }


    void middle()
    {
        var p = new Vector3[4];
        _fenster.GetWorldCorners(p);
        _width = Mathf.Abs(p[0].x - Input.mousePosition.x);
    }

}
