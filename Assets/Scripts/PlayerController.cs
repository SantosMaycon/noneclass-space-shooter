using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 7f;
  [SerializeField] private GameObject ammunition;
  [SerializeField] private Transform pointOfShot;
  [SerializeField] private int life;
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
      var position = transform.position;
      position.y += 0.8f;
      Instantiate(ammunition, pointOfShot.position, Quaternion.identity);
    } 
  }

  public void damegedLife (int damege) {
    life -= damege;

    if (life <= 0) {
      Destroy(gameObject);
    }
  }
}
