using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionMenu : MonoBehaviour
{
    public void NewGame()
    {
        ResetGame(); // Gọi ResetGame trước khi tải lại cảnh mới
        SceneManager.LoadScene(1); // Tải cảnh mới (cảnh trò chơi)
    }

    public void Exit()
    {
        Application.Quit(); // Thoát game
    }

    public void Back()
    {
        SceneManager.LoadScene(0); // Quay lại menu chính
    }

    public void ResetGame()
    {
        Time.timeScale = 1f; // Đặt thời gian trò chơi về bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại cảnh hiện tại
    }
}
