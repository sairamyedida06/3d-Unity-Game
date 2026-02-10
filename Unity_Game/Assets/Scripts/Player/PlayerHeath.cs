using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerHeath : MonoBehaviour,IDamageable
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

            points = Mathf.Max(value, 0);

            if (points <= 0 && wasAlive)
            {
                Died.Invoke();
                Uimanager.Instance.ShowGameOverMenu();
            }
        }
    }

    public UnityEvent Damaged;
    public UnityEvent Died;

    public bool Alive => HealthPoints > 0;

    private void Update()
    {
        if (Alive)
        {
            if (transform.position.y < -1f)
            {
                OnFall();

            }
        }

        Uimanager.Instance.HealthDisplay.HealthPoints = this.points;
    }


    void OnFall()
    {
        Damage(points);
   
    }


    public void Damage(int damage)
    {
        HealthPoints -= damage;

        if (Alive)
        {
            Damaged.Invoke();
           
        }
       
    }

}
    
