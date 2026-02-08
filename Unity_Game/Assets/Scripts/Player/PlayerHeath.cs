using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerHeath : MonoBehaviour
{
     int points = 6;

    public int Points
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


    public void Damage(int damage)
    {
        points -= damage;
        Damaged.Invoke();
    }

}
    
