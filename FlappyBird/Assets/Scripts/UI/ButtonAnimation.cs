using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField]
    private float buttonAppearTime;
    [SerializeField]
    private Image buttonImage;

    public Tween buttonAnimationTween;

    public Tween PlayButtonAnimation()
    {
        Color colorOriginal = buttonImage.color;
        Color color = buttonImage.color;
        color.a = 0;
        buttonImage.color = color;
        buttonAnimationTween = buttonImage.DOColor(colorOriginal, buttonAppearTime);
        return buttonAnimationTween;
    }
}
