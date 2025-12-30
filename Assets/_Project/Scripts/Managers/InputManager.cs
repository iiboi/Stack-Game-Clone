using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BlockSpawnManager blockSpawnManager;

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            blockSpawnManager.StopAndSplitBlock();
        }
    }
}
