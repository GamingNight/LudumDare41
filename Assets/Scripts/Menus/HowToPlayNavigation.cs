using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayNavigation : MonoBehaviour {

    public GameObject backText;
    public GameObject mainContainer;
    public GameObject howToPlayContainer;

    void Update() {
        bool submit = Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0);

        if (submit) {
            mainContainer.SetActive(true);
            howToPlayContainer.SetActive(false);
        }
    }
}
