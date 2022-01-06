using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _initSpeed = 5;

    private Rigidbody2D _rb;
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

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
}
