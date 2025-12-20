using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float directionLimit;
    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        float newX = Mathf.PingPong(Time.time * speed, 4);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
