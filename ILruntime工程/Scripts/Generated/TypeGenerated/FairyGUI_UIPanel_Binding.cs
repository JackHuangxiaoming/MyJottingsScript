using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class FairyGUI_UIPanel_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.UIPanel);
            args = new Type[]{};
            method = type.GetMethod("get_container", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_container_0);
            args = new Type[]{};
            method = type.GetMethod("get_ui", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_ui_1);
            args = new Type[]{};
            method = type.GetMethod("CreateUI", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CreateUI_2);
            args = new Type[]{typeof(System.Int32), typeof(System.Boolean)};
            method = type.GetMethod("SetSortingOrder", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetSortingOrder_3);
            args = new Type[]{typeof(FairyGUI.HitTestMode)};
            method = type.GetMethod("SetHitTestMode", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetHitTestMode_4);
            args = new Type[]{};
            method = type.GetMethod("CacheNativeChildrenRenderers", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CacheNativeChildrenRenderers_5);
            args = new Type[]{typeof(System.Boolean), typeof(System.Boolean)};
            method = type.GetMethod("ApplyModifiedProperties", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ApplyModifiedProperties_6);
            args = new Type[]{typeof(UnityEngine.Vector3)};
            method = type.GetMethod("MoveUI", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, MoveUI_7);
            args = new Type[]{};
            method = type.GetMethod("GetUIWorldPosition", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetUIWorldPosition_8);
            args = new Type[]{};
            method = type.GetMethod("EM_BeforeUpdate", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, EM_BeforeUpdate_9);
            args = new Type[]{typeof(FairyGUI.UpdateContext)};
            method = type.GetMethod("EM_Update", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, EM_Update_10);
            args = new Type[]{};
            method = type.GetMethod("EM_Reload", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, EM_Reload_11);

            field = type.GetField("packageName", flag);
            app.RegisterCLRFieldGetter(field, get_packageName_0);
            app.RegisterCLRFieldSetter(field, set_packageName_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_packageName_0, AssignFromStack_packageName_0);
            field = type.GetField("componentName", flag);
            app.RegisterCLRFieldGetter(field, get_componentName_1);
            app.RegisterCLRFieldSetter(field, set_componentName_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_componentName_1, AssignFromStack_componentName_1);
            field = type.GetField("fitScreen", flag);
            app.RegisterCLRFieldGetter(field, get_fitScreen_2);
            app.RegisterCLRFieldSetter(field, set_fitScreen_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_fitScreen_2, AssignFromStack_fitScreen_2);
            field = type.GetField("sortingOrder", flag);
            app.RegisterCLRFieldGetter(field, get_sortingOrder_3);
            app.RegisterCLRFieldSetter(field, set_sortingOrder_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_sortingOrder_3, AssignFromStack_sortingOrder_3);


            app.RegisterCLRCreateDefaultInstance(type, () => new FairyGUI.UIPanel());
            app.RegisterCLRCreateArrayInstance(type, s => new FairyGUI.UIPanel[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* get_container_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.container;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_ui_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ui;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* CreateUI_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.CreateUI();

            return __ret;
        }

        static StackObject* SetSortingOrder_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @apply = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @value = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetSortingOrder(@value, @apply);

            return __ret;
        }

        static StackObject* SetHitTestMode_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.HitTestMode @value = (FairyGUI.HitTestMode)typeof(FairyGUI.HitTestMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetHitTestMode(@value);

            return __ret;
        }

        static StackObject* CacheNativeChildrenRenderers_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.CacheNativeChildrenRenderers();

            return __ret;
        }

        static StackObject* ApplyModifiedProperties_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @fitScreenChanged = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Boolean @sortingOrderChanged = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ApplyModifiedProperties(@sortingOrderChanged, @fitScreenChanged);

            return __ret;
        }

        static StackObject* MoveUI_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Vector3 @delta = (UnityEngine.Vector3)typeof(UnityEngine.Vector3).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.MoveUI(@delta);

            return __ret;
        }

        static StackObject* GetUIWorldPosition_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetUIWorldPosition();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* EM_BeforeUpdate_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.EM_BeforeUpdate();

            return __ret;
        }

        static StackObject* EM_Update_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UpdateContext @context = (FairyGUI.UpdateContext)typeof(FairyGUI.UpdateContext).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.EM_Update(@context);

            return __ret;
        }

        static StackObject* EM_Reload_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.UIPanel instance_of_this_method = (FairyGUI.UIPanel)typeof(FairyGUI.UIPanel).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.EM_Reload();

            return __ret;
        }


        static object get_packageName_0(ref object o)
        {
            return ((FairyGUI.UIPanel)o).packageName;
        }

        static StackObject* CopyToStack_packageName_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.UIPanel)o).packageName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_packageName_0(ref object o, object v)
        {
            ((FairyGUI.UIPanel)o).packageName = (System.String)v;
        }

        static StackObject* AssignFromStack_packageName_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @packageName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((FairyGUI.UIPanel)o).packageName = @packageName;
            return ptr_of_this_method;
        }

        static object get_componentName_1(ref object o)
        {
            return ((FairyGUI.UIPanel)o).componentName;
        }

        static StackObject* CopyToStack_componentName_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.UIPanel)o).componentName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_componentName_1(ref object o, object v)
        {
            ((FairyGUI.UIPanel)o).componentName = (System.String)v;
        }

        static StackObject* AssignFromStack_componentName_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @componentName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((FairyGUI.UIPanel)o).componentName = @componentName;
            return ptr_of_this_method;
        }

        static object get_fitScreen_2(ref object o)
        {
            return ((FairyGUI.UIPanel)o).fitScreen;
        }

        static StackObject* CopyToStack_fitScreen_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.UIPanel)o).fitScreen;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_fitScreen_2(ref object o, object v)
        {
            ((FairyGUI.UIPanel)o).fitScreen = (FairyGUI.FitScreen)v;
        }

        static StackObject* AssignFromStack_fitScreen_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            FairyGUI.FitScreen @fitScreen = (FairyGUI.FitScreen)typeof(FairyGUI.FitScreen).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            ((FairyGUI.UIPanel)o).fitScreen = @fitScreen;
            return ptr_of_this_method;
        }

        static object get_sortingOrder_3(ref object o)
        {
            return ((FairyGUI.UIPanel)o).sortingOrder;
        }

        static StackObject* CopyToStack_sortingOrder_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.UIPanel)o).sortingOrder;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_sortingOrder_3(ref object o, object v)
        {
            ((FairyGUI.UIPanel)o).sortingOrder = (System.Int32)v;
        }

        static StackObject* AssignFromStack_sortingOrder_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @sortingOrder = ptr_of_this_method->Value;
            ((FairyGUI.UIPanel)o).sortingOrder = @sortingOrder;
            return ptr_of_this_method;
        }



        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new FairyGUI.UIPanel();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
