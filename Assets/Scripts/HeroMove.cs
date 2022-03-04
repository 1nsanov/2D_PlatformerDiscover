using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpAcceleration;
    [SerializeField] private LayerCheck _groundCheck;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    //private void Update()
    //{
    //    if (_direction.magnitude > 0)
    //    {
    //        var delta = _direction * _speed * Time.deltaTime;
    //        transform.position = transform.position + new Vector3(delta.x, delta.y, transform.position.z);
    //    }
    //}

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        var isJumping = _direction.y > 0;
        if (isJumping)
        {
            if (IsGround())
            {
                _rigidbody.AddForce(Vector2.up * _jumpAcceleration, ForceMode2D.Impulse);
            }
            //else if (_rigidbody.velocity.y > 0)
            //{
            //    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            //}
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color =IsGround() ? Color.green : Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.1f);
    //}

    private bool IsGround()
    {
        return _groundCheck.IsTouchingLayers;
    }

    public void Fire()
    {
        Debug.Log("Fire !!!");
    }
}
