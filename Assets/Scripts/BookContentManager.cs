using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BookContentManager : MonoBehaviour
{
    public Transform Root;
    public GameObject ItemPrafab;
    private Vector3 consVector3 = new Vector3(0, -479, 0);
    private float constInter = 614;

    void Start()
    {
        Transform retTrans = transform.Find("RtBtn");
        Button retBtn = retTrans.GetComponent<Button>();
        retBtn.onClick.AddListener(ClickReturn);
    }

    public void ClickReturn()
    {
            Destroy(transform.gameObject);

    }
    public void InitBookContent(List<TextSprite> list)
    {
        //float size = 613 * list.Count + 208;
        //transform.Find("Scroll View").Find("Viewport").Find("Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, size); 
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 vector3 = new Vector3(consVector3.x, consVector3.y - i * constInter, consVector3.z);
            GameObject obj = Instantiate(ItemPrafab);
            obj.transform.SetParent(Root);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(vector3.x, vector3.y);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            AnimalBookItemSetting bookItem = obj.GetComponent<AnimalBookItemSetting>();
            bookItem.Init(list[i].Asset.text, list[i].Sprite);
        }


    }
}

[System.Serializable]
public class BookContent
{
    public string Content;
    public Sprite Sprite;
}
