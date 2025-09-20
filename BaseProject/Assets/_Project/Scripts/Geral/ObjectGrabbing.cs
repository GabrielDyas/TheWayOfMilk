using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq; // Necess�rio para usar o OrderBy

public class ObjectGrabbing : MonoBehaviour
{
    [SerializeField] private Transform handPoint;
    [SerializeField] private float grabRadius = 1.5f; // Raio da �rea de detec��o na frente do jogador
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private Camera playerCamera;

    private GameObject grabbedObject = null;
    private Rigidbody grabbedObjectRb = null;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (playerCamera == null)
        {
            Debug.LogError("A c�mera do jogador n�o foi atribu�da no Inspector do ObjectGrabbing!");
            return;
        }

        if (context.performed)
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ThrowObject();
            }
        }
    }

    private void TryGrabObject()
    {
        // Cria uma esfera de detec��o na frente do jogador para encontrar objetos peg�veis
        Collider[] grabbableColliders = Physics.OverlapSphere(transform.position + transform.forward, grabRadius, grabbableLayer);

        if (grabbableColliders.Length > 0)
        {
            // Pega o objeto mais pr�ximo do jogador dentro da esfera
            Transform closestGrabbable = grabbableColliders.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).First().transform;

            grabbedObject = closestGrabbable.gameObject;
            grabbedObjectRb = grabbedObject.GetComponent<Rigidbody>();

            if (grabbedObjectRb != null)
            {
                grabbedObjectRb.isKinematic = true;
                grabbedObject.transform.SetParent(handPoint);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void ThrowObject()
    {
        if (grabbedObjectRb != null)
        {
            grabbedObject.transform.SetParent(null);
            grabbedObjectRb.isKinematic = false;

            // Agora, arremessa na dire��o que o personagem est� olhando
            grabbedObjectRb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }

        grabbedObject = null;
        grabbedObjectRb = null;
    }

    // Opcional: Desenha a esfera de detec��o no Editor da Unity para facilitar o debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward, grabRadius);
    }
}

