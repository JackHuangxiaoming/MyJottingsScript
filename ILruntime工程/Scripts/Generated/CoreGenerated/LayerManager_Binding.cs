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
    unsafe class LayerManager_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::LayerManager);
            args = new Type[]{};
            method = type.GetMethod("get_FightLiarLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_FightLiarLayer_0);
            args = new Type[]{};
            method = type.GetMethod("get_SystemPopLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_SystemPopLayer_1);
            args = new Type[]{};
            method = type.GetMethod("get_FightLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_FightLayer_2);
            args = new Type[]{};
            method = type.GetMethod("get_NormalLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_NormalLayer_3);
            args = new Type[]{};
            method = type.GetMethod("get_LittleHigherLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_LittleHigherLayer_4);
            args = new Type[]{};
            method = type.GetMethod("get_HighestLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_HighestLayer_5);
            args = new Type[]{};
            method = type.GetMethod("get_FightBackgroundLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_FightBackgroundLayer_6);
            args = new Type[]{};
            method = type.GetMethod("get_LoadingLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_LoadingLayer_7);

            field = type.GetField("LiarFightLayer_Unity", flag);
            app.RegisterCLRFieldGetter(field, get_LiarFightLayer_Unity_0);
            app.RegisterCLRFieldSetter(field, set_LiarFightLayer_Unity_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_LiarFightLayer_Unity_0, AssignFromStack_LiarFightLayer_Unity_0);
            field = type.GetField("LiarFightBGLayer_Unity", flag);
            app.RegisterCLRFieldGetter(field, get_LiarFightBGLayer_Unity_1);
            app.RegisterCLRFieldSetter(field, set_LiarFightBGLayer_Unity_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_LiarFightBGLayer_Unity_1, AssignFromStack_LiarFightBGLayer_Unity_1);
            field = type.GetField("FightLayer_Unity", flag);
            app.RegisterCLRFieldGetter(field, get_FightLayer_Unity_2);
            app.RegisterCLRFieldSetter(field, set_FightLayer_Unity_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_FightLayer_Unity_2, AssignFromStack_FightLayer_Unity_2);
            field = type.GetField("FightBGLayer_Unity", flag);
            app.RegisterCLRFieldGetter(field, get_FightBGLayer_Unity_3);
            app.RegisterCLRFieldSetter(field, set_FightBGLayer_Unity_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_FightBGLayer_Unity_3, AssignFromStack_FightBGLayer_Unity_3);


        }


        static StackObject* get_FightLiarLayer_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.FightLiarLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_SystemPopLayer_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.SystemPopLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_FightLayer_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.FightLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_NormalLayer_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.NormalLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_LittleHigherLayer_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.LittleHigherLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_HighestLayer_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.HighestLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_FightBackgroundLayer_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.FightBackgroundLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_LoadingLayer_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::LayerManager.LoadingLayer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_LiarFightLayer_Unity_0(ref object o)
        {
            return global::LayerManager.LiarFightLayer_Unity;
        }

        static StackObject* CopyToStack_LiarFightLayer_Unity_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::LayerManager.LiarFightLayer_Unity;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_LiarFightLayer_Unity_0(ref object o, object v)
        {
            global::LayerManager.LiarFightLayer_Unity = (System.Int32)v;
        }

        static StackObject* AssignFromStack_LiarFightLayer_Unity_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @LiarFightLayer_Unity = ptr_of_this_method->Value;
            global::LayerManager.LiarFightLayer_Unity = @LiarFightLayer_Unity;
            return ptr_of_this_method;
        }

        static object get_LiarFightBGLayer_Unity_1(ref object o)
        {
            return global::LayerManager.LiarFightBGLayer_Unity;
        }

        static StackObject* CopyToStack_LiarFightBGLayer_Unity_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::LayerManager.LiarFightBGLayer_Unity;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_LiarFightBGLayer_Unity_1(ref object o, object v)
        {
            global::LayerManager.LiarFightBGLayer_Unity = (System.Int32)v;
        }

        static StackObject* AssignFromStack_LiarFightBGLayer_Unity_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @LiarFightBGLayer_Unity = ptr_of_this_method->Value;
            global::LayerManager.LiarFightBGLayer_Unity = @LiarFightBGLayer_Unity;
            return ptr_of_this_method;
        }

        static object get_FightLayer_Unity_2(ref object o)
        {
            return global::LayerManager.FightLayer_Unity;
        }

        static StackObject* CopyToStack_FightLayer_Unity_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::LayerManager.FightLayer_Unity;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_FightLayer_Unity_2(ref object o, object v)
        {
            global::LayerManager.FightLayer_Unity = (System.Int32)v;
        }

        static StackObject* AssignFromStack_FightLayer_Unity_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @FightLayer_Unity = ptr_of_this_method->Value;
            global::LayerManager.FightLayer_Unity = @FightLayer_Unity;
            return ptr_of_this_method;
        }

        static object get_FightBGLayer_Unity_3(ref object o)
        {
            return global::LayerManager.FightBGLayer_Unity;
        }

        static StackObject* CopyToStack_FightBGLayer_Unity_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::LayerManager.FightBGLayer_Unity;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_FightBGLayer_Unity_3(ref object o, object v)
        {
            global::LayerManager.FightBGLayer_Unity = (System.Int32)v;
        }

        static StackObject* AssignFromStack_FightBGLayer_Unity_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @FightBGLayer_Unity = ptr_of_this_method->Value;
            global::LayerManager.FightBGLayer_Unity = @FightBGLayer_Unity;
            return ptr_of_this_method;
        }



    }
}
