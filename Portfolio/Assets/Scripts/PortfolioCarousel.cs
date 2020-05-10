using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortfolioCarousel : MonoBehaviour
{
    public GameObject left_button;
    public GameObject right_button;
    public GameObject up_button;
    public GameObject down_button;

    private List<GameObject> _pages;
    private List<GameObject> _skills;
    private GameObject _current_page;
    private Transform _content;
    private Image _veil;
    private Color _alpha;
       
    private bool _doorbell = false;
    private bool _br = false;

    private bool _action = true;
    private bool _move = false;
    private float _speed = 100.0f;

    private bool _left = false;
    private bool _right = false;
    private bool _up = false;
    private bool _down = false;

    private int _index;
    private int _dir = 10;

    /// <summary>
    /// Use of so many Invoke methods was necessary because FixedUpdate was acting like
    /// an entitled B*TCH. I only hope this doesn't fry anything whilst running...
    /// P.S.: Update was even worse...
    /// </summary>

    private void Awake(){
        _pages = new List<GameObject>();
        _pages.Add(GameObject.Find("UI/Carousel/BR Cover"));
        _pages.Add(GameObject.Find("UI/Carousel/Profile 1"));
        _pages.Add(GameObject.Find("UI/Carousel/Profile 2"));

        _skills = new List<GameObject>();
        _skills.Add(GameObject.Find("UI/Carousel/BR Concept"));
        _skills.Add(GameObject.Find("UI/Carousel/BR Story"));
        _skills.Add(GameObject.Find("UI/Carousel/BR Characters"));
        _skills.Add(GameObject.Find("UI/Carousel/BR Demo"));

        _current_page = _pages[0];
        _content = _current_page.transform.Find("Background");
        _veil = _content.transform.Find("Veil").GetComponent<Image>();
        _alpha = _veil.color;
    }

    private void Start() {
        InvokeRepeating("Appear", 0, 0.1f);
    }

    private void FixedUpdate(){
        //Escape key as back button
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_br) {
                MoveUp();
            } else {
                SceneManager.LoadScene("Start Scene");
            }
        }

        //Button management - can't think of a better way atm...
        if (_br) {
            down_button.SetActive(false);
            left_button.SetActive(true);  right_button.SetActive(true); up_button.SetActive(true);
        } else {
            if(_content.parent.name == "BR Cover") {
                left_button.SetActive(false); right_button.SetActive(false); down_button.SetActive(false);
                up_button.SetActive(true);
            } else if(_content.parent.name == "Profile 1"){
                right_button.SetActive(false); up_button.SetActive(false);
                left_button.SetActive(true); down_button.SetActive(true);
            } else if (_content.parent.name == "Profile 2") {
                left_button.SetActive(false); up_button.SetActive(false);
                right_button.SetActive(true); down_button.SetActive(true);
            }
        }

        //Enter skills pages
        if (_doorbell && _alpha.a >= 1f) {
            CancelInvoke();
            _move = false;
            ResetAll();
            SetActivePage();
            _doorbell = false;
        }

        //Turning pages - Moving out
        if ((_left || _right || _up || _down) && _move) {
            if (_alpha.a >= 1f) {
                CancelInvoke();
                _move = false;
                ResetAll();
                SetActivePage();
            }
        }

        //Turning pages - Moving in
        if (!_left && !_right && !_up && !_down) {
            if (_move && _content.transform.localPosition == Vector3.zero) {
                CancelInvoke("MoveIn");
                _move = false;
            }

            if (!_move && _alpha.a <= 0f) {
                CancelInvoke("Appear");
                _action = false;
            }
        }
    }

    //*******Button presses*******
    public void EnterSkillsPages() {
        if (!_action) {
            _br = true;
            _doorbell = true;

            _action = true;
            _up = true;
            _move = true;

            EmptyPage();
        }
    }

    public void StartDemo() {
        if (!_action) {
            SceneManager.LoadScene("Game Scene");
        }
    }

    public void MoveLeft(){
        if (!_action){
            _dir = 1; //eventually, move index foward by 1
            _action = true;
            _left = true;
            _move = true;

            EmptyPage();
        }
    }

    public void MoveRight(){
        if (!_action){
            _dir = -1; //eventually, move index back by 1
            _action = true;
            _right = true;
            _move = true;

            EmptyPage();
        }
    }

    public void MoveUp() {
        if (!_action) {
            _dir = 1; //used for actual direction
            _action = true;
            _move = true;

            if (_br) {
                _doorbell = true;
                _br = false;
                _down = true;
            } else _up = true;

            EmptyPage();
        }
    }

    public void MoveDown() {
        if (!_action) {
            _dir = -1; //used for actual direction
            _action = true;
            _down = true;
            _move = true;

            EmptyPage();
        }
    }

    //******Veil alpha*******
    private void Appear() {
        _alpha = _veil.color;
        _alpha.a -= 0.1f;
        _veil.color = _alpha;
    }

    private void Disappear() {
        _alpha = _veil.color;
        _alpha.a += 0.1f;
        _veil.color = _alpha;
    }

    //*******Page contents********
    private void EmptyPage() {
        if (_left || _right || _up || _down) {
            SelectPage();

            if (_move) {
                _content.transform.localPosition = Vector3.zero;
                InvokeRepeating("Disappear", 0, 0.1f);

                if (!_doorbell) {
                    if (_left) InvokeRepeating("MoveOutLeft", 0.5f, 0.1f);
                    if (_right) InvokeRepeating("MoveOutRight", 0.5f, 0.1f);
                    if (_up) InvokeRepeating("MoveOutDown", 0.5f, 0.1f);
                    if (_down) InvokeRepeating("MoveOutUp", 0.5f, 0.1f);
                }
            }
        }
    }

    private void FillPage() {
        if (!_left && !_right && !_up && !_down) {
            _alpha = _veil.color;
            if (_alpha.a != 1) {
                _alpha.a = 1;
                _veil.color = _alpha;
            }

            InvokeRepeating("Appear", 0, 0.1f);
            InvokeRepeating("MoveIn", 0, 0.1f);
        }
    }

    //*****Set next page******
    private void SetActivePage() {
        _left = false; _right = false;

        if (_br) {
            if (_up) {
                _index = 0;
            } else {
                if (_index == 0 && _dir == -1) {
                    _index = _skills.Count - 1;
                } else if (_index == (_skills.Count - 1) && _dir == 1) {
                    _index = 0;
                } else {
                    _index += _dir;
                }
            }

            _skills[_index].SetActive(true);

            if(_down) _pages[0].SetActive(true);

        } else {
            if (_up) {
                _index = 1;
            } else if (_down) {
                _index = 0;
            } else {
                _index += _dir;
            }

            _pages[_index].SetActive(true);
        }

        _move = true;

        SelectPage();

        if (_up || _down) {
            if(!_doorbell) _content.transform.localPosition = Vector3.up * 10 * _dir;
        } else {
            _content.transform.localPosition = Vector3.right * 10 * _dir;
        }

        _up = false; _down = false;
        FillPage();
    }

    //*****Select current active page******
    private void SelectPage() {
        if (_action) {

            var temp_list = new List<GameObject>();

            if (_br) {
                temp_list = _skills;
            } else {
                temp_list = _pages;
            }

            foreach (GameObject page in temp_list) {
                if (page.activeSelf) {
                    _current_page = page;
                    _index = temp_list.IndexOf(page);
                }
            }

            _content = _current_page.transform.Find("Background");
            _veil = _content.transform.Find("Veil").GetComponent<Image>();
        }
    }

    //*******Page turning*********
    private void MoveIn() {
        _content.transform.localPosition = Vector3.MoveTowards(_content.transform.localPosition, Vector3.zero, _speed * Time.deltaTime);
    }

    private void MoveOutLeft() {
        _content.transform.localPosition += Vector3.left;
    }

    private void MoveOutRight() {
        _content.transform.localPosition += Vector3.right;
    }

    private void MoveOutUp() {
        _content.transform.localPosition += Vector3.up;
    }

    private void MoveOutDown() {
        _content.transform.localPosition += Vector3.down;
    }

    //******Reset alpha and pages******
    private void ResetAll(){
        _alpha = _veil.color;
        _alpha.a = 1f;
        _veil.color = _alpha;

        foreach(GameObject page in _pages) {
            page.transform.localPosition = Vector3.zero;
            page.SetActive(false);
        }

        foreach (GameObject skill in _skills) {
            skill.transform.localPosition = Vector3.zero;
            skill.SetActive(false);
        }
    }
}
