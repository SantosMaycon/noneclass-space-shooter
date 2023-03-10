using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  [SerializeField] protected float speed;
  [SerializeField] protected int life;
  [SerializeField] protected GameObject explodeEffect;
  [SerializeField] protected float shotSpeed = 8f;
  [SerializeField] protected float yMax = 2.3f;
  [SerializeField] protected int points = 10;
  private bool isLive = false;
  [SerializeField] protected GameObject powerUp;
  [SerializeField] protected float chanceOfDopPowerUp = 0.9f;
  [SerializeField] protected AudioClip mySound;

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  public void damegedLife (int damege) {
    life -= damege;

    if (life <= 0) {
      Destroy(gameObject);
      Instantiate(explodeEffect, transform.position, Quaternion.identity);

      FindObjectOfType<SpawnController>().earnPoints(points);

      if (powerUp) {
        float chance = Random.Range(0f, 1f);

        if (chance > chanceOfDopPowerUp) {
          var instancePowerUp = Instantiate(powerUp, transform.position, Quaternion.identity);
          instancePowerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 2;
          Destroy(instancePowerUp, 3f);
        }
      }
    }
  }

  protected void checkIsLive (bool isVisible) {
    if (isVisible && !isLive) {
      isLive = true;
    }

    if (!isVisible && isLive) {
      Destroy(gameObject);
    }
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.CompareTag("Nave")) {
      this.damegedLife(1);
      other.gameObject.GetComponent<PlayerController>().damegedLife(1);
    }

    if (other.gameObject.CompareTag("Shield")) {
      this.damegedLife(1);
    }
  }

  private void OnDestroy() {
    FindObjectOfType<SpawnController>()?.decreaseEnemies();
  }
}
