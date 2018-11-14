using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI: MonoBehaviour {

    public GameObject[] Players;
    public Transform PlayerTrans;
    public Vector3 StartPos;

    public bool HaveCheckPoints = false;
    public Transform[] NavCheckpoints;
    public Transform CurrentNavCheckpoint;
    public int index =0;

    public float distance;
    public float MoveRange = 10f;
    public float FireRange = 2f;
    public GameObject Projectile;
    public Transform FireLocation;
    public float shootForce = 20;
    public float fireRate=5f;
    private float nextFire;
    public bool Spotted = false;
    public AudioClip[] Sounds;

    public float MaxRange =15f;
    public float MinRange = 0.2f;

    NavMeshAgent agent;


    private Animator Anim;
    // Use this for initialization
    void Start () {
        Anim = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        StartPos = transform.position;
    }
    public void StartAttack()
    {

    }
    public void EndAttack()
    {
        if (PlayerTrans.GetComponent<playerNormalMovement>())
            PlayerTrans.GetComponent<playerNormalMovement>().Death();
        else if (PlayerTrans.GetComponent<LocalPlayerMovement>())
            PlayerTrans.GetComponent<LocalPlayerMovement>().Death();
    }

    // Update is called once per frame
    void Update () {
        if (gameObject.GetComponent<LocalMindControl>().Enabled == true)
        {
            if (agent.isStopped)
                return;
            else
            {
                agent.Stop();
                Anim.SetBool("Attack", false);
                Anim.SetBool("Chase", false);
                Anim.SetBool("Pathing", false);
            }
                
            return;
        }
           

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (PlayerTrans == null)
                PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        }
        else
        {
            return;
        }

        Vector3 targetDir = PlayerTrans.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
        RaycastHit hit;
        distance = Vector3.Distance(PlayerTrans.position, transform.position);
        
        // Check if player is in 180° FOV
        if (angleToPlayer >= -90 && angleToPlayer <= 90 && distance < MoveRange)
        { 
           
            if (Physics.Linecast(FireLocation.position, PlayerTrans.position,out hit))
            {
                Debug.DrawLine(FireLocation.position, PlayerTrans.position, Color.blue);
                Debug.Log("Hit: " + hit.transform.gameObject.name);
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    Spotted = true;
                }
            }
        }
        //check if distance is too far away
        if (distance >= MoveRange)
        {
            Spotted = false;
        }
        
        //log agents destination
        Debug.Log("agent dest: "+agent.destination);

        if (Spotted && distance < MoveRange)
        {
            agent.stoppingDistance = FireRange;
            agent.SetDestination(PlayerTrans.position);
            Anim.SetBool("Chase", true);
            Anim.SetBool("Pathing", false);
            if (distance <= agent.stoppingDistance)
            {
                agent.Stop();
                //face the target and attack
                AttackPlayer();
            }
            else
            {
                Anim.SetBool("Attack", false);
                agent.Resume();
            }
               
        }


        //reset position
        if (!Spotted && Vector3.Distance(StartPos, transform.position) >1f)
        {

            Anim.SetBool("Attack", false);
            Anim.SetBool("Chase", false);
            Anim.SetBool("Pathing", true);
            agent.stoppingDistance = 1f;
            agent.SetDestination(StartPos);
            
        }
        if (!Spotted && Vector3.Distance(StartPos, transform.position) <= 1f)
        {
            Anim.SetBool("Pathing", false);
        }

    }
    public void Shoot()
    {
        GameObject go = Instantiate(Projectile, FireLocation.position, FireLocation.rotation);
        go.GetComponent<Rigidbody>().AddForce(FireLocation.forward * shootForce);
    }
    void DoNothing()
    {
    }
        void AttackPlayer()
    {

        Debug.DrawLine( transform.position, PlayerTrans.position, Color.red);
        transform.LookAt(PlayerTrans.position);
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Anim.SetBool("Attack", true);
            if (Projectile!=null) {

            }
            else
            {

            }
        }
    }
    public void PlayStep()
    {
        //play audio Sounds for stepping
    }
    public void SwitchCheckpoint()
    {
        index++;
        if (index == NavCheckpoints.Length)
        {
            index = 0;
        }
        if (NavCheckpoints[index].gameObject.activeInHierarchy)
        {
            CurrentNavCheckpoint = NavCheckpoints[index];
        }
        agent.SetDestination(CurrentNavCheckpoint.position);


    }
    public void Death()
    {
        Anim.SetBool("Dead", true);
        StartCoroutine(WaitToDie());
    }
    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);

    }
    private void OnDrawGizmos()
    {
        //draw FOV in editor
        Gizmos.DrawFrustum(transform.position+ new Vector3(0,1,0), 180f, MaxRange, MinRange,2f);

    }
}
