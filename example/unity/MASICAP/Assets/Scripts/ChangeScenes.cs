using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Todos estos métodos hacen refencia a los cambios de escenas que serán llamado por lo botones dentro de unity, así mostrando y ocultando según sea indicado
public class ChangeScenes: MonoBehaviour {
    public Animator transition;
    public RealTimeCounter realTimeCounter;
    public GameObject noAR_panel;

    public void ChangeSceneStart() {
        ChangeSceneWithTransition(0);
    }


    public void ChangeSceneNewPietrario() {
        ChangeSceneWithTransition(1);
    }

    public void ChangeScenePietrarioList() {
        if (realTimeCounter != null) realTimeCounter.SaveRest();

        ChangeSceneWithTransition(2);
    }

    public void ChangeScenePomodoro() {
        ChangeSceneWithTransition(3);
    }

    public void ChangeSceneAR() {
        if (PlayerPrefs.GetInt("CanAr", 0) == 1)
        {
            realTimeCounter.SaveRest();

            ChangeSceneWithTransition(4);            
        }
        else
        {
            noAR_panel.SetActive(true);
        }
    }

    public void CloseNoAR(){
        noAR_panel.SetActive(false);
    }
    
    private void ChangeSceneWithTransition(int sceneIndex) {
        StartCoroutine(LoadNewSceneCoroutine(sceneIndex));
    }

    IEnumerator LoadNewSceneCoroutine(int sceneIndex) {
        transition.SetTrigger("OnChangeSceneRequested");

        yield
        return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneIndex);
    }
}