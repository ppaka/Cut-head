using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputImage : MonoBehaviour
{
    public GameObject Content;
    
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            // Debug.Log(PlayerPrefs.GetInt("AchievedEnding" + (i + 1)));
            if (PlayerPrefs.GetInt("AchievedEnding" + (i + 1), 0) == 1)
            {
                Content.transform.GetChild(i).Find("Button").Find("EndingImage").gameObject.SetActive(true);
                Content.transform.GetChild(i).Find("Lock").gameObject.SetActive(false);
                Content.transform.GetChild(i).GetComponent<SlotSet>().endingSprite =
                    Resources.Load<Sprite>("Ending/" + (i + 1));
            }
            else
            {
                Content.transform.GetChild(i).Find("Button").GetComponent<Button>().enabled = false;
            }
        }
    }
}
