using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public BoolVariable gameover;
    public BoolVariable escape;
    public BoolVariable finish_line;
    public IntVariable track_count;

    public GameObject end_screen;
    public GameObject exit;
    public GameObject win;
    public GameObject lose;

    private void Awake() {
        gameover.Value = false;
    }

    private void Update() {
        if (gameover.Value) {
            end_screen.SetActive(true);

            if (escape.Value) exit.SetActive(true);
            else {
                if (finish_line.Value) win.SetActive(true); else lose.SetActive(true);
            }

            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] track = GameObject.FindGameObjectsWithTag("Track");
            GameObject[] token = GameObject.FindGameObjectsWithTag("Token");

            GameObject[] assets = enemy.Concat(track).ToArray();
            assets = assets.Concat(token).ToArray();

            foreach (GameObject asset in assets) {
                Destroy(asset);
            }
        }
    }

    public void GoBack() {
        SceneManager.LoadScene("Portfolio Scene");
    }

}
