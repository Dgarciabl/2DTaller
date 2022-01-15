using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevelScore : MonoBehaviour
{
    private const string SCORE_TEXT_TEMPLATE = "{0} pts";
    private const string LEVEL_TEXT_TEMPLATE = "Level {0}";

    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _levelText;

    void Start()
    {
        _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        ArkanoidEvent.OnScoreUpdatedEvent += OnScoreUpdated;
        ArkanoidEvent.OnLevelUpdatedEvent += OnLevelUpdated;
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnScoreUpdatedEvent -= OnScoreUpdated;
        ArkanoidEvent.OnLevelUpdatedEvent -= OnLevelUpdated;
    }

    private void OnScoreUpdated(int score, int totalScore)
    {
        _scoreText.text = string.Format(SCORE_TEXT_TEMPLATE, totalScore);
    }

    private void OnLevelUpdated(int level)
    {
        _levelText.text = string.Format(LEVEL_TEXT_TEMPLATE, level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
