using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour

{ 
    public Text[] Dialogs;
    public Button[] buttons;
    [SerializeField] GameObject[] gameCanvas;
    [SerializeField]Map map ;


    public void HideMenus()
    {
        foreach (var c in gameCanvas)
        {
            c.SetActive(false);
        }
    }
    public void ShowMenu(int i)
    {
        gameCanvas[i].SetActive(true);
    }
    public void ChangeText(string[] text, int i)
    {
        switch (i)
        {
            case 0:
                Dialogs[0].text = text[0];
                Dialogs[1].text = text[1];
                buttons[i].gameObject.SetActive(true);
                break;
            case 1:
                Dialogs[0].text = text[0];
                Dialogs[1].text = text[1];
                buttons[0].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Continue()
    {
         map.Level += 1;
    }
}
