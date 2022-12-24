using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour {
  [SerializeField] private AudioClip mySound;
  // Start is called before the first frame update
  void Start() {
    if (mySound) {
      AudioSource.PlayClipAtPoint(mySound, Vector3.zero);
    }
  }

  // Update is called once per frame
  void Update() {}

  public void Die() {
    Destroy(gameObject);
  }
}
