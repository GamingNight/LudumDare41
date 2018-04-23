using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

    public GameObject cursorLeft;
    public GameObject cursorRight;
    public GameObject playText;
    public GameObject controlsText;
    public GameObject quitText;
    public GameObject mainContainer;
    public GameObject howToPlayContainer;

    private int cursorIndex;
    private float prevVertical;
    private int prevCursorIndex;
    private float initCursorLeftPosX;
    private float initCursorRightPosX;

    void Start() {

        cursorIndex = 0;
        prevCursorIndex = 0;
        prevVertical = 0;
        initCursorLeftPosX = cursorLeft.transform.position.x;
        initCursorRightPosX = cursorRight.transform.position.x;
    }

    void Update() {

        float vertical = Input.GetAxisRaw("Vertical");
        bool submit = Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0);

        UpdateCursorPosition(vertical);

        if (submit) {
            if (cursorIndex == 0) {
                Play();
            } else if (cursorIndex == 1) {
                DisplayHowToPlay();
            } else if (cursorIndex == 2) {
                Quit();
            }
        }
    }

    private void UpdateCursorPosition(float vertical) {

        if (prevVertical == 0) {//allow to have one move of the cursor per pressing
            if (vertical < 0) {
                cursorIndex++;
            } else if (vertical > 0) {
                cursorIndex--;
            }
        }
        prevVertical = vertical;
        cursorIndex = Mathf.Clamp(cursorIndex, 0, 2);

        if (prevCursorIndex != cursorIndex) {
            Vector3 prevPos = cursorLeft.transform.position;
            float newLeftPosX = 0f;
            float newRightPosX = 0f;
            if (cursorIndex == 0) {
                newLeftPosX = initCursorLeftPosX;
                newRightPosX = initCursorRightPosX;
            } else if (cursorIndex == 1) {
                newLeftPosX = initCursorLeftPosX - 60;
                newRightPosX = initCursorRightPosX + 60;
            } else {//cursorIndex == 2
                newLeftPosX = initCursorLeftPosX;
                newRightPosX = initCursorRightPosX;
            }
            float newPosY = prevPos.y + (prevCursorIndex - cursorIndex) * 50;
            cursorLeft.transform.position = new Vector3(newLeftPosX, newPosY, prevPos.z);
            cursorRight.transform.position = new Vector3(newRightPosX, newPosY, prevPos.z);
            prevCursorIndex = cursorIndex;
        }
    }

    public void HighlightPlayText() {
        cursorIndex = 0;
    }

    public void HighlightControlsText() {
        cursorIndex = 1;
    }

    public void HighlightQuitText() {
        cursorIndex = 2;
    }

    private void Play() {
        SceneManager.LoadScene("main");
    }

    private void Quit() {
        Application.Quit();
    }

    private void DisplayHowToPlay() {

        mainContainer.SetActive(false);
        howToPlayContainer.SetActive(true);
    }
}
