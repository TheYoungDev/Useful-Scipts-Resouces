using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour {

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
    /* private void OnTriggerEnter(Collider other)
     {
         if(other.transform.gameObject.CompareTag("Player"))
             Spotted = true;
     }
     private void OnTriggerExit(Collider other)
     {
         if (other.transform.gameObject.CompareTag("Player"))
             Spotted = false;
     }*/
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
           
        //RaycastHit hit;
        //Vector3 p1 = transform.position;

        //if(Physics.SphereCast(p1, MaxRange, transform.forward,out hit, 1))
        // {
        //
        //}
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (PlayerTrans == null)
                PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;//.transform.GetChild(2).

        }
        else
        {
            return;
        }

        Vector3 targetDir = PlayerTrans.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
        //Vector3 fromPosition = transform.position;
        //Vector3 toPosition = PlayerTrans.transform.position;
        //Vector3 direction = toPosition - fromPosition;
        RaycastHit hit;

        distance = Vector3.Distance(PlayerTrans.position, transform.position);

        if (angleToPlayer >= -90 && angleToPlayer <= 90 && distance < MoveRange)
        { // 180° FOV
            Debug.Log("Spootted?");
            if (Physics.Linecast(FireLocation.position, PlayerTrans.position,out hit))
            {
                Debug.DrawLine(FireLocation.position, PlayerTrans.position, Color.blue);
                Debug.Log("Hit: " + hit.transform.gameObject.name);
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    
                    Spotted = true;
                    //Anim.SetBool("Chase", true);
                }
                else
                {
                    //Debug.Log("Player NOT in sight!");
                    
                }
            }



        }
        if (distance >= MoveRange)
        {
            //Debug.Log("Player NOT in sight!");
            Spotted = false;
        }
        //Debug.Log(distance);


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
        //Debug.Log(Vector3.Distance(StartPos, transform.position));
        if (!Spotted && Vector3.Distance(StartPos, transform.position) >1f)//&& !HaveCheckPoints
        {

            Anim.SetBool("Attack", false);
            Anim.SetBool("Chase", false);
            Anim.SetBool("Pathing", true);
            agent.stoppingDistance = 1f;
            agent.SetDestination(StartPos);
            
        }
        if (!Spotted && Vector3.Distance(StartPos, transform.position) <= 1f)//&& !HaveCheckPoints
        {
            Anim.SetBool("Pathing", false);
        }


       /* if (HaveCheckPoints)
        {
            if (Vector3.Distance(transform.position, CurrentNavCheckpoint.position) <= FireRange)
            {

            }
        }*/




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
        //Vector3 direction = (PlayerTrans.position - transform.position).normalized;
        Debug.DrawLine( transform.position, PlayerTrans.position, Color.red);
        //Quaternion lookRot = Quaternion.Lo(new Vector3(direction.x, 0, direction.y));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        transform.LookAt(PlayerTrans.position);
        if (Time.time > nextFire) {
            //Anim.SetBool("Attack", true);
            nextFire = Time.time + fireRate;
            Anim.SetBool("Attack", true);
            if (Projectile!=null) {

                //Shoot();
            }
            else
            {
                /*if(PlayerTrans.GetComponent<LocalPlayerMovement>())
                    PlayerTrans.GetComponent<LocalPlayerMovement>().Death();
                else if (PlayerTrans.GetComponent<playerNormalMovement>())
                    PlayerTrans.GetComponent<playerNormalMovement>().Death();*/
            }
        }
    }
    public void PlayStep()
    {
        //play audio Sounds
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
        StartCoroutine(WaitToDeSpawn());
    }
    IEnumerator WaitToDeSpawn()
    {
       // print(Time.time);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
       // print(Time.time);
    }
    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.green;
       // Gizmos.DrawWireSphere(transform.position, MoveRange);
       // Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, FireRange);
       // Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, MinRange);
        Gizmos.DrawFrustum(transform.position+ new Vector3(0,1,0), 180f, MaxRange, MinRange,2f);
       // Gizmos.DrawSphere
    }
}
