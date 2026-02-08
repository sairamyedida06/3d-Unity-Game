using UnityEngine;

public class PlayerTrapActivator : MonoBehaviour
{
    PlayerHeath health;

    private void Start()
    {
        health = GetComponent<PlayerHeath>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Spikes trap = other.GetComponent<Spikes>();

        if (trap != null) 
        {
            Debug.Log("Player");
            trap.Activate();
            Damageplayer();

        }
    }



    void Damageplayer()
    {
        health.Damage(1);
    }
}

