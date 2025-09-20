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
    public void OnInteract(InputAction.CallbackContext context)
    {
        // S� executa se o bot�o for pressionado e o jogador estiver perto
        if (context.performed && isPlayerNear)
        {
            if (puzzleManager != null)
            {
                Debug.Log("Bot�o de reset ativado pelo jogador.");
                puzzleManager.ResetPuzzle();
            }
            else
            {
                Debug.LogError("A refer�ncia para o PuzzleManager n�o foi encontrada no ResetButton!");
            }
        }
    }

    // Detecta se o jogador entrou na �rea de intera��o
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Detecta se o jogador saiu da �rea de intera��o
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}

