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
    unsafe class FairyGUI_DisplayObject_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.DisplayObject);
            args = new Type[]{};
            method = type.GetMethod("get_cachedTransform", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_cachedTransform_0);
            args = new Type[]{typeof(System.Single)};
            method = type.GetMethod("set_rotation", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_rotation_1);
            args = new Type[]{typeof(System.Single), typeof(System.Single), typeof(System.Single)};
            method = type.GetMethod("SetScale", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetScale_2);
            args = new Type[]{};
            method = type.GetMethod("Dispose", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Dispose_3);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_visible", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_visible_4);
            args = new Type[]{typeof(System.Single)};
            method = type.GetMethod("set_rotationX", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_rotationX_5);
            args = new Type[]{};
            method = type.GetMethod("get_xy", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_xy_6);
            args = new Type[]{typeof(UnityEngine.Vector2)};
            method = type.GetMethod("set_xy", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_xy_7);
            args = new Type[]{};
            method = type.GetMethod("get_x", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_x_8);
            args = new Type[]{};
            method = type.GetMethod("get_y", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_y_9);

            field = type.GetField("gOwner", flag);
            app.RegisterCLRFieldGetter(field, get_gOwner_0);
            app.RegisterCLRFieldSetter(field, set_gOwner_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_gOwner_0, AssignFromStack_gOwner_0);
            field = type.GetField("name", flag);
            app.RegisterCLRFieldGetter(field, get_name_1);
            app.RegisterCLRFieldSetter(field, set_name_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_name_1, AssignFromStack_name_1);


        }


        static StackObject* get_cachedTransform_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.cachedTransform;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_rotation_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @value = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.rotation = value;

            return __ret;
        }

        static StackObject* SetScale_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @zv = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @yv = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Single @xv = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetScale(@xv, @yv, @zv);

            return __ret;
        }

        static StackObject* Dispose_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Dispose();

            return __ret;
        }

        static StackObject* set_visible_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.visible = value;

            return __ret;
        }

        static StackObject* set_rotationX_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @value = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.rotationX = value;

            return __ret;
        }

        static StackObject* get_xy_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.xy;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_xy_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Vector2 @value = (UnityEngine.Vector2)typeof(UnityEngine.Vector2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.xy = value;

            return __ret;
        }

        static StackObject* get_x_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.x;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_y_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.DisplayObject instance_of_this_method = (FairyGUI.DisplayObject)typeof(FairyGUI.DisplayObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.y;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }


        static object get_gOwner_0(ref object o)
        {
            return ((FairyGUI.DisplayObject)o).gOwner;
        }

        static StackObject* CopyToStack_gOwner_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.DisplayObject)o).gOwner;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_gOwner_0(ref object o, object v)
        {
            ((FairyGUI.DisplayObject)o).gOwner = (FairyGUI.GObject)v;
        }

        static StackObject* AssignFromStack_gOwner_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            FairyGUI.GObject @gOwner = (FairyGUI.GObject)typeof(FairyGUI.GObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((FairyGUI.DisplayObject)o).gOwner = @gOwner;
            return ptr_of_this_method;
        }

        static object get_name_1(ref object o)
        {
            return ((FairyGUI.DisplayObject)o).name;
        }

        static StackObject* CopyToStack_name_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((FairyGUI.DisplayObject)o).name;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_name_1(ref object o, object v)
        {
            ((FairyGUI.DisplayObject)o).name = (System.String)v;
        }

        static StackObject* AssignFromStack_name_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @name = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((FairyGUI.DisplayObject)o).name = @name;
            return ptr_of_this_method;
        }



    }
}
