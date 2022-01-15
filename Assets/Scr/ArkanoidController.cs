using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkanoidController : MonoBehaviour
{

    [SerializeField]
    private GridController _gridController;

    [Space(20)]
    [SerializeField]
    private List<LevelData> _levels = new List<LevelData>();

    private int _totalScore = 0;
    private bool _GO = false;
    private bool game = true;

    private const string BALL_PREFAB_PATH = "Prefabs/Ball";
    private readonly Vector2 BALL_INIT_POSITION = new Vector2(0, -0.86f);

    private Ball _ballPrefab = null;
    private List<Ball> _balls = new List<Ball>();

    private int _currentLevel = 0;

    private void Start()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent += OnBallReachDeadZone;
        ArkanoidEvent.OnBlockDestroyedEvent += OnBlockDestroyed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (game)
            {
                InitGame();
            }
            else
            {
                game = true;
                if (_GO)
                {
                    ArkanoidEvent.OnVictoryMenuEvent?.Invoke();
                }
                else
                {
                    ArkanoidEvent.OnGameOverMenuEvent?.Invoke();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearBalls();
            _gridController.ClearGrid();
            ArkanoidEvent.OnExitMenuEvent?.Invoke();
        }
    }

    private void InitGame()
    {
        _currentLevel = 0;
        _totalScore = 0;
        _currentLevel = 0;
        _gridController.BuildGrid(_levels[0]);
        ArkanoidEvent.OnGameStartEvent?.Invoke();
        ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(0, _totalScore);
        ArkanoidEvent.OnLevelUpdatedEvent?.Invoke(_currentLevel);
        SetInitialBall();
    }

    private void SetInitialBall()
    {
        ClearBalls();
        Ball ball = CreateBallAt(BALL_INIT_POSITION);
        ball.Init();
        _balls.Add(ball);
    }

    private Ball CreateBallAt(Vector2 position)
    {
        if (_ballPrefab == null)
        {
            _ballPrefab = Resources.Load<Ball>(BALL_PREFAB_PATH);
        }
        return Instantiate(_ballPrefab, position, Quaternion.identity);
    }

    private void ClearBalls()
    {
        for (int i = _balls.Count - 1; i >= 0; i--)
        {
            _balls[i].gameObject.SetActive(false);
            Destroy(_balls[i]);
        }

        _balls.Clear();
    }

    private void OnBallReachDeadZone(Ball ball)
    {
        ball.Hide();
        _balls.Remove(ball);
        Destroy(ball.gameObject);
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        //Game over
        if (_balls.Count == 0)
        {
            ClearBalls();
            _gridController.ClearGrid();
            game = false;
            _GO = false;
            ArkanoidEvent.OnGameOverEvent?.Invoke(_totalScore);
        }
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent -= OnBallReachDeadZone;
        ArkanoidEvent.OnBlockDestroyedEvent -= OnBlockDestroyed;
    }

    private void OnBlockDestroyed(int blockId)
    {
        BlockTile blockDestroyed = _gridController.GetBlockBy(blockId);
        if (blockDestroyed != null)
        {
            _totalScore += blockDestroyed.Score;
            ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(blockDestroyed.Score, _totalScore);
        }

        if (_gridController.GetBlocksActive() == 0)
        {
            _currentLevel++;
            if (_currentLevel >= _levels.Count)
            {
                ClearBalls();
                game = false;
                _GO = true;
                ArkanoidEvent.OnVictoryEvent?.Invoke(_totalScore);
            }
            else
            {
                ArkanoidEvent.OnLevelUpdatedEvent?.Invoke(_currentLevel);
                SetInitialBall();
                _gridController.BuildGrid(_levels[_currentLevel]);
            }

        }
    }

}
