using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatButton : MonoBehaviour
{
    public GameObject StatPanel;

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            ToggleStatPanel();
        }
    }

    public void ToggleStatPanel()
    {
        StatPanel.SetActive(!StatPanel.activeSelf);
    }
}
