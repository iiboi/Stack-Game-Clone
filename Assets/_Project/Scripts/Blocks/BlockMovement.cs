using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 9f;

    // Bloğun başlangıç pozisyonundan en fazla ne kadar uzağa gidebileceği.
    [SerializeField] private float distanceLimit = 6f;
    
    // Hareket yönü: pozitif eksen için 1, negatif eksen için -1. 
    [SerializeField] private int directionFactor = 1;

    // True ise nesne X ekseninde, false ise Z ekseninde hareket eder.
    [SerializeField] public bool isMovingOnX = true;

    private void Update() 
    {
        MoveBlock();
        CheckLimit();
    }

    private void MoveBlock()
    {
        // Bloğun bu frame’de ne kadar hareket edeceğini hesaplar.
        float moveDelta = speed * Time.deltaTime * directionFactor;

        // Eğer X ekseninde hareket ediyorsa.
        if (isMovingOnX == true)
        {   
            // X eksenindeki pozisyonu günceller.
            transform.position = new Vector3(
                transform.position.x + moveDelta,
                transform.position.y,
                transform.position.z
            );
        }
        else
        {
            // Z eksenindeki pozisyonu günceller.
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + moveDelta
            );
        }
    }

    private void CheckLimit()
    {
        // X eksenindeki hareket sınırlarını kontrol eder.
        if (isMovingOnX)
        {
            // X ekseni sınırlarını kontrol eder.
            if (transform.position.x > distanceLimit)
            {
                directionFactor = -1;
            }
            else if (transform.position.x < -distanceLimit)
            {
                directionFactor = 1;
            }
        }
        else
        {
            // Z ekseni sınırlarını kontrol eder.
            if (transform.position.z > distanceLimit)
            {
                directionFactor = -1;
            }
            else if (transform.position.z < -distanceLimit)
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
