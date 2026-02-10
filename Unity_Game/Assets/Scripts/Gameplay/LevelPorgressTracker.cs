using UnityEngine;
using System.Collections.Generic;


public class LevelPorgressTracker : MonoBehaviour
{
    public int RemainingCoins => Coins.Count;

    [SerializeField] List<GameObject> Coins;

    [SerializeField] LevelExit LevelExit;

    private void Start()
    {
       Coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectble")); 
    }

    private void Update()
    {
        int previousremaiCoins = RemainingCoins;
            
        for (int i = Coins.Count - 1; i >= 0; i--) 
        {
            var coin = Coins[i];

            if (coin == null)
            {
                Coins.RemoveAt(i);
            }
        }



        Uimanager.Instance.ProgressDisplay.SetRemainigCoints(RemainingCoins);

        if(previousremaiCoins != RemainingCoins && RemainingCoins == 0)
        {
            LevelExit.GateOpen();
        }


    }





}
