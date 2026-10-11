using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Image image;

    void Start()
    {
        image.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainMap");
    }
}
