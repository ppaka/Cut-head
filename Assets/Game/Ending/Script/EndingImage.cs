using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndingImage : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().DOFade(1, 2);
    }
}
