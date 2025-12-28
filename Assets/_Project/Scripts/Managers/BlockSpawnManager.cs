using Unity.Mathematics;
using UnityEngine;

public class BlockSpawnManager : MonoBehaviour
{
#region Inspector

    [Header("Referances")]
    [SerializeField, Tooltip("Kamera Referansı")]
    private CameraController cam;

    [SerializeField, Tooltip("Oluşturulacak bloğun referansı")]
    private GameObject blockPrefab;

    [SerializeField, Tooltip("Oluşturulacak parçanın referansı")]

    private GameObject rubblePrefab;

    [SerializeField, Tooltip("Yerleştirilen son bloğun konumu (Başlangıçta Ground objesinin konumu).")]
    private Transform lastBlock;
    
    [SerializeField, Tooltip("Oyun başladığında Hareket eden blok.")]
    GameObject currentBlock;
    
    [Header("Settings")]
    [SerializeField, Tooltip("Yeni bloğun, doğacağı mesafenin uzaklığı.")]
    private float spawnDistance = 6f;
    
    [SerializeField, Tooltip("Bloğun Göz yumulacak eksen farkı.")]
    private float blockTolerance = 0.13f;
    
    [SerializeField, Tooltip("Bloğun Yüksekliği.")]
    private float blockHeight;
    
    [Header("State / Control")]
    [SerializeField, Tooltip("Bloğun Yönü (İşaretliyse X, İşaretli değilse Z)")]
    private bool isMovingOnX = true;

#endregion

    private void Start() 
    {
        // Bloğun yüksekliğini mesh boyutuna göre al.
        blockHeight = lastBlock.GetComponent<MeshRenderer>().bounds.size.y;

        // İlk hareket eden bloğu oluştur.
        SpawnBlock();
    }

