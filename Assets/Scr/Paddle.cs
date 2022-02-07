using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _movementLimit = 7;

    private Vector3 _targetPosition;
    private Camera _cam;
    private Camera Camera
    {
        get
        {
            if (_cam == null)
            {
                _cam = Camera.main;
            }
            return _cam;
        }
    }

    void Update()
    {
        _targetPosition.x = Camera.ScreenToWorldPoint(Input.mousePosition).x;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -_movementLimit, _movementLimit);
        _targetPosition.y = this.transform.position.y;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
    }

    public void changeHorizontalScale(float scale)
    {

        gameObject.transform.localScale += new Vector3(scale, 0, 0);
    }

    public void resetHorizontalScale(float scale)
    {

        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
