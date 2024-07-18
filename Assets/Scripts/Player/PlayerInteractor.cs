using UnityEditor.ShaderGraph.Drawing;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] private TextEventChannel ui_EventChannel;

    [Header("Interactor Settings")]
    [SerializeField] private int interactLayer;

    private bool isInteractableLayer;

    private bool canInteract;

    IInteract interactableObject;

    private TextEvent emptyEvent;

    private void Start()
    {
        emptyEvent = new TextEvent(string.Empty, 0, false);
    }

    public void CanInteract(bool interactInput) => canInteract = interactInput;

    private void OnTriggerEnter(Collider other)
    {
        isInteractableLayer = other.gameObject.layer == interactLayer;

        if (other.gameObject.TryGetComponent(out interactableObject))
            ui_EventChannel.CallEvent(new(interactableObject.Prompt, 0, false));
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isInteractableLayer)
        {
            ui_EventChannel.CallEvent(emptyEvent);
            return;
        }

        if (interactableObject == null) return;

        if (canInteract)
            interactableObject.Interact();
        else 
            interactableObject.ExitInteract();
    }
    private void OnTriggerExit(Collider other)
    {
        ui_EventChannel.CallEvent(emptyEvent);
        interactableObject = null;
    }
}

public interface IInteract
{
    string Prompt { get; }
    public void Interact();
    public void ExitInteract();
}
