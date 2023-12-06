using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecentList : MonoBehaviour
{
    public List<TextMeshProUGUI> recentText;
    public int listIndex = 0;
    public GameObject textConPrefab;        //TextContainerPrefab
    public RectTransform rectTransform;
    public int textHieght = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textHieght = 0;
    }
    public void SetText(int active1,int active2,int reduce)
    {
        GameObject textContainer = Instantiate(textConPrefab);
        textContainer.transform.SetParent(gameObject.transform);
        
        recentText.Add(textContainer.GetComponent<TextMeshProUGUI>());
        recentText[listIndex].text = string.Format("{0}. {1} {2} <color=#DE4544>{3}</color>", listIndex+1,active1,active2,reduce);
        listIndex++;
        rectTransform.sizeDelta = new Vector2(0, 50 + textHieght);
        textHieght += 50;
    }
}
