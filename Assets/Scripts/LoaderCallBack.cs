using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour {
    private bool firstUpdate = true;
    private void Update() {
        if (firstUpdate == true) {
            Loader.Instance.StartCoroutine(Loader.WaitForSecondsThenLoadGameScene());
            firstUpdate = false;
        }
    }
}

