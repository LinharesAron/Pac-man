using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyController : GhostsAI
{
    public bool useOriginalGlitch = true;
    private float inkyAmbusherRanger = 2f;

    protected override Vector3 GetTargetPosition()
    {
        if (CurrentMode == GameModeManager.GameMode.CHASE)
        {
            MoveCharacter player = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveCharacter>();
            Vector3 target = player.CurrentDir;
            if (target == Vector3.up && useOriginalGlitch)
                target += Vector3.left;
            target = player.transform.position + target * inkyAmbusherRanger;

            GameObject blinky = GameObject.FindGameObjectWithTag("Blinky");
            if (blinky == null)
                return target;

            var heading = target - blinky.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            Vector3 t = blinky.transform.position + direction.normalized * distance * 2f;
            Debug.DrawLine(blinky.transform.position, t, Color.blue);
            Debug.DrawLine(blinky.transform.position, target, Color.red);
            return t;
        }
        return waypoint.position;
    }
}
