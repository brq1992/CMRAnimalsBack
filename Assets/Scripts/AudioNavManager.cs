using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;  
public class AudioNavManager : BaseManager
{
    private AudioSource audioSource;
    private string getServer = @"http://www.vmetis.org/ft/yishousaomiao/getserverurl.php";

    private string lastMac = string.Empty;
    private string currentMac = string.Empty;
    public string webAddress = "";
    public string path = "";
    Button btnPlay;
    Button btnPause;
    private bool playing;

    private float constTime = 2f;
    private float currentTime = 0f;
    private string mp3Address = "";
    private int delay = 1;

#region wwwcase

    private void SeccessGetUrl(string json)
    {
        string[] ar = json.Split('=');
        string finalUrl = ar[ar.Length - 1];
        if (!string.IsNullOrEmpty(finalUrl) && finalUrl != mp3Address)
        {
            GameObject obj = GameObject.Find("webtool");
            if (obj)
            {
                obj.GetComponent<WebRequestUtility.WebDownload>().StopDownload();
            }
            WebRequestUtility.WebDownload webRequestUtility = new GameObject("webtool").AddComponent<WebRequestUtility.WebDownload>();
            webRequestUtility.OnSuccess += SuccessCallback;
            webRequestUtility.OnFailed += FailedCallback;
            webRequestUtility.StartDownload(finalUrl);
            mp3Address = finalUrl;
        }
        //Result result = JsonUtility.FromJson<Result>(json);
        //if (null != result)
        //{
        //    if (result.code == 1)
        //    {
        //        WebRequestUtility.WebDownload webRequestUtility = new GameObject("webtool").AddComponent<WebRequestUtility.WebDownload>();
        //        webRequestUtility.OnSuccess += SuccessCallback;
        //        webRequestUtility.OnFailed += FailedCallback;
        //        webRequestUtility.StartDownload(result.musicurl);

        //    }
        //    else
        //    {
        //        Debug.Log("result code is wrong, code " + result.code);
        //    }
        //}
        //else
        //{
        //    Debug.Log("result json is null! www.text is " + json);
        //}
    }

    public void FailedGetUrl(string json)
    {
        Debug.Log("get url failed " + json);
    }

    private void FailedCallback(string obj)
    {
        Debug.Log("download audio error: " + obj);
    }

    public void SuccessCallback(AudioClip clip)
    {
        AddAudioSource(clip);
    }

#endregion

    public override void InitView()
    {
        playing = true;
        base.InitView();
        audioSource = transform.Find("AudioSource").GetComponent<AudioSource>();
        btnPlay = transform.Find("Play").GetComponent<Button>();
        btnPlay.onClick.AddListener(PlayClick);

        btnPause = transform.Find("Pause").GetComponent<Button>();
        btnPause.onClick.AddListener(PauseClick);
        btnPlay.gameObject.SetActive(false);
    }

    private void PlayClick()
    {
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
        playing = true;
        PlayingAudio(true);
    }

    public void PauseClick()
    {
        btnPause.gameObject.SetActive(false);
        btnPlay.gameObject.SetActive(true);
        PlayingAudio(false);
        playing = false;
        //Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO); 
    }

    private void Update()
    {
        if (!playing)
        {
            return;
        }
        currentTime += Time.deltaTime;
        if (currentTime >= constTime)
        {
           
            StartCoroutine(GetServerAddress());


            //if (!string.IsNullOrEmpty(currentMac))// && currentMac != lastMac)
            //{
                
            //}
            currentTime = 0f;
        }

        
    }

    private IEnumerator GetServerAddress()
    {
        WWW www = new WWW(getServer);
        yield return www;
        while (!www.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www.error))
        {
            //Debug.Log("MAC: " + Android.GetWifiMacAddress());
            currentMac = "00:00:00:00:00:00";//Android.GetWifiMacAddress();
            path = string.Format("{0}{1}", www.text, currentMac);
            WebRequestUtility.WebAudio webAudio =
                new GameObject("audiourlget").AddComponent<WebRequestUtility.WebAudio>();
            webAudio.OnSuccess += SeccessGetUrl;
            webAudio.OnFailed += FailedGetUrl;
            webAudio.StartGet(path);
        }
        else
        {
            Debug.Log("www.error: "+www.error);
        }
        www.Dispose();
    }

    private void AddAudioSource(AudioClip clip)
    {
        if(audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.PlayDelayed(delay);
        }
    }

    private void PlayingAudio(bool action)
    {
        if(null == audioSource.clip)
        {
            return;
        }
        if(action)
        {
            audioSource.PlayDelayed(0.5f);
        }
        else
        {
            audioSource.Pause();
        }
    }
}

public class Result
{
    public int code;
    public string musicurl;
    public string region;
}
