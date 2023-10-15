using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해상도 처리자 */
public class CResolutionHandler : CComponent
{
    #region 변수
    private Camera m_oCamera = null;
    [SerializeField] private EProjection m_eProjection = EProjection._3D;
    #endregion // 변수

    #region 프로퍼티
    public float Distance
    {
        get
        {
            float fAngle = this.FOV / 2.0f;
            float fHeight = KDefine.G_DESIGN_SCREEN_HEIGHT / 2.0f;

            return fHeight / Mathf.Tan(fAngle);
        }
    }

    public float FOV => Mathf.PI / 4.0f;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        m_oCamera = this.GetComponent<Camera>();

        this.SetupResolution();
    }

    /** 상태를 리셋한다 */
    public override void Reset()
    {
        base.Reset();
        m_oCamera = this.GetComponent<Camera>();

        this.SetupResolution();
    }

    /** 해상도를 설정한다 */
    private void SetupResolution()
    {
        var oCamera = m_oCamera ?? this.GetComponent<Camera>();
        oCamera.nearClipPlane = 0.1f;
        oCamera.farClipPlane = 25000.0f;

        oCamera.orthographic = m_eProjection != EProjection._3D;
        oCamera.orthographicSize = KDefine.G_DESIGN_SCREEN_HEIGHT / 2.0f;

        oCamera.fieldOfView = this.FOV * Mathf.Rad2Deg;
        oCamera.transform.position = new Vector3(0.0f, 0.0f, -this.Distance);
    }
    #endregion // 함수
}
