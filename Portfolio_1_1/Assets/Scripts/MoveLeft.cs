using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody.AddForce(Vector2.left * _speed, ForceMode2D.Impulse);
    }
}
