using UnityEngine;

public class PhotoSetActive : MonoBehaviour
{
    public GameObject TapePos;
    public GameObject Tape;

    private GResponse result;
    private void Start()
    {
        result.OnResponseCallback += TapeActive;
    }

    private void TapeActive(bool _state)
    {
        Vector3 tapeTr = TapePos.transform.position;
        Tape.transform.position = tapeTr;
        Tape.SetActive(true);
    }
}
