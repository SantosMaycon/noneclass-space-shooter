using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : Enemy {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private GameObject ammunition;
  private float waitShot;
  private SpriteRenderer childrenSpriteRender;
  [SerializeField] private Transform pointOfShot;

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
      var shot = Instantiate(ammunition, pointOfShot.position, Quaternion.identity);
      shot.GetComponent<Rigidbody2D>().velocity = Vector3.down * shotSpeed;
      waitShot = Random.Range(0.5f, 1.5f);
    }
  }
}
