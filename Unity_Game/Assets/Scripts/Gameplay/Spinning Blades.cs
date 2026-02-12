using System.Threading;
using UnityEngine;

public class SpinningBlades : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float spinningSpeed;
    [SerializeField] private float waitDuration = 1f;

    [SerializeField] Transform MeshTransform;
    [SerializeField] Vector3 destinationOffset = new Vector3 (10f, 0f, 0f);


    

    private float timer = 0f;
    enum Estates
    {
        waitStart,
        waitEnd,
        MoveToEnd,
        MoveToStart
    }

    Estates state = Estates.MoveToEnd;

    Vector3 startPosition;
    Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + destinationOffset;

        
    }

    void ChangeState(Estates newstate)
    {
        state = newstate;


        timer = 0f;
    }

    bool MoveTo(Vector3 destination)
    {
        float arriveDistance = 0.5f;

        Vector3 direction = destination - transform.position;
        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;

        return Vector3.Distance(transform.position, destination) <= arriveDistance;
        
    }


    void UpdateFSM()
    {
        if (state == Estates.MoveToEnd)
        { 
            if (MoveTo(endPosition))
            {
                ChangeState(Estates.waitEnd);
            }
        }
        else if (state == Estates.MoveToStart)
        {
            if (MoveTo(startPosition))
            {
                ChangeState(Estates.waitStart);
            }
        }

        else if(state == Estates.waitEnd)
        {
            if(timer >= waitDuration)
            {
                ChangeState(Estates.MoveToStart);
            }
        }
        else if (state == Estates.waitStart)
        {
            if (timer >= waitDuration)
            {
                ChangeState(Estates.MoveToEnd);
            }
        }



        timer += Time.deltaTime;
    }



    private void Update()
    {
        UpdateMesh();
        UpdateFSM();
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponentInParent<IDamageable>();

        if(health != null)
        {
            health.Damage(1);
        }
    }

    void UpdateMesh()
    {
        MeshTransform.Rotate(0f, spinningSpeed * Time.deltaTime, 0f);
    }
}
