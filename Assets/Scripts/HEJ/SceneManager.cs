using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PressAnyKey;
    [SerializeField] private GameObject InputBox;
    //[SerializeField] private Button SignUp;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            PressAnyKey.enabled = false;
            InputBox.SetActive(true);
        }
    }
    private void Start()
    {

    }

}
