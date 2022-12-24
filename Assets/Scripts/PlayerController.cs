using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  [SerializeField] private Text textLife;
  [SerializeField] private Text textShield;
  [SerializeField] private AudioClip shotSound;
  [SerializeField] private AudioClip shieldSound;
  [SerializeField] private AudioClip shieldSoundDown;

  private GameObject instantiateShield;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    textLife.text = life.ToString();
    textShield.text = shieldLimit.ToString();
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
      
      if (shotSound) {
        AudioSource.PlayClipAtPoint(shotSound, Vector3.zero);
      }
    } 

    if (Input.GetButtonDown("Shield") && !instantiateShield && shieldLimit > 0) {
      instantiateShield = Instantiate(shield, transform.position, Quaternion.identity);
      timeShieldAux = 6.2f;
      shieldLimit--;
      textShield.text = shieldLimit.ToString();

      if (shieldSound) {
        AudioSource.PlayClipAtPoint(shieldSound, Vector3.zero);
      }

      Invoke("shieldExpiredSound", 6f);
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
    textLife.text = life.ToString();

    if (life <= 0) {
      gameObject.SetActive(false); // Disable
      Instantiate(explodeEffect, pointOfShot.position, Quaternion.identity);
      Invoke("gameOver", 3f);
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("PowerUp")) {
      shotLevel++;
      Destroy(other.gameObject);
    }
  }

  private void gameOver () {
    FindObjectOfType<GameManager>()?.goToTheStartScene();
  }

  private void shieldExpiredSound () {
    if (shieldSoundDown) {
      AudioSource.PlayClipAtPoint(shieldSoundDown, Vector3.zero);
    }
  }
}
