using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject CardSelectionPanel;
    public GameObject mainMenuPanel;


    public void PlayBtn()
    {
        mainMenuPanel.SetActive(false);
        CardSelectionPanel.SetActive(true);
    }
    public void cardSelectionToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        CardSelectionPanel.SetActive(false);
    }
}
