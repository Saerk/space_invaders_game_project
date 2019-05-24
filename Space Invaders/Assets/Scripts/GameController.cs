using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int winCondition = 200; 

    public float spawnTime;
    public float waveTime;

    private bool gameOver;
    private bool restart;

    [Header("Vectors for Enemies")]
    public Vector3 spawnMeleeVector;
    public Vector3 spawnRangeVector;

    [Header("Enemy Gameobject")]
    public GameObject meleeEnemy;
    public GameObject rangeEnemy;

    [Header("Text")]
    
    public Text scoreText;
    public Text highScoreText;
    public Text restartText;
    public Text gameOverText;

    private int score;
    private int highScore;


    private Quaternion spawnRotation = Quaternion.Euler(-180, 90, 90);

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        highScoreText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {

      if(restart)
      {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main Scene");
        }
      }

      if (gameOver)
      {
        restartText.text = "Press 'R' for Restart";
        restart = true;
        
      }


        if (score == winCondition)
      {
            WinScenario();
      }

      if (Input.GetKeyDown(KeyCode.Q))
      {
            Application.Quit();
      }
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {

            if (gameOver)
            {
                break;
            }

            for (int i = 0; i < 10; i++)
            {
                Vector3 spawnRangePosition = new Vector3
                   (
                   spawnRangeVector.x,
                   spawnRangeVector.y,
                   Random.Range(-spawnRangeVector.z, spawnRangeVector.z)
                   );

                Vector3 spawnPosition = new Vector3
                      (
                      spawnMeleeVector.x,
                      spawnMeleeVector.y,
                      Random.Range(-spawnMeleeVector.z, spawnMeleeVector.z)
                      );

                Instantiate(meleeEnemy, spawnRangePosition, spawnRotation);

                yield return new WaitForSeconds(spawnTime);

                Instantiate(rangeEnemy, spawnPosition, spawnRotation);

                
            }
            yield return new WaitForSeconds(waveTime);

            
        }
    }

    public void AddScore(int newScoreValue)
    {
        if(gameOver == false)
        {
            score += newScoreValue;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateHighScore()
    {
        highScore = score;
        highScoreText.text = "HighScore:" + highScore;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        scoreText.text = "";
        gameOver = true;
    }
    void WinScenario()
    {
         SceneManager.LoadScene("Win Scene", LoadSceneMode.Single);
    }
    
}
