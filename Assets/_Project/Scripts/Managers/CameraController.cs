using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Kamera ile ilk bloğun arasındaki mesafe.
    [SerializeField] private float offset;
    private Camera cam;

    private void Awake() 
    {
        cam = GetComponent<Camera>();
    }

    // Kamera yukarı çıkar.
    public void MoveUp(float targetY)
    {
        transform.DOMoveY(targetY + offset, 1.3f).SetEase(Ease.OutSine);
    }

    // Oyun sonu kamerayı geriye çek.
    public void ShowTower(float topStackPosition)
    {
        float targetSize = (topStackPosition / 2f) + 5f;

        targetSize = Mathf.Clamp(targetSize, 5f, 12f);

        float targetY;

        if (targetSize < 12f)
        {
            targetY = topStackPosition / 2f;
        }
        else
        {
            targetY = topStackPosition -5f;
        }

        transform.DOMoveY(targetY + offset, 4f).SetEase(Ease.OutBack);

        cam.DOOrthoSize(targetSize, 4f).SetEase(Ease.OutBack);
    }
}

