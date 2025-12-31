using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip perfectClip;

    private AudioSource audioSource;

    public float currentPitch = 1.0f;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPerfectSound()
    {
        audioSource.pitch = currentPitch;
        
        audioSource.PlayOneShot(perfectClip);

        currentPitch += 0.1f;

        if (currentPitch > 3.0f)
        {
            currentPitch = 3.0f;
        }
    }
}
