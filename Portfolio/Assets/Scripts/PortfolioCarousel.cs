using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortfolioCarousel : MonoBehaviour
{
    public GameObject left_button;
    public GameObject right_button;
    public GameObject up_button;
    public GameObject down_button;

    public GameObject actuator;
    private Animator _animator;
    private List<GameObject> _pages;

    private bool _start_page = false;
    private bool _prev = false;
    private bool _next = false;
    private bool _goto_profile = false;
    private bool _goto_list = false;
    private bool _goto_project = false;
    private bool _start_demo = false;
    private bool _project_pages = false;

    private int _index;
    private Vector3 _reset = Vector3.zero;
    private Color _alpha;

    private void Awake(){
        _pages = new List<GameObject>();
        _pages.Add(GameObject.Find("UI/Carousel/Bio"));
        _pages.Add(GameObject.Find("UI/Carousel/Expertise"));
        _pages.Add(GameObject.Find("UI/Carousel/BattleRush"));
        _pages.Add(GameObject.Find("UI/Carousel/CoverArt"));
        _pages.Add(GameObject.Find("UI/Carousel/Story"));
        _pages.Add(GameObject.Find("UI/Carousel/Characters"));
        _pages.Add(GameObject.Find("UI/Carousel/Demo"));

        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _pages[0].SetActive(true);
        _pages[0].transform.SetParent(actuator.transform);
        _index = 0;

        _alpha = _pages[_index].GetComponentInChildren<Image>().color;
        _alpha.a = 0;
        _pages[_index].GetComponentInChildren<Image>().color = _alpha;

        _start_page = true;
        InvokeRepeating("Appear", 0, 0.05f);
    }

    private void Update(){
        //Escape key as back button
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_index > 2) {
                GoToProfile();
            } else {
                SceneManager.LoadScene("Start Scene");
            }
        }

        if(_alpha.a <= 0) {
            if (_start_demo) {
                _start_demo = false;
                CancelInvoke();

                _alpha.a = 1f;

                foreach (var page in _pages) {
                    page.transform.SetParent(transform.GetChild(0));
                    page.GetComponentInChildren<Image>().color = _alpha;
                    page.SetActive(false);
                }

                _pages[0].SetActive(true);
                _pages[0].transform.SetParent(actuator.transform);

                SceneManager.LoadScene("Game Scene");
            }

            if (_goto_project) {
                _goto_project = false;
                CancelInvoke();

                _alpha.a = 1f;

                _pages[_index].transform.SetParent(transform.GetChild(0));
                _pages[_index].GetComponentInChildren<Image>().color = _alpha;
                _pages[_index].SetActive(false);

                _index++;
                _pages[_index].SetActive(true);
                _pages[_index].transform.SetParent(actuator.transform);

                _project_pages = true;
                InvokeRepeating("Appear", 0, 0.05f);
            }
        }

        if (_alpha.a >= 1f) {
            if (_start_page) {
                _start_page = false;
                CancelInvoke();
            }

            if (_project_pages) {
                _project_pages = false;
                CancelInvoke();
            }
        }

        //Button management - can't think of a better way atm...
        //if (_br) {
        //    down_button.SetActive(false);
        //    left_button.SetActive(true);  right_button.SetActive(true); up_button.SetActive(true);
        //} else {
        //    if(_content.parent.name == "BR Cover") {
        //        left_button.SetActive(false); right_button.SetActive(false); down_button.SetActive(false);
        //        up_button.SetActive(true);
        //    } else if(_content.parent.name == "Profile 1"){
        //        right_button.SetActive(false); up_button.SetActive(false);
        //        left_button.SetActive(true); down_button.SetActive(true);
        //    } else if (_content.parent.name == "Profile 2") {
        //        left_button.SetActive(false); up_button.SetActive(false);
        //        right_button.SetActive(true); down_button.SetActive(true);
        //    }
        //}
    }

    //*******Button presses*******

    //Reset pages and start demo
    public void StartDemo() {
        _start_demo = true;
        InvokeRepeating("Disappear", 0, 0.1f);
    }

    public void ProjectInfo() {
        _goto_project = true;
        InvokeRepeating("Disappear", 0, 0.1f);
    }

    public void GoToProfile() {
        _goto_profile = true;
        _animator.SetTrigger("MoveOutGoDown");
        StartCoroutine(Pause());
    }

    public void GoToProjectList() {
        _goto_list = true;
        _animator.SetTrigger("MoveOutGoUp");
        StartCoroutine(Pause());
    }

    public void Next(){
        _next = true;
        _animator.SetTrigger("MoveOutGoLeft");
        StartCoroutine(Pause());
    }

    public void Previous(){
        _prev = true;
        _animator.SetTrigger("MoveOutGoRight");
        StartCoroutine(Pause());
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(1f);

        _pages[_index].transform.SetParent(transform.GetChild(0));
        _pages[_index].SetActive(false);

        if (_prev) { if (_index != 0) _index--; }
        if (_next) { if (_index != _pages.Count - 1) _index++; }
        if (_goto_profile) _index = 0;
        if (_goto_list) _index = 2;

        if (_pages[_index].transform.childCount == 0) {
            _alpha.a = 0;
            _pages[_index].GetComponent<Image>().color = _alpha;
        } else {
            for (int i = 0; i < _pages[_index].transform.childCount; i++) {
                _pages[_index].transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        _pages[_index].SetActive(true);
        _pages[_index].transform.SetParent(actuator.transform);
        _pages[_index].transform.position = actuator.transform.position;

        MoveIn();
    }

    private void MoveIn() {
        if (_prev) {
             _animator.SetTrigger("MoveInGoRight");
            _prev = false;
        }

        if (_next) {
            _animator.SetTrigger("MoveInGoLeft");
            _next = false;
        }

        if (_goto_profile) {
            _animator.SetTrigger("MoveInGoDown");
            _goto_profile = false;
        }

        if (_goto_list) {
            _animator.SetTrigger("MoveInGoUp");
            _goto_list = false;
        }
    }

    private void SetPage() {
        if (_pages[_index].transform.childCount == 0) {
            _alpha.a = 1;
            _pages[_index].GetComponent<Image>().color = _alpha;
        } else {
            for (int i = 0; i < _pages[_index].transform.childCount; i++) {
                _pages[_index].transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    //******Alpha*******
    private void Appear() {
        _alpha = _pages[_index].GetComponentInChildren<Image>().color;
        _alpha.a += 0.05f;
        _pages[_index].GetComponentInChildren<Image>().color = _alpha;
    }

    private void Disappear() {
        _alpha = _pages[_index].GetComponentInChildren<Image>().color;
        _alpha.a -= 0.1f;
        _pages[_index].GetComponentInChildren<Image>().color = _alpha;
    }
}
