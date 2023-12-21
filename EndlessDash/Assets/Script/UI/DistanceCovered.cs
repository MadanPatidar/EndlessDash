using UnityEngine;
using UnityEngine.UI;
public class DistanceCovered : MonoBehaviour
{
    [SerializeField]
    private Text _txtDistance;
    [SerializeField]
    private Transform _player;

    private float _distanceCovered;
    void Init()
    {
        _distanceCovered = 0;
    }
    private void OnEnable()
    {
        EventManager.OnInit += Init;
    }
    private void OnDisable()
    {
        EventManager.OnInit -= Init;
    }

    private void LateUpdate()
    {
        // Calculate distance covered by the character
        _distanceCovered = _player.position.z;
        UpdateDistanceText();
        Constant.DistanceCover = (int)_distanceCovered;
    }

    void UpdateDistanceText()
    {
        _txtDistance.text = "Distance : " + (int)_distanceCovered + "Mtr";
    }
}