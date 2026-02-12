using System.Collections;
using UnityEngine;


public class SceneInitalizer : MonoBehaviour
{
    [SerializeField] Uimanager UiManagerPrefab;

    private void Awake()
    {
        if(Uimanager.Instance == null)
        {
            Instantiate(UiManagerPrefab);

            DontDestroyOnLoad(Uimanager.Instance);
        }
    }
    public enum SceneType
    {
        GamepLay,
        MainMenu
    }
    [SerializeField] SceneType sceneType = SceneType.GamepLay;

    private void Start()
    {
        if(sceneType == SceneType.GamepLay)
        {
            Uimanager.Instance.ShowHud();
        }
        else if(sceneType == SceneType.MainMenu)
        {
            Uimanager.Instance.ShowManiMenu();
        }
    }







}

 