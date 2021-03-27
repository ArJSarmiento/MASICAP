using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//Realiza toda la carga de escenas dentro de unity
public class LevelLoader: MonoBehaviour {
    public Animator transition;
    public  canvasVisibility canvas;
    public InfoManager info;
    public float transitionTime = 1f;
    bool canBack = true;
    int scene;
    void Start() {
        scene = SceneManager.GetActiveScene().buildIndex ;
    }
    private void Update() {
        if ( Input.GetKeyDown(KeyCode.Escape) && canBack)
        {
            canBack = false;
            if (scene == 0)
            {
               Application.Quit(); 
            }
            else if (scene == 1 || scene == 2)
            {
                ChangeSceneWithTransition(0);
            }
            else if (scene == 3)
            {
                if (canvas.menu.activeSelf || canvas.inventory.activeSelf || canvas.rewardPanel.activeSelf || info.infoModal.activeSelf )
                {
                    canBack = true;
                    info.infoModal.SetActive(false);
                    canvas.menu.SetActive(false);
                    canvas.inventory.SetActive(false);
                    canvas.rewardPanel.SetActive(false);
                }
                else
                {
                    ChangeSceneWithTransition(0);
                }
            }
            else
            {
                ChangeSceneWithTransition(3);
            }
        }
    }
    IEnumerator LoadAsynchronously(int sceneName)
    {
        transition.SetTrigger("OnChangeSceneRequested");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    

    public void ChangeSceneWithTransition(int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex)) ;
    }
}