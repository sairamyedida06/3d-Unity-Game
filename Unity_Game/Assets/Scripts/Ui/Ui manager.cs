using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Uimanager : MonoBehaviour
{
    
    public static Uimanager Instance { get; private set; }

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Hud;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject PauseMenu;



    public HealthDisplay HealthDisplay;
    public ProgressDisplay ProgressDisplay;


    
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowHud()
    {
        MainMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Hud.SetActive(true);
    }

    public void ShowManiMenu()
    {
        MainMenu.SetActive(true);
        GameOverMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Hud.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("Menu");
    }
    private void Awake()
    {
        Instance = this;

        GameOverMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Hud.SetActive(false);


    }

    private void Start()
    {
        
    }

    public void ShowGameOverMenu()
    {
        if (PauseMenu.activeSelf)
        {
            return;
        }

        GameOverMenu.SetActive(true);
        ClearCameraTraget();
    }

     void ShowPauseMenu()
    {
        if (GameOverMenu.activeSelf)
        {
            return;
        }

        PauseMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (PauseMenu.activeSelf)
        {
            Debug.Log("PauseMenu Active");
            ResumeGame();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    public void ReStartLevel()
    {
        GameOverMenu.SetActive(false);
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ClearCameraTraget()
    {
        var cam = (CinemachineCamera)CinemachineBrain.GetActiveBrain(0).ActiveVirtualCamera;

        cam.Target.TrackingTarget = null;
    }

    
}