    private void Update() 
    {
        // Fare tıklanınca bloğun hareketini durdur ve kesme işlemini yap.
        if (Input.GetMouseButtonDown(0))
        {
            currentBlock.GetComponent<BlockMovement>().StopMoving();
            SplitBlock();
        }
    }

#region Block Spawn
    public void SpawnBlock()
    {
        // Yeni bloğun doğacağı pozisyon.
        Vector3 spawnPos;

        if (isMovingOnX)
        {
            // X ekseninde doğacak şekilde pozisyonu ayarla.
            spawnPos = new Vector3(
                lastBlock.position.x + spawnDistance,
                lastBlock.position.y + blockHeight,
                lastBlock.position.z
            );
        }
        else
        {
            // Z ekseninde doğacak şekilde pozisyonu ayarla.
            spawnPos = new Vector3(
                lastBlock.position.x,
                lastBlock.position.y + blockHeight,
                lastBlock.position.z + spawnDistance
            );
        }

        // Hesaplanan pozisyonda yeni bir blok oluştur.
        GameObject newBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

        //Kameraya yeni konumunu ayarla.
        cam.MoveUp(lastBlock.position.y);
        
        // Yeni bloğun hareket scriptini al.
        BlockMovement movementscript = newBlock.GetComponent<BlockMovement>();
        
        // Bloğun hareket edeceği ekseni ayarla.
        movementscript.isMovingOnX = isMovingOnX;

        // Bu bloğu şu anki aktif (hareket eden) blok olarak ata.
        currentBlock = newBlock;

        // Yeni bloğun ölçeğini bir önceki blokla aynı yap.
        newBlock.transform.localScale = lastBlock.localScale;
    }
#endregion

#region Block Cut
    public void SplitBlock()
    {
        if (isMovingOnX)
        {
            // Mevcut blok ile önceki blok arasındaki X ekseni farkını hesapla.
            float diffX = currentBlock.transform.position.x - lastBlock.position.x;

            //Oyuncuya blokların X ekseninde üst üste gelmesi için tolerans bırak.
            if (Mathf.Abs(diffX) <= blockTolerance)
            {
                currentBlock.transform.position = new Vector3(lastBlock.position.x,
                currentBlock.transform.position.y, lastBlock.position.z);
                
                diffX = 0;

                Debug.Log("Perfect X !");
                //TODO
                //    EFFECT
            }

            // Kesildikten sonra kalacak yeni genişliği hesapla.
            float newXSize = lastBlock.localScale.x - Mathf.Abs(diffX);
#region Kaybetme koşulu X
            //
            //  KAYBETME KOŞULU
            //
            if (newXSize <= 0)
            {
                GameManager.instance.GameOver();

                currentBlock.AddComponent<Rigidbody>();
                
                return;
            }
#endregion
            // Kesimden sonra bloğun yeni X ekseni ölçüsünü uygula.
            currentBlock.transform.localScale = new Vector3(
                newXSize,
                currentBlock.transform.localScale.y,
                currentBlock.transform.localScale.z
            );
            
            // Kalan parçayı ortalayacak şekilde bloğun pozisyonunu ayarla.
            float newXPosition = lastBlock.position.x + (diffX / 2);

            currentBlock.transform.position = new Vector3(
                newXPosition,
                currentBlock.transform.position.y,
                currentBlock.transform.position.z
            );

            // Eğer parça üretmek gerekiyorsa parça üret.
            if (Mathf.Abs(diffX) > 0)
            {
                //Parçanın X boyutunu hesapla.
                Vector3 rubbleXScale = new Vector3(Mathf.Abs(diffX), lastBlock.localScale.y,
                lastBlock.localScale.z);

                float direction = diffX > 0 ? 1 : -1;
                float rubbleXPos = lastBlock.position.x + (lastBlock.localScale.x / 2 * direction)
                + (diffX / 2);

                //Parçanın X Pozisyonunu hesapla
                Vector3 rubblePos = new Vector3(rubbleXPos, currentBlock.transform.position.y,
                currentBlock.transform.position.z);

                CreateRubble(rubblePos, rubbleXScale);
            }
                //GameManager içerisindeki Score değişkenini artır.
                GameManager.instance.IncreaseScore();
        }
        else
        {
            // Mevcut blok ile önceki blok arasındaki Z ekseni farkını hesapla.
            float diffZ = currentBlock.transform.position.z - lastBlock.position.z;
            
            //Oyuncuya blokların Z ekseninde üst üste gelmesi için tolerans bırak.
            if (Mathf.Abs(diffZ) <= blockTolerance)
            {
                currentBlock.transform.position = new Vector3(lastBlock.position.x,
                currentBlock.transform.position.y, lastBlock.position.z);

                diffZ = 0;

                Debug.Log("Perfect Z !");
                //TODO
                //    EFFECT
            }

            // Kesildikten sonra kalacak yeni derinliği hesapla.
            float newZSize = lastBlock.localScale.z - Mathf.Abs(diffZ);
#region Kaybetme koşulu Z
            //
            // KAYBETME KOŞULU!
            //
            if (newZSize <= 0)
            {
                GameManager.instance.GameOver();

                currentBlock.AddComponent<Rigidbody>();
                
                return;
            }
#endregion
            // Kesimden sonra bloğun yeni ölçeğini uygula.
            currentBlock.transform.localScale = new Vector3(
                currentBlock.transform.localScale.x,
                currentBlock.transform.localScale.y,
                newZSize
            );

            // Kalan parçayı ortalayacak şekilde bloğun pozisyonunu ayarla.
            float newZPosition = lastBlock.position.z + (diffZ / 2);

            currentBlock.transform.position = new Vector3(
                currentBlock.transform.position.x,
                currentBlock.transform.position.y,
                newZPosition
            );

            if (Mathf.Abs(diffZ) > 0)
            {
                
                // Parçanın Z Boyutunu hesapla.
                Vector3 rubbleZScale = new Vector3(lastBlock.localScale.x,
                lastBlock.localScale.y, Mathf.Abs(diffZ));

                float direction = diffZ > 0 ? 1 : -1;
                float rubbleZPos = lastBlock.position.z + (lastBlock.localScale.z / 2 * direction)
                + (diffZ / 2);

                //Parçanın Z Pozisyonunu hesapla
                Vector3 rubblePos = new Vector3(currentBlock.transform.position.x,
                currentBlock.transform.position.y, rubbleZPos);

                CreateRubble(rubblePos, rubbleZScale);

            }
                //GameManager içerisindeki Score değişkenini artır.
                GameManager.instance.IncreaseScore();
        }

        // Bir sonraki blok için hareket eksenini değiştir.
        isMovingOnX = !isMovingOnX;

        // Son yerleştirilen bloğu güncelle.
        lastBlock = currentBlock.transform;

        // Yeni hareket eden bloğu oluştur.
        SpawnBlock();
    }
#endregion
    
#region Create Rubble    
    // Kesilen parçayı oluştur.
    private void CreateRubble(Vector3 pos, Vector3 scale)
    {   
        GameObject rubble;

        // Kesilen parçayı yarat.
        rubble = Instantiate(rubblePrefab, pos, quaternion.identity);

        rubble.transform.position = pos;
        rubble.transform.localScale = scale;

        // 5 Saniye sonra parçayı yok et.
        Destroy(rubble, 5f);

    } 
#endregion
}