using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject VictoryPanel;
    public GameObject _teleportationObject;
    public GameObject _EnemySpawn;
    void Update()
    {
        
    }
    public void Pause(){
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue(){
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ContinueVictory(){
        VictoryPanel.SetActive(false);
        _teleportationObject.SetActive(true);
        _EnemySpawn.SetActive(true);
        Time.timeScale = 1;
    }
}
