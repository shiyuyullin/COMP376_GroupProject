using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] Image progressBar;
    //[SerializeField] float duration;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //progressBar = GetComponent<Image>();
        //hideProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        //startCountdown();
    }

    void showProgressBar()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void hideProgressBar()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("Hide Progress Bar");
    }

    public void startCountdown(float duration)
    {
        showProgressBar();
        Debug.Log("Show Progress Bar");

        timer += Time.deltaTime;
        progressBar.fillAmount = 1;
        progressBar.fillAmount = Mathf.Lerp(1, 0, timer / duration);

        if (timer > duration)
        {
            hideProgressBar();
            timer = 0;
        }

    }
}
