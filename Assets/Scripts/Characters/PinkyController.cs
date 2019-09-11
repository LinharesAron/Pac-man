using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyController : GhostsAI
{
    public bool useOriginalGlitch = true;
    private float pinkyAmbusherRanger = 4f;

    protected override Vector3 GetTargetPosition()
    {
        if (CurrentMode == GameModeManager.GameMode.CHASE)
        {
            MoveCharacter player = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveCharacter>();
            Vector3 target = player.CurrentDir;
            if (target == Vector3.up && useOriginalGlitch)
                target += Vector3.left;

            return player.transform.position + target * pinkyAmbusherRanger;
        }
        return waypoint.position;
    }
}

    
