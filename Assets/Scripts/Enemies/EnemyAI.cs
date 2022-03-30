using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Vector3 target;

    Path path;
    Seeker seeker;

    Rigidbody2D myBody;
    int currentWayPoint;

    public float distanceToNextPoint;
    public float speed;

    bool reachEndOfPath;

    public float distanceToFollow;
    public float distanceToStop;
    public LayerMask whatIsPlayer;

    Vector3 startPoint;

    public bool canShot, onView, onZone, playerOnZone;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        InvokeRepeating("UpdatePath", 0, .5f);
        startPoint = transform.position;
        target = startPoint;
    }

    void UpdatePath() {
        if (target!=null)
            seeker.StartPath(myBody.position,target,OnPathComplete);
    }

    void OnPathComplete(Path p) {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void Update()
    {
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            stage = Stage.Idle;
            return;
        }
        else reachEndOfPath = false;

        if (target!=null)
        {
            float distance = Vector2.Distance(myBody.position, target);

            if (distance > distanceToStop || (distance <= distanceToStop && canShot == false))
            {
                Movement();
            }
        }
    }

    void Movement() {
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - myBody.position).normalized;

        myBody.MovePosition(myBody.position + direction * speed * Time.deltaTime);

        float distance = Vector2.Distance(myBody.position, path.vectorPath[currentWayPoint]);

        if (distance < distanceToNextPoint)
            currentWayPoint++;
    }

    private void FixedUpdate()
    {
        if (!onZone && !playerOnZone)
        {
            target = startPoint;
            stage = Stage.Move;
            canShot = false;
            onView = false;
            return;
        }

        if (onZone && stage == Stage.Idle)
            MakeDecision();

        if (onZone && playerOnZone)
        {
            CheckPLayerInView();
            CheckPLayerInShotRange();
        }

    }

    //---------------------MAKE DECISION----------------------
    
    public int decision;
    private float timeToMakeNewDecision;
    public float timeTMNDSetting;
    public Stage stage;

    void MakeDecision() {
        if (timeToMakeNewDecision <= 0f)
        {
            decision = Random.Range(0, 10);
            timeToMakeNewDecision = timeTMNDSetting;
            if (decision<=3)
            {
                //Idle
                stage = Stage.Idle;
            }

            stage = Stage.Move;
            target = RandomTarget();
        }

        if (timeToMakeNewDecision > 0f && stage == Stage.Idle)
            timeToMakeNewDecision -= Time.deltaTime;
    }

    Vector3 RandomTarget() {
        float x = transform.position.x + Random.Range(-2f, 2f);
        float y = transform.position.y + Random.Range(-2f, 2f);
        Vector3 newTarget = new Vector3(x, y);
        return newTarget;
    }
    
    void CheckPLayerInView() {
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, distanceToFollow, whatIsPlayer);

        if (hitPlayer != null)
        {
            target = hitPlayer.transform.position;
            onView = true;
        }
        else {
            onView = false;
            target = startPoint;
        }
    }

    void CheckPLayerInShotRange() {

        if (!onView)
            return;

        Collider2D hitPlayer = Physics2D.OverlapCircle(
            transform.position, distanceToStop, whatIsPlayer);
        
        if (hitPlayer != null)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, hitPlayer.transform.position);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    canShot = true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    canShot = false;
                }
            }
            else canShot = false;
            
        }
        else canShot = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToFollow);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToStop);
    }
}

public enum Stage {Idle, Move}
