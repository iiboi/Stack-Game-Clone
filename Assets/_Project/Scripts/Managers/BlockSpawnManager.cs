using UnityEngine;

public class BlockSpawnManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Transform lastBlock;
    
    [Header("Settings")]
    [SerializeField] private float spawnDistance = 6f;
    
    [SerializeField] private bool isMovingOnX = true;
    private float blockHeight;

    GameObject currentBlock;

    private void Start() 
    {
        blockHeight = lastBlock.GetComponent<MeshRenderer>().bounds.size.y;
        SpawnBlock();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBlock.GetComponent<BlockMovement>().StopMoving();
            SpawnBlock();
        }
        
    }

    public void SpawnBlock()
    {
        Vector3 spawnPos;

        if(isMovingOnX)
        {
            spawnPos = new Vector3(lastBlock.position.x + spawnDistance, lastBlock.position.y + blockHeight, lastBlock.position.z);
        }
        else
        {
            spawnPos = new Vector3(lastBlock.position.x, lastBlock.position.y + blockHeight, lastBlock.position.z + spawnDistance);
        }

        GameObject newBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

        BlockMovement movementscript = newBlock.GetComponent<BlockMovement>();

        movementscript.isMovingOnX = isMovingOnX;

        lastBlock = newBlock.transform;

        isMovingOnX = !isMovingOnX;

        currentBlock = newBlock;
    }
}
