using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Header("Settings")]
    
    // Rengin ne kadar hızlı değişeceğini belirleyen ayar.
    [SerializeField] private float hueStep = 0.03f;
    
    // Bunlar artık 'sabit' değer değil, formülün 'tavan' değerleri gibi çalışacak.
    [SerializeField] private float saturation = 0.45f;
    [SerializeField] private float value = 0.85f;
    
    // Arkaplan renginin geçiş hızı.
    [SerializeField] private float backgroundColorLerpSpeed = 2.5f;

    // Renk, Canlılık ve Işık için birbirinden bağımsız sayaçlar.
    private float currentHue;
    private float satParam;
    private float valParam;
    
    private Color targetBackgroundColor;

    private void Awake() 
    {   
        // Oyun başında hepsine rastgele bir başlangıç noktası veriyoruz.
        // Hepsi yarışa farklı yerden başlasın ki çakışmasınlar.
        currentHue = Random.value;
        satParam = Random.value;
        valParam = Random.value;

        // Oyunun açıldığı ilk karede zemin rengine uygun arka planı ayarla.
        targetBackgroundColor = Color.HSVToRGB(currentHue, saturation * 0.5f, value * 0.5f);
    }

    private void Update() 
    {   
        // Kameranın rengini hedef renge doğru yavaşça kaydır (yumuşak geçiş).
        Camera.main.backgroundColor = Color.Lerp(
            Camera.main.backgroundColor, targetBackgroundColor,
            Time.deltaTime * backgroundColorLerpSpeed
        );
    }

    public Color GetNextColor()
    {   
        // 1. Renk çarkını döndür.
        currentHue += hueStep;

        // Canlılık ve Işık değerlerini de döndür.
        satParam += hueStep; 
        valParam += hueStep;
        
        // Eğer renk 1'i geçerse başa sar (Sonsuz döngü).
        if (currentHue >= 1f)
        {
            currentHue -= 1f;
        }

        // Yeni renge göre arka plan hedefini belirle.
        targetBackgroundColor = Color.HSVToRGB(currentHue, saturation * 0.75f, value * 0.75f);

        // Hesaplanan son rengi gönder.
        return CalculateColor();
    }

    public Color GetCurrentColor()
    {
        // Zemin bloğu istediğinde rengi değiştirmeden sadece hesapla ve ver.
        return CalculateColor();
    }

    private Color CalculateColor()
    {   
        
        // Canlılığı (Saturation) dalgalandır:
        // satParam'ı 3 ile çarpıyoruz ki rengin değişiminden 3 kat hızlı dalgalansın.
        float dynamicSat = Mathf.Lerp(0.2f, 1.0f, (Mathf.Sin(satParam * 3f) + 1f) / 2f);

        // Işığı (Value) dalgalandır:
        // valParam'ı 7 ile çarpıyoruz (asal sayı) ki diğerleriyle hiç denk gelmesin, kafasına göre takılsın.
        float dynamicVal = Mathf.Lerp(0.1f, 1.0f, (Mathf.Cos(valParam * 7f) + 1f) / 2f);
        
        // Hepsini karıştırıp rengi oluştur.
        return Color.HSVToRGB(currentHue, dynamicSat, dynamicVal);
    }
}