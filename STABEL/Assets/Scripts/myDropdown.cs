﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myDropdown : MonoBehaviour {
    public static string selected;
    public Dropdown platform;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        selected = platform.options[platform.value].text;
        Debug.Log(selected);
    }
}