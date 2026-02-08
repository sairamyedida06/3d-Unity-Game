using UnityEngine;
using UnityEngine.Rendering;

public class Blink : MonoBehaviour
{
    [SerializeField] GameObject[] Target;
    [SerializeField] float OnDuration = 0.2f;
    [SerializeField] float OffDuration = 0.4f;

    float timer = 0f;
    bool state = true;

    float blinkStopTime = 0f;

    public void ActiveBlink(float duration)
    {
        blinkStopTime = Time.time + duration ;
    }




    private void Update()
    {
        if (Time.time <= blinkStopTime)
        {
            UpdateBlinkFSM();
        }
        else
        {
            SetTargetActive(true);
        }

    }

    void UpdateBlinkFSM()
    {
        if (state == true)
        {
            SetTargetActive(true);
            if (timer >= OnDuration)
            {
                timer = 0f;
                state = false;
            }
        }
        else
        {
            SetTargetActive(false);

            if (timer >= OffDuration)
            {
                timer = 0f;
                state = true;
            }
        }




        timer += Time.deltaTime;
  
   
    }


    void SetTargetActive(bool active)
    {
        for (int i = 0; i < Target.Length; i++)
        {
            Target[i].SetActive(active);
        }
    }
}

    

