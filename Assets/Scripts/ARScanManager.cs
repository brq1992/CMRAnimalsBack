using UnityEngine;
using UnityEngine.UI;

public class ARScanManager : BaseManager
{
    private Transform lineTrans;
    public bool update;
    private int topPos;
    private int bottomPos;
    private float currentPos;
    public int speed = 5;
    private Transform warnTrans;
    private Transform image1;
    private Transform image2;
    public override void InitView()
    {
        base.InitView();
        update = false;
        image1 = transform.Find("Image");
        image2 = transform.Find("Image (1)");
        warnTrans = transform.Find("WarningBtn");
        Button warnBtn = warnTrans.GetChild(0).GetChild(0).GetComponent<Button>();
        warnBtn.onClick.AddListener(OnClickWarning);
        warnTrans.gameObject.SetActive(false);

        lineTrans = transform.Find("ScanLine");
        Image image = lineTrans.GetComponent<Image>();
        int center =(int) (image.GetComponent<RectTransform>().sizeDelta.y / 2) ;
        int halfScreen = Screen.height / 2;
        topPos = halfScreen + center;
        currentPos  = topPos;
        bottomPos = -topPos;
        RectTransform rect = lineTrans.GetComponent<RectTransform>();
        Vector3 vector3 = new Vector3(rect.localPosition.x, topPos, rect.localPosition.z);
        rect.localPosition = vector3;
        Core.NotificationEx.getSingleton().AddObserver<bool>(GlobelConst.FOUNDTARGET, Callback);
    }

    private void OnDestroy()
    {
        Core.NotificationEx.getSingleton().RemoveObserver<bool>(GlobelConst.FOUNDTARGET, Callback);
    }

    private void Callback(bool obj)
    {
        Debug.Log("obj " + obj);
        update = obj;
        if (!update)
        {
            lineTrans.gameObject.SetActive(true);
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(true);
        }
        else
        {
            lineTrans.gameObject.SetActive(false);
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);

        }
    }

    private void OnClickWarning()
    {

    }

    private void Scaning()
    {
        Vector3 pos = lineTrans.localPosition;
        pos.y -= speed;
        if(pos.y<= bottomPos)
        {
            pos.y = topPos;
        }
        lineTrans.localPosition = pos;
        currentPos = pos.y;

    }

    private void Update()
    {
        if(!update)
        {
            Scaning();
        }
    }


}
