using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArkanoidEvent
{
    public delegate void BallDeadZoneAction(Ball ball);
    public static BallDeadZoneAction OnBallReachDeadZoneEvent;

    public delegate void BlockDestroyedAction(int blockID);
    public static BlockDestroyedAction OnBlockDestroyedEvent;

    //UI Events
    public delegate void ScoreUpdatedAction(int score, int totalScore);
    public static ScoreUpdatedAction OnScoreUpdatedEvent;

    public delegate void LevelUpdatedAction(int level);
    public static LevelUpdatedAction OnLevelUpdatedEvent;

    public delegate void GameStartAction();
    public static GameStartAction OnGameStartEvent;

    public delegate void GameOverAction(int FinalScore);
    public static GameOverAction OnGameOverEvent;

    public delegate void VictoryAction(int FinalScore);
    public static VictoryAction OnVictoryEvent;

    public delegate void GameOverMenuAction();
    public static GameOverMenuAction OnGameOverMenuEvent;

    public delegate void VictoryMenuAction();
    public static VictoryMenuAction OnVictoryMenuEvent;

    public delegate void ExitMenuAction();
    public static ExitMenuAction OnExitMenuEvent;

    //PowerUp Events
    public delegate void PowerUpScoreAction(int score);
    public static PowerUpScoreAction OnPowerUpScoreEvent;

    public delegate void PowerUpChangeBallSpeed(float velocity, int seconds);
    public static PowerUpChangeBallSpeed OnPowerUpChangeBallSpeedEvent;

    public delegate void PowerUpChangeScalePaddle(float scale);
    public static PowerUpChangeScalePaddle OnPowerUpChangeScalePaddleEvent;

    public delegate void PowerUpAddMoreBalls();
    public static PowerUpAddMoreBalls OnPowerUpAddMoreBallsEvent;

}
