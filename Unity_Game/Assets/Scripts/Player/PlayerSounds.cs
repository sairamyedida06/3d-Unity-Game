using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("AudioClips")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip CoinSound;

    [Space(20)]
    [Header("Audiosource")]
    [SerializeField] private AudioSource audioSource;
    public void OnJumped()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void OnCoinCollected()
    {
        audioSource.PlayOneShot(CoinSound);
        
    }
}
