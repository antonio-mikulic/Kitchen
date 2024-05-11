using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_StateChanged;
    }

    private void Update()
    {
        countdownText.text = KitchenGameManager.Instance.GetCountdownToStartTimer().ToString("0.0");
    }

    private void KitchenGameManager_StateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
            Show();
        else
            Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
