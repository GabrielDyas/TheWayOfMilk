using UnityEngine;
using UnityEngine.InputSystem;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject passwordUIPanel; // Arraste o seu 'PanelAceso' da Hierarquia para c�

    private bool isPlayerNear = false;

    private void Awake()
    {
        // Garante que a UI de senha comece desativada
        if (passwordUIPanel != null)
        {
            passwordUIPanel.SetActive(false);
        }
    }

    // Este m�todo ser� chamado pelo seu Input System (A��o 'Interact')
    public void OnInteract(InputAction.CallbackContext context)
    {
        // S� executa se o bot�o for pressionado e o jogador estiver perto
        if (context.performed && isPlayerNear)
        {
            if (passwordUIPanel != null)
            {
                // Ativa a UI de senha e "pausa" o jogo para o jogador digitar
                passwordUIPanel.SetActive(true);
                // Voc� pode adicionar aqui c�digo para travar o movimento do jogador, se desejar
                // Time.timeScale = 0f; // Exemplo de como pausar o tempo
            }
        }
    }
    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ClosePanel();
        }
    }

    // Detecta se o jogador entrou na �rea de intera��o
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Jogador pode interagir com o painel.");
            // Opcional: Mostrar uma dica na tela como "Pressione E para interagir"
        }
    }

    // Detecta se o jogador saiu da �rea de intera��o
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Jogador se afastou do painel.");
            // Opcional: Esconder a dica da tela
        }
    }
    public void ClosePanel()
    {
        if (passwordUIPanel != null)
        {
            passwordUIPanel.SetActive(false);
            // Time.timeScale = 1f; // Retoma o tempo normal
        }
    }
}
