using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTriggerOnKeyPress : MonoBehaviour
{
    [SerializeField]
    string tag;
    [SerializeField]
    KeyCode triggerButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider c)
    {
        if (Input.GetKey(triggerButton))
        {
            if (c.gameObject.tag == tag)
            {
                TryInteractWithObject(c.gameObject);
            }
        }
    }

    void TryInteractWithObject(GameObject gameObject)
    {
        var interactive = gameObject.GetComponent<Interactive>();
        if (interactive == null) return;
        interactive.Interact();
    }
} 
