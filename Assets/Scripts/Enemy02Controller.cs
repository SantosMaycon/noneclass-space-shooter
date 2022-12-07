using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : Enemy {
  // Start is called before the first frame update
  private float waitShot;
  private SpriteRenderer childrenSpriteRender;
  [SerializeField] private GameObject ammunition;
  [SerializeField] private Transform pointOfShot;

  void Start() {
    var rigidbody2d = GetComponent<Rigidbody2D>();
    rigidbody2d.velocity = Vector2.down * speed;
    waitShot = Random.Range(1.5f, 2f);
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
}
