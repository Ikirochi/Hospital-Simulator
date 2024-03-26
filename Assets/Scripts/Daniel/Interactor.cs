using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E.");
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("INTERACT!");
                    interactObj.Interact();
                }
                else
                {
                    Debug.Log("No interactable component found on object: " + hitInfo.collider.gameObject.name);
                }
            }
        }
    }
    */
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E.");
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

            // Create layer mask to ignore player layer
            int layerMask = ~LayerMask.GetMask("Doctor");

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, layerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("INTERACT!");
                    interactObj.Interact();
                }
                else
                {
                    Debug.Log("No interactable component found on object: " + hitInfo.collider.gameObject.name);
                }
            }
        }
    }
    
}