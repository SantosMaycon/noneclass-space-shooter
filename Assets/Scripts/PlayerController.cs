using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 8f;
  [SerializeField] private float shotSpeed = 10f;
  [SerializeField] private GameObject ammunition;
  [SerializeField] private Transform pointOfShot;
  [SerializeField] private int life;
  [SerializeField] private GameObject explodeEffect;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    var direction = new Vector2(horizontal, vertical).normalized;
    rigidbody2d.velocity = direction * speed;

    if (Input.GetButtonDown("Fire1")) {
      var shot = Instantiate(ammunition, pointOfShot.position, Quaternion.identity);
      shot.GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
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
