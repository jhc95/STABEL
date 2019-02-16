using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameE : MonoBehaviour {
    public GameObject panel;
    public GameObject menu;
    public GameObject bg;
    public GameObject health;
    public GameObject time;
    public GameObject score;
    public GameObject play;

    public Text name;
	// Use this for initialization
	void Start () {
        StartCoroutine(Message());
        name.text = Manager.username;
    }
    IEnumerator Message()
    {
        yield return new WaitForSeconds(2);
        panel.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        bg.SetActive(true);
        health.SetActive(true);
        time.SetActive(true);
        score.SetActive(true);
        play.SetActive(true);
    }
}
