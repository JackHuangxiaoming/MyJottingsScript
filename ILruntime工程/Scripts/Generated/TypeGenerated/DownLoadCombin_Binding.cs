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
    unsafe class DownLoadCombin_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::DownLoadCombin);
            args = new Type[]{};
            method = type.GetMethod("get_IsLoading", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsLoading_0);
            args = new Type[]{};
            method = type.GetMethod("get_IsDone", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsDone_1);
            args = new Type[]{};
            method = type.GetMethod("get_IsError", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsError_2);
            args = new Type[]{};
            method = type.GetMethod("get_Progress", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Progress_3);
            args = new Type[]{};
            method = type.GetMethod("get_TotalBytesKB", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_TotalBytesKB_4);
            args = new Type[]{typeof(global::GameResFileInfo), typeof(global::DownLoadCompletedHandler)};
            method = type.GetMethod("CreatToStartDownLoad", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CreatToStartDownLoad_5);
            args = new Type[]{};
            method = type.GetMethod("Destroy", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Destroy_6);

            field = type.GetField("gameResInfo", flag);
            app.RegisterCLRFieldGetter(field, get_gameResInfo_0);
            app.RegisterCLRFieldSetter(field, set_gameResInfo_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_gameResInfo_0, AssignFromStack_gameResInfo_0);
            field = type.GetField("progressHandler", flag);
            app.RegisterCLRFieldGetter(field, get_progressHandler_1);
            app.RegisterCLRFieldSetter(field, set_progressHandler_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_progressHandler_1, AssignFromStack_progressHandler_1);
            field = type.GetField("completedHandler", flag);
            app.RegisterCLRFieldGetter(field, get_completedHandler_2);
            app.RegisterCLRFieldSetter(field, set_completedHandler_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_completedHandler_2, AssignFromStack_completedHandler_2);
            field = type.GetField("downLoadThread", flag);
            app.RegisterCLRFieldGetter(field, get_downLoadThread_3);
            app.RegisterCLRFieldSetter(field, set_downLoadThread_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_downLoadThread_3, AssignFromStack_downLoadThread_3);
            field = type.GetField("downLoadThreadList", flag);
            app.RegisterCLRFieldGetter(field, get_downLoadThreadList_4);
            app.RegisterCLRFieldSetter(field, set_downLoadThreadList_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_downLoadThreadList_4, AssignFromStack_downLoadThreadList_4);


            app.RegisterCLRCreateDefaultInstance(type, () => new global::DownLoadCombin());
            app.RegisterCLRCreateArrayInstance(type, s => new global::DownLoadCombin[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* get_IsLoading_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCombin instance_of_this_method = (global::DownLoadCombin)typeof(global::DownLoadCombin).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IsLoading;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_IsDone_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCombin instance_of_this_method = (global::DownLoadCombin)typeof(global::DownLoadCombin).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IsDone;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_IsError_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCombin instance_of_this_method = (global::DownLoadCombin)typeof(global::DownLoadCombin).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IsError;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_Progress_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCombin instance_of_this_method = (global::DownLoadCombin)typeof(global::DownLoadCombin).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Progress;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_TotalBytesKB_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCombin instance_of_this_method = (global::DownLoadCombin)typeof(global::DownLoadCombin).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.TotalBytesKB;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* CreatToStartDownLoad_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCompletedHandler @handler = (global::DownLoadCompletedHandler)typeof(global::DownLoadCompletedHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::GameResFileInfo @info = (global::GameResFileInfo)typeof(global::GameResFileInfo).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = global::DownLoadCombin.CreatToStartDownLoad(@info, @handler);

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Destroy_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::DownLoadCombin instance_of_this_method = (global::DownLoadCombin)typeof(global::DownLoadCombin).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Destroy();

            return __ret;
        }


        static object get_gameResInfo_0(ref object o)
        {
            return ((global::DownLoadCombin)o).gameResInfo;
        }

        static StackObject* CopyToStack_gameResInfo_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DownLoadCombin)o).gameResInfo;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_gameResInfo_0(ref object o, object v)
        {
            ((global::DownLoadCombin)o).gameResInfo = (global::GameResFileInfo)v;
        }

        static StackObject* AssignFromStack_gameResInfo_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::GameResFileInfo @gameResInfo = (global::GameResFileInfo)typeof(global::GameResFileInfo).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::DownLoadCombin)o).gameResInfo = @gameResInfo;
            return ptr_of_this_method;
        }

        static object get_progressHandler_1(ref object o)
        {
            return ((global::DownLoadCombin)o).progressHandler;
        }

        static StackObject* CopyToStack_progressHandler_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DownLoadCombin)o).progressHandler;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_progressHandler_1(ref object o, object v)
        {
            ((global::DownLoadCombin)o).progressHandler = (global::DownLoadProgressHandler)v;
        }

        static StackObject* AssignFromStack_progressHandler_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::DownLoadProgressHandler @progressHandler = (global::DownLoadProgressHandler)typeof(global::DownLoadProgressHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((global::DownLoadCombin)o).progressHandler = @progressHandler;
            return ptr_of_this_method;
        }

        static object get_completedHandler_2(ref object o)
        {
            return ((global::DownLoadCombin)o).completedHandler;
        }

        static StackObject* CopyToStack_completedHandler_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DownLoadCombin)o).completedHandler;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_completedHandler_2(ref object o, object v)
        {
            ((global::DownLoadCombin)o).completedHandler = (global::DownLoadCompletedHandler)v;
        }

        static StackObject* AssignFromStack_completedHandler_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::DownLoadCompletedHandler @completedHandler = (global::DownLoadCompletedHandler)typeof(global::DownLoadCompletedHandler).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((global::DownLoadCombin)o).completedHandler = @completedHandler;
            return ptr_of_this_method;
        }

        static object get_downLoadThread_3(ref object o)
        {
            return ((global::DownLoadCombin)o).downLoadThread;
        }

        static StackObject* CopyToStack_downLoadThread_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DownLoadCombin)o).downLoadThread;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_downLoadThread_3(ref object o, object v)
        {
            ((global::DownLoadCombin)o).downLoadThread = (global::DownLoadThread)v;
        }

        static StackObject* AssignFromStack_downLoadThread_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::DownLoadThread @downLoadThread = (global::DownLoadThread)typeof(global::DownLoadThread).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::DownLoadCombin)o).downLoadThread = @downLoadThread;
            return ptr_of_this_method;
        }

        static object get_downLoadThreadList_4(ref object o)
        {
            return ((global::DownLoadCombin)o).downLoadThreadList;
        }

        static StackObject* CopyToStack_downLoadThreadList_4(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DownLoadCombin)o).downLoadThreadList;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_downLoadThreadList_4(ref object o, object v)
        {
            ((global::DownLoadCombin)o).downLoadThreadList = (System.Collections.Generic.List<global::DownLoadThread>)v;
        }

        static StackObject* AssignFromStack_downLoadThreadList_4(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::DownLoadThread> @downLoadThreadList = (System.Collections.Generic.List<global::DownLoadThread>)typeof(System.Collections.Generic.List<global::DownLoadThread>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::DownLoadCombin)o).downLoadThreadList = @downLoadThreadList;
            return ptr_of_this_method;
        }



        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new global::DownLoadCombin();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
