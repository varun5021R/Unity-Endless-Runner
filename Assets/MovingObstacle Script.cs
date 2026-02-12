using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveRange = 1.5f;
    public float speed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos +
            Vector3.right * Mathf.Sin(Time.time * speed) * moveRange;
    }
}
