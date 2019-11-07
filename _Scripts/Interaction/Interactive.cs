using NetworkGameTemplate.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityHelperLib;

public class Interactive : MonoBehaviour
{
    internal bool active = false;
    public static event GenericRequest GenericRequest,StateChange;
       /// <summary>
       /// use this to chain together interactions,before, during or after
       /// </summary>
    [SerializeField]
    Interactive beforeHook,hook,afterHook;

    [SerializeField]
    int id = 0;


    public void Trigger()
    {
        BeforeHook();
        Hook();
        AfterHook();
    }

    public void BeforeHook()
    {
        if (beforeHook != null)
        beforeHook.Interact();
    }

    private void Hook()
    {
        if (hook != null)
            hook.Interact();
    }

    public void AfterHook()
    {
        if (afterHook != null)
            afterHook.Interact();
    }

    /// <summary>
    /// This method only changes the state of the object
    /// </summary>
    public void Activate()
    {
        StateChange?.Invoke(gameObject, GameActions.StateChange, new GenericMessage() { values = new string[] { id.ToString(), (!active).ToString() } });
    }

    /// <summary>
    /// This is an attempt by another object to interact with this object directly
    /// </summary>
    public virtual void Interact()
    {
        GenericRequest?.Invoke(gameObject, GameActions.Interact, new GenericMessage() { values = new string[] { id.ToString() } });
    }


    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }

    void OnDisable()
    {
        Unsubscribe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Subscribe()
    {
        NetworkGameManager.Interact += NetworkGameManager_Interact;
        NetworkGameManager.StateChange += NetworkGameManager_ChangeState;
    }

    void Unsubscribe()
    {
        NetworkGameManager.Interact -= NetworkGameManager_Interact;
        NetworkGameManager.StateChange -= NetworkGameManager_ChangeState;
    }

    /// <summary>
    /// A message has some in that this objects state has been changed
    /// Trigger actions if the state is different to the current state
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    /// <param name="message"></param>
    private void NetworkGameManager_ChangeState(GameObject sender, GameActions eventName, GenericMessage message)
    {
        var id = message.values.FirstOrDefault();
        //Debug.Log("state change for " + id);

        if (id == this.id.ToString())
        {
            var newState = bool.Parse(message.values[1]);
            //Debug.Log("from "+active +" to " +newState);

            if (active == newState) return; //already in this state
            active = newState;
            Trigger();
        }
    }

    /// <summary>
    /// Message has arrived that an object has attempted ot activate this interactive component
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    /// <param name="message"></param>
    private void NetworkGameManager_Interact(GameObject sender, GameActions eventName, GenericMessage message)
    {
        var id = message.values.FirstOrDefault();
        if (id == this.id.ToString())
        {
            Activate();
        }
    }


}

