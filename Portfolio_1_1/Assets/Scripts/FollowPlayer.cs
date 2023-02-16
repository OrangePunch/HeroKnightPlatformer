using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetZ;

    private void LateUpdate()
    {
        transform.position = _player.transform.position + new Vector3(_offsetX, _offsetY, _offsetZ);
    }
}
