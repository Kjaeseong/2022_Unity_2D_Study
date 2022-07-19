using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{


    public void Activate()
    {
        gameObject.SetActive(true);
    }



    void Awake()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameEnd.AddListener(Activate);
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameEnd.RemoveListener(Activate);

    }


}
