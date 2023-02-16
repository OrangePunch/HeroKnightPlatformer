using UnityEngine;

public class DoInteraction : MonoBehaviour
{
    public void DoInteractionWithObject(GameObject go)
    {
        var interactable = go.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
