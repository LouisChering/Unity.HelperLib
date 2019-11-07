using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHelperLib;

public class FlipOnInteract : Interactive
{
    [SerializeField]
    PositionEnum FlipAxis;

    [SerializeField]
    float flipAmount;

    public override void Interact()
    {
        var targetValue = active ? -flipAmount : flipAmount;
        //Debug.Log("trigger!"+targetValue);

        Vector3 temp = transform.rotation.eulerAngles;

        if (FlipAxis == PositionEnum.X)
        {
            temp.x = targetValue; 
        }else if (FlipAxis == PositionEnum.Y)
        {
            temp.y = targetValue;
        }
        else
        {
            temp.z = targetValue;
        }

        transform.rotation = Quaternion.Euler(temp);
        active = !active;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
