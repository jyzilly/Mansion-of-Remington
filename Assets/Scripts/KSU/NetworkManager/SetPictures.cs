using UnityEngine;

public class SetPictures : MonoBehaviour
{
    private GameObject pictures;
    private void Start()
    {
        pictures = GameObject.Find("Pictures");
        pictures.transform.SetParent(transform);
        pictures.transform.localPosition = Vector3.zero;
        pictures.transform.localRotation = Quaternion.identity;
    }
}
