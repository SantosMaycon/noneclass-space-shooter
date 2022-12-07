using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 3.5f;
  [SerializeField] private GameObject ammunition;
  private float waitShot;
  private SpriteRenderer childrenSpriteRender;
  [SerializeField] private Transform pointOfShot;
  [SerializeField] private int life;

  [SerializeField] private GameObject explodeEffect;

  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    rigidbody2d.velocity = Vector2.down * speed;
    waitShot = Random.Range(0.5f, 2f);
    childrenSpriteRender = GetComponentInChildren<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update() {
    waitShot -= Time.deltaTime;
   
    if (waitShot <= 0 && childrenSpriteRender.isVisible) {
      Instantiate(ammunition, pointOfShot.position, Quaternion.identity);
      waitShot = Random.Range(0.5f, 1.5f);
    }
  }


  public void damegedLife (int damege) {
    life -= damege;

    if (life <= 0) {
      Destroy(gameObject);
      Instantiate(explodeEffect, pointOfShot.position, Quaternion.identity);
    }
  }
}
