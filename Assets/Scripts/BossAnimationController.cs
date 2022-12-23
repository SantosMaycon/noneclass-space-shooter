using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour {
  [SerializeField] private GameObject bossComabet;
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  public void InstantiateBossComabet () {
    Destroy(gameObject.transform.parent.gameObject);
    Instantiate(bossComabet, new Vector3(0f, 2.05f, 0), Quaternion.identity);
  }
}
