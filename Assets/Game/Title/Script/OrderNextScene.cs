using DG.Tweening;
using UnityEngine;

public class OrderNextScene : MonoBehaviour
{
    public MovingText movingText;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0))
        {
            movingText.StopTween();
        }
    }
}
