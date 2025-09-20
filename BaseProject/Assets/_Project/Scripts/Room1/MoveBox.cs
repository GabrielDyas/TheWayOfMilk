using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] // Garante que o objeto sempre tenha um Rigidbody
public class MoveBox : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Arraste o objeto do jogador aqui no Inspector
    [SerializeField] private float pushForce = 10f;   // A for�a do empurr�o a ser aplicada

    private Rigidbody rb;
    private bool isPlayerNear = false;

    private void Awake()
    {
        // Pega o componente Rigidbody para usarmos na f�sica
        rb = GetComponent<Rigidbody>();
    }

    // Este m�todo ser� chamado pelo seu Input System
    public void OnMoveBox(InputAction.CallbackContext context)
    {
        // S� executa se o bot�o for pressionado e o jogador estiver perto
        if (context.performed && isPlayerNear)
        {
            // Calcula a dire��o do empurr�o (sempre para longe do jogador)
            Vector3 pushDirection = transform.position - playerTransform.position;
            pushDirection.y = 0; // Garante que o movimento seja apenas horizontal
            pushDirection.Normalize();

            // Aplica uma for�a instant�nea na caixa, como um empurr�o
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    // Ativado quando algo entra no gatilho
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Jogador est� perto da caixa.");
        }
    }

    // Ativado quando algo sai do gatilho
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Jogador se afastou da caixa.");
        }
    }
}

