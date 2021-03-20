using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private GameObject optionWindow;
    [SerializeField] private GameObject manual;

    private bool optionActive;
    private bool manualActive;

    void Start()
    {
        optionActive = false;
        manualActive = false;
    }

    void Update()
    {
        optionWindow.SetActive(optionActive);
        manual.SetActive(manualActive);
    }

    public void OptionController()
    {
        optionActive = !optionActive;
    }

    public void ManualController()
    {
        manualActive = !manualActive;
    }
}
