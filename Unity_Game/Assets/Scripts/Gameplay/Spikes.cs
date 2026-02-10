using System.Collections.Generic;
using UnityEngine;

public enum EtrapActivation
{
    Pressure,
    Looping
}

public class Spikes : MonoBehaviour
{
    public EtrapActivation activation = EtrapActivation.Pressure;

    [SerializeField] bool activeOnStart = false;
    [SerializeField] float activeDuration = 0.5f;
    [SerializeField] float activationDelay = 1f;
    [SerializeField] float transitionDuration = 0.5f;

    [SerializeField] Vector3 spikesActivePosition = Vector3.zero;
    [SerializeField] Vector3 spikesIdlePosition = new Vector3(0, -0.5f, 0);

    [Header("Sounds")]
    [SerializeField] AudioClip activationSound;

    [Header("References")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject spikesMesh;

    float timer;
    List<IDamageable> damageables = new List<IDamageable>();

    #region FSM

    enum EState
    {
        Idle,
        Wait,
        TransitionToActive,
        Active,
        TransitionToIdle
    }

    EState state = EState.Idle;

    void ChangeState(EState newState)
    {
        state = newState;
        timer = 0f;

        spikesMesh.SetActive(state != EState.Idle);

        if (state == EState.Active)
        {
            TryDamage();
        }
        else if (state == EState.TransitionToActive)
        {
            audioSource.PlayOneShot(activationSound);
        }
    }

    void UpdateFSM()
    {
        if (state == EState.Wait)
        {
            spikesMesh.transform.localPosition = spikesIdlePosition;

            if (timer >= activationDelay)
                ChangeState(EState.TransitionToActive);
        }
        else if (state == EState.TransitionToActive)
        {
            spikesMesh.transform.localPosition =
                Vector3.Lerp(spikesIdlePosition, spikesActivePosition, timer / transitionDuration);

            if (timer >= transitionDuration)
                ChangeState(EState.Active);
        }
        else if (state == EState.Active)
        {
            if (timer >= activeDuration)
                ChangeState(EState.TransitionToIdle);
        }
        else if (state == EState.TransitionToIdle)
        {
            spikesMesh.transform.localPosition =
                Vector3.Lerp(spikesActivePosition, spikesIdlePosition, timer / transitionDuration);

            if (timer >= transitionDuration)
            {
                ChangeState(
                    activation == EtrapActivation.Looping
                        ? EState.Wait
                        : EState.Idle
                );
            }
        }

        timer += Time.deltaTime;
    }

    #endregion

    void Start()
    {
        if (activation == EtrapActivation.Pressure)
            ChangeState(activeOnStart ? EState.Active : EState.Idle);
        else
            ChangeState(activeOnStart ? EState.Active : EState.Wait);
    }

    void Update()
    {
        UpdateFSM();
    }

    void OnTriggerEnter(Collider other)
    {
        IDamageable health = other.GetComponentInParent<IDamageable>();

        if (health != null && !damageables.Contains(health))
            damageables.Add(health);

        if (health != null && state == EState.Idle && activation == EtrapActivation.Pressure)
            ChangeState(EState.Wait);

        if (health != null && state == EState.Active)
            health.Damage(1);
    }

    void OnTriggerExit(Collider other)
    {
        IDamageable health = other.GetComponentInParent<IDamageable>();

        if (health != null)
            damageables.Remove(health);
    }

    void TryDamage()
    {
        foreach (IDamageable health in damageables)
            health.Damage(1);
    }
}
