using Scripts.Components;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HeroPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpAcceleration;
    [SerializeField] private float _damageJumpAcceleration;
    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private Vector2 _interactRadius;
    [SerializeField] private LayerMask _interactLayer;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _allowDoubleJump;
    private Collider2D[] _interactItem = new Collider2D[1];


    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsVerticalVelocity = Animator.StringToHash("vertical-velocity");
    private static readonly int hit = Animator.StringToHash("hit");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    private void FixedUpdate()
    {
        Movement();  
        SetAnimation();
    }
    private void Movement()
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalcYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
    }

    private float CalcYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;  
        var isJumpingPressing = _direction.y > 0;

        if (IsGrounded()) _allowDoubleJump = true;
        if (isJumpingPressing)
        {
            yVelocity = CalcJumpVelocity(yVelocity);
        }
        //else if (_rigidbody.velocity.y > 0)
        //{
        //    yVelocity *=  0.75f;
        //}

        return yVelocity;
    }

    private float CalcJumpVelocity(float yVelocity)
    {
        var isFaling = _rigidbody.velocity.y <= 0.001f;
        if (!isFaling) return yVelocity;

        if (IsGrounded())
        {
            yVelocity += _jumpAcceleration;
        }
        else if (_allowDoubleJump)
        {
            yVelocity = _jumpAcceleration;
            _allowDoubleJump = false;
        }

        return yVelocity;
    }

    private void SetAnimation()
    {
        _animator.SetBool(IsRunning, _direction.x != 0);
        _animator.SetBool(IsGroundKey, IsGrounded());
        _animator.SetFloat(IsVerticalVelocity, _rigidbody.velocity.y);
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    private bool IsGrounded()
    {
        return _groundCheck.IsTouchingLayers;
    }
    public void Fire()
    {
        Debug.Log("Fire !!!");
    }
     
    public void TakeDamage()
    {
        _animator.SetTrigger(hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpAcceleration);
    }

    public void Interact()
    {
        var overlapSize = Physics2D.OverlapAreaNonAlloc(
            transform.position, 
            _interactRadius, 
            _interactItem, 
            _interactLayer);
        for (int i = 0; i < overlapSize; i++)
        {
            var interactable = _interactItem[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    //private void Update()
    //{
    //    if (_direction.magnitude > 0)
    //    {
    //        var delta = _direction * _speed * Time.deltaTime;
    //        transform.position = transform.position + new Vector3(delta.x, delta.y, transform.position.z);
    //    }
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = IsGrounded() ? Color.green : Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.1f);
    //}

}
