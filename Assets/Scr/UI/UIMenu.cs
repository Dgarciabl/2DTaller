using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
        ArkanoidEvent.OnGameStartEvent += OnGameStart;
        ArkanoidEvent.OnMainMenuEvent += OnMainMenu;
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnGameStartEvent -= OnGameStart;
        ArkanoidEvent.OnMainMenuEvent -= OnMainMenu;
    }

    private void OnGameStart()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnMainMenu()
    {
        _canvasGroup.alpha = 1;
    }
}
