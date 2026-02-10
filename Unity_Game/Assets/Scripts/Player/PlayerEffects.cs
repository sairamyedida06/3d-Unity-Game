using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] GameObject LandingFX;

    public void OnLanded()
    {
        Instantiate(LandingFX, transform.position,Quaternion.identity);
    }

    
}
