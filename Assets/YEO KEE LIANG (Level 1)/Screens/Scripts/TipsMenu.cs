using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TipsMenu : MonoBehaviour
{
    public static bool tipsActive = false;

    public GameObject tipsUI;
    public GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (tipsActive)
            {
                offTips();
            }
            else
            {
                onTips();
            }
        }
    }

    public void offTips()
    {
        panel.SetActive(true);
        tipsUI.SetActive(false);
        Time.timeScale = 1f;
        tipsActive = false;
    }

    void onTips()
    {
        panel.SetActive(false);
        tipsUI.SetActive(true);
        Time.timeScale = 0f;
        tipsActive = true;
    }

}
