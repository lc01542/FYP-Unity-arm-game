using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection { None = 0, Positive = 1, Negative = -1 };

public class ArticulationJointController : MonoBehaviour
{
    public RotationDirection rotationState = RotationDirection.None;
    public float speed = 300.0f;
    public bool respondToInput = true;
    public KeyCode increaseKey = KeyCode.UpArrow;
    public KeyCode decreaseKey = KeyCode.DownArrow;

    private ArticulationBody articulation;

    void Start()
    {
        articulation = GetComponent<ArticulationBody>();

        // 確保articulation已經被指派且不是固定關節
        if (articulation == null || articulation.jointType == ArticulationJointType.FixedJoint)
        {
            Debug.LogError("ArticulationJointController 需要被添加到擁有 ArticulationBody 的 GameObject上，且該 GameObject 不是固定關節。", this);
        }
    }

    void FixedUpdate() 
    {
        if (articulation == null || articulation.jointType == ArticulationJointType.FixedJoint)
        {
            return; // 如果articulation是null或是固定關節，則不進行任何操作
        }

        if (respondToInput)
        {
            UpdateRotationStateFromInput();
        }

        if (rotationState != RotationDirection.None)
        {
            RotateJoint();
        }
    }

    private void UpdateRotationStateFromInput()
    {
        if (Input.GetKey(increaseKey))
        {
            rotationState = RotationDirection.Positive;
        }
        else if (Input.GetKey(decreaseKey))
        {
            rotationState = RotationDirection.Negative;
        }
        else
        {
            rotationState = RotationDirection.None;
        }
    }

    private void RotateJoint()
    {
        float rotationChange = (float)rotationState * speed * Time.fixedDeltaTime;
        float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
        RotateTo(rotationGoal);
    }

    float CurrentPrimaryAxisRotation()
    {
        // 確認 ArticulationBody 存在，且不是固定關節
        if (articulation != null && articulation.jointType != ArticulationJointType.FixedJoint)
        {
            // 獲取第一個自由度的當前值（一般情況下對於旋轉關節來說）
            float currentRotationDegrees = articulation.jointPosition[0] * Mathf.Rad2Deg;
            return currentRotationDegrees;
        }
        else
        {
            Debug.LogError("ArticulationJointController 未能找到 ArticulationBody 或關節是固定的。", this);
            return 0f;
        }
    }

    void RotateTo(float primaryAxisRotation)
    {
        var drive = articulation.xDrive;
        drive.target = primaryAxisRotation;
        articulation.xDrive = drive;
    }
}