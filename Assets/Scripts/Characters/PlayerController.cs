using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MoveCharacter
{
    public float speed = 1f;
    private Vector3 lastPosition;

    private int ghostCount = 0;

    protected override void Start()
    {
        base.Start();
        lastPosition = transform.position;
        stopTags.Add("Door");
        Events.Instance.OnGameModeChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameModeManager.GameMode newMode)
    {
        if (newMode == GameModeManager.GameMode.FRIGHTENED)
            ghostCount = 0;
    }

    protected override float Speed() {
        return speed;
    }

    void Update()
    {
        int h = (int)(Input.GetAxis("Horizontal"));
        int v = (int)(Input.GetAxis("Vertical"));

        if (h != 0)
            v = 0;

        if (h != 0 || v != 0)
        {
            Vector3 _dir = new Vector3(h, v);
            ChangeDirection(_dir);
        }
    }

    protected override void AnimationController()
    {
        if (lastPosition == transform.position)
            animator.enabled = false;
        else
            animator.enabled = true;

        animator.SetFloat("DirX", _currentDir.x);
        animator.SetFloat("DirY", _currentDir.y);

        lastPosition = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if( Vector3.Distance(transform.position, collision.transform.position) < 0.1f )
        {
            IScorePoints s = collision.GetComponent<IScorePoints>();
            if( s != null)
            {
                int value = s.GetScorePoints().score;
                if (s is GhostsAI)
                {
                    Debug.Log(collision.name);
                    if(((GhostsAI)s).CurrentMode != GameModeManager.GameMode.FRIGHTENED)
                        return;

                    if (ghostCount == 0)
                        value = s.GetScorePoints().score;
                    else
                        value *= 2;
                }

                GameManager.Instance.IncreaseScore(value);
                s.Destroy();
            }
        }
    }
}
