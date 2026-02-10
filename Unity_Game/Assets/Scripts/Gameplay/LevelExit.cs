using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] Collider GateCollider;
    [SerializeField] Transform gateMesh;
    [SerializeField] string targetScene;

    bool open = false;

    public void GateOpen()
    {
        open = true;

        GateCollider.enabled = true;

       
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene(targetScene);
    }

    private void Update()
    {
        if (open)
        {
            Vector3 targetPosition = new Vector3(0f, .8f, -1.53f);

            gateMesh.localPosition = Vector3.Lerp(gateMesh.localPosition, targetPosition, 2f * Time.deltaTime);
        }
    }
}
