using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject HealthIconPrefab;

    [SerializeField] List <GameObject> Icons;

    [SerializeField] private int points = 6;

    public int HealthPoints
    {
        get
        {
            return points;
        }

        set
        {
            int oldValue = points;

            points = value;

            ManageIcons(points - oldValue);
        }
    }

    void ManageIcons(int deltaPoints)
    {
        if (deltaPoints == 0)
        {
            return;
        }

        if (deltaPoints > 0)
        {
            for (int i = 0; i < deltaPoints; i++)
            {
               var  newIcon = Instantiate(HealthIconPrefab, transform);

                Icons.Add(newIcon);
            }


        }
        else
        {
            for (int i = 0; i < -deltaPoints; i++)
            {
                var icon = Icons[Icons.Count - 1];

                Icons.Remove(icon);
                Destroy(icon);
            }
        }
    }
}

