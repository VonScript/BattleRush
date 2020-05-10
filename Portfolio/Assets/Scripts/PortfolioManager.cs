using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PortfolioManager : MonoBehaviour
{
    public Image titlecard;
    public TextMeshProUGUI enter;
    private Color _alpha;
    private Color32 _textalpha;
    private bool _doorbell;

    void Start(){
        InvokeRepeating("Appear", 0.5f, 0.1f);
        InvokeRepeating("TextAppear", 2.0f, 0.1f);
        _doorbell = false;
    }


    private void FixedUpdate(){
        if (_alpha.a >= 1f) CancelInvoke("Appear");
        if (_textalpha.a == 250) CancelInvoke("TextAppear");

        if (_doorbell){
            if (_alpha.a <= 0 && _textalpha.a <= 50){
                CancelInvoke("Disappear");
                titlecard.enabled = false;
                enter.enabled = false;
                _doorbell = false;

                SceneManager.LoadScene("Portfolio Scene");
            }
        }
    }


    private void Appear(){
        _alpha = titlecard.GetComponent<Image>().color;
        _alpha.a += 0.1f;
        titlecard.GetComponent<Image>().color = _alpha;
    }

    private void TextAppear(){
        _textalpha = enter.GetComponent<TextMeshProUGUI>().color;
        _textalpha.a += 25;
        enter.GetComponent<TextMeshProUGUI>().color = _textalpha;
    }

    public void Enter(){
        _doorbell = true;
        InvokeRepeating("Disappear", 0.5f, 0.1f);
    }

    private void Disappear() {
        _alpha = titlecard.GetComponent<Image>().color;
        _alpha.a -= 0.1f;
        titlecard.GetComponent<Image>().color = _alpha;

        _textalpha = enter.GetComponent<TextMeshProUGUI>().color;

        if (_textalpha.a > 50){
            _textalpha.a -= 25;
            enter.GetComponent<TextMeshProUGUI>().color = _textalpha;
        }
    }
}
