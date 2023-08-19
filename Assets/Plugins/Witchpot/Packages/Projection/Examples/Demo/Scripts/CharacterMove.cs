using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent m_agent;

    [SerializeField]
    private Camera m_Camera;

    [SerializeField]
    private Camera m_FollowCamera = default;

    //[SerializeField]
    private Transform m_CameraTransform = default;

    [SerializeField]
    private ParticleSystem m_TargetPosition = default;

    [SerializeField]
    private Animator m_animator = default;

    private Vector3 m_Offset = new Vector3(0, 0.1f, 0);

    [SerializeField]
    private Transform m_target = default;

    private Vector3 m_CameraOffset = default;

    private void Awake()
    {
        m_CameraTransform = m_FollowCamera.transform;
        m_CameraOffset = m_CameraTransform.position - gameObject.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = m_FollowCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                m_agent.destination = hit.point;
                m_TargetPosition.transform.position = hit.point + m_Offset;
                m_TargetPosition.Emit(1);
            }
        }

        if (m_agent.remainingDistance < 1f)
        {
            m_animator.SetFloat("Speed", 0f);
        }
        else
        {
            m_animator.SetFloat("Speed", m_agent.desiredVelocity.magnitude);
        }
    }

    private void LateUpdate()
    {
        m_FollowCamera.transform.position = this.transform.position + m_CameraOffset;
    }
}
