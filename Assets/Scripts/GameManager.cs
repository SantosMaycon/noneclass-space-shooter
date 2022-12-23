using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  private void Awake() {
    var quantity = FindObjectsOfType<GameManager>().Length;

    if (quantity > 1) {
      Destroy(gameObject);
    } 
      
    DontDestroyOnLoad(gameObject);
  }

  // Update is called once per frame
  void Update() {}

  public void goToTheGameScene () {
    SceneManager.LoadScene(1);
  }

  public void goToTheStartScene () {
    SceneManager.LoadScene(0);
  }

  public void quitGame () {
    Application.Quit();
  }
}
