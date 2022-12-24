using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : Enemy {
  [SerializeField] private string state = "First";
  [SerializeField] private GameObject[] ammunition;
  [SerializeField] private Transform[] pointsOfShot;
  [SerializeField] private float XLimit = 7.3f;
  [SerializeField] private float delayShot = 1;
  [SerializeField] private string[] states;
  [SerializeField] private float waitBetweenState = 10f;
  [SerializeField] private Image lifeBar;
  private float waitState;
  private bool isDirection = true;
  private Rigidbody2D rigidbody2d;
  private float waitShot;
  private float waitShot2;
  private SpriteRenderer childrenSpriteRender;
  private float fullLife;
  // Start is called before the first frame update
  void Start() {
    speed = 7f;
    rigidbody2d = GetComponent<Rigidbody2D>();
    waitShot = 1f;
    waitState = waitBetweenState;
    childrenSpriteRender = GetComponentInChildren<SpriteRenderer>();
    fullLife = life;
  }

  // Update is called once per frame
  void Update() {
    changeState();
    waitShot -= Time.deltaTime;
    waitShot2 -= Time.deltaTime;
    lifeBar.fillAmount = ((float) life / fullLife);
    lifeBar.color = new Color32(190, (byte) (lifeBar.fillAmount * 255), 54, 255);

    switch (state) {
      case "First":
        firstState();
        break;
      case "Second":
        secondState();
        break;
      case "Third":
        thirdState();
        break;
      default:
        firstState();
        break;
    }
  }

  private void moveHorizontal () {
    if (isDirection) {
      rigidbody2d.velocity = Vector2.right * speed;
    } else {
      rigidbody2d.velocity = Vector2.left * speed;
    }

    if (transform.position.x > XLimit) {
      isDirection = false;
    } else if (transform.position.x < -XLimit) {
      isDirection = true;
    }
  }

  private void doubleShot() {
    Instantiate(ammunition[0], pointsOfShot[0].position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.down * shotSpeed;
    Instantiate(ammunition[0], pointsOfShot[2].position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = Vector3.down * shotSpeed;
    waitShot = delayShot;

    if (mySound) {
      AudioSource.PlayClipAtPoint(mySound, Vector3.zero);
    }
  }
  private void remoteShot() {
    var player = FindObjectOfType<PlayerController>();

    if (player) {
      var shot =  Instantiate(ammunition[1], pointsOfShot[1].position, Quaternion.identity);
      var direction = player.transform.position - shot.transform.position;
      direction.Normalize();

      shot.GetComponent<Rigidbody2D>().velocity = direction * shotSpeed;

      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      shot.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

      waitShot2 = delayShot/2;

      if (mySound) {
        AudioSource.PlayClipAtPoint(mySound, Vector3.zero);
      }
    }
  }
  private void firstState () {
    moveHorizontal();
   
    if (waitShot <= 0 && childrenSpriteRender.isVisible) {
      doubleShot();
    }

    checkIsLive(childrenSpriteRender.isVisible);
  }
  private void secondState () {
    rigidbody2d.velocity = Vector2.zero;

    if (waitShot2 <= 0 && childrenSpriteRender.isVisible) {
      remoteShot();
    }

    checkIsLive(childrenSpriteRender.isVisible);
  }

  private void thirdState () {
    if (childrenSpriteRender.isVisible) {
      if (waitShot <= 0) {
        doubleShot();
      }

      if (waitShot2 <= -0.5) {
        remoteShot();
      }
    }

    checkIsLive(childrenSpriteRender.isVisible);
  }

  private void changeState () {
    if (waitState <= 0) {
      state = states[Random.Range(0, states.Length)];
      waitState = waitBetweenState; 
    } else {
      waitState -= Time.deltaTime;
    }
  }
}
