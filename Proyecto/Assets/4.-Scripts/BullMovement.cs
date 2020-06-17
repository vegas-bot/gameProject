using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullMovement : Balas
{

  

    private void Start()
    {
        base.Dir(15, new Vector3(PlayerMovement.instnacie.posX,0, PlayerMovement.instnacie.posZ));
    }


   

    // Update is called once per frame
    void Update()
    {
        base.Mov();
    }
   
}
