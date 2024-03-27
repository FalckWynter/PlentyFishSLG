using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFramework.GFUIManager
{
    public abstract class BasePanel : MonoBehaviour
    {
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

        protected CanvasGroup canvasGroup;

        private float alphaSpeed = 10.0f; // 淡入淡出速度
        private UnityAction showCallBack;
        private UnityAction hideCallBack;
        private bool isShow = false;

        protected virtual void Awake()
        {
            if (!this.TryGetComponent<CanvasGroup>(out canvasGroup))
            {
                canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
            }

            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<TMP_Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<InputField>();
            FindChildrenControl<Slider>();
            FindChildrenControl<ScrollRect>();
        }

        protected virtual void Start()
        {
            InitalizePanel();
        }

        protected virtual void Update()
        {
        }

        /// <summary>
        /// 面板初始化函数
        /// </summary>
        protected abstract void InitalizePanel();

        /// <summary>
        /// 根据控件类型，找到所有相关联的控件，存入容器中统一管理
        /// </summary>
        /// <typeparam controlName="T">控件类型</typeparam>
        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] controls = GetComponentsInChildren<T>();

            foreach (T control in controls)
            {
                if (controlDic.ContainsKey(control.name))
                {
                    controlDic[control.name].Add(control);
                }
                else
                {
                    controlDic.Add(control.name, new List<UIBehaviour>() { control });
                }

                if (control is Button)
                {
                    (control as Button).onClick.AddListener(() =>
                    {
                        OnButtonClick(control.name);
                    });
                }
                else if (control is Toggle)
                {
                    (control as Toggle).onValueChanged.AddListener((value) =>
                    {
                        OnToggleValueChanged(control.name, value);
                    });
                }
                else if (control is InputField)
                {
                    (control as InputField).onValueChanged.AddListener((value) =>
                    {
                        OnInputFieldValueChanged(control.name, value);
                    });
                }
                else if (control is Slider)
                {
                    (control as Slider).onValueChanged.AddListener((value) =>
                    {
                        OnSliderValueChanged(control.name, value);
                    });
                }
            }
        }

        /// <summary>
        /// Slider 事件监听
        /// </summary>
        /// <param name="sliderName">控件名字</param>
        /// <param name="value"></param>
        protected virtual void OnSliderValueChanged(string sliderName, float value)
        {
        }

        /// <summary>
        /// InputField 事件监听
        /// </summary>
        /// <param name="inputName">控件名字</param>
        /// <param name="value"></param>
        protected virtual void OnInputFieldValueChanged(string inputName, string value)
        {
        }

        /// <summary>
        /// Toggle 事件监听
        /// </summary>
        /// <param name="toggleName">控件名字</param>
        /// <param name="value"></param>
        protected virtual void OnToggleValueChanged(string toggleName, bool value)
        {
        }

        /// <summary>
        /// Button 事件监听
        /// </summary>
        /// <param name="btnName">控件名字</param>
        /// <param name="value"></param>
        protected virtual void OnButtonClick(string btnName)
        {
        }

        /// <summary>
        /// 通过控件名字在容器中找到对应的控件
        /// </summary>
        /// <typeparam controlName="T">控件类型</typeparam>
        /// <param controlName="controlName">控件名字</param>
        /// <returns></returns>
        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(controlName))
            {
                foreach (UIBehaviour control in controlDic[controlName])
                {
                    if (control.name == controlName)
                    {
                        return control as T;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 显示自己
        /// </summary>
        public virtual void ShowMe(UnityAction callback = null, float targetAlpha = 1f)
        {
            isShow = true;
            canvasGroup.alpha = 0f;
            //canvasGroup.interactable = false;
            showCallBack = callback;
            FadeIn(targetAlpha);
        }

        /// <summary>
        /// 隐藏自己
        /// </summary>
        public virtual void HideMe(UnityAction callback = null, float targetAlpha = 0f)
        {
            isShow = false;
            canvasGroup.alpha = 1.0f;
            //canvasGroup.interactable = false;
            hideCallBack = callback;
            FadeOut(targetAlpha);
        }

        /// <summary>
        /// 画布淡入
        /// </summary>
        public virtual void FadeIn(float targetAlpha = 1f)
        {
            StartCoroutine(FadeInAsync(targetAlpha));
        }

        /// <summary>
        /// 画布淡出
        /// </summary>
        public virtual void FadeOut(float targetAlpha = 0f)
        {
            StartCoroutine(FadeInAsync(targetAlpha));
        }

        /// <summary>
        /// 异步执行画布淡入淡出
        /// </summary>
        /// <param name="targetAlpha"></param>
        /// <returns></returns>
        private IEnumerator FadeInAsync(float targetAlpha)
        {
            //canvasGroup.interactable = true;

            while (Mathf.Abs(canvasGroup.alpha - targetAlpha) > 0.05f)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, alphaSpeed * Time.deltaTime);
                yield return null;
            }
            // 画布淡入淡出后，想要执行的事件
            if (isShow)
            {
                showCallBack?.Invoke();
            }
            else
            {
                hideCallBack?.Invoke();
            }

            canvasGroup.alpha = targetAlpha;
        }
    }
}