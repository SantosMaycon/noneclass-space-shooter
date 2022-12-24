using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour {
  [SerializeField] private GameObject[] enemies;
  [SerializeField] private int points = 0;
  [SerializeField] private int level = 1;
  [SerializeField] private int baseLevel = 100;
  [SerializeField] private float waitEnemy = 0f;
  [SerializeField] private float spawnWaitTime = 5f;
  [SerializeField] private int quantityEnemies = 0;
  [SerializeField] private GameObject bossAnimation;
  [SerializeField] private Text textSpawn;
  [SerializeField] private AudioClip boosMusic;
  [SerializeField] private AudioSource music;
  private bool wakeUpBoss = true;

  // Start is called before the first frame update
  void Start() {
    textSpawn.text = "Points : " + points + "\nLevel : " + level;
    music = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update() {
    if (quantityEnemies <= 0) {
      waitEnemy -= Time.deltaTime;
    }

    spawnController();
    textSpawn.text = "Points : " + points + "\nLevel : " + level;
  }

  public void earnPoints (int points) {
    this.points += points;
    earnLevel();
  }

  void earnLevel () {
    if (points >= level * baseLevel) {
      level++;
    }
  }

  public void decreaseEnemies () {
    quantityEnemies--;
  }

  private bool checkPosition (Vector3 position, Vector3 size) {
    Collider2D hit = Physics2D.OverlapBox(position, size, 0f);
    return hit;
  }

  private void spawnController () {
    if (waitEnemy <= 0) {
      if ( level < 10) {
        spawnEnemies();
      } else if (level >= 10 && wakeUpBoss) {
        spawnBossAnimation();
      }

      waitEnemy = spawnWaitTime;
    }
  }

  private void spawnBossAnimation () {
    if (wakeUpBoss) {
      var instanceBossAnimation = Instantiate(bossAnimation, Vector3.zero, Quaternion.identity);
      quantityEnemies++;
      wakeUpBoss = false;

      music.clip = boosMusic;
      music.Play();
    }
  }

  private void spawnBoss () {}

  private void spawnEnemies () {
    int quantity = level * 3;
    int tryCheck = 0;

    if (quantityEnemies <= 0) {
      while (quantityEnemies < quantity) {
        if (enemies.Length >= 1) {
          tryCheck++;

          if (tryCheck > 200) { break; }

          GameObject enemyObject;

          float chance = Random.Range(0f, level);
          if (chance > 2f) {
            enemyObject = enemies[1];
          } else {
            enemyObject = enemies[0];
          }

          var randomPosition = new Vector3(Random.Range(-7, 7), Random.Range(5, 14), 0f);

          var colision = checkPosition(randomPosition, enemyObject.transform.localScale);
          
          if (!colision) {
            Instantiate(enemyObject, randomPosition, Quaternion.identity);
            quantityEnemies++;
          }
        }   
      }
    }
  }
}
