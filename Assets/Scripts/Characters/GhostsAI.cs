using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostsAI : MoveCharacter, IScorePoints
{
    public float speed;
    public Transform waypoint;

    public LayerMask intersections;
    public LayerMask walls;

    [SerializeField] private GameModeManager.GameMode _currentMode;
    public GameModeManager.GameMode CurrentMode
    {
        get { return _currentMode; }
    }

    Vector3[] sequence = new Vector3[] { Vector3.up, Vector3.left, Vector3.down, Vector3.right };

    public ScorePoints points;

    protected override float Speed()
    {
        return speed;
    }

    protected override void Start()
    {
        base.Start();

        _currentDir = Vector3.zero;
        _dest = transform.position;

        _currentDir = GetNearestTile(transform);
        ChoiceNextTarget();

        Events.Instance.OnGameModeChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameModeManager.GameMode newMode)
    {
        _currentMode = newMode;
        if (_currentMode == GameModeManager.GameMode.FRIGHTENED)
        {
            animator.SetBool("GhostsFrightened", true);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ChoiceNextTarget();
    }

    void ChoiceNextTarget()
    {
        Vector3 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + _currentDir * 2f, pos, intersections);
        Debug.DrawLine(pos + _currentDir * 2f, pos, Color.blue);
        if (hit)
        {
            Vector3 nearest = GetNearestTile(hit.transform);
            ChangeDirection(nearest);
        }
    }

    bool IsValid(Transform target, Vector3 d)
    {
        Vector3 start = target.position;
        if (target.tag == "Restrict" && d == Vector3.up)
            return false;
        RaycastHit2D hit = Physics2D.Linecast(start + d, start + d * 1.5f, walls);
        return !hit;
    }

    Vector3 GetNearestTile(Transform startPoint)
    {
        Dictionary<int, int> options = new Dictionary<int, int>();
        for (int i = 0; i < sequence.Length; i++)
        {
            if (!_currentDir.IsOpposite(sequence[i]) &&
                IsValid(startPoint, sequence[i]))
            {
                int d = (int)Vector3.Distance(startPoint.position + sequence[i], GetTargetPosition());
                options.Add(i, d);
            }
        }

        int index;
        if (_currentMode == GameModeManager.GameMode.FRIGHTENED)
            index = Enumerable.ToList(options.Keys)[Random.Range(0, options.Keys.Count)];
        else
             index = options.Aggregate((l, r) => l.Value < r.Value ? l : l.Value == r.Value && l.Key < r.Key ? l : r).Key;
        return sequence[index];
    }

    protected virtual Vector3 GetTargetPosition()
    {
        if (_currentMode == GameModeManager.GameMode.CHASE)
            return GameObject.FindGameObjectWithTag("Player").transform.position;         
        return waypoint.position;
    }

    protected override void AnimationController()
    {
        animator.SetFloat("DirX", _currentDir.x);
        animator.SetFloat("DirY", _currentDir.y);
    }

    public ScorePoints GetScorePoints()
    {
        return points;
    }

    public void Destroy()
    {
        Debug.Log("DEATH ANIMATION");
    }
}
