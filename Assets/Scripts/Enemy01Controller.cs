using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 3.5f;

  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    rigidbody2d.velocity = Vector2.down * speed;
  }

  // Update is called once per frame
  void Update() {}
}
