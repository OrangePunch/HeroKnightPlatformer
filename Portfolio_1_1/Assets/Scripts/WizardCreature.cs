using System.Collections;
using UnityEngine;

public class WizardCreature : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private bool _invertScale;
    [SerializeField] private float speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _damageVelocity;
    [SerializeField] private float _sizeX;
    [SerializeField] private float _sizeY;
    [SerializeField] private float _sizeZ;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _attackSound;

    [Header("Checkers")]
    //[SerializeField] protected LayerMask _groundLayer;
    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private MobCheckCircleOverlap _attackRange;
    //[SerializeField] protected SpawnListComponent _particles;


    private Rigidbody2D Rigidbody;
    private Animator Animator;
    private Hero _hero;
    private bool IsGrounded;
    private Vector2 Direction;
    private bool _isJumping;
    private AudioSource _playerAudio;

    //private static readonly int verticalVelocity = Animator.StringToHash("vartical velocity");
    private static readonly int isRunning = Animator.StringToHash("Running");
    private static readonly int Hit = Animator.StringToHash("Hurt");
    private static readonly int AttackKey = Animator.StringToHash("Attack");

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _hero = GameObject.Find("LightBandit").GetComponent<Hero>();
        _playerAudio = GetComponent<AudioSource>();
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
        if (speed != 0)
        {
            var xVelocity = Direction.x * speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(isRunning, Direction.x != 0);
            //Animator.SetFloat(verticalVelocity, Rigidbody.velocity.y);
            //     Animator.SetBool(isGroundKey, IsGrounded);

            UpdateSpriteDirection();
        }
    }

    protected virtual float CalculateYVelocity()
    {
        var yVelocity = Rigidbody.velocity.y;
        var isJumpPressing = Direction.y > 0; // ïðûæîê

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
            //_particles.Spawn("Jump");
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

    public virtual void TakeDamage()
    {
        Animator.SetTrigger(Hit);
        _playerAudio.PlayOneShot(_damageSound, 0.7f);
    }

    public virtual void Attack()
    {
        Animator.SetTrigger(AttackKey);
    }

    public void OnAttack()
    {
        Invoke(nameof(StartPlay), 0.1f);
        if (_hero.m_forceField == false)
        {
            _attackRange.Check();
            //_particles.Spawn("Slash");
        }
    }

    public void StartPlay()
    {
        _playerAudio.PlayOneShot(_attackSound, 0.3f);
    }
}
