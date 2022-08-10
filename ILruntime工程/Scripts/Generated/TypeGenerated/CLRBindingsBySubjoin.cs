using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
                {     public class CLRBindingsBySubjoin
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }
        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            DownLoadCombin_Binding.Register(app);
            FairyGUI_GComboBox_Binding.Register(app);
            FairyGUI_GearAnimation_Binding.Register(app);
            FairyGUI_GearBase_Binding.Register(app);
            FairyGUI_GearColor_Binding.Register(app);
            FairyGUI_GGroup_Binding.Register(app);
            FairyGUI_GMovieClip_Binding.Register(app);
            FairyGUI_GRichTextField_Binding.Register(app);
            FairyGUI_GSlider_Binding.Register(app);
            FairyGUI_GTween_Binding.Register(app);
            FairyGUI_GTweener_Binding.Register(app);
            FairyGUI_Stage_Binding.Register(app);
            FairyGUI_UIPanel_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            UnityEngine_Shader_Binding.Register(app);
            UnityEngine_RectTransform_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_LogType_Binding.Register(app);
            System_Int16_Binding.Register(app);
            System_IComparable_Binding.Register(app);
            System_Collections_IComparer_Binding.Register(app);
            System_EventArgs_Binding.Register(app);
            System_Collections_Generic_IEnumerator_1_Object_Binding.Register(app);
            System_Collections_Generic_List_1_Int64_Binding.Register(app);
            System_Collections_Generic_List_1_Object_Binding.Register(app);
            ILRuntime_Runtime_Intepreter_ILTypeInstance_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
