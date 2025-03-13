using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
    public GameObject panelOptions;

    public void ShowOptions()
    {
        panelOptions.SetActive(true);
    }
    public void HideOptions()
    {
        panelOptions.SetActive(false);
    }
    public void PlayGame()
    {
        Debug.Log("Play");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
    }
    // Start is called before the first frame update
    void Start()
    {
        HideOptions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
