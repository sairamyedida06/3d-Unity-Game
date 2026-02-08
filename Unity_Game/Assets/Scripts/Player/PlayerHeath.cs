using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerHeath : MonoBehaviour
{
     [SerializeField] int points = 6;

    public int HealthPoints
    { get
        {
            return points;
        }

        set
        {
            bool wasAlive = points > 0;

            points = value;

            if (points <= 0 && wasAlive)
            {
                Died.Invoke();
            }
        }
    }

    public UnityEvent Damaged;
    public UnityEvent Died;

    public bool Alive => HealthPoints > 0;


    public void Damage(int damage)
    {
        HealthPoints -= damage;

        if (Alive)
        {
            Damaged.Invoke();
        }
       
    }

}
    
