using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float moveSide;
    private float reverseMoveSide;
    private float zMin = -3f;
    private float zMax = 3f;
    public float speed;
    private float fireDelta;
    private float nextFire = 3f;
    private float myTime = 0.0F;
    private float forwardTime;
    private float sideTime;

    private Vector3 moveForwardVector;
    private Vector3 moveSideVector;

    public GameObject projectile;

    public  Rigidbody rb;

    public Transform pilot;
    public Transform shotSpawn;

    void Awake()
    {
        
        forwardTime = Random.Range(1f, 4f);
        sideTime = Random.Range(1f, 3f); 
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveForwardVector = transform.forward * speed;
        fireDelta = Random.Range(1f, 4f);
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        while (true)
        {
            rb.velocity = moveForwardVector;

            if (rb.velocity == moveForwardVector)
            {
                yield return new WaitForSeconds(forwardTime);

                moveSide = Random.Range(-1f, 1f);
                reverseMoveSide = Mathf.Abs(1 / moveSide);
                moveSide *= reverseMoveSide;

                moveSideVector = new Vector3(0, 0, moveSide * speed);
                rb.velocity = moveSideVector;
            }
            if (rb.velocity == moveSideVector )
            {
                yield return new WaitForSeconds(sideTime);
                rb.velocity = moveForwardVector;
                
            }
        }
    }
    void Update()
    {
        // shooting
        myTime += Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(projectile, shotSpawn.position, Quaternion.Euler(0, 90, 0), pilot);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
        // bound movement by Z
        if(transform.position.z >= zMax )
        {
            rb.velocity = -rb.velocity;
            transform.position = new Vector3(transform.position.x,0,zMax);
        }
        if(transform.position.z <= zMin)
        {
            rb.velocity = -rb.velocity;
            transform.position = new Vector3(transform.position.x, 0, zMin);
        }
    }
}
