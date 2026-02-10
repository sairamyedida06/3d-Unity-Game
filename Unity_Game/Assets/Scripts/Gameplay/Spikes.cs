using System.Linq.Expressions;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] float activeDuration = .5f;
    [SerializeField] float TransitionDuraion = .5f;

    [SerializeField] Vector3 SpikesActivePosition = Vector3.zero;
    [SerializeField] Vector3 SpikesIdelePosition = new Vector3(0,-.5f,0);

    [Header("Sounds")] 
    [SerializeField] AudioClip activationSound = null;


    [Header("References")]
    [SerializeField] AudioSource audioSourse;
    [SerializeField] GameObject SpikesMesh;

    private float timer;
    enum EState
    {
        Idle,
        TransitionToActive,
        Active,
        TransitionToIdle
    }

    EState state = EState.Idle;

    void ChangeState(EState newstate)
    {
        state = newstate;

        timer = 0;
        if(state == EState.Idle )
        {
            SpikesMesh.SetActive(false);
        }
        else
        {
            SpikesMesh.SetActive(true);
        }

        if(state == EState.TransitionToActive )
        {
            audioSourse.PlayOneShot(activationSound);
        }

    }
       
    
    void Start()
    {
        SpikesMesh.SetActive(false);
    }


    private void Update()
    {
        if (state == EState.TransitionToActive) 
        {
            Vector3 P = Vector3.Lerp(SpikesIdelePosition, SpikesActivePosition, timer/ TransitionDuraion);

            SpikesMesh.transform.localPosition = P;

            if(timer >= TransitionDuraion)
            {
                ChangeState(EState.Active);
            }

        }
        else if(state == EState.TransitionToIdle)
        {
            Vector3 P = Vector3.Lerp(SpikesActivePosition, SpikesIdelePosition, timer / TransitionDuraion);
            SpikesMesh.transform.localPosition = P;

            if (timer >= TransitionDuraion)
            {
                ChangeState(EState.Idle);
            }
        }
        else if (state == EState.Active)
        {
            if(timer >= activeDuration)
            {
                ChangeState(EState.TransitionToIdle);
            }
        }






            timer += Time.deltaTime;
    }

    [ContextMenu("Activate")]
    public void Activate()
    {
        if (state == EState.Idle)
        {
            ChangeState(EState.TransitionToActive);
        }

        
    }
}
