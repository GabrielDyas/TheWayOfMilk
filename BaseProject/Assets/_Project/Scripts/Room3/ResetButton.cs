using UnityEngine;
using UnityEngine.InputSystem;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    private bool isPlayerNear = false;

    private void Awake()
    {
        // Tenta encontrar o PuzzleManager automaticamente se n�o for atribu�do no Inspector
        if (puzzleManager == null)
        {
            puzzleManager = FindObjectOfType<PuzzleManager>();
        }
    }

    // Este m�todo ser� chamado pela sua A��o de Input 'Interact'

}

