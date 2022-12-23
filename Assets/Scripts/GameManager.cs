using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
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
