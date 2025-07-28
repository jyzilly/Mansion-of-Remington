using UnityEngine;

public class AnimalCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject monkeyPos;
    [SerializeField]
    private GameObject mousePos;
    [SerializeField]
    private GameObject board;

    private PushAndPull monkey;
    private PushAndPull mouse;
    private bool activeOnce = false;

    private GCondition solve;

    private void Start()
    {
        monkey = monkeyPos.GetComponent<PushAndPull>();
        mouse = mousePos.GetComponent<PushAndPull>();
        solve = GetComponent<GCondition>();
    }

    private void Update()
    {
        if(!activeOnce)
        {
            if (monkey.curGO == null || mouse.curGO == null) return;

            if (monkey.curGO.name == "MONKEY" && mouse.curGO.name == "MOUSE")
            {
                activeOnce = true;

                // 뭔가 이벤트가 일어나도록?
                solve.OnSolved(true);

                board.SetActive(true);
                monkey.curGO.SetActive(false);
                mouse.curGO.SetActive(false);
            }
        }
    }
}
