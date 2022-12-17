using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
  [SerializeField] private GameObject[] enemies;
  [SerializeField] private int points = 0;
  [SerializeField] private int level = 1;
  [SerializeField] private int baseLevel = 100;
  private float waitEnemy = 0f;
  [SerializeField] private float spawnWaitTime = 5f;
  [SerializeField] private int quantityEnemies = 0;

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {
    if (quantityEnemies <= 0) {
      timerSpawn(waitEnemy - Time.deltaTime);
    }
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

  private void timerSpawn (float time) {
    waitEnemy = time;

    if (waitEnemy <= 0) {
      int quantity = level * 3;

      if (quantityEnemies <= 0) {
        while (quantityEnemies < quantity) {
          if (enemies.Length >= 1) {
            GameObject enemyObject;

            float chance = Random.Range(0f, level);
            if (chance > 2f) {
              enemyObject = enemies[1];
            } else {
              enemyObject = enemies[0];
            }

            var randomPosition = new Vector3(Random.Range(-7, 7), Random.Range(5, 14), 0f);
            Instantiate(enemyObject, randomPosition, Quaternion.identity);
            quantityEnemies++;
          }   
        }
      }
      waitEnemy = spawnWaitTime;
    }
  }
}
