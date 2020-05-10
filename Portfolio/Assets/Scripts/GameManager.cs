using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoolVariable gameover;
    public GameObject end_screen;

    private void Awake() {
        gameover.Value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover.Value) {
            end_screen.SetActive(true);

            GameObject[] assets = GameObject.FindGameObjectsWithTag("Enemy");
            //GameObject[] tracks = GameObject.FindGameObjectsWithTag("Track");

            foreach(GameObject asset in assets) {
                Destroy(asset);
            }      
        }
    }

    public void GoBack() {
        SceneManager.LoadScene("Portfolio Scene");
    }

}
