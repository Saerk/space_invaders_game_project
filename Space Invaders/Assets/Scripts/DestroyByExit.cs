using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByExit : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "EnemyBolt" && tag == "Enemy")
        {
            return;
        }
        Destroy(other.gameObject);
    }

}