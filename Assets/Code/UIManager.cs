using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Assets")]
    [Header("HUD")]
    [SerializeField] private Canvas HUDCanvas;
    [Header("Pause")]
    [SerializeField] private Canvas pauseCanvas;
    [Header("Settings")]
    [SerializeField] private Canvas settingsCanvas;


    [Header("Listener Events")]
    [SerializeField] private EventChannel PauseGameEvent;
    [SerializeField] private EventChannel UnpauseGameEvent;


    private void OnEnable()
    {
        PauseGameEvent.OnEventTriggered += ShowPauseMenu;
        UnpauseGameEvent.OnEventTriggered += HidePauseMenu;
    }

    private void OnDisable()
    {
        PauseGameEvent.OnEventTriggered -= ShowPauseMenu;
        UnpauseGameEvent.OnEventTriggered -= HidePauseMenu;

    }

    private void ShowPauseMenu()
    {
        HUDCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        HUDCanvas.gameObject.SetActive(true);
    }

    public void ShowSettings()
    {
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        settingsCanvas.gameObject.SetActive(false);
        ShowPauseMenu();
    }
}
