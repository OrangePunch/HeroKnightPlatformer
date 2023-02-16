using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _state;
    [SerializeField] private string _animationKey;

    public void SwitchObject()
    {
        _state = !_state;
        _animator.SetBool(_animationKey, _state);
    }

    [ContextMenu("Switch")]
    public void SwitchIt()
    {
        SwitchObject();
    }
}
