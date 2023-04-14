using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    // подсказка о возможности взаимодействия
    [SerializeField] GameObject hint;

    // доступно ли взаимодействие
    bool canInteract = false;
    // список всех интерактивных объектов, с которыми возможно взаимодействовать в данный момент
    List<GameObject> interactiveOverlaps = new List<GameObject>();

    void Update()
    {
        if (canInteract && InputManager.InteractWasPressedThisFrame)
        {
            foreach (GameObject obj in interactiveOverlaps)
                obj.GetComponent<Interactive>().Activate();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Interactive"))
        {
            interactiveOverlaps.Add(col.gameObject);

            canInteract = true;
            hint.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Interactive"))
        {
            interactiveOverlaps.Remove(col.gameObject);

            if (interactiveOverlaps.Count == 0)
            {
                canInteract = false;
                hint.SetActive(false);
            }
        }
    }
}
