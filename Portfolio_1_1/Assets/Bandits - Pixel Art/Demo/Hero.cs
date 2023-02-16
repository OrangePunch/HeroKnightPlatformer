using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float _damageVelocity;

    [SerializeField] private CheckCircleOverlap _interactionCheck;
    [SerializeField] private CheckCircleOverlap _attackRange;
    [SerializeField] private GameObject _forceFieldActive;

    [SerializeField] private AudioClip _onAttackSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _jumpSound;

    private AudioSource _playerAudio;
    private Animator m_animator;
    private Rigidbody2D _rigidbody;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;

    private GameSession _session;
    private GameManager _gameManager;

    public bool m_combatIdle = false;
    public bool m_forceField = false;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        _playerAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _gameManager = FindObjectOfType<GameManager>();

        var health = GetComponent<Health>();
        health.SetHealth(_session.Data.Hp);
    }

    public void OnHealthChanged(float currentHealth)
    {
        _session.Data.Hp = currentHealth;
    }

    private void Update()
    {
        _gameManager.UpdateDamage();
        _gameManager.UpdateHealth();
        _gameManager.UpdateCoinsAmount();

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            m_animator.SetBool("Running", true);
            transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
        }
        else if (inputX < 0)
        {
            m_animator.SetBool("Running", true);
            transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        else if (inputX == 0)
        {
            m_animator.SetBool("Running", false);
        }

        // Move
        _rigidbody.velocity = new Vector2(inputX * m_speed, _rigidbody.velocity.y);

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (m_combatIdle)
            {
                m_combatIdle = false;
                m_forceField = false;
                m_speed = 5;
            }

            ActivateForceField();
            m_animator.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
        {
            m_combatIdle = !m_combatIdle;

            if (!m_combatIdle)
            {
                m_speed = 5;
                m_forceField = false;
                ActivateForceField();
            }

            if (m_combatIdle)
            {
                m_speed = 2;
                m_forceField = true;
                ActivateForceField();
            }
        }
        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            _playerAudio.PlayOneShot(_jumpSound);
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        else if (Input.GetKeyDown("e"))
        {
            Interact();
        }
        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    public void TakeDamage()
    {
        m_animator.SetTrigger("Hurt");
        _rigidbody.velocity = new Vector2(-_rigidbody.velocity.x, _damageVelocity);
        _playerAudio.PlayOneShot(_damageSound, 0.4f);
    }

    public void Interact()
    {
        _interactionCheck.Check();
    }

    public virtual void Attack()
    {
        m_animator.SetTrigger("Attack");
    }

    public void OnAttack()
    {
        _attackRange.Check();
        _playerAudio.PlayOneShot(_onAttackSound);
    }

    public void ActivateForceField()
    {
        if (m_forceField)
        {
            _forceFieldActive.SetActive(true);
        }
        else if (!m_forceField)
        {
            _forceFieldActive.SetActive(false);
        }
    }
}

