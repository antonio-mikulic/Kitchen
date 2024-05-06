using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounteChanged += Player_OnSelectedCounteChanged;
    }

    private void Player_OnSelectedCounteChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(false);
    }
}
