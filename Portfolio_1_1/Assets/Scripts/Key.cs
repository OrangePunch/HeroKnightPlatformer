using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private string _tag;

    public void ChangeTag()
    {
        if (_target.gameObject.tag != _tag)
        {
            _target.gameObject.tag = _tag;
        }
    }
}
