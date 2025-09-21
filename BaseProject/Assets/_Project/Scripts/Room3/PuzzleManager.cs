using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleManager : MonoBehaviour
{
    [Tooltip("Refer�ncia para o controlador da porta que ser� aberta ao completar o puzzle")]
    [SerializeField] private DoorController doorController; 

    [Header("Configura��es do Puzzle")]
    [Tooltip("Lista de tubos no puzzle")]
    [SerializeField] private List<Tubes> tubes; 

    private bool isPlayerNear = false;



    //Fun��es para o reset do puzzle
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerNear)
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}

