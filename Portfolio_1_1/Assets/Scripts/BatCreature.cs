using UnityEngine;

public class BatCreature : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private bool _invertScale;
    [SerializeField] protected float _jumpSpeed;
    [SerializeField] private float _damageVelocity;
    [SerializeField] private float _sizeX;
    [SerializeField] private float _sizeY;
    [SerializeField] private float _sizeZ;

    [SerializeField] private float _offset;

    [SerializeField] protected float _speed;
    [SerializeField] private bool _invertX;
    [SerializeField] private AudioClip _attackSound;

    private int direction;

    [Header("Checkers")]
    //[SerializeField] protected LayerMask _groundLayer;
    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private MobCheckCircleOverlap _attackRange;
    //[SerializeField] protected SpawnListComponent _particles;


    private Rigidbody2D Rigidbody;
    private Animator Animator;
    private bool IsGrounded;
    private Vector2 Direction;
    private bool _isJumping;
    private AudioSource _playerAudio;

    //private static readonly int verticalVelocity = Animator.StringToHash("vartical velocity");
    private static readonly int isRunning = Animator.StringToHash("Running");

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();

        var mod = _invertX ? -1 : 1;

        direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    protected virtual void Update()
    {
        IsGrounded = _groundCheck.IsTouchingLayer;
    }

    private void FixedUpdate()
    {
        var xVelocity = Direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        //var lookForward = (_target.transform.position - transform.position).normalized;
        //transform.Translate(lookForward * _speed);

        Animator.SetBool(isRunning, Direction.x != 0);

        UpdateSpriteDirection();
    }

    protected virtual float CalculateYVelocity()
    {
        var yVelocity = Rigidbody.velocity.y;
        var isJumpPressing = Direction.y > 0;

        if (IsGrounded)
        {
            _isJumping = false;
        }

        if (isJumpPressing)
        {
            _isJumping = true;

            var isFalling = Rigidbody.velocity.y <= 0.001f;

            yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
        }
        else if (Rigidbody.velocity.y > 0 && _isJumping)
        {
            yVelocity *= 0.5f;
        }

        return yVelocity;
    }

    protected virtual float CalculateJumpVelocity(float yVelocity)
    {
        if (IsGrounded)
        {
            yVelocity = _jumpSpeed;
        }

        return yVelocity;
    }

    private void UpdateSpriteDirection()
    {
        var multiplier = _invertScale ? -_sizeX : _sizeX;

        if (Direction.x > 0)
        {
            transform.localScale = new Vector3(multiplier, _sizeY, _sizeZ);
        }
        else if (Direction.x < 0)
        {
            transform.localScale = new Vector3(-1 * multiplier, _sizeY, _sizeZ);
        }
    }

    public virtual void Attack()
    {
        Animator.SetTrigger("Death");
        _speed = 0;
    }

    public void OnAttack()
    {
        _attackRange.Check();
        _playerAudio.PlayOneShot(_attackSound, 0.4f);
    }
}
