using UnityEditor.ShaderGraph.Drawing;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] private UIEventChannel ui_EventChannel;

    [Header("Interactor Settings")]
    [SerializeField] private int interactLayer;

    private bool isInteractableLayer;

    private bool canInteract;

    IInteract interactableObject;

    public void CanInteract(bool interactInput) => canInteract = interactInput;

    private void OnTriggerEnter(Collider other)
    {
        isInteractableLayer = other.gameObject.layer == interactLayer;

        if (other.gameObject.TryGetComponent(out interactableObject))
            ui_EventChannel.TriggerEvent(interactableObject.Prompt);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isInteractableLayer)
        {
            ui_EventChannel.TriggerEvent(string.Empty);
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
        ui_EventChannel.TriggerEvent(string.Empty);
        interactableObject = null;
    }
}

public interface IInteract
{
    string Prompt { get; }
    public void Interact();
    public void ExitInteract();
}
