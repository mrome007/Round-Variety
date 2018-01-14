using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour 
{
    [SerializeField]
    private EnemyWaveController waveController;

    [SerializeField]
    private GameObject title;

    [SerializeField]
    private GameObject play;

    private int uiLayer;
    private bool start;

    private void Start()
    {
        uiLayer = 1 << LayerMask.NameToLayer("UI");
        start = true;
    }

    public static void StartTheGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartRound()
    {
        waveController.StartRound();
    }

    private void HideStartGameElements()
    {
        title.SetActive(false);
        play.SetActive(false);
    }

    private void Update()
    {
        if(!start)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 20f, uiLayer))
            {
                start = false;
                HideStartGameElements();
                StartRound();
            }
        }
    }
}
