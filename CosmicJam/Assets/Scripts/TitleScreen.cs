using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    //[SerializeField] private GameObject titleScreen;

    //void Start()
    //{
    //    titleScreen.gameObject.SetActive(true);
    //}

    public void StartGame()
    {
        SceneManager.LoadScene("MainMap");
    }
}
