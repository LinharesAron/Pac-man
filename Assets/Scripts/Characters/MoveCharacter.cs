using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveCharacter : MonoBehaviour
{
    protected Vector3 _currentDir;
    public Vector3 CurrentDir
    {
        get { return _currentDir; }
    }
    protected Vector3 _dest;
    protected Vector3 _nextDir;

    protected LayerMask layers;
    
    private float distanceMax = 15f;
    Rigidbody2D rigid;

    protected Animator animator;
    protected List<string> stopTags;

    protected abstract float Speed();
    protected abstract void AnimationController();


    protected virtual void Start()
    {
        _currentDir = Vector3.left;
        _nextDir = _currentDir;

        rigid = GetComponent<Rigidbody2D>();
        _dest = transform.position + _currentDir * 0.25f;
        animator = GetComponent<Animator>();

        layers = LayerMask.GetMask("Intersection", "Wall", "Door");
        stopTags = new List<string>();
        stopTags.Add("Wall");
    }

    protected virtual void FixedUpdate()
    {
        Vector2 p = Vector2.MoveTowards(transform.position,
                                        _dest, 
                                        Speed() * Time.fixedDeltaTime);
        rigid.MovePosition(p);
        float distance = Vector2.Distance(_dest, transform.position);
        if (distance < 0.00001f || _nextDir.IsOpposite(_currentDir))
        {
            Vector3 _nextDest;
            if (_nextDir != Vector3.zero && FindNextDest(_nextDir, out _nextDest))
            {
                _dest = _nextDest;
                _currentDir = _nextDir;
                _nextDir = Vector3.zero;
            }
            else if (FindNextDest(_currentDir, out _nextDest))
                _dest = _nextDest;
        }

        AnimationController();
    }

    public void ChangeDirection(Vector3 nextDir)
    {
        _nextDir = nextDir;
    }

    public void TransportTo(Vector3 to )
    {
        transform.position = to;
        if (FindNextDest(_currentDir, out Vector3 nextDest))
            _dest = nextDest;
    }

    bool FindNextDest(Vector2 dir, out Vector3 nextDest)
    {
        nextDest = Vector3.zero;
        Vector2 pos = transform.position;

        Debug.DrawLine(pos + dir * 1.25f, pos + dir * distanceMax, Color.yellow);
        RaycastHit2D hit = Physics2D.Linecast(pos + dir * 1.25f, pos + dir * distanceMax, layers);
        if (hit && !stopTags.Contains(hit.collider.tag))
        {
            nextDest = hit.transform.position;
            return true;
        }
        return false;
    }
}
