using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Uimanager : MonoBehaviour
{
    
    public static Uimanager Instance { get; private set; }


    [SerializeField] GameObject GameOverMenu;

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
    }


    public void ReStartLevel()
    {
        GameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
