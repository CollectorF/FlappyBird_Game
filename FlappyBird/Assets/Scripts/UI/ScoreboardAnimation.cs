using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreboardAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform scoreboardRect;
    [SerializeField]
    private float scoreboardPopupTime;

    public Tween ScoreboardTween;

    public Tween BoardAnimation()
    {
        float initialScoreboardY = scoreboardRect.position.y;
        scoreboardRect.position = new Vector2(scoreboardRect.position.x, -1000);
        ScoreboardTween = scoreboardRect.DOMoveY(initialScoreboardY, scoreboardPopupTime);
        return ScoreboardTween;
    }


}
