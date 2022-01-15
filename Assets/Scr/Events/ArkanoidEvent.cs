using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArkanoidEvent
{
    public delegate void BallDeadZoneAction(Ball ball);
    public static BallDeadZoneAction OnBallReachDeadZoneEvent;

    public delegate void BlockDestroyedAction(int blockID);
    public static BlockDestroyedAction OnBlockDestroyedEvent;

    public delegate void ScoreUpdatedAction(int score, int totalScore);
    public static ScoreUpdatedAction OnScoreUpdatedEvent;

    public delegate void LevelUpdatedAction(int level);
    public static LevelUpdatedAction OnLevelUpdatedEvent;
}
