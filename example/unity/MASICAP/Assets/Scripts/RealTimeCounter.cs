using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
//pomodoro

public class RealTimeCounter: MonoBehaviour {
    public float timer;
    public float timerSetUpMin;
    public float shortRestMin = 5f;
    public float longRestMin = 30f;
    public int timePhase = 0;
    public int tempseg;
    public float timerTemp;
    public int compWarn;
    public static RealTimeCounter instance;
    public bool canTime = true;
    public bool hasReward = true;
    [SerializeField] Text timerLabel;
    [SerializeField]public Text textTimer;
    [SerializeField] GameObject suc1;
    [SerializeField] GameObject suc2;
    [SerializeField] GameObject suc3;
    [SerializeField] GameObject rewardPanel;
    InventoryController inv = new InventoryController();
    public GameObject[] startObjects;
    public GameObject[] pauseObjects;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject[] restObjects;
    public bool isRest = false;

    public canvasVisibility _canvasVisibility;

    //Inicializa, instacia, y realiza una vez
    void Start() {
        if (PlayerPrefs.GetInt("canTime", 2) == 2) 
        {
            PlayerPrefs.SetInt("canTime", 1);
        }
        else if (PlayerPrefs.GetInt("canTime", 2) == 1)
        {
            canTime = true;         
        }
        else if (PlayerPrefs.GetInt("canTime", 2) == 0)
        { 
            canTime = false;
        }

        if (PlayerPrefs.GetInt("compWarn", 2) == 2) {
            PlayerPrefs.SetInt("compWarn", 0);
        }
        else if (PlayerPrefs.GetInt("compWarn", 2) == 1)
        {
            foreach (GameObject obj in startObjects)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(true);
            }

            if (PlayerPrefs.GetInt("canTime", 2) == 1)
            {
                pauseButton.SetActive(true);
                resumeButton.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("canTime", 2) == 0)
            { 
                pauseButton.SetActive(false);
                resumeButton.SetActive(true); 
            }   
        }
        else if (PlayerPrefs.GetInt("compWarn", 2) == 0)
        {
            foreach (GameObject obj in startObjects)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }            
        }
        //compWarn = PlayerPrefs.GetInt("compWarn", 2);
   

        if (PlayerPrefs.GetInt("timePhase", 5) == 5) 
        {
            PlayerPrefs.SetInt("timePhase", 1);
            timePhase = 1;
        }
        else if (PlayerPrefs.GetInt("timePhase", 5) == 1)
        {
            timePhase = 1;
        }
        else if (PlayerPrefs.GetInt("timePhase", 5) == 2)
        { 
           timePhase =2;
        }  
        else if (PlayerPrefs.GetInt("timePhase", 5) == 3)
        { 
           timePhase = 3;
        } 
        else if (PlayerPrefs.GetInt("timePhase", 5) == 4)
        { 
           timePhase = 4;
        } 

        if (PlayerPrefs.GetInt("isBreak", 2) == 2) 
        {
            PlayerPrefs.SetInt("isBreak", 0);
        }
        else if (PlayerPrefs.GetInt("isBreak", 2) == 1)
        {
            isRest = true;
        }
        else if (PlayerPrefs.GetInt("isBreak", 2) == 0)
        {  
            isRest = false;
        }  

        if (PlayerPrefs.GetInt("hasReward", 2) == 2) 
        {
            PlayerPrefs.SetInt("hasReward", 1);
        }
        else if (PlayerPrefs.GetInt("hasReward", 2) == 1)
        {
            hasReward = true;
        }
        else if (PlayerPrefs.GetInt("hasReward", 2) == 0)
        {  
            hasReward = false;
        }  


        if (canTime)
        {
            timer = TimeMaster.instance.CheckDate()[1];
            timer -= TimeMaster.instance.CheckDate()[0];  
        }
        else
        {
            timer = PlayerPrefs.GetFloat("pauseTime", 0f);   
            timerLabel.text = Formatting();         
        }

        if (PlayerPrefs.GetInt("isBreaking", 2) == 2) 
        {
            PlayerPrefs.SetInt("isBreaking", 0);
        }
        else if (PlayerPrefs.GetInt("isBreaking", 2) == 1)
        {
            if(timer > 0f)
            {
                foreach (GameObject obj in startObjects)
                {
                    obj.SetActive(false);
                }

                foreach (GameObject obj in pauseObjects)
                {
                    obj.SetActive(false);
                } 
                foreach (GameObject obj in restObjects)
                {
                    obj.SetActive(true);
                }  
            }
        }
        else if (PlayerPrefs.GetInt("isBreaking", 2) == 0)
        { 
            foreach (GameObject obj in restObjects)
            {
                obj.SetActive(false);
            }  
        }
       
        timerSetUpMin = 25f;

        timerTemp = 0;

        tempseg = 0;
        instance = this;
    }
    //Actualiza cada vez que se llegue al 0 o el usuario lo force
    void Update() 
    {
        if (canTime)
        {
            if (timer > 0) 
            {
                timer -= Time.deltaTime;
                timerLabel.text = Formatting();
                PlayerPrefs.SetInt("compWarn", 1);
            } 
            else 
            {
                if (PlayerPrefs.GetInt("compWarn", 2) == 1) 
                {
                    timerLabel.text = "00:00:00";

                    if(hasReward && !isRest)
                    {
                        Reward(); 
                        isRest = true;
                        PlayerPrefs.SetInt("isBreak", 1);
                    }
                    else
                    {
                        isRest = false;
                        PlayerPrefs.SetInt("isBreak", 0);
                    }

                    if (!isRest)
                    {
                        foreach (GameObject obj in startObjects)
                        {
                            obj.SetActive(true);
                        }

                        foreach (GameObject obj in pauseObjects)
                        {
                            obj.SetActive(false);
                        }       
                        foreach (GameObject obj in restObjects)
                        {
                            obj.SetActive(false);
                        }
                    }
                    else
                    {
                        foreach (GameObject obj in startObjects)
                        {
                            obj.SetActive(false);
                        }

                        foreach (GameObject obj in pauseObjects)
                        {
                            obj.SetActive(false);
                        } 

                        foreach (GameObject obj in restObjects)
                        {
                            obj.SetActive(true);
                        }                        

                        if (timePhase ==1 ||timePhase ==2 ||timePhase ==3)
                        {
                            SetTime(shortRestMin);
                            timePhase =timePhase+1;
                        }
                        else
                        {
                            SetTime(longRestMin);
                            timePhase = 1;
                        }
                        
                        isRest = false;
                        hasReward = false;
                        PlayerPrefs.SetInt("hasReward", 0);
                        PlayerPrefs.SetInt("isBreak", 0);
                        PlayerPrefs.SetInt("timePhase", timePhase);
                    }
                    PlayerPrefs.SetInt("compWarn", 0);
                }
            }           
        }
    }

    bool AreAllRestActive()
    {
        foreach(GameObject ball in restObjects) 
        {
            if(!ball.activeSelf) 
            {
                return false;
            }
        }
        return true;
    }

    public void SaveRest()
    {
        if (AreAllRestActive())
        {
            PlayerPrefs.SetInt("isBreaking", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isBreaking", 0);
        } 
    }

    public void togglePause()
    {
        if(canTime)
        {
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
            canTime=false;
            PlayerPrefs.SetInt("canTime", 0);   
        }
        else
        {
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
            canTime = true;
            PlayerPrefs.SetInt("canTime", 1);
        }
        PlayerPrefs.SetFloat("pauseTime", timer);
    }

    public void timeStop()
    {
        foreach (GameObject obj in startObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in pauseObjects)
        {
            obj.SetActive(false);
        }

        hasReward = false;
         PlayerPrefs.SetInt("hasReward", 0);

        TimeMaster.instance.SaveDate();
        timer = 0f;        
        TimeMaster.instance.SaveFF(timer.ToString());
        timerTemp = timer;

        canTime = true;
        PlayerPrefs.SetInt("canTime", 1);
    }

    //Realiza un control de forma que una vez se termine el contador regresivo, da un mensaje con una recompensa al usuario
    void Reward() {
        int rd = UnityEngine.Random.Range(1, 3);
        _canvasVisibility.offFunc();
        rewardPanel.SetActive(true);
        if (rd == 1) {
            suc1.SetActive(true);
            suc2.SetActive(false);
            suc3.SetActive(false);
            inv.increaseItem("SUC" + rd + ".1");
        }
        if (rd == 2) {
            suc1.SetActive(false);
            suc2.SetActive(true);
            suc3.SetActive(false);
            inv.increaseItem("SUC" + rd + ".1");
        }
        if (rd == 3) {
            suc1.SetActive(false);
            suc2.SetActive(false);
            suc3.SetActive(true);
            inv.increaseItem("SUC" + rd + ".1");
        }
    }

    //Reinicia el reloj cuando es requerido
    public void ResetClock() {
        foreach (GameObject obj in startObjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in pauseObjects)
        {
            obj.SetActive(true);
        }
        hasReward = true;
        PlayerPrefs.SetInt("hasReward", 1);
        SetTime(timerSetUpMin);
    }

    void SetTime(float SetupTime)
    {
        TimeMaster.instance.SaveDate();
        timer = SetupTime * 60;
        TimeMaster.instance.SaveFF(timer.ToString());
        timerTemp = timer;
        timer -= TimeMaster.instance.CheckDate()[0];
    }

    public void Reinitialize() {

        // Debug.Log(PlayerPrefs.GetString("compWarn","Hola que hace"));

        this.ResetClock();
    }
    //Realiza control sobre el botón de aumentar el contador (por minutos)
    public void AumentCounter() {
        timerSetUpMin += 1;
        textTimer.text = timerSetUpMin.ToString("0");

    }
    //Realiza control sobre el botón de disminuir el contador (por minutos)

    public void DecreaseCounter() {
        if (timerSetUpMin != 0) {
            timerSetUpMin -= 1;
            textTimer.text = timerSetUpMin.ToString("0");
        }

    }
    //Carga nueva escena cuando es requerido
    public void ChangeScene() {
        SceneManager.LoadScene(0);
    }

    //Realiza un formato al tiempo de modo que sea de entendimientos para el usuario
    public string Formatting() {
        float h, m, s;
        h = timer / 3600;
        m = (timer % 3600) / 60;
        s = (timer % 3600) % 60;
        return LeadingZero(Math.Floor(h)) + ":" + LeadingZero (Math.Floor(m)) + ":" + LeadingZero (Math.Floor(s));
    }

    string LeadingZero (double n)
    {
        return n.ToString ().PadLeft (2, '0');
    }
}