using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : Enemy {
  // Start is called before the first frame update
  private float waitShot;
  private Rigidbody2D rigidbody2d;
  private SpriteRenderer childrenSpriteRender;
  [SerializeField] private GameObject ammunition;
  [SerializeField] private Transform pointOfShot;
  private bool isMoveHorizontal = false;
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    rigidbody2d.velocity = Vector2.down * speed;
    waitShot = Random.Range(1.5f, 2f);
    childrenSpriteRender = GetComponentInChildren<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update() {
    waitShot -= Time.deltaTime;
   
    if (transform.position.y < yMax && !isMoveHorizontal) {
      if (transform.position.x > 0f) {
        // Left
        rigidbody2d.velocity = new Vector2(-speed, -speed);
        isMoveHorizontal = true;
      } else {
        // Right
        rigidbody2d.velocity = new Vector2(speed, -speed);
        isMoveHorizontal = true;
      }
    }

    if (waitShot <= 0 && childrenSpriteRender.isVisible) {
      var player = FindObjectOfType<PlayerController>();

      if (player) {
        var shot = Instantiate(ammunition, pointOfShot.position, Quaternion.identity);
        var direction = player.transform.position - shot.transform.position;
        direction.Normalize();

        shot.GetComponent<Rigidbody2D>().velocity = direction * shotSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shot.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

        waitShot = Random.Range(0.5f, 1.5f);
      }
    }

    checkIsLive(childrenSpriteRender.isVisible);
  }
}
