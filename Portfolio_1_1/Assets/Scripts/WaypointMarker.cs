using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointMarker : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Transform _target;

    private void Update()
    {
        float minX = _image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = _image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(_target.position);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        _image.transform.position = pos;
    }
}
