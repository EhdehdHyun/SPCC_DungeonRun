using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; //이동 시작 점
    public Transform pointB; //이동 도착 점
    public float speed = 2f;
    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
