using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    plus50,
    plus100,
    plus250,
    plus500,
    slow,
    fast,
    multi
}

public class PowerUp : MonoBehaviour
{
    private const string POWER_PATH = "Sprites/Power/Power_{0}";

    [SerializeField]
    private PowerType _power = PowerType.plus50;
    private Vector3 direction = new Vector3(0, -1, 0);
    private float speed = 0.5;

    private SpriteRenderer _renderer;
    private Collider2D _collider;
    private Transform _body;

    public void SetData(PowerType power)
    {
        _power = power;
    }

    public void Init()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _renderer.sprite = GetPowerSprite(_power);
    }


    static Sprite GetPowerSprite(PowerType _power)
    {
        string path = string.Empty;
        path = string.Format(POWER_PATH, _power);

        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Resources.Load<Sprite>(path);
    }

    private void Update()
    {
        _body.position.y = speed;
    }
}
