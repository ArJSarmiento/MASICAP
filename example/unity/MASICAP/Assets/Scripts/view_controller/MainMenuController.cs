using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARSupportCheck;


public class MainMenuController: MonoBehaviour {
    // El motivo por el que existe un método (este) que sólo llama a otro es para evitar instanciar la clase PietrarioRepository.
    private void OnEnable() {

        if (PlayerPrefs.GetInt("Checked", 0) == 0) 
        {
            if (ARSupportChecker.IsSupported()) 
            {
                PlayerPrefs.SetInt("CanAr", 1);
            }
            PlayerPrefs.SetInt("Checked", 1);
        }
    }

    public void ResetPlayerPrefs() {
        PietrarioRepository.Reset();
    }
}