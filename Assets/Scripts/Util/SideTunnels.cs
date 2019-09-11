using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTunnels : MonoBehaviour
{
    public Transform opposite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoveCharacter move = collision.GetComponent<MoveCharacter>();
        if (move != null)
            move.TransportTo(opposite.position + opposite.right * 1.5f);
    }
}
