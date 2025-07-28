using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class CheckClickRoom : MonoBehaviour
{
    public delegate void CheckClickRoomDelegate(string _roomName);
    public CheckClickRoomDelegate roomClickedCallback;


    public void OnButtonClick()
    {
        string text = transform.GetChild(0).GetComponent<TMP_Text>().text;

        int index = text.IndexOf(":") + 2;
        string roomNumber = text.Substring(index);

        Debug.Log("OnButtonClick »£√‚");

        roomClickedCallback?.Invoke(roomNumber);
    }
}
