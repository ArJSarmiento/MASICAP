using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialManager: MonoBehaviour {
    [SerializeField] public GameObject pomodoroF1;

    [SerializeField] public GameObject pomodoroF2;

    [SerializeField] public GameObject pomodoroF3;

    public RealTimeCounter real;

    private
    const string ownPropertyName = "pomodoroTutorialHasBeenCalled";
    // Update is called once per frame

    /*
     *   I prefer not to use a toggle for show / hide because
     * the tutorial would be shown 1 or few times (if the user wishes).
     * explicitly call show and hide gives clarity about what you want to do with the interface.
     */

    public void showPomodoroF1() {
        if (!hasBeenCalled(false, ownPropertyName)) {
            real.timerSetUpMin =  24f;
            real.textTimer.text = real.timerSetUpMin.ToString("0");
            pomodoroF1.SetActive(true);
        }
    }
    public void hidePomodoroF1() {
        pomodoroF1.SetActive(false);
    }


    public void showPomodoroF2() {
        if (!hasBeenCalled(false, ownPropertyName)) {
            pomodoroF2.SetActive(true);
        }
    }
    public void hidePomodoroF2() {
        pomodoroF2.SetActive(false);
    }


    public void showPomodoroF3() {
        if (!hasBeenCalled(true, ownPropertyName)) {
            pomodoroF3.SetActive(true);
        }
    }
    public void hidePomodoroF3() {
        pomodoroF3.SetActive(false);
    }

    private bool hasBeenCalled(bool shouldInitializeTutorial, string propertyName) {
        bool resSnapshot = bool.Parse(PlayerPrefs.GetString(propertyName));
        Debug.Log(resSnapshot);

        if (shouldInitializeTutorial) {
            PlayerPrefs.SetString(propertyName, "true");
        }

        return resSnapshot;
    }
}