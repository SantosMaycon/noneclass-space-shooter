using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody2D rigidbody2d;
  [SerializeField] private float speed = 8f;
  [SerializeField] private float shotSpeed = 10f;
  [SerializeField] private GameObject ammunition;
  [SerializeField] private GameObject ammunition2;
  [SerializeField] private GameObject shield;
  private float timeShieldAux;
  [SerializeField] private int shotLevel = 1;
  [SerializeField] private Transform pointOfShot;
  [SerializeField] private int life;
  [SerializeField] private GameObject explodeEffect;
  [SerializeField] private float XLimit;
  [SerializeField] private float YLimit;
  [SerializeField] private float shieldLimit = 3;

  private GameObject instantiateShield;
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

    float myX = Mathf.Clamp(transform.position.x, -XLimit, XLimit);
    float myY = Mathf.Clamp(transform.position.y, -YLimit, YLimit);

    transform.position = new Vector3(myX, myY, transform.position.z);

    if (Input.GetButtonDown("Fire1")) {
      switch (shotLevel)
      {
        case 1:
          Instantiate(ammunition, pointOfShot.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
        break;
        case 2:
          Instantiate(ammunition2, new Vector3(pointOfShot.position.x - 0.4f, pointOfShot.position.y - 0.3f, pointOfShot.position.z), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
          Instantiate(ammunition2, new Vector3(pointOfShot.position.x + 0.4f, pointOfShot.position.y - 0.3f, pointOfShot.position.z), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
        break;
        default:
          Instantiate(ammunition2, new Vector3(pointOfShot.position.x - 0.6f, pointOfShot.position.y - 0.3f, pointOfShot.position.z), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
          Instantiate(ammunition, pointOfShot.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
          Instantiate(ammunition2, new Vector3(pointOfShot.position.x + 0.6f, pointOfShot.position.y - 0.3f, pointOfShot.position.z), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.up * shotSpeed;
        break;
      }
    } 

    if (Input.GetButtonDown("Shield") && !instantiateShield && shieldLimit > 0) {
      instantiateShield = Instantiate(shield, transform.position, Quaternion.identity);
      timeShieldAux = 6.2f;
      shieldLimit--;
    }

    if (instantiateShield) {
      instantiateShield.transform.position = transform.position;
      timeShieldAux -= Time.deltaTime;
    }

    if (timeShieldAux <= 0) {
      Destroy(instantiateShield);
    }
  }

  public void damegedLife (int damege) {
    life -= damege;

    if (life <= 0) {
      Destroy(gameObject);
      Instantiate(explodeEffect, pointOfShot.position, Quaternion.identity);
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("PowerUp")) {
      shotLevel++;
      Destroy(other.gameObject);
    }
  }
}
