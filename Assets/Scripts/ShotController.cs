using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
  [SerializeField] private float speed = 20f;
  private Rigidbody2D rigidbody2d;

  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    rigidbody2d.velocity = new Vector2(0f, speed);
  }

  // Update is called once per frame
  void Update() {}
}
