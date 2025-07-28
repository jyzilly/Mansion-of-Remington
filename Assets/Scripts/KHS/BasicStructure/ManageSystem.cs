using System.Collections.Generic;
using UnityEngine;

public class ManageSystem : MonoBehaviour
{
    public delegate void RequestDeleagate();

    private RequestDeleagate doResponseCallback = null;
    public RequestDeleagate DoResonseCallback
    {
        get { return doResponseCallback; }
        set { doResponseCallback = value; }
    }

    public List<GCondition> gConList = null;

    public List<GResponse> gResList = null;

    private void Start()
    {
        for(int i = 0; i < gConList.Count; ++i)
        {
            gConList[i].OnSolvedCallback += gResList[i].OnResponse;
        }
    }

}
