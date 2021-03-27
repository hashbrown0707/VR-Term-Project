using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirelessCharger: ObjectEvent
{
    public GameObject owlpple;
    private OwlPhone owlPhone;
    private void Start()
    {
        owlPhone = owlpple.GetComponent<OwlPhone>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (state_i == 1)
            owlPhone.power = true;
    }
    public override void Keydown()
    {
        if (state_i == 0)
            state_i = 1;
    }
}
