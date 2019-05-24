using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0F;
    public float zMin = 1;
    public float zMax = 3;
    private float powerUpTime = 5f;
    private float playerPosition = -8f;

    public bool tripleShot;
    

    private float fireDelta = 0.5F;
    private float nextFire = 1F;
    private float myTime = 0.0F;

    public GameObject projectile;
    public Transform shotSpawn;
    public Transform player;

    private Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tripleShot = false;
    }

    void Update()
    {
        if(tripleShot == true)
        {
            StopCoroutine(Shoot(fireDelta));
            StartCoroutine(Shoot(fireDelta / 3));

        }
        StopCoroutine(Shoot(fireDelta));
        StartCoroutine(Shoot(fireDelta));
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector3(0, 0, moveVertical);
        transform.position = new Vector3
            (
                playerPosition,
                0,
                Mathf.Clamp(transform.position.z, zMin, zMax)
            );
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerUp")
        {
            PoweringUp();
            Destroy(other.gameObject);

        }
    }
    public void PoweringUp()
    {
        if(tripleShot == false)
        {
            tripleShot = true;
        }

        
    }

    IEnumerator Shoot(float fireDelta)
    {
        myTime += Time.deltaTime;
        if(Input.GetButton("Fire1") && myTime > fireDelta)
        {
            Instantiate(projectile, shotSpawn.position, Quaternion.Euler(0, 90, 0));
            myTime = 0.0F;
            yield return new WaitForSeconds(fireDelta);
        }
        if (tripleShot == true)
        {
            yield return new WaitForSeconds(powerUpTime);
            tripleShot = false;
        }
    }
}
