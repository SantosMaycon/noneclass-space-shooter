using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 7f;
  
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    var direction = new Vector2(horizontal, vertical).normalized;
    Debug.Log(direction);
    rigidbody2d.velocity = direction * speed;
  }
}
