using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject[] _drawArts;

    public void DrawNewArts()
    {
        foreach (var art in _drawArts)
        {
            art.gameObject.SetActive(true);
        }
    }

    public void DeleteArts()
    {
        foreach (var art in _drawArts)
        {
            art.gameObject.SetActive(false);
        }
    }
}
