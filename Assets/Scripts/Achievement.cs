[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string title;
    public string description;
    public bool unlocked;
    public Sprite icon;
}