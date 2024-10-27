using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _victoryPanel; // Bảng Victory UI
    private void Awake()
    {
        // Ẩn bảng Victory khi bắt đầu trò chơi
        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(false);
        }
    }

    public void ShowVictoryScreen()
    {
        // Hiển thị bảng Victory và cập nhật điểm cuối cùng
        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(true);
        }
    }
}
