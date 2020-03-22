using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour {

	public GameObject loadingScreen;
	public Text progressText;
	public Slider sliderBar;
	private string strProgress;

    public Animator animator;
    public Image black;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine (LoadAsynchronously (sceneIndex));

    }


    IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

        while (!operation.isDone) 
		{
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);

			sliderBar.value = progress;

			progress = progress * 100f;

			progressText.text = progress + "%";
		}

        animator.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);

      //  yield return null;
    }

    IEnumerator fading()
    {
        animator.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
    }
}