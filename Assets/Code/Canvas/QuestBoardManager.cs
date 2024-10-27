using UnityEngine;
using System.Collections;

public class QuestBoardManager : MonoBehaviour
{
    public GameObject questBoard;  // Đối tượng UI của bảng nhiệm vụ

    private void Start()
    {
        // Khi trò chơi bắt đầu, hiển thị bảng nhiệm vụ
        ShowQuestBoard();

        // Bắt đầu coroutine để tắt bảng sau 2 giây
        StartCoroutine(HideQuestBoardAfterDelay(2f));
    }

    public void ShowQuestBoard()
    {
        questBoard.SetActive(true);
    }

    public void HideQuestBoard()
    {
        questBoard.SetActive(false);
    }

    private IEnumerator HideQuestBoardAfterDelay(float delay)
    {
        // Đợi trong khoảng thời gian được chỉ định (2 giây)
        yield return new WaitForSeconds(delay);

        // Sau khi đợi, ẩn bảng nhiệm vụ
        HideQuestBoard();
    }
}
