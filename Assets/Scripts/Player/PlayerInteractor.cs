using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] private TextEventChannel interactText_EventChannel;
    [SerializeField] private VoidEventChannel clearText_EventChannel;

    [Header("Interactor Settings")]
    [SerializeField] private int interactLayer;

    private bool isInteractableLayer;

    private bool canInteract;

    IInteract interactableObject;

    private VoidEvent clearEvent;

    public void CanInteract(bool interactInput) => canInteract = interactInput;

    private void OnTriggerEnter(Collider other)
    {
        isInteractableLayer = other.gameObject.layer == interactLayer;

        if (other.gameObject.TryGetComponent(out interactableObject))
            interactText_EventChannel.CallEvent(interactableObject.Prompt);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isInteractableLayer)
        {
            clearText_EventChannel.CallEvent(clearEvent);
            return;
        }

        if (interactableObject == null) return;

        //Interact update loop
        interactableObject.OnStay();

        if (canInteract)
        {
            interactableObject.Interact();
            canInteract = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isInteractableLayer)
        {
            return;
        }
        clearText_EventChannel.CallEvent(clearEvent);
        interactableObject.ExitInteract();
        interactableObject = null;
    }
}

public interface IInteract
{
    TextEvent Prompt { get; }
    public void Interact();
    public void ExitInteract();
    public void OnStay();
}
