using UnityEngine;
using UnityEngine.UI;

public class SlotSet : MonoBehaviour
{
    [Header("엔딩 이미지")]
    public Sprite endingSprite;

    private GameObject endingImage;
    static int slotSet_Count;
    // Start is called before the first frame update
    void Awake()
    {
        endingImage = GameObject.Find("EndingCanvas").transform.GetChild(0).gameObject;
        transform.Find("Button").GetChild(0).GetComponent<Image>().sprite = endingSprite;
    }

    public void ViewEnding(bool on)
    {
        if (on)
        {
            endingImage.transform.GetChild(1).GetComponent<Image>().sprite = endingSprite;
            endingImage.SetActive(true);
        }
        else
        {
            endingImage.SetActive(false);
        }
    }
}
