using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 10f;
  [SerializeField] private bool isLookingUp = true;

  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    rigidbody2d.velocity = new Vector2(0f, isLookingUp ? speed : -speed);
  }

  // Update is called once per frame
  void Update() {}

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Enemy 01")) {
      other.GetComponent<Enemy01Controller>().damegedLife(1);
    }

    if (other.CompareTag("Nave")) {
      other.GetComponent<PlayerController>().damegedLife(1);
    }

    Destroy(gameObject);
  }
}
