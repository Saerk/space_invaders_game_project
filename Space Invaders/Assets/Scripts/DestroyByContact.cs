using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int score;

    public int probabilityOfPowerUp;

    public GameObject explosion;
    public GameObject powerUp;

    private GameController gameController;
    
    

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        if(other.tag == "EnemyBolt" && tag == "Enemy")
        {
            return;
        }

        if (other.tag == "Enemy" && tag == "Enemy")
        {
            return;
        }

        if (tag == "Player" && other.tag == "PowerUp")
        {
            return;
        }

        if (tag == "Enemy" && other.tag == "PowerUp")
        {
            return;
        }
        if (tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.UpdateHighScore();
            gameController.GameOver();
        }
        
        gameController.AddScore(score);

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(other.gameObject);
        Destroy(gameObject);

        DropPowerUp();
        
    }
    public void DropPowerUp()
    {
        probabilityOfPowerUp = Random.Range(0,10);
        if(probabilityOfPowerUp == 0)
        {
            Instantiate(powerUp, transform.position, transform.rotation);
        }
    }
}
