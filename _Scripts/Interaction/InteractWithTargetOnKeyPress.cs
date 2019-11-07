using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTargetOnKeyPress : MonoBehaviour
{
    [SerializeField]
    Interactive target;
    [SerializeField]
    KeyCode triggerButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(triggerButton))
        {
            target.Interact();
        }
    }
} 
