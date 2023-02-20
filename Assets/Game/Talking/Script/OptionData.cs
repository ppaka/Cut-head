using UnityEngine;

public class OptionData : MonoBehaviour
{
    public int score;
    public string select, answer, motion;

    public void Select()
    {
        FindObjectOfType<ReadDialog>().OnOptionSelected.Invoke(select, score, answer, motion);
    }
}