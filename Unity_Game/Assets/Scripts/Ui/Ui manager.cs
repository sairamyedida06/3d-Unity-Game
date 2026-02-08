using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Uimanager : MonoBehaviour
{
    
    public static Uimanager Instance { get; private set; }


    [SerializeField] GameObject GameOverMenu;

    public HealthDisplay HealthDisplay;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       GameOverMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        GameOverMenu.SetActive(true);
        ClearCameraTraget();
    }


    public void ReStartLevel()
    {
        GameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ClearCameraTraget()
    {
        var cam = (CinemachineCamera)CinemachineBrain.GetActiveBrain(0).ActiveVirtualCamera;

        cam.Target.TrackingTarget = null;
    }



}
