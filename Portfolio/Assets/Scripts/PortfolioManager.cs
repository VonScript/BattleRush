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
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    private void Start() {
        _animator.SetTrigger("FadeIn");
    }

    public void Enter() {
        _animator.SetBool("FadeOut", true);
        StartCoroutine(FadeOut());
    }

    private void Update() {
        if (Input.anyKeyDown) Enter();
    }

    private IEnumerator FadeOut() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Portfolio Scene");
    }
}
