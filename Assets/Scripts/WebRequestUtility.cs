using System;
using System.Collections;
using UnityEngine;

namespace WebRequestUtility 
{
    public class WebAudio : MonoBehaviour
    {
        public event Action<string> OnSuccess;
        public event Action<string> OnFailed;

        public void StartGet(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                StartCoroutine(GetAudioJson(url));
            }
            else
            {
                Debug.Log("url is null");
            }
        }

        private IEnumerator GetAudioJson(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            while (!www.isDone)
            {
                yield return null;
            }
            if (string.IsNullOrEmpty(www.error))
            {
                if (OnSuccess != null)
                {
                    OnSuccess(www.text);
                }
            }
            else
            {
                if (OnFailed != null)
                {
                    OnFailed(www.error);
                }
            }
            www.Dispose();
            Destroy(gameObject);

        }
    }

    public class WebDownload:MonoBehaviour
    {
        public event Action<AudioClip> OnSuccess;
        public event Action<string> OnFailed;

        public void StartDownload(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                StartCoroutine(DownloadAudio(url));
            }
            else
            {
                Debug.Log("url is null");
            }
        }

        private IEnumerator DownloadAudio(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            while (!www.isDone)
            {
                yield return null;
            }
            if (string.IsNullOrEmpty(www.error))
            {
                if (OnSuccess != null)
                {
                    OnSuccess(www.GetAudioClip());
                }
            }
            else
            {
                if (OnFailed != null)
                {
                    OnFailed(www.error);
                }
            }
            www.Dispose();
            Destroy(gameObject);
        }

        public void StopDownload()
        {
            Destroy(gameObject);
        }
    }
}
