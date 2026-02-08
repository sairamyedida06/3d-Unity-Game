using UnityEngine;
using System.Collections.Generic;


public class LevelPorgressTracker : MonoBehaviour
{
    public int RemainingCoins => Coins.Count;

    [SerializeField] List<GameObject> Coins;


    private void Start()
    {
       Coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectble")); 
    }

    private void Update()
    {
        for (int i = Coins.Count - 1; i >= 0; i--) 
        {
            var coin = Coins[i];

            if (coin == null)
            {
                Coins.RemoveAt(i);
            }
        }



        Uimanager.Instance.ProgressDisplay.SetRemainigCoints(RemainingCoins);



    }





}
