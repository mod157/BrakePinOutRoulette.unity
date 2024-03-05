# BrakePinOutRoulette for Unity
[![Releases](https://img.shields.io/github/release/mod157/BrakePinOutRoulette.unity.svg)](https://github.com/mod157/BrakePinOutRoulette.unity/releases) [![Readme_EN](https://img.shields.io/badge/BrakePinOutRoulette.unity-en-red)](https://github.com/mod157/BrakePinOutRoulette.unity/README_EN.md)

BrakePinOutRoulette for Unity는 핀볼과 벽돌깨기 게임을 합친 슬롯 게임입니다.
* 입력 수 만큼 사용자를 늘릴 수 있습니다.
* 두가지 버전의 승리 선택을 제공합니다.
  
[![Sample](https://img.shields.io/badge/YouTube-red?style=for-the-badge&logo=youtube&logoColor=white)](https://youtu.be/1gNE5MmUmAQ)

---
Nammu - [DreamAntDev](https://github.com/DreamAntDev)  
Email : nammu8395@gmail.com  
GitHub Issue : https://github.com/mod157/ipp.unity/issues  

---
Table of Contents
---
- [BrakePinOutRoulette for Unity](#brakepinoutroulette-for-unity)
  - [Table of Contents](#table-of-contents)
  - [History](#history)
  - [Getting started](#getting-started)
  - [Download](#download)
  - [License](#license)


History
---
v1.0.0 - Add Package

Getting started
---
asset package(`ipp.unity.*.*.*.unitypackage`) available in [ipp.unity/releases](https://github.com/mod157/ipp.unity/releases) page.
![image](docs/image/inputPad_Inspector.png)
| Title | Name | Context | 
| --- | --- | --- |
| Component | `InputColider` | 입력을 위한 충돌 판정 콜라이더 |
|  | `InputLineRenderer` | 입력 버튼 및 드래그 상태에 따른 현재 포인터까지의 라인| 
|  | `PadButtonList` | 동기적으로 생성된 PadButton List |
| InputPad | `ButtonObject` | Base Button 오브젝트 |
|  | `PadCount` | 생성할 행과 열 갯수 | 
|  | `ScaleSlider` | 간 버튼 간격의 배율 |
|  | `Radius` | 버튼 인식 범위 | 
|  | `PadGrideLayoutGroup` | 버튼 Layout |
|  | `InputTrigger` | 충돌 판정 이벤트 |
| Option | `IsInputLine` | InputLineRenderer 활성/비활성 |
|  | `IsPointer` | Pointer 표시 활성/비활성 | 
|  | `IsPadButtonRadius` | 각 Button 범위 활성/비활성 |
|  | `IsEditorDebug` | Debug 활성/비활성 | 

Download
---


License
---
This library is under the [MIT](https://github.com/mod157/ipp.unity?tab=MIT-1-ov-file) License.
