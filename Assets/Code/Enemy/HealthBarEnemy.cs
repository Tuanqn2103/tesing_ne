using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider slider; // Tham chiếu đến Slider UI
    public Gradient gradient; // Gradient để thay đổi màu thanh máu theo phần trăm
    public Image fill; // Hình ảnh thanh bên trong Slider để thay đổi màu sắc

    // Phương thức đặt giá trị tối đa cho thanh máu
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // Thiết lập giá trị tối đa của thanh máu
        slider.value = health;    // Đặt giá trị ban đầu của thanh máu

        // Đặt màu sắc đầy đủ cho thanh máu khi sức khỏe ở mức tối đa
        fill.color = gradient.Evaluate(1f); 
    }

    // Phương thức cập nhật giá trị hiện tại của thanh máu
    public void SetHealth(int health)
    {
        slider.value = health; // Cập nhật giá trị hiện tại của thanh máu

        // Cập nhật màu sắc dựa trên phần trăm sức khỏe còn lại
        fill.color = gradient.Evaluate(slider.normalizedValue); 
    }
}
