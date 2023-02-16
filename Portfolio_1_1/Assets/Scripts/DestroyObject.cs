using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDestroy;
    [SerializeField] private float _timeToDestroy;
    public void DestroyThisObject()
    {
        Destroy(_objectToDestroy, _timeToDestroy);
    }
}
