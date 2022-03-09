using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HeroMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpAcceleration;
    [SerializeField] private LayerCheck _groundCheck;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsVerticalVelocity = Animator.StringToHash("vertical-velocity");

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
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
        var isJumping = _direction.y > 0;
        if (isJumping)
        {
            if (IsGrounded() && _rigidbody.velocity.y <= 0.01)
            {
                _rigidbody.AddForce(Vector2.up * _jumpAcceleration, ForceMode2D.Impulse);
            }
        }
        else if (_rigidbody.velocity.y > 0) 
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.75f);
        }
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
    //    Gizmos.color =IsGround() ? Color.green : Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.1f);
    //}

}
