using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int level = 0;
    TMP_Dropdown selector;

    // https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        selector = GameObject.Find("LevelSelector").GetComponent<TMP_Dropdown>();
    }

    public void SetLevel() {
        Debug.Log("Set starting level " + selector.value);
        level = selector.value;
    }
}
