using System;
using System.Collections.Generic;
using System.Linq;
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
    unsafe class FairyGUI_ScrollPane_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(FairyGUI.ScrollPane);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_touchEffect", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_touchEffect_0);
            args = new Type[]{typeof(System.Single), typeof(System.Boolean)};
            method = type.GetMethod("SetPercX", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetPercX_1);
            args = new Type[]{typeof(System.Single), typeof(System.Boolean)};
            method = type.GetMethod("SetPercY", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetPercY_2);
            args = new Type[]{};
            method = type.GetMethod("get_contentHeight", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_contentHeight_3);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_bouncebackEffect", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_bouncebackEffect_4);
            args = new Type[]{};
            method = type.GetMethod("get_onScroll", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_onScroll_5);
            args = new Type[]{};
            method = type.GetMethod("get_onScrollEnd", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_onScrollEnd_6);
            args = new Type[]{};
            method = type.GetMethod("get_percX", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_percX_7);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_snapToItem", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_snapToItem_8);
            args = new Type[]{typeof(System.Single), typeof(System.Boolean)};
            method = type.GetMethod("ScrollRight", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ScrollRight_9);
            args = new Type[]{typeof(System.Single), typeof(System.Boolean)};
            method = type.GetMethod("ScrollLeft", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ScrollLeft_10);
            args = new Type[]{};
            method = type.GetMethod("ScrollTop", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ScrollTop_11);


        }


        static StackObject* set_touchEffect_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.touchEffect = value;

            return __ret;
        }

        static StackObject* SetPercX_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @ani = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @value = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetPercX(@value, @ani);

            return __ret;
        }

        static StackObject* SetPercY_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @ani = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @value = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetPercY(@value, @ani);

            return __ret;
        }

        static StackObject* get_contentHeight_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.contentHeight;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* set_bouncebackEffect_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.bouncebackEffect = value;

            return __ret;
        }

        static StackObject* get_onScroll_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.onScroll;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_onScrollEnd_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.onScrollEnd;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_percX_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.percX;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* set_snapToItem_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.snapToItem = value;

            return __ret;
        }

        static StackObject* ScrollRight_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @ani = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @ratio = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ScrollRight(@ratio, @ani);

            return __ret;
        }

        static StackObject* ScrollLeft_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @ani = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @ratio = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ScrollLeft(@ratio, @ani);

            return __ret;
        }

        static StackObject* ScrollTop_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ScrollPane instance_of_this_method = (FairyGUI.ScrollPane)typeof(FairyGUI.ScrollPane).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ScrollTop();

            return __ret;
        }



    }
}
