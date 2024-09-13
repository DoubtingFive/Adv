using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static float sens = 1;
    Slider sensitivitySlider;
    private void Start()
    {
        sensitivitySlider = GameObject.Find("Canvas/Sensitivity Slider").GetComponent<Slider>();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Sensitivity()
    {
        sens = sensitivitySlider.value;
    }
}
