using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
    Image fader;

    IEnumerator Prompted()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
    void Start () {
        fader = GameObject.Find("Fader").GetComponent<Image>();

        fader.color = new Color(0f, 0f, 0f, 1f);
        fader.CrossFadeAlpha(0f, 1f, false);
	}
	void Update () {
        if(Input.GetButton("action") == true)
        {
            fader.CrossFadeAlpha(1f, 1f, false);
            StartCoroutine(Prompted());
        }
	}
}
