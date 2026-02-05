using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] GameObject coinVfx;
    Player1 player;

    private void Start()
    {
        player = GetComponent<Player1>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectble"))
        {
            Instantiate(coinVfx,other.transform.position,Quaternion.identity);

            
            Destroy(other.gameObject);

            player.coinCollected.Invoke();
        }
    }

}
