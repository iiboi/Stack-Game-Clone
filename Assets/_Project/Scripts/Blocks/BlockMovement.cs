using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 9f;
    [SerializeField] private float limit = 6f;
    [SerializeField] private int directionFactor = 1;
    [SerializeField] private bool isMovingOnX = true;



    private void Update() 
    {
        MoveBlock();
        CheckLimit();
    }

    private void MoveBlock()
    {
        float moveDelta = speed * Time.deltaTime * directionFactor;

        if (isMovingOnX == true)
        {
            transform.position = new Vector3(transform.position.x + moveDelta, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDelta);
        }
    }

    private void CheckLimit()
    {
        
        if (isMovingOnX)
        {
            if (transform.position.x > limit)
            {
                directionFactor = -1;
            }
            else if (transform.position.x < -limit)
            {
                directionFactor = 1;
            }
        }
        else
        {
            if (transform.position.z > limit)
            {
                directionFactor = -1;
            }
            else if (transform.position.z < -limit)
            {
                directionFactor = 1;
            }
        }
    }

    public void StopMoving()
    {
        enabled = false;
    }
}
