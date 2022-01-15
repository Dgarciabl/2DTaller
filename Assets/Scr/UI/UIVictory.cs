using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIVictory : MonoBehaviour
{
    private const string Final_SCORE_TEXT_TEMPLATE = "Final Score: {0} pts";
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _scoreText;

    void Start()
    {
        _scoreText = transform.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        ArkanoidEvent.OnGameOverEvent += OnGameOver;
        ArkanoidEvent.OnMainMenuEvent += OnMainMenu;
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnGameOverEvent -= OnGameOver;
        ArkanoidEvent.OnMainMenuEvent -= OnMainMenu;
    }

    private void OnGameOver(int FinalScore)
    {
        _scoreText.text = string.Format(Final_SCORE_TEXT_TEMPLATE, FinalScore);
        _canvasGroup.alpha = 1;
    }

    private void OnMainMenu()
    {
        _canvasGroup.alpha = 0;
    }
}
