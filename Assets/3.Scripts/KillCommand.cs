using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        Debug.Log("kill enemy");
    }

   
}
