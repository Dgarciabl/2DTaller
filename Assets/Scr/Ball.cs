using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _initSpeed = 5;
    [SerializeField]
    private float _minSpeed = 4;
    [SerializeField]
    private float _maxSpeed = 7;

    private const float BALL_VELOCITY_MIN_AXIS_VALUE = 0.5f;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private bool activePowerUp;
    private float maxTime;
    private float velocityMultiplier;

    public void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _collider.enabled = true;
        _rb.velocity = Random.insideUnitCircle.normalized * _initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (activePowerUp)
        {
            if (Time.time > maxTime)
            {
                activePowerUp = false;
                _rb.velocity = _rb.velocity / velocityMultiplier;
            }
        }
        CheckVelocity();
    }

    private void CheckVelocity()
    {
        Vector2 velocity = _rb.velocity;
        float currentSpeed = velocity.magnitude;
        if (currentSpeed < _minSpeed && !activePowerUp)
        {
            velocity = velocity.normalized * _minSpeed;
        }
        else if (currentSpeed > _maxSpeed && !activePowerUp)
        {
            velocity = velocity.normalized * _maxSpeed;
        }

        if (Mathf.Abs(velocity.x) < BALL_VELOCITY_MIN_AXIS_VALUE)
        {
            velocity.x += Mathf.Sign(velocity.x) * BALL_VELOCITY_MIN_AXIS_VALUE * Time.deltaTime;
        }
        else if (Mathf.Abs(velocity.y) < BALL_VELOCITY_MIN_AXIS_VALUE)
        {
            velocity.y += Mathf.Sign(velocity.y) * BALL_VELOCITY_MIN_AXIS_VALUE * Time.deltaTime;
        }
        _rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BlockTile blockTileHit;
        if (!other.collider.TryGetComponent(out blockTileHit))
        {
            return;
        }

        ContactPoint2D contactPoint = other.contacts[0];
        blockTileHit.OnHitCollision(contactPoint);
    }

    public void Hide()
    {
        _collider.enabled = false;
        gameObject.SetActive(false);
    }

    public void changeVelocity(float velocity, int seconds)
    {
        activePowerUp = true;
        velocityMultiplier = velocity;
        _rb.velocity = _rb.velocity * velocity;
        maxTime = Time.time + seconds;
    }
}
