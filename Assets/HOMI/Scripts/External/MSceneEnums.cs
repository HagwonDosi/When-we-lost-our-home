using UnityEngine;
using System.Collections;

/// <summary>
/// 주의:
///    씬이름과 똑같이 적어줘야합니다^^
///    ex) 씬이름이 1_LogoScene ----> E_1_LogoScene 이렇게 붙여주세요^^
/// </summary>
public enum E_H_RESOURCELOAD
{
    E_0_Common,             //!< PrefabMng안에 있는 전역 Resource에서 읽어와요(경고창 같은거^^)
    E_101_DevelopPJHScene,
    E_004_GameScene,
    E_102_LogoScene,
    E_102_MenuScene,
    E_102_GameScene,
    E_MAX
}
