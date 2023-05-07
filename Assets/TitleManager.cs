using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    [SerializeField]
    string _sceneName = default;
    /// <summary>シーンの移り変わり</summary>
    public void SceneSquence()
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void ResetNum()
    {
        GameManager.Instance.ResetNum();
    }
}
