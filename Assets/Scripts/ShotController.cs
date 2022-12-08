using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
  [SerializeField] private GameObject explodeShot;

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Enemy")) {
      other.GetComponent<Enemy>().damegedLife(1);
    }

    if (other.CompareTag("Nave")) {
      other.GetComponent<PlayerController>().damegedLife(1);
    }

    Destroy(gameObject);
    Instantiate(explodeShot, transform.position, Quaternion.identity);
  }
}
