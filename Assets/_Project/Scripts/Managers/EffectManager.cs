using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject perfectEffectPrefab;

    public void PlayPerfectEffect(Vector3 effectPosition, Color effectColor, Vector3 effectScale)
    {
        GameObject effect = Instantiate(perfectEffectPrefab);

        effect.transform.position = effectPosition;
        effect.transform.localScale = effectScale;

        SpriteRenderer sr = effect.GetComponentInChildren<SpriteRenderer>();

        if ( sr != null)
        {
            sr.color = effectColor;
        }

        Destroy(effect, 1f);
        
    }
}
