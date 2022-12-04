using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 7f;
  [SerializeField] private GameObject ammunition;
  
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
      position.y += 0.5f;
      var gameObject = Instantiate(ammunition, position, Quaternion.identity);
      Destroy(gameObject, 1.5f);
    } 
  }
}
