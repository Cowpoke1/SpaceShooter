using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGuiListener
{

}

public class GUIScreen : MonoBehaviour
{
    private bool showed = false;

    protected IGuiListener gameListener;

    public bool IsShowed
    {
        get { return showed; }
    }

    public void Show()
    {
        showed = true;

        ApplyEffect(true);

        StartCoroutine(OnShowNextFrame());

    }

    IEnumerator OnShowNextFrame()
    {
        yield return null;
        OnShow();
    }

    public void Hide()
    {
        showed = false;
        ApplyEffect(false);
        OnHide();

    }

    public void SetGuiListener(IGuiListener gameListener)
    {
        this.gameListener = gameListener;
        UpdateListener();
    }

    public void ClearListener()
    {
        this.gameListener = null;
        UpdateListener();
    }

    protected virtual void UpdateListener()
    {

    }

    protected virtual void OnShow()
    {

    }

    protected virtual void OnHide()
    {

    }

    private void ApplyEffect(bool isAppear)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (isAppear)
        {
            gameObject.SetActive(true);
            LeanTween.alpha(rectTransform, 0, 0);
            LeanTween.alpha(rectTransform, 1, 0.2f);
        }
        else
        {
            LeanTween.alpha(rectTransform, 0, .2f).setOnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
    }
}
