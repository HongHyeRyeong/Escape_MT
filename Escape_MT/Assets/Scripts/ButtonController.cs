using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject optionWindow;
    [SerializeField]
    private GameObject manual;
    [SerializeField]
    private Slider volume;

    public void OptionController()
    {
        SoundManager.Instance.SetEffect("Click");
        manual.SetActive(false);
        optionWindow.SetActive(!optionWindow.activeSelf);
    }

    public void ManualController()
    {
        SoundManager.Instance.SetEffect("Click");
        optionWindow.SetActive(false);
        manual.SetActive(!manual.activeSelf);
    }

    public void VolumeController()
    {
        SoundManager.Instance.SetVolume(volume.value);
    }
}