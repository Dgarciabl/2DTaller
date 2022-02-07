using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    multiball,
    score50,
    score100,
    score250,
    score500,
    slow,
    fast,
    paddlesmall,
    paddlelarge,
}
public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    private const string POWER_UP_PATH = "Sprites/PowerUp/pu_{0}";
    private PowerUpType type;
    private SpriteRenderer _renderer;

    [SerializeField]
    private float scale = 0.5f;

    [SerializeField]
    int _effectTime = 5;
    [SerializeField]
    float _velocityChange = 2;
    [SerializeField]
    float yVelocity = 0.5f;

    [SerializeField]
    private Paddle _paddle;

    private void Start()
    {
        Init();
    }

    private void Update()
    {

    }
    private void Init()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        type = (PowerUpType)Random.Range(0, 9);
        _renderer.sprite = GetPowerUpSprite();

    }

    private Sprite GetPowerUpSprite()
    {
        string path = string.Empty;
        path = string.Format(POWER_UP_PATH, type);
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Resources.Load<Sprite>(path);
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Paddle")
        {
            Effect();
            Destroy(gameObject);
        }
        if (obj.gameObject.tag == "Bottom")
        {
            Destroy(gameObject);
        }
    }

    private void Effect()
    {
        switch (type)
        {
            case PowerUpType.score50:
                {
                    Score(50);
                    break;
                }
            case PowerUpType.score100:
                {
                    Score(100);
                    break;
                }
            case PowerUpType.score250:
                {
                    Score(250);
                    break;
                }
            case PowerUpType.score500:
                {
                    Score(500);
                    break;
                }
            case PowerUpType.multiball:
                {
                    MultiBall();
                    break;
                }
            case PowerUpType.slow:
                {
                    SlowBall();
                    break;
                }
            case PowerUpType.fast:
                {
                    FastBall();
                    break;
                }
            case PowerUpType.paddlelarge:
                {
                    ScalePaddle(scale);
                    break;
                }
            case PowerUpType.paddlesmall:
                {
                    ScalePaddle(-scale);
                    break;
                }
            default: break;
        }
    }

    private void MultiBall()
    {
        ArkanoidEvent.OnPowerUpAddMoreBallsEvent?.Invoke();
    }
    private void Score(int score)
    {
        ArkanoidEvent.OnPowerUpScoreEvent?.Invoke(score);
    }
    private void SlowBall()
    {
        ArkanoidEvent.OnPowerUpChangeBallSpeedEvent?.Invoke(1 / _velocityChange, _effectTime);
    }
    private void FastBall()
    {
        ArkanoidEvent.OnPowerUpChangeBallSpeedEvent?.Invoke(_velocityChange, _effectTime);
    }

    private void ScalePaddle(float scale)
    {
        ArkanoidEvent.OnPowerUpChangeScalePaddleEvent?.Invoke(scale);
    }

}
