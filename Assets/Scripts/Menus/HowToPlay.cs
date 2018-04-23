using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour {

    public GameObject howToPlayerContainer;
    public GameObject mainContainer;

    public void Display() {
        howToPlayerContainer.SetActive(true);
        mainContainer.SetActive(false);
    }
}
