using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeController : GhostsAI
{
    private float clydePursuitRange = 8f;

    protected override Vector3 GetTargetPosition()
    {
        if( CurrentMode == GameModeManager.GameMode.CHASE) { 
            Transform pos = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log(Vector3.Distance(pos.position, transform.position));
            if (Vector3.Distance(pos.position, transform.position) > clydePursuitRange)
                return pos.position;
        }
        return waypoint.position;
    }
}
