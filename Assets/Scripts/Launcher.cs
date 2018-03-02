
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour 
{
    void Start()
    {
        Button button = transform.Find("Btn").GetComponent<Button>();
        button.onClick.AddListener(CallBack);
    }

    private void CallBack()
    {
        StartCoroutine(NextScene());
        
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
