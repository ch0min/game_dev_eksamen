using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI slider_value;

    void Start() {
        volumeSlider.onValueChanged.AddListener((v) => {
            slider_value.text = v.ToString("0.00");
        });
        
        if (!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 1);
            // Load();
        }
        else {
            // Load();
        }
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        // Save();
    }

    // private void Load() {
    //     volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    // }
    //
    // private void Save() {
    //     PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    // }
}